﻿namespace WiremockUI
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
            this.txtTitle = new WiremockUI.EditorTextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtContent = new WiremockUI.EditorTextBox();
            this.SuspendLayout();
            // 
            // txtTitle
            // 
            this.txtTitle.AcceptsTab = true;
            this.txtTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtTitle.EnableOptions = false;
            this.txtTitle.EnableHistory = true;
            this.txtTitle.Location = new System.Drawing.Point(0, 0);
            this.txtTitle.MaxLength = 0;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.ReadOnly = true;
            this.txtTitle.Size = new System.Drawing.Size(284, 20);
            this.txtTitle.TabIndex = 1;
            this.txtTitle.TextValue = "";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(0, 26);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Fechar";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtContent
            // 
            this.txtContent.AcceptsTab = true;
            this.txtContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContent.EnableOptions = false;
            this.txtContent.EnableHistory = true;
            this.txtContent.Location = new System.Drawing.Point(0, 20);
            this.txtContent.MaxLength = 0;
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(284, 241);
            this.txtContent.TabIndex = 4;
            this.txtContent.TextValue = "";
            // 
            // FormTextView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FormTextView";
            this.Text = "...";
            this.Load += new System.EventHandler(this.FormTextView_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private EditorTextBox txtTitle;
        private System.Windows.Forms.Button btnClose;
        private EditorTextBox txtContent;
    }
}