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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtUrl = new WiremockUI.SimpleEditor();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnExecute = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbVerb = new System.Windows.Forms.ComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabRequest = new System.Windows.Forms.TabControl();
            this.tabRequestHeaders = new System.Windows.Forms.TabPage();
            this.txtRequestHeaders = new WiremockUI.SimpleEditor();
            this.tabRequestBody = new System.Windows.Forms.TabPage();
            this.txtRequestBody = new WiremockUI.AdvancedEditor();
            this.tabRequestHeadersReal = new System.Windows.Forms.TabPage();
            this.txtRequestHeadersFinal = new WiremockUI.SimpleEditor();
            this.tabRequestOptions = new System.Windows.Forms.TabPage();
            this.chkKeepAlive = new System.Windows.Forms.CheckBox();
            this.chkAutoRedirect = new System.Windows.Forms.CheckBox();
            this.chk100Expect = new System.Windows.Forms.CheckBox();
            this.chkAutoContentLength = new System.Windows.Forms.CheckBox();
            this.txtTimeout = new System.Windows.Forms.NumericUpDown();
            this.lblMiliseconds = new System.Windows.Forms.Label();
            this.lblTimeout = new System.Windows.Forms.Label();
            this.tabResponse = new System.Windows.Forms.TabControl();
            this.tabResponseBody = new System.Windows.Forms.TabPage();
            this.txtResponseBody = new WiremockUI.AdvancedEditor();
            this.tabResponseHeaders = new System.Windows.Forms.TabPage();
            this.txtResponseHeaders = new WiremockUI.SimpleEditor();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stsTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsTimeValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsStatusValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabRequest.SuspendLayout();
            this.tabRequestHeaders.SuspendLayout();
            this.tabRequestBody.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(674, 40);
            this.panel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtUrl);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(101, 5);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(486, 35);
            this.panel4.TabIndex = 32;
            this.panel4.Resize += new System.EventHandler(this.panel4_Resize);
            // 
            // txtUrl
            // 
            this.txtUrl.BackColor = System.Drawing.SystemColors.Control;
            this.txtUrl.EnableFormatters = false;
            this.txtUrl.EnableOptions = false;
            this.txtUrl.EnableSearch = false;
            this.txtUrl.Location = new System.Drawing.Point(0, 5);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(423, 22);
            this.txtUrl.TabIndex = 0;
            this.txtUrl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUrl_KeyDown);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnExecute);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(587, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(87, 35);
            this.panel3.TabIndex = 31;
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(8, 1);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(71, 31);
            this.btnExecute.TabIndex = 2;
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
            this.cmbVerb.TabIndex = 1;
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
            this.splitContainer1.Size = new System.Drawing.Size(674, 474);
            this.splitContainer1.SplitterDistance = 208;
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
            this.tabRequest.Size = new System.Drawing.Size(674, 208);
            this.tabRequest.TabIndex = 3;
            // 
            // tabRequestHeaders
            // 
            this.tabRequestHeaders.Controls.Add(this.txtRequestHeaders);
            this.tabRequestHeaders.Location = new System.Drawing.Point(4, 28);
            this.tabRequestHeaders.Name = "tabRequestHeaders";
            this.tabRequestHeaders.Size = new System.Drawing.Size(666, 176);
            this.tabRequestHeaders.TabIndex = 0;
            this.tabRequestHeaders.Text = "Headers";
            this.tabRequestHeaders.UseVisualStyleBackColor = true;
            // 
            // txtRequestHeaders
            // 
            this.txtRequestHeaders.BackColor = System.Drawing.SystemColors.Control;
            this.txtRequestHeaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRequestHeaders.EnableFormatters = false;
            this.txtRequestHeaders.Location = new System.Drawing.Point(0, 0);
            this.txtRequestHeaders.Name = "txtRequestHeaders";
            this.txtRequestHeaders.Size = new System.Drawing.Size(666, 176);
            this.txtRequestHeaders.TabIndex = 4;
            this.txtRequestHeaders.WordWrap = false;
            // 
            // tabRequestBody
            // 
            this.tabRequestBody.Controls.Add(this.txtRequestBody);
            this.tabRequestBody.Location = new System.Drawing.Point(4, 28);
            this.tabRequestBody.Name = "tabRequestBody";
            this.tabRequestBody.Size = new System.Drawing.Size(666, 176);
            this.tabRequestBody.TabIndex = 1;
            this.tabRequestBody.Text = "Body";
            this.tabRequestBody.UseVisualStyleBackColor = true;
            // 
            // txtRequestBody
            // 
            this.txtRequestBody.BackColor = System.Drawing.SystemColors.Control;
            this.txtRequestBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRequestBody.Location = new System.Drawing.Point(0, 0);
            this.txtRequestBody.Name = "txtRequestBody";
            this.txtRequestBody.ShowLanguages = true;
            this.txtRequestBody.Size = new System.Drawing.Size(666, 176);
            this.txtRequestBody.TabIndex = 6;
            // 
            // tabRequestHeadersReal
            // 
            this.tabRequestHeadersReal.Controls.Add(this.txtRequestHeadersFinal);
            this.tabRequestHeadersReal.Location = new System.Drawing.Point(4, 28);
            this.tabRequestHeadersReal.Name = "tabRequestHeadersReal";
            this.tabRequestHeadersReal.Size = new System.Drawing.Size(666, 176);
            this.tabRequestHeadersReal.TabIndex = 3;
            this.tabRequestHeadersReal.Text = "Header (Final)";
            this.tabRequestHeadersReal.UseVisualStyleBackColor = true;
            // 
            // txtRequestHeadersFinal
            // 
            this.txtRequestHeadersFinal.BackColor = System.Drawing.SystemColors.Control;
            this.txtRequestHeadersFinal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRequestHeadersFinal.EnableFormatters = false;
            this.txtRequestHeadersFinal.Location = new System.Drawing.Point(0, 0);
            this.txtRequestHeadersFinal.Name = "txtRequestHeadersFinal";
            this.txtRequestHeadersFinal.Size = new System.Drawing.Size(666, 176);
            this.txtRequestHeadersFinal.TabIndex = 2;
            this.txtRequestHeadersFinal.WordWrap = false;
            // 
            // tabRequestOptions
            // 
            this.tabRequestOptions.Controls.Add(this.chkKeepAlive);
            this.tabRequestOptions.Controls.Add(this.chkAutoRedirect);
            this.tabRequestOptions.Controls.Add(this.chk100Expect);
            this.tabRequestOptions.Controls.Add(this.chkAutoContentLength);
            this.tabRequestOptions.Controls.Add(this.txtTimeout);
            this.tabRequestOptions.Controls.Add(this.lblMiliseconds);
            this.tabRequestOptions.Controls.Add(this.lblTimeout);
            this.tabRequestOptions.Location = new System.Drawing.Point(4, 28);
            this.tabRequestOptions.Name = "tabRequestOptions";
            this.tabRequestOptions.Size = new System.Drawing.Size(666, 176);
            this.tabRequestOptions.TabIndex = 2;
            this.tabRequestOptions.Text = "Opções";
            this.tabRequestOptions.UseVisualStyleBackColor = true;
            // 
            // chkKeepAlive
            // 
            this.chkKeepAlive.AutoSize = true;
            this.chkKeepAlive.Location = new System.Drawing.Point(9, 106);
            this.chkKeepAlive.Name = "chkKeepAlive";
            this.chkKeepAlive.Size = new System.Drawing.Size(77, 17);
            this.chkKeepAlive.TabIndex = 7;
            this.chkKeepAlive.Text = "Keep-Alive";
            this.chkKeepAlive.UseVisualStyleBackColor = true;
            // 
            // chkAutoRedirect
            // 
            this.chkAutoRedirect.AutoSize = true;
            this.chkAutoRedirect.Location = new System.Drawing.Point(9, 83);
            this.chkAutoRedirect.Name = "chkAutoRedirect";
            this.chkAutoRedirect.Size = new System.Drawing.Size(170, 17);
            this.chkAutoRedirect.TabIndex = 6;
            this.chkAutoRedirect.Text = "Redirecionar automáticamente";
            this.chkAutoRedirect.UseVisualStyleBackColor = true;
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
            // lblMiliseconds
            // 
            this.lblMiliseconds.AutoSize = true;
            this.lblMiliseconds.Location = new System.Drawing.Point(130, 8);
            this.lblMiliseconds.Name = "lblMiliseconds";
            this.lblMiliseconds.Size = new System.Drawing.Size(67, 13);
            this.lblMiliseconds.TabIndex = 2;
            this.lblMiliseconds.Text = "milisegundos";
            // 
            // lblTimeout
            // 
            this.lblTimeout.AutoSize = true;
            this.lblTimeout.Location = new System.Drawing.Point(3, 8);
            this.lblTimeout.Name = "lblTimeout";
            this.lblTimeout.Size = new System.Drawing.Size(48, 13);
            this.lblTimeout.TabIndex = 0;
            this.lblTimeout.Text = "Timeout:";
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
            this.tabResponse.Size = new System.Drawing.Size(674, 240);
            this.tabResponse.TabIndex = 6;
            // 
            // tabResponseBody
            // 
            this.tabResponseBody.Controls.Add(this.txtResponseBody);
            this.tabResponseBody.Location = new System.Drawing.Point(4, 28);
            this.tabResponseBody.Name = "tabResponseBody";
            this.tabResponseBody.Size = new System.Drawing.Size(666, 208);
            this.tabResponseBody.TabIndex = 1;
            this.tabResponseBody.Text = "Body";
            this.tabResponseBody.UseVisualStyleBackColor = true;
            // 
            // txtResponseBody
            // 
            this.txtResponseBody.BackColor = System.Drawing.SystemColors.Control;
            this.txtResponseBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResponseBody.Location = new System.Drawing.Point(0, 0);
            this.txtResponseBody.Name = "txtResponseBody";
            this.txtResponseBody.ShowLanguages = true;
            this.txtResponseBody.Size = new System.Drawing.Size(666, 208);
            this.txtResponseBody.TabIndex = 7;
            // 
            // tabResponseHeaders
            // 
            this.tabResponseHeaders.Controls.Add(this.txtResponseHeaders);
            this.tabResponseHeaders.Location = new System.Drawing.Point(4, 28);
            this.tabResponseHeaders.Name = "tabResponseHeaders";
            this.tabResponseHeaders.Size = new System.Drawing.Size(666, 208);
            this.tabResponseHeaders.TabIndex = 0;
            this.tabResponseHeaders.Text = "Headers";
            this.tabResponseHeaders.UseVisualStyleBackColor = true;
            // 
            // txtResponseHeaders
            // 
            this.txtResponseHeaders.BackColor = System.Drawing.SystemColors.Control;
            this.txtResponseHeaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResponseHeaders.EnableFormatters = false;
            this.txtResponseHeaders.Location = new System.Drawing.Point(0, 0);
            this.txtResponseHeaders.Name = "txtResponseHeaders";
            this.txtResponseHeaders.Size = new System.Drawing.Size(666, 208);
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 240);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(674, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
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
            // FormWebRequest
            // 
            this.AcceptButton = this.btnExecute;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 514);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormWebRequest";
            this.Text = "FormComposer";
            this.Load += new System.EventHandler(this.FormWebRequest_Load);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabRequest.ResumeLayout(false);
            this.tabRequestHeaders.ResumeLayout(false);
            this.tabRequestBody.ResumeLayout(false);
            this.tabRequestHeadersReal.ResumeLayout(false);
            this.tabRequestOptions.ResumeLayout(false);
            this.tabRequestOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimeout)).EndInit();
            this.tabResponse.ResumeLayout(false);
            this.tabResponseBody.ResumeLayout(false);
            this.tabResponseHeaders.ResumeLayout(false);
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
        private SimpleEditor txtRequestHeaders;
        private System.Windows.Forms.TabPage tabRequestOptions;
        private System.Windows.Forms.Label lblTimeout;
        private System.Windows.Forms.Label lblMiliseconds;
        private System.Windows.Forms.NumericUpDown txtTimeout;
        private System.Windows.Forms.Panel panel4;
        private SimpleEditor txtUrl;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cmbVerb;
        private System.Windows.Forms.CheckBox chkAutoContentLength;
        private System.Windows.Forms.CheckBox chk100Expect;
        private System.Windows.Forms.TabPage tabRequestHeadersReal;
        private System.Windows.Forms.TabControl tabResponse;
        private System.Windows.Forms.TabPage tabResponseBody;
        private AdvancedEditor txtResponseBody;
        private System.Windows.Forms.TabPage tabResponseHeaders;
        private SimpleEditor txtResponseHeaders;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stsStatus;
        private System.Windows.Forms.ToolStripStatusLabel stsStatusValue;
        private System.Windows.Forms.ToolStripStatusLabel stsTime;
        private System.Windows.Forms.ToolStripStatusLabel stsTimeValue;
        private System.Windows.Forms.CheckBox chkAutoRedirect;
        private System.Windows.Forms.CheckBox chkKeepAlive;
        private AdvancedEditor txtRequestBody;
        private SimpleEditor txtRequestHeadersFinal;
    }
}