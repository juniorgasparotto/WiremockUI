namespace WiremockUI
{
    partial class UcJsonView
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
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabRaw = new System.Windows.Forms.TabPage();
            this.txtContent = new WiremockUI.EditorTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnFormat = new System.Windows.Forms.Button();
            this.tabTree = new System.Windows.Forms.TabPage();
            this.lblError = new System.Windows.Forms.Label();
            this.treeRaw = new System.Windows.Forms.TreeView();
            this.tabs.SuspendLayout();
            this.tabRaw.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabTree.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabRaw);
            this.tabs.Controls.Add(this.tabTree);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(150, 150);
            this.tabs.TabIndex = 6;
            this.tabs.Click += new System.EventHandler(this.tabs_Click);
            // 
            // tabRaw
            // 
            this.tabRaw.Controls.Add(this.txtContent);
            this.tabRaw.Controls.Add(this.panel1);
            this.tabRaw.Location = new System.Drawing.Point(4, 22);
            this.tabRaw.Name = "tabRaw";
            this.tabRaw.Padding = new System.Windows.Forms.Padding(3);
            this.tabRaw.Size = new System.Drawing.Size(142, 124);
            this.tabRaw.TabIndex = 0;
            this.tabRaw.Text = "Raw";
            this.tabRaw.UseVisualStyleBackColor = true;
            // 
            // txtContent
            // 
            this.txtContent.AcceptsTab = true;
            this.txtContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContent.EnableFormatter = false;
            this.txtContent.Location = new System.Drawing.Point(3, 42);
            this.txtContent.MaxLength = 0;
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            //this.txtContent.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtContent.Size = new System.Drawing.Size(136, 79);
            this.txtContent.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnFormat);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(136, 39);
            this.panel1.TabIndex = 10;
            // 
            // btnFormat
            // 
            this.btnFormat.Location = new System.Drawing.Point(0, 10);
            this.btnFormat.Name = "btnFormat";
            this.btnFormat.Size = new System.Drawing.Size(124, 23);
            this.btnFormat.TabIndex = 0;
            this.btnFormat.Text = "Formatar JSON";
            this.btnFormat.UseVisualStyleBackColor = true;
            this.btnFormat.Click += new System.EventHandler(this.btnFormat_Click);
            // 
            // tabTree
            // 
            this.tabTree.Controls.Add(this.lblError);
            this.tabTree.Controls.Add(this.treeRaw);
            this.tabTree.Location = new System.Drawing.Point(4, 22);
            this.tabTree.Name = "tabTree";
            this.tabTree.Padding = new System.Windows.Forms.Padding(3);
            this.tabTree.Size = new System.Drawing.Size(142, 124);
            this.tabTree.TabIndex = 1;
            this.tabTree.Text = "JSON Viewer";
            this.tabTree.UseVisualStyleBackColor = true;
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.BackColor = System.Drawing.Color.White;
            this.lblError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(3, 3);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(33, 13);
            this.lblError.TabIndex = 6;
            this.lblError.Text = "error";
            this.lblError.Visible = false;
            // 
            // treeRaw
            // 
            this.treeRaw.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeRaw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeRaw.Location = new System.Drawing.Point(3, 3);
            this.treeRaw.Name = "treeRaw";
            this.treeRaw.Size = new System.Drawing.Size(136, 118);
            this.treeRaw.TabIndex = 7;
            // 
            // UcJsonView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabs);
            this.Name = "UcJsonView";
            this.tabs.ResumeLayout(false);
            this.tabRaw.ResumeLayout(false);
            this.tabRaw.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabTree.ResumeLayout(false);
            this.tabTree.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabRaw;
        private System.Windows.Forms.TabPage tabTree;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.TreeView treeRaw;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnFormat;
        private EditorTextBox txtContent;
    }
}
