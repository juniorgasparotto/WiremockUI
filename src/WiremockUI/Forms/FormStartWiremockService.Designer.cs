namespace WiremockUI
{
    partial class FormStartWiremockService
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tabLogs = new System.Windows.Forms.TabControl();
            this.tabLogText = new System.Windows.Forms.TabPage();
            this.rtxtLog = new WiremockUI.EditorTextBox();
            this.tabLogTable = new System.Windows.Forms.TabPage();
            this.gridLog = new System.Windows.Forms.DataGridView();
            this.Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TypeLog = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Method = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Url = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RequestTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResponseTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Raw = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusDiff = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.linkOpenFolder = new System.Windows.Forms.LinkLabel();
            this.linkUrlTarget = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkDisableTextLog = new System.Windows.Forms.CheckBox();
            this.chkDisableTableLog = new System.Windows.Forms.CheckBox();
            this.chkAutoScroll = new System.Windows.Forms.CheckBox();
            this.btnClean = new System.Windows.Forms.Button();
            this.txtTo = new WiremockUI.EditorTextBox();
            this.txtFrom = new WiremockUI.EditorTextBox();
            this.linkUrlServer = new System.Windows.Forms.LinkLabel();
            this.lblUrlServer = new System.Windows.Forms.Label();
            this.lblUrlTarget = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabLogs.SuspendLayout();
            this.tabLogText.SuspendLayout();
            this.tabLogTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLog)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 108);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(584, 153);
            this.panel2.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tabLogs);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(584, 153);
            this.panel4.TabIndex = 17;
            // 
            // tabLogs
            // 
            this.tabLogs.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabLogs.Controls.Add(this.tabLogText);
            this.tabLogs.Controls.Add(this.tabLogTable);
            this.tabLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabLogs.ItemSize = new System.Drawing.Size(58, 25);
            this.tabLogs.Location = new System.Drawing.Point(0, 0);
            this.tabLogs.Name = "tabLogs";
            this.tabLogs.SelectedIndex = 0;
            this.tabLogs.Size = new System.Drawing.Size(584, 153);
            this.tabLogs.TabIndex = 0;
            this.tabLogs.TabIndexChanged += new System.EventHandler(this.gridLog_SelectionChanged);
            // 
            // tabLogText
            // 
            this.tabLogText.Controls.Add(this.rtxtLog);
            this.tabLogText.Location = new System.Drawing.Point(4, 29);
            this.tabLogText.Name = "tabLogText";
            this.tabLogText.Padding = new System.Windows.Forms.Padding(3);
            this.tabLogText.Size = new System.Drawing.Size(576, 120);
            this.tabLogText.TabIndex = 0;
            this.tabLogText.Text = "Log em texto";
            this.tabLogText.UseVisualStyleBackColor = true;
            // 
            // rtxtLog
            // 
            this.rtxtLog.BackColor = System.Drawing.SystemColors.Control;
            this.rtxtLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtLog.EnableFormatters = false;
            this.rtxtLog.EnableHistory = false;
            this.rtxtLog.Location = new System.Drawing.Point(3, 3);
            this.rtxtLog.Name = "rtxtLog";
            this.rtxtLog.ReadOnly = true;
            this.rtxtLog.Size = new System.Drawing.Size(570, 114);
            this.rtxtLog.TabIndex = 1;
            // 
            // tabLogTable
            // 
            this.tabLogTable.Controls.Add(this.gridLog);
            this.tabLogTable.Controls.Add(this.statusStrip1);
            this.tabLogTable.Location = new System.Drawing.Point(4, 29);
            this.tabLogTable.Name = "tabLogTable";
            this.tabLogTable.Padding = new System.Windows.Forms.Padding(3);
            this.tabLogTable.Size = new System.Drawing.Size(576, 120);
            this.tabLogTable.TabIndex = 1;
            this.tabLogTable.Text = "Tabela";
            this.tabLogTable.UseVisualStyleBackColor = true;
            // 
            // gridLog
            // 
            this.gridLog.AllowUserToAddRows = false;
            this.gridLog.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gridLog.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridLog.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.gridLog.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Number,
            this.TypeLog,
            this.Method,
            this.Url,
            this.Status,
            this.RequestTime,
            this.ResponseTime,
            this.Raw});
            this.gridLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLog.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.gridLog.Location = new System.Drawing.Point(3, 3);
            this.gridLog.Name = "gridLog";
            this.gridLog.RowHeadersWidth = 30;
            this.gridLog.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gridLog.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.gridLog.Size = new System.Drawing.Size(570, 92);
            this.gridLog.TabIndex = 2;
            this.gridLog.SelectionChanged += new System.EventHandler(this.gridLog_SelectionChanged);
            // 
            // Number
            // 
            this.Number.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Number.HeaderText = "Seq.";
            this.Number.Name = "Number";
            this.Number.ReadOnly = true;
            this.Number.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Number.Width = 54;
            // 
            // TypeLog
            // 
            this.TypeLog.HeaderText = "Type";
            this.TypeLog.Name = "TypeLog";
            this.TypeLog.ReadOnly = true;
            this.TypeLog.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TypeLog.Width = 70;
            // 
            // Method
            // 
            this.Method.HeaderText = "Method";
            this.Method.Name = "Method";
            this.Method.ReadOnly = true;
            this.Method.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Method.Width = 50;
            // 
            // Url
            // 
            this.Url.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Url.HeaderText = "Url";
            this.Url.Name = "Url";
            this.Url.ReadOnly = true;
            this.Url.Width = 45;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 62;
            // 
            // RequestTime
            // 
            this.RequestTime.HeaderText = "Request Time";
            this.RequestTime.Name = "RequestTime";
            this.RequestTime.ReadOnly = true;
            // 
            // ResponseTime
            // 
            this.ResponseTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ResponseTime.HeaderText = "Response Time";
            this.ResponseTime.MinimumWidth = 120;
            this.ResponseTime.Name = "ResponseTime";
            this.ResponseTime.ReadOnly = true;
            this.ResponseTime.Width = 120;
            // 
            // Raw
            // 
            this.Raw.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Raw.HeaderText = "Raw";
            this.Raw.Name = "Raw";
            this.Raw.ReadOnly = true;
            this.Raw.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Raw.Width = 54;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelCount,
            this.toolStripStatusCount,
            this.toolStripStatusDiff,
            this.toolStripStatusValue});
            this.statusStrip1.Location = new System.Drawing.Point(3, 95);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(570, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelCount
            // 
            this.toolStripStatusLabelCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabelCount.Name = "toolStripStatusLabelCount";
            this.toolStripStatusLabelCount.Size = new System.Drawing.Size(114, 17);
            this.toolStripStatusLabelCount.Text = "Total de resultados:";
            // 
            // toolStripStatusCount
            // 
            this.toolStripStatusCount.Name = "toolStripStatusCount";
            this.toolStripStatusCount.Size = new System.Drawing.Size(13, 17);
            this.toolStripStatusCount.Text = "0";
            // 
            // toolStripStatusDiff
            // 
            this.toolStripStatusDiff.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusDiff.Name = "toolStripStatusDiff";
            this.toolStripStatusDiff.Size = new System.Drawing.Size(122, 17);
            this.toolStripStatusDiff.Text = "Diferença de tempo:";
            // 
            // toolStripStatusValue
            // 
            this.toolStripStatusValue.Name = "toolStripStatusValue";
            this.toolStripStatusValue.Size = new System.Drawing.Size(317, 15);
            this.toolStripStatusValue.Text = "(Selecione duas celulas de tempo para calcular a diferença)";
            // 
            // linkOpenFolder
            // 
            this.linkOpenFolder.AutoSize = true;
            this.linkOpenFolder.Location = new System.Drawing.Point(90, 84);
            this.linkOpenFolder.Name = "linkOpenFolder";
            this.linkOpenFolder.Size = new System.Drawing.Size(53, 13);
            this.linkOpenFolder.TabIndex = 23;
            this.linkOpenFolder.TabStop = true;
            this.linkOpenFolder.Text = "Abrir local";
            this.linkOpenFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblOpenFolder_LinkClicked);
            // 
            // linkUrlTarget
            // 
            this.linkUrlTarget.AutoSize = true;
            this.linkUrlTarget.Location = new System.Drawing.Point(543, 27);
            this.linkUrlTarget.Name = "linkUrlTarget";
            this.linkUrlTarget.Size = new System.Drawing.Size(23, 13);
            this.linkUrlTarget.TabIndex = 20;
            this.linkUrlTarget.TabStop = true;
            this.linkUrlTarget.Text = "Ver";
            this.linkUrlTarget.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblUrlTarget_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.chkDisableTextLog);
            this.panel1.Controls.Add(this.chkDisableTableLog);
            this.panel1.Controls.Add(this.chkAutoScroll);
            this.panel1.Controls.Add(this.btnClean);
            this.panel1.Controls.Add(this.txtTo);
            this.panel1.Controls.Add(this.txtFrom);
            this.panel1.Controls.Add(this.linkUrlTarget);
            this.panel1.Controls.Add(this.linkOpenFolder);
            this.panel1.Controls.Add(this.linkUrlServer);
            this.panel1.Controls.Add(this.lblUrlServer);
            this.panel1.Controls.Add(this.lblUrlTarget);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(584, 108);
            this.panel1.TabIndex = 0;
            // 
            // chkDisableTextLog
            // 
            this.chkDisableTextLog.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.chkDisableTextLog.AutoSize = true;
            this.chkDisableTextLog.Checked = true;
            this.chkDisableTextLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDisableTextLog.Location = new System.Drawing.Point(149, 84);
            this.chkDisableTextLog.Name = "chkDisableTextLog";
            this.chkDisableTextLog.Size = new System.Drawing.Size(129, 17);
            this.chkDisableTextLog.TabIndex = 28;
            this.chkDisableTextLog.Text = "Desativar log de texto";
            this.chkDisableTextLog.UseVisualStyleBackColor = true;
            this.chkDisableTextLog.CheckedChanged += new System.EventHandler(this.chkDisableTextLog_CheckedChanged);
            // 
            // chkDisableTableLog
            // 
            this.chkDisableTableLog.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.chkDisableTableLog.AutoSize = true;
            this.chkDisableTableLog.Location = new System.Drawing.Point(284, 84);
            this.chkDisableTableLog.Name = "chkDisableTableLog";
            this.chkDisableTableLog.Size = new System.Drawing.Size(135, 17);
            this.chkDisableTableLog.TabIndex = 27;
            this.chkDisableTableLog.Text = "Desativar log de tabela";
            this.chkDisableTableLog.UseVisualStyleBackColor = true;
            this.chkDisableTableLog.CheckedChanged += new System.EventHandler(this.chkDisableTableLog_CheckedChanged);
            // 
            // chkAutoScroll
            // 
            this.chkAutoScroll.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.chkAutoScroll.AutoSize = true;
            this.chkAutoScroll.Checked = true;
            this.chkAutoScroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoScroll.Location = new System.Drawing.Point(423, 84);
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
            this.txtTo.BackColor = System.Drawing.SystemColors.Control;
            this.txtTo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTo.DetectUrls = true;
            this.txtTo.EnableFormatters = false;
            this.txtTo.EnableHistory = false;
            this.txtTo.EnableOptions = false;
            this.txtTo.EnableSearch = false;
            this.txtTo.Location = new System.Drawing.Point(102, 54);
            this.txtTo.Multiline = false;
            this.txtTo.Name = "txtTo";
            this.txtTo.ReadOnly = true;
            this.txtTo.Size = new System.Drawing.Size(440, 22);
            this.txtTo.TabIndex = 24;
            this.txtTo.WordWrap = false;
            // 
            // txtFrom
            // 
            this.txtFrom.BackColor = System.Drawing.SystemColors.Control;
            this.txtFrom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFrom.DetectUrls = true;
            this.txtFrom.EnableFormatters = false;
            this.txtFrom.EnableHistory = false;
            this.txtFrom.EnableOptions = false;
            this.txtFrom.EnableSearch = false;
            this.txtFrom.Location = new System.Drawing.Point(102, 23);
            this.txtFrom.Multiline = false;
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.ReadOnly = true;
            this.txtFrom.Size = new System.Drawing.Size(440, 22);
            this.txtFrom.TabIndex = 24;
            this.txtFrom.WordWrap = false;
            // 
            // linkUrlServer
            // 
            this.linkUrlServer.AutoSize = true;
            this.linkUrlServer.Location = new System.Drawing.Point(543, 57);
            this.linkUrlServer.Name = "linkUrlServer";
            this.linkUrlServer.Size = new System.Drawing.Size(23, 13);
            this.linkUrlServer.TabIndex = 21;
            this.linkUrlServer.TabStop = true;
            this.linkUrlServer.Text = "Ver";
            this.linkUrlServer.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblUrlServer_LinkClicked);
            // 
            // lblUrlServer
            // 
            this.lblUrlServer.AutoSize = true;
            this.lblUrlServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUrlServer.Location = new System.Drawing.Point(6, 55);
            this.lblUrlServer.Name = "lblUrlServer";
            this.lblUrlServer.Size = new System.Drawing.Size(94, 13);
            this.lblUrlServer.TabIndex = 19;
            this.lblUrlServer.Text = "Url do servidor:";
            // 
            // lblUrlTarget
            // 
            this.lblUrlTarget.AutoSize = true;
            this.lblUrlTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUrlTarget.Location = new System.Drawing.Point(6, 26);
            this.lblUrlTarget.Name = "lblUrlTarget";
            this.lblUrlTarget.Size = new System.Drawing.Size(72, 13);
            this.lblUrlTarget.TabIndex = 18;
            this.lblUrlTarget.Text = "Url destino:";
            // 
            // FormStartWiremockService
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
            this.Name = "FormStartWiremockService";
            this.Text = "Form1";
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.tabLogs.ResumeLayout(false);
            this.tabLogText.ResumeLayout(false);
            this.tabLogTable.ResumeLayout(false);
            this.tabLogTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLog)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TabControl tabLogs;
        private System.Windows.Forms.TabPage tabLogText;
        private System.Windows.Forms.TabPage tabLogTable;
        private System.Windows.Forms.LinkLabel linkOpenFolder;
        private System.Windows.Forms.LinkLabel linkUrlTarget;
        private EditorTextBox txtTo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkDisableTableLog;
        private System.Windows.Forms.CheckBox chkAutoScroll;
        private System.Windows.Forms.Button btnClean;
        private EditorTextBox txtFrom;
        private System.Windows.Forms.LinkLabel linkUrlServer;
        private System.Windows.Forms.Label lblUrlServer;
        private System.Windows.Forms.Label lblUrlTarget;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusDiff;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusValue;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCount;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusCount;
        private System.Windows.Forms.DataGridView gridLog;
        private System.Windows.Forms.DataGridViewTextBoxColumn Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeLog;
        private System.Windows.Forms.DataGridViewTextBoxColumn Method;
        private System.Windows.Forms.DataGridViewTextBoxColumn Url;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn RequestTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ResponseTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Raw;
        private EditorTextBox rtxtLog;
        private System.Windows.Forms.CheckBox chkDisableTextLog;
    }
}