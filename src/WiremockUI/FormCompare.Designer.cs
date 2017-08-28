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
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtContent1 = new WiremockUI.EditorTextBox();
            this.txtContent2 = new WiremockUI.EditorTextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(744, 32);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(4, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Comparar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 229);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(744, 32);
            this.panel2.TabIndex = 1;
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
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtContent2);
            this.splitContainer1.Size = new System.Drawing.Size(744, 197);
            this.splitContainer1.SplitterDistance = 372;
            this.splitContainer1.TabIndex = 4;
            // 
            // txtContent1
            // 
            this.txtContent1.AcceptsTab = true;
            this.txtContent1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContent1.EnableFormatter = false;
            this.txtContent1.Location = new System.Drawing.Point(0, 0);
            this.txtContent1.Multiline = true;
            this.txtContent1.Name = "txtContent1";
            this.txtContent1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtContent1.Size = new System.Drawing.Size(372, 197);
            this.txtContent1.TabIndex = 0;
            this.txtContent1.WordWrap = false;
            // 
            // txtContent2
            // 
            this.txtContent2.AcceptsTab = true;
            this.txtContent2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContent2.EnableFormatter = false;
            this.txtContent2.Location = new System.Drawing.Point(0, 0);
            this.txtContent2.Multiline = true;
            this.txtContent2.Name = "txtContent2";
            this.txtContent2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtContent2.Size = new System.Drawing.Size(368, 197);
            this.txtContent2.TabIndex = 1;
            this.txtContent2.WordWrap = false;
            // 
            // FormCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 261);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormCompare";
            this.Text = "FormCompare";
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Button button1;
        private SplitContainer splitContainer1;
        private EditorTextBox txtContent1;
        private EditorTextBox txtContent2;
    }
}