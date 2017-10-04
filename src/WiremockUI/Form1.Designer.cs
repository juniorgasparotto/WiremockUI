namespace WiremockUI
{
    partial class Form1
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
            this.editorTextBox1 = new WiremockUI.EditorTextBox();
            this.SuspendLayout();
            // 
            // editorTextBox1
            // 
            this.editorTextBox1.BackColor = System.Drawing.SystemColors.Control;
            this.editorTextBox1.EnableHistory = false;
            this.editorTextBox1.Location = new System.Drawing.Point(30, 3);
            this.editorTextBox1.Name = "editorTextBox1";
            this.editorTextBox1.ReadOnly = false;
            this.editorTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
            this.editorTextBox1.SelectedText = "";
            this.editorTextBox1.SelectionColor = System.Drawing.Color.Black;
            this.editorTextBox1.SelectionFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editorTextBox1.SelectionLength = 0;
            this.editorTextBox1.SelectionStart = 0;
            this.editorTextBox1.ShowSelectionMargin = false;
            this.editorTextBox1.Size = new System.Drawing.Size(629, 287);
            this.editorTextBox1.TabIndex = 0;
            this.editorTextBox1.WordWrap = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.editorTextBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private EditorTextBox editorTextBox1;
    }
}