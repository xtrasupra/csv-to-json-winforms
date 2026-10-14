using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Windows.Forms;

namespace CsvToJson
{
    public partial class Form1 : Form
    {
        private string? _lastJson;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                Title = "Выберите CSV-файл"
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtCsvPath.Text = dlg.FileName;
                lblStatus.Text = "Файл выбран";
                btnSave.Enabled = false;
                _lastJson = null;
                txtPreview.Clear();
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCsvPath.Text) || !File.Exists(txtCsvPath.Text))
                {
                    MessageBox.Show("Выбери существующий CSV-файл.", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var csv = File.ReadAllText(txtCsvPath.Text, DetectEncoding(txtCsvPath.Text));
                var rows = ParseCsv(csv);

                if (rows.Count == 0)
                {
                    MessageBox.Show("Файл пуст.", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _lastJson = ConvertToJson(rows, chkHeader.Checked);
                txtPreview.Text = _lastJson;
                btnSave.Enabled = true;
                lblStatus.Text = $"Готово: {rows.Count} строк";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_lastJson))
            {
                MessageBox.Show("Сначала нажми «Конвертировать».", "Инфо",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var dlg = new SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json",
                FileName = Path.GetFileNameWithoutExtension(txtCsvPath.Text) + ".json"
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(dlg.FileName, _lastJson, new UTF8Encoding(false));
                lblStatus.Text = "Сохранено: " + dlg.FileName;
            }
        }

        private static Encoding DetectEncoding(string path)
        {
            using (var fs = File.OpenRead(path))
            {
                var bom = new byte[3];
                int read = fs.Read(bom, 0, 3);

                if (read == 3 && bom[0] == 0xEF && bom[1] == 0xBB && bom[2] == 0xBF)
                    return new UTF8Encoding(true);

                if (read >= 2 && bom[0] == 0xFF && bom[1] == 0xFE)
                    return Encoding.Unicode;

                if (read >= 2 && bom[0] == 0xFE && bom[1] == 0xFF)
                    return Encoding.BigEndianUnicode;
            }

            var bytes = File.ReadAllBytes(path);
            try
            {
                var strictUtf8 = new UTF8Encoding(false, throwOnInvalidBytes: true);
                strictUtf8.GetString(bytes);
                return new UTF8Encoding(false);
            }
            catch (DecoderFallbackException)
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                return Encoding.GetEncoding(1251);
            }
        }

        private static List<List<string>> ParseCsv(string text)
        {
            var result = new List<List<string>>();
            var row = new List<string>();
            var field = new StringBuilder();
            bool inQuotes = false;

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];

                if (inQuotes)
                {
                    if (c == '"')
                    {
                        if (i + 1 < text.Length && text[i + 1] == '"')
                        {
                            field.Append('"');
                            i++;
                        }
                        else
                        {
                            inQuotes = false;
                        }
                    }
                    else
                    {
                        field.Append(c);
                    }
                }
                else
                {
                    switch (c)
                    {
                        case '"':
                            inQuotes = true;
                            break;
                        case ',':
                            row.Add(field.ToString());
                            field.Clear();
                            break;
                        case '\r':
                            break;
                        case '\n':
                            row.Add(field.ToString());
                            field.Clear();
                            result.Add(row);
                            row = new List<string>();
                            break;
                        default:
                            field.Append(c);
                            break;
                    }
                }
            }

            if (field.Length > 0 || row.Count > 0)
            {
                row.Add(field.ToString());
                result.Add(row);
            }

            return result;
        }

        private static string ConvertToJson(List<List<string>> rows, bool hasHeader)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            if (hasHeader && rows.Count > 0)
            {
                var headers = rows[0];
                var list = new List<Dictionary<string, string>>();

                for (int r = 1; r < rows.Count; r++)
                {
                    var obj = new Dictionary<string, string>();
                    for (int c = 0; c < rows[r].Count; c++)
                    {
                        var key = c < headers.Count ? headers[c] : $"Column{c + 1}";
                        obj[key] = rows[r][c];
                    }
                    list.Add(obj);
                }

                return JsonSerializer.Serialize(list, options);
            }

            return JsonSerializer.Serialize(rows, options);
        }
    }
}