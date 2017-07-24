﻿namespace WiremockUI
{
    partial class FormViewFile
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tabResponse = new System.Windows.Forms.TabControl();
            this.tabRaw = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.tabJson = new System.Windows.Forms.TabPage();
            this.lblError = new System.Windows.Forms.Label();
            this.jsonExplorer = new System.Windows.Forms.TreeView();
            this.panel3.SuspendLayout();
            this.tabResponse.SuspendLayout();
            this.tabRaw.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabJson.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel3.Controls.Add(this.btnOpen);
            this.panel3.Controls.Add(this.btnClose);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 227);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(488, 34);
            this.panel3.TabIndex = 3;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(12, 6);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Abrir";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(93, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Fechar";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tabResponse
            // 
            this.tabResponse.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabResponse.Controls.Add(this.tabRaw);
            this.tabResponse.Controls.Add(this.tabJson);
            this.tabResponse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabResponse.Location = new System.Drawing.Point(0, 0);
            this.tabResponse.Name = "tabResponse";
            this.tabResponse.SelectedIndex = 0;
            this.tabResponse.Size = new System.Drawing.Size(488, 227);
            this.tabResponse.TabIndex = 4;
            this.tabResponse.Click += new System.EventHandler(this.tabResponse_Click);
            // 
            // tabRaw
            // 
            this.tabRaw.Controls.Add(this.panel1);
            this.tabRaw.Controls.Add(this.panel2);
            this.tabRaw.Location = new System.Drawing.Point(4, 25);
            this.tabRaw.Name = "tabRaw";
            this.tabRaw.Padding = new System.Windows.Forms.Padding(3);
            this.tabRaw.Size = new System.Drawing.Size(480, 198);
            this.tabRaw.TabIndex = 0;
            this.tabRaw.Text = "Raw";
            this.tabRaw.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(474, 0);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtContent);
            this.panel2.Controls.Add(this.txtPath);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(474, 192);
            this.panel2.TabIndex = 3;
            // 
            // txtContent
            // 
            this.txtContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContent.Location = new System.Drawing.Point(0, 20);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.ReadOnly = true;
            this.txtContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtContent.Size = new System.Drawing.Size(474, 172);
            this.txtContent.TabIndex = 6;
            // 
            // txtPath
            // 
            this.txtPath.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtPath.Location = new System.Drawing.Point(0, 0);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(474, 20);
            this.txtPath.TabIndex = 5;
            // 
            // tabJson
            // 
            this.tabJson.Controls.Add(this.lblError);
            this.tabJson.Controls.Add(this.jsonExplorer);
            this.tabJson.Location = new System.Drawing.Point(4, 25);
            this.tabJson.Name = "tabJson";
            this.tabJson.Padding = new System.Windows.Forms.Padding(3);
            this.tabJson.Size = new System.Drawing.Size(480, 198);
            this.tabJson.TabIndex = 1;
            this.tabJson.Text = "JSON";
            this.tabJson.UseVisualStyleBackColor = true;
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(6, 11);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(325, 13);
            this.lblError.TabIndex = 1;
            this.lblError.Text = "Ocorreu um erro ao tentar montar a visualização do json";
            this.lblError.Visible = false;
            // 
            // jsonExplorer
            // 
            this.jsonExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jsonExplorer.Location = new System.Drawing.Point(3, 3);
            this.jsonExplorer.Name = "jsonExplorer";
            this.jsonExplorer.Size = new System.Drawing.Size(474, 192);
            this.jsonExplorer.TabIndex = 0;
            // 
            // FormViewFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 261);
            this.Controls.Add(this.tabResponse);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormViewFile";
            this.Text = "...";
            this.panel3.ResumeLayout(false);
            this.tabResponse.ResumeLayout(false);
            this.tabRaw.ResumeLayout(false);
            this.tabRaw.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabJson.ResumeLayout(false);
            this.tabJson.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TabControl tabResponse;
        private System.Windows.Forms.TabPage tabRaw;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.TabPage tabJson;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.TreeView jsonExplorer;
    }
}