using System.Windows.Forms;

namespace WiremockUI
{
    partial class SimpleEditor : UserControl
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
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.pnlReplace = new System.Windows.Forms.Panel();
            this.btnReplaceAll = new System.Windows.Forms.Button();
            this.btnReplace = new System.Windows.Forms.Button();
            this.txtReplaceText = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.imgClose = new System.Windows.Forms.PictureBox();
            this.imgToggleReplace = new System.Windows.Forms.PictureBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearchValue = new System.Windows.Forms.TextBox();
            this.txtContent = new System.Windows.Forms.RichTextBox();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlSearch.SuspendLayout();
            this.pnlReplace.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgToggleReplace)).BeginInit();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlSearch.Controls.Add(this.pnlReplace);
            this.pnlSearch.Controls.Add(this.panel1);
            this.pnlSearch.Controls.Add(this.imgToggleReplace);
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Controls.Add(this.txtSearchValue);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(0, 0);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(629, 67);
            this.pnlSearch.TabIndex = 31;
            this.pnlSearch.Visible = false;
            // 
            // pnlReplace
            // 
            this.pnlReplace.Controls.Add(this.btnReplaceAll);
            this.pnlReplace.Controls.Add(this.btnReplace);
            this.pnlReplace.Controls.Add(this.txtReplaceText);
            this.pnlReplace.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlReplace.Location = new System.Drawing.Point(0, 36);
            this.pnlReplace.Name = "pnlReplace";
            this.pnlReplace.Size = new System.Drawing.Size(604, 31);
            this.pnlReplace.TabIndex = 9;
            // 
            // btnReplaceAll
            // 
            this.btnReplaceAll.Location = new System.Drawing.Point(331, 1);
            this.btnReplaceAll.Name = "btnReplaceAll";
            this.btnReplaceAll.Size = new System.Drawing.Size(95, 22);
            this.btnReplaceAll.TabIndex = 9;
            this.btnReplaceAll.Text = "Substituir todos";
            this.btnReplaceAll.UseVisualStyleBackColor = true;
            this.btnReplaceAll.Click += new System.EventHandler(this.btnReplaceAll_Click);
            // 
            // btnReplace
            // 
            this.btnReplace.Location = new System.Drawing.Point(250, 1);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(75, 22);
            this.btnReplace.TabIndex = 8;
            this.btnReplace.Text = "Substituir";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
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
            // panel1
            // 
            this.panel1.Controls.Add(this.imgClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(604, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(25, 67);
            this.panel1.TabIndex = 8;
            // 
            // imgClose
            // 
            this.imgClose.Image = global::WiremockUI.Properties.Resources.close_2;
            this.imgClose.InitialImage = null;
            this.imgClose.Location = new System.Drawing.Point(5, 5);
            this.imgClose.Name = "imgClose";
            this.imgClose.Size = new System.Drawing.Size(12, 10);
            this.imgClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgClose.TabIndex = 8;
            this.imgClose.TabStop = false;
            this.imgClose.Click += new System.EventHandler(this.imgClose_Click);
            // 
            // imgToggleReplace
            // 
            this.imgToggleReplace.Image = global::WiremockUI.Properties.Resources.arrow_down;
            this.imgToggleReplace.InitialImage = null;
            this.imgToggleReplace.Location = new System.Drawing.Point(11, 13);
            this.imgToggleReplace.Name = "imgToggleReplace";
            this.imgToggleReplace.Size = new System.Drawing.Size(13, 13);
            this.imgToggleReplace.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgToggleReplace.TabIndex = 7;
            this.imgToggleReplace.TabStop = false;
            this.imgToggleReplace.Click += new System.EventHandler(this.imgToggleReplace_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(250, 8);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 22);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Buscar";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearchValue
            // 
            this.txtSearchValue.AcceptsTab = true;
            this.txtSearchValue.Location = new System.Drawing.Point(30, 9);
            this.txtSearchValue.MaxLength = 0;
            this.txtSearchValue.Name = "txtSearchValue";
            this.txtSearchValue.Size = new System.Drawing.Size(214, 20);
            this.txtSearchValue.TabIndex = 0;
            this.txtSearchValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchValue_KeyDown);
            // 
            // txtContent
            // 
            this.txtContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContent.Location = new System.Drawing.Point(0, 0);
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(629, 220);
            this.txtContent.TabIndex = 32;
            this.txtContent.Text = "";
            this.txtContent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContent_KeyDown);
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.SystemColors.Control;
            this.pnlContent.Controls.Add(this.txtContent);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 67);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(629, 220);
            this.pnlContent.TabIndex = 33;
            // 
            // SimpleEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlSearch);
            this.Name = "SimpleEditor";
            this.Size = new System.Drawing.Size(629, 287);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlReplace.ResumeLayout(false);
            this.pnlReplace.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgToggleReplace)).EndInit();
            this.pnlContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel pnlSearch;
        private Button btnSearch;
        private TextBox txtSearchValue;
        private RichTextBox txtContent;
        private Panel pnlContent;
        private PictureBox imgToggleReplace;
        private Panel panel1;
        private PictureBox imgClose;
        private Panel pnlReplace;
        private Button btnReplaceAll;
        private Button btnReplace;
        private TextBox txtReplaceText;
    }
}
