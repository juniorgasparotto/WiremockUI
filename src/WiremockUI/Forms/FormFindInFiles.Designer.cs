namespace WiremockUI
{
    partial class FormFindInFiles
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.lblFolder = new System.Windows.Forms.Label();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.pnlReplace = new System.Windows.Forms.Panel();
            this.btnReplaceAll = new System.Windows.Forms.Button();
            this.txtReplaceText = new System.Windows.Forms.TextBox();
            this.imgToggleReplace = new System.Windows.Forms.PictureBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearchValue = new System.Windows.Forms.TextBox();
            this.gridFiles = new System.Windows.Forms.DataGridView();
            this.chkCaseSensitive = new System.Windows.Forms.CheckBox();
            this.colSequence = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlSearch.SuspendLayout();
            this.pnlReplace.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgToggleReplace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridFiles)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlSearch.Controls.Add(this.chkCaseSensitive);
            this.pnlSearch.Controls.Add(this.lblFolder);
            this.pnlSearch.Controls.Add(this.txtFolder);
            this.pnlSearch.Controls.Add(this.pnlReplace);
            this.pnlSearch.Controls.Add(this.imgToggleReplace);
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Controls.Add(this.txtSearchValue);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(0, 0);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(496, 125);
            this.pnlSearch.TabIndex = 32;
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFolder.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblFolder.Location = new System.Drawing.Point(27, 9);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(103, 13);
            this.lblFolder.TabIndex = 11;
            this.lblFolder.Text = "Buscar na pasta:";
            // 
            // txtFolder
            // 
            this.txtFolder.AcceptsTab = true;
            this.txtFolder.Location = new System.Drawing.Point(30, 25);
            this.txtFolder.MaxLength = 0;
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(225, 20);
            this.txtFolder.TabIndex = 10;
            // 
            // pnlReplace
            // 
            this.pnlReplace.Controls.Add(this.btnReplaceAll);
            this.pnlReplace.Controls.Add(this.txtReplaceText);
            this.pnlReplace.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlReplace.Location = new System.Drawing.Point(0, 94);
            this.pnlReplace.Name = "pnlReplace";
            this.pnlReplace.Size = new System.Drawing.Size(496, 31);
            this.pnlReplace.TabIndex = 9;
            // 
            // btnReplaceAll
            // 
            this.btnReplaceAll.Location = new System.Drawing.Point(250, 1);
            this.btnReplaceAll.Name = "btnReplaceAll";
            this.btnReplaceAll.Size = new System.Drawing.Size(95, 22);
            this.btnReplaceAll.TabIndex = 9;
            this.btnReplaceAll.Text = "Substituir todos";
            this.btnReplaceAll.UseVisualStyleBackColor = true;
            this.btnReplaceAll.Click += new System.EventHandler(this.btnReplaceAll_Click);
            // 
            // txtReplaceText
            // 
            this.txtReplaceText.AcceptsTab = true;
            this.txtReplaceText.Location = new System.Drawing.Point(30, 2);
            this.txtReplaceText.MaxLength = 0;
            this.txtReplaceText.Name = "txtReplaceText";
            this.txtReplaceText.Size = new System.Drawing.Size(214, 20);
            this.txtReplaceText.TabIndex = 7;
            // 
            // imgToggleReplace
            // 
            this.imgToggleReplace.Image = global::WiremockUI.Properties.Resources.arrow_down;
            this.imgToggleReplace.InitialImage = null;
            this.imgToggleReplace.Location = new System.Drawing.Point(11, 56);
            this.imgToggleReplace.Name = "imgToggleReplace";
            this.imgToggleReplace.Size = new System.Drawing.Size(13, 13);
            this.imgToggleReplace.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgToggleReplace.TabIndex = 7;
            this.imgToggleReplace.TabStop = false;
            this.imgToggleReplace.Click += new System.EventHandler(this.imgToggleReplace_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(250, 51);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(95, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Buscar";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearchValue
            // 
            this.txtSearchValue.AcceptsTab = true;
            this.txtSearchValue.Location = new System.Drawing.Point(30, 52);
            this.txtSearchValue.MaxLength = 0;
            this.txtSearchValue.Name = "txtSearchValue";
            this.txtSearchValue.Size = new System.Drawing.Size(214, 20);
            this.txtSearchValue.TabIndex = 0;
            // 
            // gridFiles
            // 
            this.gridFiles.AllowUserToAddRows = false;
            this.gridFiles.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gridFiles.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridFiles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridFiles.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.gridFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSequence,
            this.colFilePath,
            this.colContent});
            this.gridFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridFiles.Location = new System.Drawing.Point(0, 125);
            this.gridFiles.Name = "gridFiles";
            this.gridFiles.Size = new System.Drawing.Size(496, 136);
            this.gridFiles.TabIndex = 33;
            this.gridFiles.DoubleClick += new System.EventHandler(this.gridFiles_DoubleClick);
            // 
            // chkCaseSensitive
            // 
            this.chkCaseSensitive.AutoSize = true;
            this.chkCaseSensitive.Location = new System.Drawing.Point(30, 73);
            this.chkCaseSensitive.Name = "chkCaseSensitive";
            this.chkCaseSensitive.Size = new System.Drawing.Size(94, 17);
            this.chkCaseSensitive.TabIndex = 12;
            this.chkCaseSensitive.Text = "Case sensitive";
            this.chkCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // colSequence
            // 
            this.colSequence.Frozen = true;
            this.colSequence.HeaderText = "Seq.";
            this.colSequence.Name = "colSequence";
            this.colSequence.ReadOnly = true;
            this.colSequence.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colSequence.Width = 35;
            // 
            // colFilePath
            // 
            this.colFilePath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colFilePath.HeaderText = "File";
            this.colFilePath.Name = "colFilePath";
            this.colFilePath.ReadOnly = true;
            this.colFilePath.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colFilePath.Width = 29;
            // 
            // colContent
            // 
            this.colContent.HeaderText = "Content";
            this.colContent.Name = "colContent";
            this.colContent.ReadOnly = true;
            this.colContent.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colContent.Width = 50;
            // 
            // FormFindInFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 261);
            this.Controls.Add(this.gridFiles);
            this.Controls.Add(this.pnlSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FormFindInFiles";
            this.Text = "...";
            this.Resize += new System.EventHandler(this.FormFindInFiles_Resize);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlReplace.ResumeLayout(false);
            this.pnlReplace.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgToggleReplace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridFiles)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Panel pnlReplace;
        private System.Windows.Forms.Button btnReplaceAll;
        private System.Windows.Forms.TextBox txtReplaceText;
        private System.Windows.Forms.PictureBox imgToggleReplace;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearchValue;
        private System.Windows.Forms.DataGridView gridFiles;
        private System.Windows.Forms.Label lblFolder;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.CheckBox chkCaseSensitive;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSequence;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFilePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContent;
    }
}