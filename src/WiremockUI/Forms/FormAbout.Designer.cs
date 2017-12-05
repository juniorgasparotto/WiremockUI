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
            this.btnDebug = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtAboutText = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblEmail = new System.Windows.Forms.LinkLabel();
            this.lblUrl = new System.Windows.Forms.LinkLabel();
            this.lblLabelVersion = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblBuild = new System.Windows.Forms.Label();
            this.lblLabelBuild = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblLabelDate = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.btnDebug);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.lblEmail);
            this.panel1.Controls.Add(this.lblUrl);
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(280, 196);
            this.panel1.TabIndex = 7;
            // 
            // btnDebug
            // 
            this.btnDebug.Location = new System.Drawing.Point(136, 168);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(76, 23);
            this.btnDebug.TabIndex = 11;
            this.btnDebug.Text = "DevTools";
            this.btnDebug.UseVisualStyleBackColor = true;
            this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.Controls.Add(this.lblDate);
            this.panel2.Controls.Add(this.lblLabelDate);
            this.panel2.Controls.Add(this.lblBuild);
            this.panel2.Controls.Add(this.lblLabelBuild);
            this.panel2.Controls.Add(this.lblVersion);
            this.panel2.Controls.Add(this.lblLabelVersion);
            this.panel2.Controls.Add(this.txtAboutText);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.panel2.Size = new System.Drawing.Size(280, 124);
            this.panel2.TabIndex = 9;
            // 
            // txtAboutText
            // 
            this.txtAboutText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAboutText.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtAboutText.Location = new System.Drawing.Point(0, 20);
            this.txtAboutText.Name = "txtAboutText";
            this.txtAboutText.Size = new System.Drawing.Size(280, 104);
            this.txtAboutText.TabIndex = 10;
            this.txtAboutText.Text = "Esse projeto foi desenvolvido por Glauber Gasparotto";
            this.txtAboutText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(58, 168);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Fechar";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(3, 130);
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
            this.lblUrl.Location = new System.Drawing.Point(3, 147);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(240, 13);
            this.lblUrl.TabIndex = 4;
            this.lblUrl.TabStop = true;
            this.lblUrl.Text = "https://github.com/juniorgasparotto/WireMockUI";
            this.lblUrl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblUrl_LinkClicked);
            // 
            // lblLabelVersion
            // 
            this.lblLabelVersion.AutoSize = true;
            this.lblLabelVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLabelVersion.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblLabelVersion.Location = new System.Drawing.Point(4, 55);
            this.lblLabelVersion.Name = "lblLabelVersion";
            this.lblLabelVersion.Size = new System.Drawing.Size(53, 13);
            this.lblLabelVersion.TabIndex = 13;
            this.lblLabelVersion.Text = "Version:";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblVersion.Location = new System.Drawing.Point(55, 55);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(35, 13);
            this.lblVersion.TabIndex = 14;
            this.lblVersion.Text = "label2";
            // 
            // lblBuild
            // 
            this.lblBuild.AutoSize = true;
            this.lblBuild.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblBuild.Location = new System.Drawing.Point(55, 74);
            this.lblBuild.Name = "lblBuild";
            this.lblBuild.Size = new System.Drawing.Size(35, 13);
            this.lblBuild.TabIndex = 16;
            this.lblBuild.Text = "label2";
            // 
            // lblLabelBuild
            // 
            this.lblLabelBuild.AutoSize = true;
            this.lblLabelBuild.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLabelBuild.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblLabelBuild.Location = new System.Drawing.Point(4, 74);
            this.lblLabelBuild.Name = "lblLabelBuild";
            this.lblLabelBuild.Size = new System.Drawing.Size(39, 13);
            this.lblLabelBuild.TabIndex = 15;
            this.lblLabelBuild.Text = "Build:";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblDate.Location = new System.Drawing.Point(55, 94);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(35, 13);
            this.lblDate.TabIndex = 18;
            this.lblDate.Text = "label4";
            // 
            // lblLabelDate
            // 
            this.lblLabelDate.AutoSize = true;
            this.lblLabelDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLabelDate.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblLabelDate.Location = new System.Drawing.Point(4, 94);
            this.lblLabelDate.Name = "lblLabelDate";
            this.lblLabelDate.Size = new System.Drawing.Size(38, 13);
            this.lblLabelDate.TabIndex = 17;
            this.lblLabelDate.Text = "Date:";
            // 
            // FormAbout
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(284, 200);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sobre";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel lblEmail;
        private System.Windows.Forms.LinkLabel lblUrl;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label txtAboutText;
        private System.Windows.Forms.Button btnDebug;
        private System.Windows.Forms.Label lblLabelVersion;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblLabelDate;
        private System.Windows.Forms.Label lblBuild;
        private System.Windows.Forms.Label lblLabelBuild;
    }
}