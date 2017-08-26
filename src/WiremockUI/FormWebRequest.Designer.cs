namespace WiremockUI
{
    partial class FormWebRequest
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtUrl = new WiremockUI.EditorTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnExecute = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbVerb = new System.Windows.Forms.ComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabRequest = new System.Windows.Forms.TabControl();
            this.tabRequestBody = new System.Windows.Forms.TabPage();
            this.txtRequestBody = new WiremockUI.EditorTextBox();
            this.tabRequestHeaders = new System.Windows.Forms.TabPage();
            this.txtRequestHeaders = new WiremockUI.EditorTextBox();
            this.tabRequestHeadersReal = new System.Windows.Forms.TabPage();
            this.txtResponseHeadersFinal = new WiremockUI.EditorTextBox();
            this.tabRequestOptions = new System.Windows.Forms.TabPage();
            this.chk100Expect = new System.Windows.Forms.CheckBox();
            this.chkAutoContentLength = new System.Windows.Forms.CheckBox();
            this.txtTimeout = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabResponse = new System.Windows.Forms.TabControl();
            this.tabResponseBody = new System.Windows.Forms.TabPage();
            this.txtResponseBody = new WiremockUI.EditorTextBox();
            this.tabResponseHeaders = new System.Windows.Forms.TabPage();
            this.txtResponseHeaders = new WiremockUI.EditorTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stsStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsStatusValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsTimeValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabRequest.SuspendLayout();
            this.tabRequestBody.SuspendLayout();
            this.tabRequestHeaders.SuspendLayout();
            this.tabRequestHeadersReal.SuspendLayout();
            this.tabRequestOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimeout)).BeginInit();
            this.tabResponse.SuspendLayout();
            this.tabResponseBody.SuspendLayout();
            this.tabResponseHeaders.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.panel1.Size = new System.Drawing.Size(617, 40);
            this.panel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtUrl);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(101, 5);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(429, 35);
            this.panel4.TabIndex = 32;
            this.panel4.Resize += new System.EventHandler(this.panel4_Resize);
            // 
            // txtUrl
            // 
            this.txtUrl.AcceptsTab = true;
            this.txtUrl.Location = new System.Drawing.Point(0, 5);
            this.txtUrl.Multiline = true;
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(423, 22);
            this.txtUrl.TabIndex = 30;
            this.txtUrl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUrl_KeyDown);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnExecute);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(530, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(87, 35);
            this.panel3.TabIndex = 31;
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(8, 1);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(71, 31);
            this.btnExecute.TabIndex = 30;
            this.btnExecute.Text = "Executar";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmbVerb);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(101, 35);
            this.panel2.TabIndex = 30;
            // 
            // cmbVerb
            // 
            this.cmbVerb.Items.AddRange(new object[] {
            "GET",
            "POST",
            "HEAD",
            "TRACE",
            "DELETE",
            "SEARCH",
            "CONNECT",
            "PROPFIND",
            "PROPPATCH",
            "PATCH",
            "MKCOL",
            "COPY",
            "MOVE",
            "LOCK",
            "UNLOCK",
            "OPTIONS"});
            this.cmbVerb.Location = new System.Drawing.Point(7, 5);
            this.cmbVerb.Name = "cmbVerb";
            this.cmbVerb.Size = new System.Drawing.Size(91, 21);
            this.cmbVerb.TabIndex = 28;
            this.cmbVerb.Text = "GET";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 40);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabRequest);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabResponse);
            this.splitContainer1.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(617, 221);
            this.splitContainer1.SplitterDistance = 109;
            this.splitContainer1.TabIndex = 1;
            // 
            // tabRequest
            // 
            this.tabRequest.Controls.Add(this.tabRequestHeaders);
            this.tabRequest.Controls.Add(this.tabRequestBody);
            this.tabRequest.Controls.Add(this.tabRequestHeadersReal);
            this.tabRequest.Controls.Add(this.tabRequestOptions);
            this.tabRequest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabRequest.Location = new System.Drawing.Point(0, 0);
            this.tabRequest.Name = "tabRequest";
            this.tabRequest.Padding = new System.Drawing.Point(9, 6);
            this.tabRequest.SelectedIndex = 0;
            this.tabRequest.Size = new System.Drawing.Size(617, 109);
            this.tabRequest.TabIndex = 0;
            // 
            // tabRequestBody
            // 
            this.tabRequestBody.Controls.Add(this.txtRequestBody);
            this.tabRequestBody.Location = new System.Drawing.Point(4, 28);
            this.tabRequestBody.Name = "tabRequestBody";
            this.tabRequestBody.Padding = new System.Windows.Forms.Padding(3);
            this.tabRequestBody.Size = new System.Drawing.Size(609, 77);
            this.tabRequestBody.TabIndex = 1;
            this.tabRequestBody.Text = "Body";
            this.tabRequestBody.UseVisualStyleBackColor = true;
            // 
            // txtRequestBody
            // 
            this.txtRequestBody.AcceptsTab = true;
            this.txtRequestBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRequestBody.Location = new System.Drawing.Point(3, 3);
            this.txtRequestBody.Multiline = true;
            this.txtRequestBody.Name = "txtRequestBody";
            this.txtRequestBody.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRequestBody.Size = new System.Drawing.Size(603, 71);
            this.txtRequestBody.TabIndex = 1;
            this.txtRequestBody.WordWrap = false;
            // 
            // tabRequestHeaders
            // 
            this.tabRequestHeaders.Controls.Add(this.txtRequestHeaders);
            this.tabRequestHeaders.Location = new System.Drawing.Point(4, 28);
            this.tabRequestHeaders.Name = "tabRequestHeaders";
            this.tabRequestHeaders.Padding = new System.Windows.Forms.Padding(3);
            this.tabRequestHeaders.Size = new System.Drawing.Size(609, 77);
            this.tabRequestHeaders.TabIndex = 0;
            this.tabRequestHeaders.Text = "Headers";
            this.tabRequestHeaders.UseVisualStyleBackColor = true;
            // 
            // txtRequestHeaders
            // 
            this.txtRequestHeaders.AcceptsTab = true;
            this.txtRequestHeaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRequestHeaders.Location = new System.Drawing.Point(3, 3);
            this.txtRequestHeaders.Multiline = true;
            this.txtRequestHeaders.Name = "txtRequestHeaders";
            this.txtRequestHeaders.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRequestHeaders.Size = new System.Drawing.Size(603, 71);
            this.txtRequestHeaders.TabIndex = 0;
            this.txtRequestHeaders.WordWrap = false;
            // 
            // tabRequestHeadersReal
            // 
            this.tabRequestHeadersReal.Controls.Add(this.txtResponseHeadersFinal);
            this.tabRequestHeadersReal.Location = new System.Drawing.Point(4, 28);
            this.tabRequestHeadersReal.Name = "tabRequestHeadersReal";
            this.tabRequestHeadersReal.Size = new System.Drawing.Size(609, 77);
            this.tabRequestHeadersReal.TabIndex = 3;
            this.tabRequestHeadersReal.Text = "Header (Final)";
            this.tabRequestHeadersReal.UseVisualStyleBackColor = true;
            // 
            // txtResponseHeadersFinal
            // 
            this.txtResponseHeadersFinal.AcceptsTab = true;
            this.txtResponseHeadersFinal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResponseHeadersFinal.Location = new System.Drawing.Point(0, 0);
            this.txtResponseHeadersFinal.Multiline = true;
            this.txtResponseHeadersFinal.Name = "txtResponseHeadersFinal";
            this.txtResponseHeadersFinal.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResponseHeadersFinal.Size = new System.Drawing.Size(609, 77);
            this.txtResponseHeadersFinal.TabIndex = 1;
            this.txtResponseHeadersFinal.WordWrap = false;
            // 
            // tabRequestOptions
            // 
            this.tabRequestOptions.Controls.Add(this.chk100Expect);
            this.tabRequestOptions.Controls.Add(this.chkAutoContentLength);
            this.tabRequestOptions.Controls.Add(this.txtTimeout);
            this.tabRequestOptions.Controls.Add(this.label2);
            this.tabRequestOptions.Controls.Add(this.label1);
            this.tabRequestOptions.Location = new System.Drawing.Point(4, 28);
            this.tabRequestOptions.Name = "tabRequestOptions";
            this.tabRequestOptions.Size = new System.Drawing.Size(609, 77);
            this.tabRequestOptions.TabIndex = 2;
            this.tabRequestOptions.Text = "Opções";
            this.tabRequestOptions.UseVisualStyleBackColor = true;
            // 
            // chk100Expect
            // 
            this.chk100Expect.AutoSize = true;
            this.chk100Expect.Location = new System.Drawing.Point(9, 60);
            this.chk100Expect.Name = "chk100Expect";
            this.chk100Expect.Size = new System.Drawing.Size(79, 17);
            this.chk100Expect.TabIndex = 5;
            this.chk100Expect.Text = "100-expect";
            this.chk100Expect.UseVisualStyleBackColor = true;
            // 
            // chkAutoContentLength
            // 
            this.chkAutoContentLength.AutoSize = true;
            this.chkAutoContentLength.Checked = true;
            this.chkAutoContentLength.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoContentLength.Location = new System.Drawing.Point(9, 35);
            this.chkAutoContentLength.Name = "chkAutoContentLength";
            this.chkAutoContentLength.Size = new System.Drawing.Size(243, 17);
            this.chkAutoContentLength.TabIndex = 4;
            this.chkAutoContentLength.Text = "Calcular o \"Content-Length\" automáticamente";
            this.chkAutoContentLength.UseVisualStyleBackColor = true;
            // 
            // txtTimeout
            // 
            this.txtTimeout.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.txtTimeout.Location = new System.Drawing.Point(50, 6);
            this.txtTimeout.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.txtTimeout.Name = "txtTimeout";
            this.txtTimeout.Size = new System.Drawing.Size(80, 20);
            this.txtTimeout.TabIndex = 3;
            this.txtTimeout.Value = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(130, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "milisegundos";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Timeout:";
            // 
            // tabResponse
            // 
            this.tabResponse.Controls.Add(this.tabResponseBody);
            this.tabResponse.Controls.Add(this.tabResponseHeaders);
            this.tabResponse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabResponse.Location = new System.Drawing.Point(0, 0);
            this.tabResponse.Name = "tabResponse";
            this.tabResponse.Padding = new System.Drawing.Point(9, 6);
            this.tabResponse.SelectedIndex = 0;
            this.tabResponse.Size = new System.Drawing.Size(617, 86);
            this.tabResponse.TabIndex = 2;
            // 
            // tabResponseBody
            // 
            this.tabResponseBody.Controls.Add(this.txtResponseBody);
            this.tabResponseBody.Location = new System.Drawing.Point(4, 28);
            this.tabResponseBody.Name = "tabResponseBody";
            this.tabResponseBody.Padding = new System.Windows.Forms.Padding(3);
            this.tabResponseBody.Size = new System.Drawing.Size(609, 54);
            this.tabResponseBody.TabIndex = 1;
            this.tabResponseBody.Text = "Body";
            this.tabResponseBody.UseVisualStyleBackColor = true;
            // 
            // txtResponseBody
            // 
            this.txtResponseBody.AcceptsTab = true;
            this.txtResponseBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResponseBody.Location = new System.Drawing.Point(3, 3);
            this.txtResponseBody.Multiline = true;
            this.txtResponseBody.Name = "txtResponseBody";
            this.txtResponseBody.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResponseBody.Size = new System.Drawing.Size(603, 48);
            this.txtResponseBody.TabIndex = 2;
            this.txtResponseBody.WordWrap = false;
            // 
            // tabResponseHeaders
            // 
            this.tabResponseHeaders.Controls.Add(this.txtResponseHeaders);
            this.tabResponseHeaders.Location = new System.Drawing.Point(4, 28);
            this.tabResponseHeaders.Name = "tabResponseHeaders";
            this.tabResponseHeaders.Padding = new System.Windows.Forms.Padding(3);
            this.tabResponseHeaders.Size = new System.Drawing.Size(609, 54);
            this.tabResponseHeaders.TabIndex = 0;
            this.tabResponseHeaders.Text = "Headers";
            this.tabResponseHeaders.UseVisualStyleBackColor = true;
            // 
            // txtResponseHeaders
            // 
            this.txtResponseHeaders.AcceptsTab = true;
            this.txtResponseHeaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResponseHeaders.Location = new System.Drawing.Point(3, 3);
            this.txtResponseHeaders.Multiline = true;
            this.txtResponseHeaders.Name = "txtResponseHeaders";
            this.txtResponseHeaders.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResponseHeaders.Size = new System.Drawing.Size(603, 48);
            this.txtResponseHeaders.TabIndex = 2;
            this.txtResponseHeaders.WordWrap = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stsTime,
            this.stsTimeValue,
            this.stsStatus,
            this.stsStatusValue});
            this.statusStrip1.Location = new System.Drawing.Point(0, 86);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(617, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // stsStatus
            // 
            this.stsStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.stsStatus.Name = "stsStatus";
            this.stsStatus.Size = new System.Drawing.Size(45, 17);
            this.stsStatus.Text = "Status:";
            // 
            // stsStatusValue
            // 
            this.stsStatusValue.Name = "stsStatusValue";
            this.stsStatusValue.Size = new System.Drawing.Size(12, 17);
            this.stsStatusValue.Text = "-";
            // 
            // stsTime
            // 
            this.stsTime.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.stsTime.Name = "stsTime";
            this.stsTime.Size = new System.Drawing.Size(48, 17);
            this.stsTime.Text = "Tempo:";
            // 
            // stsTimeValue
            // 
            this.stsTimeValue.Name = "stsTimeValue";
            this.stsTimeValue.Size = new System.Drawing.Size(12, 17);
            this.stsTimeValue.Text = "-";
            // 
            // FormWebRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 261);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormWebRequest";
            this.Text = "FormComposer";
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabRequest.ResumeLayout(false);
            this.tabRequestBody.ResumeLayout(false);
            this.tabRequestBody.PerformLayout();
            this.tabRequestHeaders.ResumeLayout(false);
            this.tabRequestHeaders.PerformLayout();
            this.tabRequestHeadersReal.ResumeLayout(false);
            this.tabRequestHeadersReal.PerformLayout();
            this.tabRequestOptions.ResumeLayout(false);
            this.tabRequestOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimeout)).EndInit();
            this.tabResponse.ResumeLayout(false);
            this.tabResponseBody.ResumeLayout(false);
            this.tabResponseBody.PerformLayout();
            this.tabResponseHeaders.ResumeLayout(false);
            this.tabResponseHeaders.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabRequest;
        private System.Windows.Forms.TabPage tabRequestHeaders;
        private System.Windows.Forms.TabPage tabRequestBody;
        private EditorTextBox txtRequestHeaders;
        private EditorTextBox txtRequestBody;
        private System.Windows.Forms.TabPage tabRequestOptions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown txtTimeout;
        private System.Windows.Forms.Panel panel4;
        private EditorTextBox txtUrl;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cmbVerb;
        private System.Windows.Forms.CheckBox chkAutoContentLength;
        private System.Windows.Forms.CheckBox chk100Expect;
        private System.Windows.Forms.TabPage tabRequestHeadersReal;
        private EditorTextBox txtResponseHeadersFinal;
        private System.Windows.Forms.TabControl tabResponse;
        private System.Windows.Forms.TabPage tabResponseBody;
        private EditorTextBox txtResponseBody;
        private System.Windows.Forms.TabPage tabResponseHeaders;
        private EditorTextBox txtResponseHeaders;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stsStatus;
        private System.Windows.Forms.ToolStripStatusLabel stsStatusValue;
        private System.Windows.Forms.ToolStripStatusLabel stsTime;
        private System.Windows.Forms.ToolStripStatusLabel stsTimeValue;
    }
}