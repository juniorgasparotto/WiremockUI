namespace WiremockUI
{
    partial class FormXmlFile
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtPath = new WiremockUI.EditorTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnFormat = new System.Windows.Forms.Button();
            this.txtContent = new WiremockUI.FCTBTextBox();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPath
            // 
            this.txtPath.BackColor = System.Drawing.SystemColors.Control;
            this.txtPath.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtPath.EnableFormatters = false;
            this.txtPath.EnableHistory = false;
            this.txtPath.EnableOptions = false;
            this.txtPath.EnableSearch = false;
            this.txtPath.Location = new System.Drawing.Point(0, 0);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(507, 20);
            this.txtPath.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel3.Controls.Add(this.btnClose);
            this.panel3.Controls.Add(this.btnOpen);
            this.panel3.Controls.Add(this.btnSave);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 227);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(507, 34);
            this.panel3.TabIndex = 4;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(279, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Fechar";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(93, 6);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(171, 23);
            this.btnOpen.TabIndex = 5;
            this.btnOpen.Text = "Abrir em um editor externo";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(7, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Salvar";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnFormat);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(507, 39);
            this.panel1.TabIndex = 11;
            // 
            // btnFormat
            // 
            this.btnFormat.Location = new System.Drawing.Point(22, 10);
            this.btnFormat.Name = "btnFormat";
            this.btnFormat.Size = new System.Drawing.Size(124, 23);
            this.btnFormat.TabIndex = 2;
            this.btnFormat.Text = "Formatar XML";
            this.btnFormat.UseVisualStyleBackColor = true;
            this.btnFormat.Click += new System.EventHandler(this.btnFormat_Click);
            // 
            // txtContent
            // 
            this.txtContent.BackColor = System.Drawing.SystemColors.Control;
            this.txtContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContent.Language = WiremockUI.FCTBTextBox.LanguageSupported.XML;
            this.txtContent.Location = new System.Drawing.Point(0, 59);
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(507, 168);
            this.txtContent.TabIndex = 3;
            // 
            // FormXmlFile
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(507, 261);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.txtPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FormXmlFile";
            this.Text = "...";
            this.Load += new System.EventHandler(this.FormXmlFile_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormTextFile_KeyDown);
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private EditorTextBox txtPath;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnFormat;
        private FCTBTextBox txtContent;
        private System.Windows.Forms.Button btnClose;
    }
}