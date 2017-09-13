namespace WiremockUI
{
    partial class FormAbout
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtAboutText = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.LinkLabel();
            this.lblUrl = new System.Windows.Forms.LinkLabel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.txtAboutText);
            this.panel1.Controls.Add(this.lblEmail);
            this.panel1.Controls.Add(this.lblUrl);
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(280, 166);
            this.panel1.TabIndex = 7;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(101, 133);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Fechar";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtAboutText
            // 
            this.txtAboutText.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtAboutText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAboutText.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtAboutText.Enabled = false;
            this.txtAboutText.ForeColor = System.Drawing.SystemColors.Info;
            this.txtAboutText.Location = new System.Drawing.Point(0, 0);
            this.txtAboutText.Multiline = true;
            this.txtAboutText.Name = "txtAboutText";
            this.txtAboutText.ReadOnly = true;
            this.txtAboutText.Size = new System.Drawing.Size(280, 69);
            this.txtAboutText.TabIndex = 6;
            this.txtAboutText.Text = "\r\nEsse projeto foi feito por Glauber Gasparotto.";
            this.txtAboutText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(3, 85);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(164, 13);
            this.lblEmail.TabIndex = 5;
            this.lblEmail.TabStop = true;
            this.lblEmail.Text = "glaubergasparottojr@hotmail.com";
            this.lblEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblEmail_LinkClicked);
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Location = new System.Drawing.Point(3, 113);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(240, 13);
            this.lblUrl.TabIndex = 4;
            this.lblUrl.TabStop = true;
            this.lblUrl.Text = "https://github.com/juniorgasparotto/WireMockUI";
            this.lblUrl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblUrl_LinkClicked);
            // 
            // FormAbout
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(284, 170);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sobre";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtAboutText;
        private System.Windows.Forms.LinkLabel lblEmail;
        private System.Windows.Forms.LinkLabel lblUrl;
        private System.Windows.Forms.Button btnClose;
    }
}