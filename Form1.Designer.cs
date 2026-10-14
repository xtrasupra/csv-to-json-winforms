namespace CsvToJson
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            txtCsvPath = new TextBox();
            btnBrowse = new Button();
            chkHeader = new CheckBox();
            btnConvert = new Button();
            btnSave = new Button();
            lblStatus = new Label();
            txtPreview = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(127, 32);
            label1.TabIndex = 0;
            label1.Text = "CSV-файл:";
            // 
            // txtCsvPath
            // 
            txtCsvPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCsvPath.Location = new Point(12, 63);
            txtCsvPath.Name = "txtCsvPath";
            txtCsvPath.Size = new Size(650, 39);
            txtCsvPath.TabIndex = 1;
            // 
            // btnBrowse
            // 
            btnBrowse.Anchor = AnchorStyles.None;
            btnBrowse.Location = new Point(668, 63);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(150, 39);
            btnBrowse.TabIndex = 2;
            btnBrowse.Text = "Обзор...";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // chkHeader
            // 
            chkHeader.AutoSize = true;
            chkHeader.Checked = true;
            chkHeader.CheckState = CheckState.Checked;
            chkHeader.Location = new Point(12, 117);
            chkHeader.Name = "chkHeader";
            chkHeader.Size = new Size(357, 36);
            chkHeader.TabIndex = 3;
            chkHeader.Text = "Первая строка — заголовки";
            chkHeader.UseVisualStyleBackColor = true;
            // 
            // btnConvert
            // 
            btnConvert.Location = new Point(12, 174);
            btnConvert.Name = "btnConvert";
            btnConvert.Size = new Size(230, 46);
            btnConvert.TabIndex = 4;
            btnConvert.Text = "Конвертировать";
            btnConvert.UseVisualStyleBackColor = true;
            btnConvert.Click += btnConvert_Click;
            // 
            // btnSave
            // 
            btnSave.Enabled = false;
            btnSave.Location = new Point(248, 174);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(230, 46);
            btnSave.TabIndex = 5;
            btnSave.Text = "Сохранить JSON";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(12, 232);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(90, 32);
            lblStatus.TabIndex = 6;
            lblStatus.Text = "Готово";
            // 
            // txtPreview
            // 
            txtPreview.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtPreview.Font = new Font("Consolas", 10.125F, FontStyle.Regular, GraphicsUnit.Point, 204);
            txtPreview.Location = new Point(12, 283);
            txtPreview.Multiline = true;
            txtPreview.Name = "txtPreview";
            txtPreview.ReadOnly = true;
            txtPreview.ScrollBars = ScrollBars.Both;
            txtPreview.Size = new Size(806, 234);
            txtPreview.TabIndex = 7;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(874, 529);
            Controls.Add(txtPreview);
            Controls.Add(lblStatus);
            Controls.Add(btnSave);
            Controls.Add(btnConvert);
            Controls.Add(chkHeader);
            Controls.Add(btnBrowse);
            Controls.Add(txtCsvPath);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimumSize = new Size(600, 400);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CSV → JSON";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtCsvPath;
        private Button btnBrowse;
        private CheckBox chkHeader;
        private Button btnConvert;
        private Button btnSave;
        private Label lblStatus;
        private TextBox txtPreview;
    }
}
