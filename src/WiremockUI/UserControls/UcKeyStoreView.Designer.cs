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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelSelectFile = new System.Windows.Forms.Panel();
            this.groupSelectFile = new System.Windows.Forms.GroupBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.txtKeyStoreFile = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gridCertificates = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colError = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileOpen = new System.Windows.Forms.OpenFileDialog();
            this.panelSelectFile.SuspendLayout();
            this.groupSelectFile.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCertificates)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSelectFile
            // 
            this.panelSelectFile.Controls.Add(this.groupSelectFile);
            this.panelSelectFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSelectFile.Location = new System.Drawing.Point(0, 0);
            this.panelSelectFile.Name = "panelSelectFile";
            this.panelSelectFile.Size = new System.Drawing.Size(721, 51);
            this.panelSelectFile.TabIndex = 0;
            // 
            // groupSelectFile
            // 
            this.groupSelectFile.Controls.Add(this.btnOpen);
            this.groupSelectFile.Controls.Add(this.txtKeyStoreFile);
            this.groupSelectFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupSelectFile.Location = new System.Drawing.Point(0, 0);
            this.groupSelectFile.Name = "groupSelectFile";
            this.groupSelectFile.Size = new System.Drawing.Size(721, 51);
            this.groupSelectFile.TabIndex = 0;
            this.groupSelectFile.TabStop = false;
            this.groupSelectFile.Text = "Store file";
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(479, 23);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 6;
            this.btnOpen.Text = "Localizar";
            this.btnOpen.UseVisualStyleBackColor = true;
            // 
            // txtKeyStoreFile
            // 
            this.txtKeyStoreFile.Location = new System.Drawing.Point(6, 24);
            this.txtKeyStoreFile.Name = "txtKeyStoreFile";
            this.txtKeyStoreFile.Size = new System.Drawing.Size(470, 20);
            this.txtKeyStoreFile.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 51);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(721, 238);
            this.panel2.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gridCertificates);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(721, 238);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Certificados";
            // 
            // gridCertificates
            // 
            this.gridCertificates.AllowUserToAddRows = false;
            this.gridCertificates.AllowUserToDeleteRows = false;
            this.gridCertificates.AllowUserToOrderColumns = true;
            this.gridCertificates.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gridCertificates.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.gridCertificates.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridCertificates.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridCertificates.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
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
            this.gridCertificates.Size = new System.Drawing.Size(715, 219);
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
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCertificates)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSelectFile;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.OpenFileDialog fileOpen;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView gridCertificates;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colError;
        private System.Windows.Forms.GroupBox groupSelectFile;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox txtKeyStoreFile;
    }
}
