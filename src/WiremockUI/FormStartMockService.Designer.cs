namespace WiremockUI
{
    partial class FormStartMockService
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
            this.btnClean = new System.Windows.Forms.Button();
            this.txtTo = new WiremockUI.EditorTextbox();
            this.txtFrom = new WiremockUI.EditorTextbox();
            this.lblUrlOriginal = new System.Windows.Forms.LinkLabel();
            this.lblOpenFolder = new System.Windows.Forms.LinkLabel();
            this.lblUrlProxy = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.btnClean);
            this.panel1.Controls.Add(this.txtTo);
            this.panel1.Controls.Add(this.txtFrom);
            this.panel1.Controls.Add(this.lblUrlOriginal);
            this.panel1.Controls.Add(this.lblOpenFolder);
            this.panel1.Controls.Add(this.lblUrlProxy);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(584, 108);
            this.panel1.TabIndex = 0;
            // 
            // btnClean
            // 
            this.btnClean.Location = new System.Drawing.Point(9, 79);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(75, 23);
            this.btnClean.TabIndex = 25;
            this.btnClean.Text = "Limpar";
            this.btnClean.UseVisualStyleBackColor = true;
            this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // txtTo
            // 
            this.txtTo.AcceptsTab = true;
            this.txtTo.Location = new System.Drawing.Point(76, 51);
            this.txtTo.Multiline = true;
            this.txtTo.Name = "txtTo";
            this.txtTo.ReadOnly = true;
            this.txtTo.Size = new System.Drawing.Size(455, 20);
            this.txtTo.TabIndex = 24;
            // 
            // txtFrom
            // 
            this.txtFrom.AcceptsTab = true;
            this.txtFrom.Location = new System.Drawing.Point(76, 23);
            this.txtFrom.Multiline = true;
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.ReadOnly = true;
            this.txtFrom.Size = new System.Drawing.Size(455, 20);
            this.txtFrom.TabIndex = 24;
            // 
            // lblUrlOriginal
            // 
            this.lblUrlOriginal.AutoSize = true;
            this.lblUrlOriginal.Location = new System.Drawing.Point(537, 27);
            this.lblUrlOriginal.Name = "lblUrlOriginal";
            this.lblUrlOriginal.Size = new System.Drawing.Size(23, 13);
            this.lblUrlOriginal.TabIndex = 20;
            this.lblUrlOriginal.TabStop = true;
            this.lblUrlOriginal.Text = "Ver";
            this.lblUrlOriginal.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblUrlOriginal_LinkClicked);
            // 
            // lblOpenFolder
            // 
            this.lblOpenFolder.AutoSize = true;
            this.lblOpenFolder.Location = new System.Drawing.Point(90, 84);
            this.lblOpenFolder.Name = "lblOpenFolder";
            this.lblOpenFolder.Size = new System.Drawing.Size(53, 13);
            this.lblOpenFolder.TabIndex = 23;
            this.lblOpenFolder.TabStop = true;
            this.lblOpenFolder.Text = "Abrir local";
            this.lblOpenFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblOpenFolder_LinkClicked_1);
            // 
            // lblUrlProxy
            // 
            this.lblUrlProxy.AutoSize = true;
            this.lblUrlProxy.Location = new System.Drawing.Point(537, 55);
            this.lblUrlProxy.Name = "lblUrlProxy";
            this.lblUrlProxy.Size = new System.Drawing.Size(23, 13);
            this.lblUrlProxy.TabIndex = 21;
            this.lblUrlProxy.TabStop = true;
            this.lblUrlProxy.Text = "Ver";
            this.lblUrlProxy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblUrlProxy_LinkClicked);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Url PARA:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Url DE:";
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(0, 108);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(584, 153);
            this.txtLog.TabIndex = 12;
            // 
            // FormStartMockService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(584, 261);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormStartMockService";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel lblOpenFolder;
        private System.Windows.Forms.LinkLabel lblUrlProxy;
        private System.Windows.Forms.LinkLabel lblUrlOriginal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLog;
        private EditorTextbox txtTo;
        private EditorTextbox txtFrom;
        private System.Windows.Forms.Button btnClean;
    }
}