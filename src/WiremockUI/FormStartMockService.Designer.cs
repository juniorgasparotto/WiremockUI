﻿namespace WiremockUI
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tabLogs = new System.Windows.Forms.TabControl();
            this.tabLogText = new System.Windows.Forms.TabPage();
            this.rtxtLog = new System.Windows.Forms.RichTextBox();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.tabLogTable = new System.Windows.Forms.TabPage();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.gridLog = new System.Windows.Forms.DataGridView();
            this.lblOpenFolder = new System.Windows.Forms.LinkLabel();
            this.lblUrlTarget = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkDisable = new System.Windows.Forms.CheckBox();
            this.chkAutoScroll = new System.Windows.Forms.CheckBox();
            this.btnClean = new System.Windows.Forms.Button();
            this.lblUrlProxy = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSearchValue = new WiremockUI.EditorTextbox();
            this.txtTo = new WiremockUI.EditorTextbox();
            this.txtFrom = new WiremockUI.EditorTextbox();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TypeLog = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Method = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Url = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RequestTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResponseTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Raw = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabLogs.SuspendLayout();
            this.tabLogText.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.tabLogTable.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLog)).BeginInit();
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
            // 
            // tabLogText
            // 
            this.tabLogText.Controls.Add(this.rtxtLog);
            this.tabLogText.Controls.Add(this.pnlSearch);
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
            this.rtxtLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtLog.HideSelection = false;
            this.rtxtLog.Location = new System.Drawing.Point(3, 39);
            this.rtxtLog.Name = "rtxtLog";
            this.rtxtLog.ReadOnly = true;
            this.rtxtLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.rtxtLog.ShowSelectionMargin = true;
            this.rtxtLog.Size = new System.Drawing.Size(570, 78);
            this.rtxtLog.TabIndex = 31;
            this.rtxtLog.Text = "";
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlSearch.Controls.Add(this.btnClose);
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Controls.Add(this.txtSearchValue);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(3, 3);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(570, 36);
            this.pnlSearch.TabIndex = 30;
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
            // tabLogTable
            // 
            this.tabLogTable.Controls.Add(this.statusStrip1);
            this.tabLogTable.Controls.Add(this.gridLog);
            this.tabLogTable.Location = new System.Drawing.Point(4, 29);
            this.tabLogTable.Name = "tabLogTable";
            this.tabLogTable.Padding = new System.Windows.Forms.Padding(3);
            this.tabLogTable.Size = new System.Drawing.Size(576, 120);
            this.tabLogTable.TabIndex = 1;
            this.tabLogTable.Text = "Tabela";
            this.tabLogTable.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelCount,
            this.toolStripStatusCount,
            this.toolStripStatusLabel1,
            this.toolStripStatusValue});
            this.statusStrip1.Location = new System.Drawing.Point(3, 95);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(570, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
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
            this.gridLog.Size = new System.Drawing.Size(570, 114);
            this.gridLog.TabIndex = 0;
            this.gridLog.SelectionChanged += new System.EventHandler(this.gridLog_SelectionChanged);
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
            this.lblOpenFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblOpenFolder_LinkClicked);
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            this.chkDisable.Location = new System.Drawing.Point(319, 83);
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
            // txtSearchValue
            // 
            this.txtSearchValue.AcceptsTab = true;
            this.txtSearchValue.Location = new System.Drawing.Point(9, 8);
            this.txtSearchValue.Multiline = true;
            this.txtSearchValue.Name = "txtSearchValue";
            this.txtSearchValue.Size = new System.Drawing.Size(214, 20);
            this.txtSearchValue.TabIndex = 0;
            this.txtSearchValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchValue_KeyDown);
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
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(122, 17);
            this.toolStripStatusLabel1.Text = "Diferença de tempo:";
            // 
            // toolStripStatusValue
            // 
            this.toolStripStatusValue.Name = "toolStripStatusValue";
            this.toolStripStatusValue.Size = new System.Drawing.Size(317, 17);
            this.toolStripStatusValue.Text = "(Selecione duas celulas de tempo para calcular a diferença)";
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
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.tabLogs.ResumeLayout(false);
            this.tabLogText.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.tabLogTable.ResumeLayout(false);
            this.tabLogTable.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLog)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TabControl tabLogs;
        private System.Windows.Forms.TabPage tabLogText;
        private System.Windows.Forms.RichTextBox rtxtLog;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSearch;
        private EditorTextbox txtSearchValue;
        private System.Windows.Forms.TabPage tabLogTable;
        private System.Windows.Forms.LinkLabel lblOpenFolder;
        private System.Windows.Forms.LinkLabel lblUrlTarget;
        private EditorTextbox txtTo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkDisable;
        private System.Windows.Forms.CheckBox chkAutoScroll;
        private System.Windows.Forms.Button btnClean;
        private EditorTextbox txtFrom;
        private System.Windows.Forms.LinkLabel lblUrlProxy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView gridLog;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusValue;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCount;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeLog;
        private System.Windows.Forms.DataGridViewTextBoxColumn Method;
        private System.Windows.Forms.DataGridViewTextBoxColumn Url;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn RequestTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ResponseTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Raw;
    }
}