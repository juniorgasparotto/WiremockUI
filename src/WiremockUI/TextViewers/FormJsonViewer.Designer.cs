namespace WiremockUI
{
    partial class FormJsonViewer
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
            this.ucJsonView = new WiremockUI.UcJsonView();
            this.SuspendLayout();
            // 
            // ucJsonView
            // 
            this.ucJsonView.ContentJson = "";
            this.ucJsonView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucJsonView.ExpandAll = false;
            this.ucJsonView.Location = new System.Drawing.Point(0, 0);
            this.ucJsonView.Name = "ucJsonView";
            this.ucJsonView.Size = new System.Drawing.Size(284, 261);
            this.ucJsonView.TabIndex = 0;
            // 
            // FormJsonViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.ucJsonView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormJsonViewer";
            this.Text = "FormJsonViewer";
            this.ResumeLayout(false);

        }

        #endregion

        private UcJsonView ucJsonView;
    }
}