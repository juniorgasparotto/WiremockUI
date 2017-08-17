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
            this.chkDisable = new System.Windows.Forms.CheckBox();
            this.chkAutoScroll = new System.Windows.Forms.CheckBox();
            this.btnClean = new System.Windows.Forms.Button();
            this.txtTo = new WiremockUI.EditorTextbox();
            this.txtFrom = new WiremockUI.EditorTextbox();
            this.lblUrlTarget = new System.Windows.Forms.LinkLabel();
            this.lblOpenFolder = new System.Windows.Forms.LinkLabel();
            this.lblUrlProxy = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.rtxtLog = new System.Windows.Forms.RichTextBox();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearchValue = new WiremockUI.EditorTextbox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlSearch.SuspendLayout();
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
            this.panel1.Controls.Add(this.lblUrlTarget);
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
            // lblUrlTarget
            // 
            this.lblUrlTarget.AutoSize = true;
            this.lblUrlTarget.Location = new System.Drawing.Point(537, 27);
            this.lblUrlTarget.Name = "lblUrlTarget";
            this.lblUrlTarget.Size = new System.Drawing.Size(23, 13);
            this.lblUrlTarget.TabIndex = 20;
            this.lblUrlTarget.TabStop = true;
            this.lblUrlTarget.Text = "Ver";
            this.lblUrlTarget.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblUrlTarget_LinkClicked);
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
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.pnlSearch);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 108);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(584, 153);
            this.panel2.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.rtxtLog);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 36);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(584, 117);
            this.panel4.TabIndex = 17;
            // 
            // rtxtLog
            // 
            this.rtxtLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtLog.HideSelection = false;
            this.rtxtLog.Location = new System.Drawing.Point(0, 0);
            this.rtxtLog.Name = "rtxtLog";
            this.rtxtLog.ReadOnly = true;
            this.rtxtLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.rtxtLog.ShowSelectionMargin = true;
            this.rtxtLog.Size = new System.Drawing.Size(584, 117);
            this.rtxtLog.TabIndex = 29;
            this.rtxtLog.Text = "";
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlSearch.Controls.Add(this.btnClose);
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Controls.Add(this.txtSearchValue);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(0, 0);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(584, 36);
            this.pnlSearch.TabIndex = 16;
            this.pnlSearch.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(310, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 27);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Fechar";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(229, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 27);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Buscar";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearchValue
            // 
            this.txtSearchValue.AcceptsTab = true;
            this.txtSearchValue.Location = new System.Drawing.Point(9, 8);
            this.txtSearchValue.Name = "txtSearchValue";
            this.txtSearchValue.Size = new System.Drawing.Size(214, 20);
            this.txtSearchValue.TabIndex = 0;
            this.txtSearchValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchValue_KeyDown);
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
            this.KeyPreview = true;
            this.Name = "FormStartMockService";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormStartMockService_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel lblOpenFolder;
        private System.Windows.Forms.LinkLabel lblUrlProxy;
        private System.Windows.Forms.LinkLabel lblUrlTarget;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private EditorTextbox txtTo;
        private EditorTextbox txtFrom;
        private System.Windows.Forms.Button btnClean;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chkAutoScroll;
        private System.Windows.Forms.CheckBox chkDisable;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RichTextBox rtxtLog;
        private System.Windows.Forms.Panel pnlSearch;
        private EditorTextbox txtSearchValue;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnClose;
    }
}