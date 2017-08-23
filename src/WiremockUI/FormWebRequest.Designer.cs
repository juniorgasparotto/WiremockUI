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
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnExecute = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbVerb = new System.Windows.Forms.ComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabRequest = new System.Windows.Forms.TabControl();
            this.tabRequestBody = new System.Windows.Forms.TabPage();
            this.tabRequestHeaders = new System.Windows.Forms.TabPage();
            this.tabRequestOptions = new System.Windows.Forms.TabPage();
            this.txtTimeout = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabResponse = new System.Windows.Forms.TabControl();
            this.tabResponseBody = new System.Windows.Forms.TabPage();
            this.tabResponseHeaders = new System.Windows.Forms.TabPage();
            this.txtRequestBody = new WiremockUI.EditorTextbox();
            this.txtRequestHeaders = new WiremockUI.EditorTextbox();
            this.txtResponseBody = new WiremockUI.EditorTextbox();
            this.txtResponseHeaders = new WiremockUI.EditorTextbox();
            this.txtUrl = new WiremockUI.EditorTextbox();
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
            this.tabRequestOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimeout)).BeginInit();
            this.tabResponse.SuspendLayout();
            this.tabResponseBody.SuspendLayout();
            this.tabResponseHeaders.SuspendLayout();
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
            this.splitContainer1.Size = new System.Drawing.Size(617, 221);
            this.splitContainer1.SplitterDistance = 109;
            this.splitContainer1.TabIndex = 1;
            // 
            // tabRequest
            // 
            this.tabRequest.Controls.Add(this.tabRequestBody);
            this.tabRequest.Controls.Add(this.tabRequestHeaders);
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
            // tabRequestOptions
            // 
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
            this.tabResponse.Size = new System.Drawing.Size(617, 108);
            this.tabResponse.TabIndex = 1;
            // 
            // tabResponseBody
            // 
            this.tabResponseBody.Controls.Add(this.txtResponseBody);
            this.tabResponseBody.Location = new System.Drawing.Point(4, 28);
            this.tabResponseBody.Name = "tabResponseBody";
            this.tabResponseBody.Padding = new System.Windows.Forms.Padding(3);
            this.tabResponseBody.Size = new System.Drawing.Size(609, 76);
            this.tabResponseBody.TabIndex = 1;
            this.tabResponseBody.Text = "Body";
            this.tabResponseBody.UseVisualStyleBackColor = true;
            // 
            // tabResponseHeaders
            // 
            this.tabResponseHeaders.Controls.Add(this.txtResponseHeaders);
            this.tabResponseHeaders.Location = new System.Drawing.Point(4, 28);
            this.tabResponseHeaders.Name = "tabResponseHeaders";
            this.tabResponseHeaders.Padding = new System.Windows.Forms.Padding(3);
            this.tabResponseHeaders.Size = new System.Drawing.Size(609, 76);
            this.tabResponseHeaders.TabIndex = 0;
            this.tabResponseHeaders.Text = "Headers";
            this.tabResponseHeaders.UseVisualStyleBackColor = true;
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
            // txtResponseBody
            // 
            this.txtResponseBody.AcceptsTab = true;
            this.txtResponseBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResponseBody.Location = new System.Drawing.Point(3, 3);
            this.txtResponseBody.Multiline = true;
            this.txtResponseBody.Name = "txtResponseBody";
            this.txtResponseBody.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResponseBody.Size = new System.Drawing.Size(603, 70);
            this.txtResponseBody.TabIndex = 2;
            this.txtResponseBody.WordWrap = false;
            // 
            // txtResponseHeaders
            // 
            this.txtResponseHeaders.AcceptsTab = true;
            this.txtResponseHeaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResponseHeaders.Location = new System.Drawing.Point(3, 3);
            this.txtResponseHeaders.Multiline = true;
            this.txtResponseHeaders.Name = "txtResponseHeaders";
            this.txtResponseHeaders.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResponseHeaders.Size = new System.Drawing.Size(603, 70);
            this.txtResponseHeaders.TabIndex = 2;
            this.txtResponseHeaders.WordWrap = false;
            // 
            // txtUrl
            // 
            this.txtUrl.AcceptsTab = true;
            this.txtUrl.Location = new System.Drawing.Point(0, 5);
            this.txtUrl.Multiline = true;
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(423, 22);
            this.txtUrl.TabIndex = 30;
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
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabRequest.ResumeLayout(false);
            this.tabRequestBody.ResumeLayout(false);
            this.tabRequestBody.PerformLayout();
            this.tabRequestHeaders.ResumeLayout(false);
            this.tabRequestHeaders.PerformLayout();
            this.tabRequestOptions.ResumeLayout(false);
            this.tabRequestOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimeout)).EndInit();
            this.tabResponse.ResumeLayout(false);
            this.tabResponseBody.ResumeLayout(false);
            this.tabResponseBody.PerformLayout();
            this.tabResponseHeaders.ResumeLayout(false);
            this.tabResponseHeaders.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabRequest;
        private System.Windows.Forms.TabPage tabRequestHeaders;
        private System.Windows.Forms.TabPage tabRequestBody;
        private System.Windows.Forms.TabControl tabResponse;
        private System.Windows.Forms.TabPage tabResponseHeaders;
        private System.Windows.Forms.TabPage tabResponseBody;
        private EditorTextbox txtRequestHeaders;
        private EditorTextbox txtRequestBody;
        private EditorTextbox txtResponseHeaders;
        private EditorTextbox txtResponseBody;
        private System.Windows.Forms.TabPage tabRequestOptions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown txtTimeout;
        private System.Windows.Forms.Panel panel4;
        private EditorTextbox txtUrl;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cmbVerb;
    }
}