namespace WiremockUI
{
    partial class frmSslSettings
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
            this.ucKeyStoreView1 = new WiremockUI.UcKeyStoreView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupOptions = new System.Windows.Forms.GroupBox();
            this.optOther = new System.Windows.Forms.RadioButton();
            this.optCacerts = new System.Windows.Forms.RadioButton();
            this.optSslEmptyStore = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupOptions.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ucKeyStoreView1);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(786, 360);
            this.panel1.TabIndex = 14;
            // 
            // ucKeyStoreView1
            // 
            this.ucKeyStoreView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucKeyStoreView1.Location = new System.Drawing.Point(10, 104);
            this.ucKeyStoreView1.Name = "ucKeyStoreView1";
            this.ucKeyStoreView1.Size = new System.Drawing.Size(766, 246);
            this.ucKeyStoreView1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupOptions);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(10, 10);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(766, 94);
            this.panel3.TabIndex = 0;
            // 
            // groupOptions
            // 
            this.groupOptions.Controls.Add(this.optOther);
            this.groupOptions.Controls.Add(this.optCacerts);
            this.groupOptions.Controls.Add(this.optSslEmptyStore);
            this.groupOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupOptions.Location = new System.Drawing.Point(0, 0);
            this.groupOptions.Name = "groupOptions";
            this.groupOptions.Size = new System.Drawing.Size(766, 94);
            this.groupOptions.TabIndex = 3;
            this.groupOptions.TabStop = false;
            this.groupOptions.Text = "Usar certificados globais";
            // 
            // optOther
            // 
            this.optOther.AutoSize = true;
            this.optOther.Location = new System.Drawing.Point(10, 68);
            this.optOther.Name = "optOther";
            this.optOther.Size = new System.Drawing.Size(118, 17);
            this.optOther.TabIndex = 4;
            this.optOther.TabStop = true;
            this.optOther.Text = "Outro (Arquivo JKS)";
            this.optOther.UseVisualStyleBackColor = true;
            this.optOther.CheckedChanged += new System.EventHandler(this.optOther_CheckedChanged);
            // 
            // optCacerts
            // 
            this.optCacerts.AutoSize = true;
            this.optCacerts.Location = new System.Drawing.Point(10, 19);
            this.optCacerts.Name = "optCacerts";
            this.optCacerts.Size = new System.Drawing.Size(42, 17);
            this.optCacerts.TabIndex = 3;
            this.optCacerts.TabStop = true;
            this.optCacerts.Text = "Sim";
            this.optCacerts.UseVisualStyleBackColor = true;
            this.optCacerts.CheckedChanged += new System.EventHandler(this.optCacerts_CheckedChanged);
            // 
            // optSslEmptyStore
            // 
            this.optSslEmptyStore.AutoSize = true;
            this.optSslEmptyStore.Location = new System.Drawing.Point(10, 43);
            this.optSslEmptyStore.Name = "optSslEmptyStore";
            this.optSslEmptyStore.Size = new System.Drawing.Size(45, 17);
            this.optSslEmptyStore.TabIndex = 2;
            this.optSslEmptyStore.TabStop = true;
            this.optSslEmptyStore.Text = "Não";
            this.optSslEmptyStore.UseVisualStyleBackColor = true;
            this.optSslEmptyStore.CheckedChanged += new System.EventHandler(this.optSslEmptyStore_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 360);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(786, 57);
            this.panel2.TabIndex = 15;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(101, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 44);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(8, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 44);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Salvar";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmSslSettings
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(786, 417);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmSslSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SSL (Global)";
            this.Shown += new System.EventHandler(this.frmSslSettings_Shown);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupOptions.ResumeLayout(false);
            this.groupOptions.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupOptions;
        private System.Windows.Forms.RadioButton optCacerts;
        private System.Windows.Forms.RadioButton optSslEmptyStore;
        private UcKeyStoreView ucKeyStoreView1;
        private System.Windows.Forms.RadioButton optOther;
    }
}

