using System.Windows.Forms;

namespace WiremockUI
{
    partial class FormCompare
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCompare = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtContent1 = new WiremockUI.EditorTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtFile1 = new System.Windows.Forms.TextBox();
            this.btnOpen1 = new WiremockUI.MenuButton();
            this.txtContent2 = new WiremockUI.EditorTextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtFile2 = new System.Windows.Forms.TextBox();
            this.btnOpen2 = new WiremockUI.MenuButton();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(744, 32);
            this.panel1.TabIndex = 0;
            this.panel1.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCompare);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 218);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(744, 43);
            this.panel2.TabIndex = 1;
            // 
            // btnCompare
            // 
            this.btnCompare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnCompare.Location = new System.Drawing.Point(328, 3);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(89, 37);
            this.btnCompare.TabIndex = 6;
            this.btnCompare.Text = "Comparar";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 32);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtContent1);
            this.splitContainer1.Panel1.Controls.Add(this.panel3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtContent2);
            this.splitContainer1.Panel2.Controls.Add(this.panel4);
            this.splitContainer1.Size = new System.Drawing.Size(744, 186);
            this.splitContainer1.SplitterDistance = 372;
            this.splitContainer1.TabIndex = 4;
            // 
            // txtContent1
            // 
            this.txtContent1.AcceptsTab = true;
            this.txtContent1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContent1.EnableFormatter = false;
            this.txtContent1.Location = new System.Drawing.Point(0, 20);
            this.txtContent1.MaxLength = 0;
            this.txtContent1.Multiline = true;
            this.txtContent1.Name = "txtContent1";
            //this.txtContent1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtContent1.Size = new System.Drawing.Size(372, 166);
            this.txtContent1.TabIndex = 4;
            this.txtContent1.WordWrap = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtFile1);
            this.panel3.Controls.Add(this.btnOpen1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(372, 20);
            this.panel3.TabIndex = 3;
            // 
            // txtFile1
            // 
            this.txtFile1.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtFile1.Location = new System.Drawing.Point(0, 0);
            this.txtFile1.Name = "txtFile1";
            this.txtFile1.Size = new System.Drawing.Size(328, 20);
            this.txtFile1.TabIndex = 0;
            this.txtFile1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFile1_KeyDown);
            // 
            // btnOpen1
            // 
            this.btnOpen1.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOpen1.Location = new System.Drawing.Point(328, 0);
            this.btnOpen1.Name = "btnOpen1";
            this.btnOpen1.Size = new System.Drawing.Size(44, 20);
            this.btnOpen1.TabIndex = 1;
            this.btnOpen1.Text = "...";
            this.btnOpen1.UseVisualStyleBackColor = true;
            // 
            // txtContent2
            // 
            this.txtContent2.AcceptsTab = true;
            this.txtContent2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContent2.EnableFormatter = false;
            this.txtContent2.Location = new System.Drawing.Point(0, 20);
            this.txtContent2.MaxLength = 0;
            this.txtContent2.Multiline = true;
            this.txtContent2.Name = "txtContent2";
            //this.txtContent2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtContent2.Size = new System.Drawing.Size(368, 166);
            this.txtContent2.TabIndex = 5;
            this.txtContent2.WordWrap = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtFile2);
            this.panel4.Controls.Add(this.btnOpen2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(368, 20);
            this.panel4.TabIndex = 4;
            // 
            // txtFile2
            // 
            this.txtFile2.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtFile2.Location = new System.Drawing.Point(0, 0);
            this.txtFile2.Name = "txtFile2";
            this.txtFile2.Size = new System.Drawing.Size(326, 20);
            this.txtFile2.TabIndex = 2;
            this.txtFile2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFile2_KeyDown);
            // 
            // btnOpen2
            // 
            this.btnOpen2.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOpen2.Location = new System.Drawing.Point(326, 0);
            this.btnOpen2.Name = "btnOpen2";
            this.btnOpen2.Size = new System.Drawing.Size(42, 20);
            this.btnOpen2.TabIndex = 3;
            this.btnOpen2.Text = "...";
            this.btnOpen2.UseVisualStyleBackColor = true;
            // 
            // FormCompare
            // 
            this.AcceptButton = this.btnCompare;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 261);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormCompare";
            this.Text = "FormCompare";
            this.Load += new System.EventHandler(this.FormCompare_Load);
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private SplitContainer splitContainer1;
        private EditorTextBox txtContent1;
        private Panel panel3;
        private TextBox txtFile1;
        private MenuButton btnOpen1;
        private EditorTextBox txtContent2;
        private Panel panel4;
        private TextBox txtFile2;
        private MenuButton btnOpen2;
        private Button btnCompare;
    }
}