namespace WiremockUI
{
    partial class FormTextView
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
            this.txtTitle = new WiremockUI.EditorTextbox();
            this.txtContent = new WiremockUI.EditorTextbox();
            this.SuspendLayout();
            // 
            // txtTitle
            // 
            this.txtTitle.AcceptsTab = true;
            this.txtTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtTitle.Location = new System.Drawing.Point(0, 0);
            this.txtTitle.Multiline = true;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.ReadOnly = true;
            this.txtTitle.Size = new System.Drawing.Size(284, 20);
            this.txtTitle.TabIndex = 1;
            // 
            // txtContent
            // 
            this.txtContent.AcceptsTab = true;
            this.txtContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContent.Location = new System.Drawing.Point(0, 20);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(284, 241);
            this.txtContent.TabIndex = 5;
            // 
            // FormTextView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.txtTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FormTextView";
            this.Text = "...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private EditorTextbox txtTitle;
        private EditorTextbox txtContent;
    }
}