namespace WiremockUI
{
    partial class UcKeyStoreView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelSelectFile = new System.Windows.Forms.Panel();
            this.groupSelectFile = new System.Windows.Forms.GroupBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.txtKeyStoreFile = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupCertificates = new System.Windows.Forms.GroupBox();
            this.gridCertificates = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colError = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileOpen = new System.Windows.Forms.OpenFileDialog();
            this.lblPwd = new System.Windows.Forms.Label();
            this.lblFile = new System.Windows.Forms.Label();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.panelSelectFile.SuspendLayout();
            this.groupSelectFile.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupCertificates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCertificates)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSelectFile
            // 
            this.panelSelectFile.Controls.Add(this.groupSelectFile);
            this.panelSelectFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSelectFile.Location = new System.Drawing.Point(0, 0);
            this.panelSelectFile.Name = "panelSelectFile";
            this.panelSelectFile.Size = new System.Drawing.Size(721, 107);
            this.panelSelectFile.TabIndex = 0;
            // 
            // groupSelectFile
            // 
            this.groupSelectFile.Controls.Add(this.btnLoad);
            this.groupSelectFile.Controls.Add(this.txtPwd);
            this.groupSelectFile.Controls.Add(this.lblFile);
            this.groupSelectFile.Controls.Add(this.lblPwd);
            this.groupSelectFile.Controls.Add(this.btnOpen);
            this.groupSelectFile.Controls.Add(this.txtKeyStoreFile);
            this.groupSelectFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupSelectFile.Location = new System.Drawing.Point(0, 0);
            this.groupSelectFile.Name = "groupSelectFile";
            this.groupSelectFile.Size = new System.Drawing.Size(721, 107);
            this.groupSelectFile.TabIndex = 0;
            this.groupSelectFile.TabStop = false;
            this.groupSelectFile.Text = "Store file";
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(479, 21);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 24);
            this.btnOpen.TabIndex = 6;
            this.btnOpen.Text = "Localizar";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // txtKeyStoreFile
            // 
            this.txtKeyStoreFile.Location = new System.Drawing.Point(68, 23);
            this.txtKeyStoreFile.Name = "txtKeyStoreFile";
            this.txtKeyStoreFile.Size = new System.Drawing.Size(408, 20);
            this.txtKeyStoreFile.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupCertificates);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 107);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(721, 182);
            this.panel2.TabIndex = 1;
            // 
            // groupCertificates
            // 
            this.groupCertificates.Controls.Add(this.gridCertificates);
            this.groupCertificates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupCertificates.Location = new System.Drawing.Point(0, 0);
            this.groupCertificates.Name = "groupCertificates";
            this.groupCertificates.Size = new System.Drawing.Size(721, 182);
            this.groupCertificates.TabIndex = 3;
            this.groupCertificates.TabStop = false;
            this.groupCertificates.Text = "Certificados";
            // 
            // gridCertificates
            // 
            this.gridCertificates.AllowUserToAddRows = false;
            this.gridCertificates.AllowUserToDeleteRows = false;
            this.gridCertificates.AllowUserToOrderColumns = true;
            this.gridCertificates.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gridCertificates.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridCertificates.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridCertificates.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridCertificates.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridCertificates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridCertificates.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colValid,
            this.colError});
            this.gridCertificates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCertificates.Location = new System.Drawing.Point(3, 16);
            this.gridCertificates.Name = "gridCertificates";
            this.gridCertificates.ReadOnly = true;
            this.gridCertificates.RowHeadersVisible = false;
            this.gridCertificates.Size = new System.Drawing.Size(715, 163);
            this.gridCertificates.TabIndex = 3;
            // 
            // colName
            // 
            this.colName.HeaderText = "Subject";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.Width = 68;
            // 
            // colValid
            // 
            this.colValid.HeaderText = "IsValid";
            this.colValid.Name = "colValid";
            this.colValid.ReadOnly = true;
            this.colValid.Width = 63;
            // 
            // colError
            // 
            this.colError.HeaderText = "Error";
            this.colError.Name = "colError";
            this.colError.ReadOnly = true;
            this.colError.Width = 54;
            // 
            // fileOpen
            // 
            this.fileOpen.FileName = "openFileDialog1";
            this.fileOpen.FileOk += new System.ComponentModel.CancelEventHandler(this.fileOpen_FileOk);
            // 
            // lblPwd
            // 
            this.lblPwd.AutoSize = true;
            this.lblPwd.Location = new System.Drawing.Point(6, 49);
            this.lblPwd.Name = "lblPwd";
            this.lblPwd.Size = new System.Drawing.Size(56, 13);
            this.lblPwd.TabIndex = 7;
            this.lblPwd.Text = "Password:";
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(6, 24);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(46, 13);
            this.lblFile.TabIndex = 8;
            this.lblFile.Text = "Arquivo:";
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(68, 46);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(95, 20);
            this.txtPwd.TabIndex = 9;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(9, 77);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 24);
            this.btnLoad.TabIndex = 10;
            this.btnLoad.Text = "Validar";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // UcKeyStoreView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelSelectFile);
            this.Name = "UcKeyStoreView";
            this.Size = new System.Drawing.Size(721, 289);
            this.panelSelectFile.ResumeLayout(false);
            this.groupSelectFile.ResumeLayout(false);
            this.groupSelectFile.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupCertificates.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCertificates)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSelectFile;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.OpenFileDialog fileOpen;
        private System.Windows.Forms.GroupBox groupCertificates;
        private System.Windows.Forms.DataGridView gridCertificates;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colError;
        private System.Windows.Forms.GroupBox groupSelectFile;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox txtKeyStoreFile;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.Label lblPwd;
        private System.Windows.Forms.Button btnLoad;
    }
}
