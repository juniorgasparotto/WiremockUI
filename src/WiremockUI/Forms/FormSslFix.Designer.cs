namespace WiremockUI
{
    partial class FormSslFix
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSslFix));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblError = new System.Windows.Forms.Label();
            this.txtError = new System.Windows.Forms.TextBox();
            this.groupFixOptions = new System.Windows.Forms.GroupBox();
            this.optOpenServerSettings = new System.Windows.Forms.RadioButton();
            this.optOpenSslSettings = new System.Windows.Forms.RadioButton();
            this.optDisableCacerts = new System.Windows.Forms.RadioButton();
            this.btnFix = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupFixOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(454, 34);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(82, 65);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Location = new System.Drawing.Point(9, 5);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(244, 13);
            this.lblError.TabIndex = 1;
            this.lblError.Text = "Ocorreu um erro ao tentar carregar os certificados:";
            // 
            // txtError
            // 
            this.txtError.Location = new System.Drawing.Point(12, 21);
            this.txtError.Multiline = true;
            this.txtError.Name = "txtError";
            this.txtError.ReadOnly = true;
            this.txtError.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtError.Size = new System.Drawing.Size(438, 89);
            this.txtError.TabIndex = 2;
            // 
            // groupFixOptions
            // 
            this.groupFixOptions.Controls.Add(this.optOpenServerSettings);
            this.groupFixOptions.Controls.Add(this.optOpenSslSettings);
            this.groupFixOptions.Controls.Add(this.optDisableCacerts);
            this.groupFixOptions.Location = new System.Drawing.Point(12, 116);
            this.groupFixOptions.Name = "groupFixOptions";
            this.groupFixOptions.Size = new System.Drawing.Size(517, 108);
            this.groupFixOptions.TabIndex = 3;
            this.groupFixOptions.TabStop = false;
            this.groupFixOptions.Text = "Escolha uma das opções abaixo para resolver o erro:";
            // 
            // optOpenServerSettings
            // 
            this.optOpenServerSettings.Location = new System.Drawing.Point(7, 63);
            this.optOpenServerSettings.Name = "optOpenServerSettings";
            this.optOpenServerSettings.Size = new System.Drawing.Size(493, 35);
            this.optOpenServerSettings.TabIndex = 2;
            this.optOpenServerSettings.Text = "Verifique se as configurações \"HttpsTrustSore\", \"HttpsKeyStore\" estão preenchidas" +
    " e se essas KeyStore são válidas";
            this.optOpenServerSettings.UseVisualStyleBackColor = true;
            // 
            // optOpenSslSettings
            // 
            this.optOpenSslSettings.AutoSize = true;
            this.optOpenSslSettings.Location = new System.Drawing.Point(7, 43);
            this.optOpenSslSettings.Name = "optOpenSslSettings";
            this.optOpenSslSettings.Size = new System.Drawing.Size(442, 17);
            this.optOpenSslSettings.TabIndex = 1;
            this.optOpenSslSettings.Text = "Verificar se existe algum certificado inválido nas configurações de SSL TrustStor" +
    "e Global";
            this.optOpenSslSettings.UseVisualStyleBackColor = true;
            // 
            // optDisableCacerts
            // 
            this.optDisableCacerts.AutoSize = true;
            this.optDisableCacerts.Checked = true;
            this.optDisableCacerts.Location = new System.Drawing.Point(7, 20);
            this.optDisableCacerts.Name = "optDisableCacerts";
            this.optDisableCacerts.Size = new System.Drawing.Size(187, 17);
            this.optDisableCacerts.TabIndex = 0;
            this.optDisableCacerts.TabStop = true;
            this.optDisableCacerts.Text = "Desativar o SSL TrustStore Global";
            this.optDisableCacerts.UseVisualStyleBackColor = true;
            // 
            // btnFix
            // 
            this.btnFix.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnFix.Location = new System.Drawing.Point(12, 230);
            this.btnFix.Name = "btnFix";
            this.btnFix.Size = new System.Drawing.Size(75, 31);
            this.btnFix.TabIndex = 4;
            this.btnFix.Text = "Continuar";
            this.btnFix.UseVisualStyleBackColor = true;
            this.btnFix.Click += new System.EventHandler(this.btnFix_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(93, 230);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 31);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FormSslFix
            // 
            this.AcceptButton = this.btnFix;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnFix;
            this.ClientSize = new System.Drawing.Size(541, 268);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnFix);
            this.Controls.Add(this.groupFixOptions);
            this.Controls.Add(this.txtError);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSslFix";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ssl: Fix error";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupFixOptions.ResumeLayout(false);
            this.groupFixOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.TextBox txtError;
        private System.Windows.Forms.GroupBox groupFixOptions;
        private System.Windows.Forms.RadioButton optDisableCacerts;
        private System.Windows.Forms.RadioButton optOpenSslSettings;
        private System.Windows.Forms.RadioButton optOpenServerSettings;
        private System.Windows.Forms.Button btnFix;
        private System.Windows.Forms.Button btnCancel;
    }
}