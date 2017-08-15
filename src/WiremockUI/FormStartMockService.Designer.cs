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
            this.chkAutoScroll = new System.Windows.Forms.CheckBox();
            this.btnClean = new System.Windows.Forms.Button();
            this.txtTo = new WiremockUI.EditorTextbox();
            this.txtFrom = new WiremockUI.EditorTextbox();
            this.lblUrlOriginal = new System.Windows.Forms.LinkLabel();
            this.lblOpenFolder = new System.Windows.Forms.LinkLabel();
            this.lblUrlProxy = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rtxtLog = new System.Windows.Forms.RichTextBox();
            this.chkDisable = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.chkDisable);
            this.panel1.Controls.Add(this.chkAutoScroll);
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
            // chkAutoScroll
            // 
            this.chkAutoScroll.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.chkAutoScroll.AutoSize = true;
            this.chkAutoScroll.Checked = true;
            this.chkAutoScroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoScroll.Location = new System.Drawing.Point(425, 85);
            this.chkAutoScroll.Name = "chkAutoScroll";
            this.chkAutoScroll.Size = new System.Drawing.Size(135, 17);
            this.chkAutoScroll.TabIndex = 26;
            this.chkAutoScroll.Text = "Rolar automáticamente";
            this.chkAutoScroll.UseVisualStyleBackColor = true;
            this.chkAutoScroll.CheckedChanged += new System.EventHandler(this.chkAutoScroll_CheckedChanged);
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
            this.txtTo.Location = new System.Drawing.Point(91, 51);
            this.txtTo.Multiline = true;
            this.txtTo.Name = "txtTo";
            this.txtTo.ReadOnly = true;
            this.txtTo.Size = new System.Drawing.Size(440, 20);
            this.txtTo.TabIndex = 24;
            // 
            // txtFrom
            // 
            this.txtFrom.AcceptsTab = true;
            this.txtFrom.Location = new System.Drawing.Point(91, 23);
            this.txtFrom.Multiline = true;
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.ReadOnly = true;
            this.txtFrom.Size = new System.Drawing.Size(440, 20);
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
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Url do proxy:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Url destino:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rtxtLog);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 108);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(584, 153);
            this.panel2.TabIndex = 1;
            // 
            // rtxtLog
            // 
            this.rtxtLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtLog.Location = new System.Drawing.Point(0, 0);
            this.rtxtLog.Name = "rtxtLog";
            this.rtxtLog.ReadOnly = true;
            this.rtxtLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.rtxtLog.ShowSelectionMargin = true;
            this.rtxtLog.Size = new System.Drawing.Size(584, 153);
            this.rtxtLog.TabIndex = 14;
            this.rtxtLog.Text = "";
            // 
            // chkDisable
            // 
            this.chkDisable.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.chkDisable.AutoSize = true;
            this.chkDisable.Location = new System.Drawing.Point(321, 84);
            this.chkDisable.Name = "chkDisable";
            this.chkDisable.Size = new System.Drawing.Size(88, 17);
            this.chkDisable.TabIndex = 27;
            this.chkDisable.Text = "Desativar log";
            this.chkDisable.UseVisualStyleBackColor = true;
            this.chkDisable.CheckedChanged += new System.EventHandler(this.chkDisable_CheckedChanged);
            // 
            // FormStartMockService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(584, 261);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormStartMockService";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel lblOpenFolder;
        private System.Windows.Forms.LinkLabel lblUrlProxy;
        private System.Windows.Forms.LinkLabel lblUrlOriginal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private EditorTextbox txtTo;
        private EditorTextbox txtFrom;
        private System.Windows.Forms.Button btnClean;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox rtxtLog;
        private System.Windows.Forms.CheckBox chkAutoScroll;
        private System.Windows.Forms.CheckBox chkDisable;
    }
}