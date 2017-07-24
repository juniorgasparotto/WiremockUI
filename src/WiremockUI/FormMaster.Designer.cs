using System;

namespace WiremockUI
{
    partial class FormMaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMaster));
            this.menuOptions = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuClose = new System.Windows.Forms.ToolStripMenuItem();
            this.menuServices = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddMockService = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuPlayAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPlayAndRecordAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStopAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.treeServices = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabForms = new System.Windows.Forms.TabControl();
            this.menuOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuOptions
            // 
            this.menuOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuServices,
            this.menuAbout});
            this.menuOptions.Location = new System.Drawing.Point(0, 0);
            this.menuOptions.Name = "menuOptions";
            this.menuOptions.Size = new System.Drawing.Size(675, 24);
            this.menuOptions.TabIndex = 0;
            this.menuOptions.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuRefresh,
            this.toolStripMenuItem3,
            this.menuClose});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(61, 20);
            this.menuFile.Text = "Arquivo";
            // 
            // menuRefresh
            // 
            this.menuRefresh.Image = ((System.Drawing.Image)(resources.GetObject("menuRefresh.Image")));
            this.menuRefresh.Name = "menuRefresh";
            this.menuRefresh.Size = new System.Drawing.Size(120, 22);
            this.menuRefresh.Text = "Atualizar";
            this.menuRefresh.Click += new System.EventHandler(this.menuRefresh_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(117, 6);
            // 
            // menuClose
            // 
            this.menuClose.Image = ((System.Drawing.Image)(resources.GetObject("menuClose.Image")));
            this.menuClose.Name = "menuClose";
            this.menuClose.Size = new System.Drawing.Size(120, 22);
            this.menuClose.Text = "Sair";
            this.menuClose.Click += new System.EventHandler(this.menuClose_Click);
            // 
            // menuServices
            // 
            this.menuServices.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAddMockService,
            this.toolStripMenuItem2,
            this.menuPlayAll,
            this.menuPlayAndRecordAll,
            this.menuStopAll});
            this.menuServices.Name = "menuServices";
            this.menuServices.Size = new System.Drawing.Size(62, 20);
            this.menuServices.Text = "Serviços";
            // 
            // menuAddMockService
            // 
            this.menuAddMockService.Image = ((System.Drawing.Image)(resources.GetObject("menuAddMockService.Image")));
            this.menuAddMockService.Name = "menuAddMockService";
            this.menuAddMockService.Size = new System.Drawing.Size(184, 22);
            this.menuAddMockService.Text = "Adicionar";
            this.menuAddMockService.Click += new System.EventHandler(this.menuAddMockService_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(181, 6);
            // 
            // menuPlayAll
            // 
            this.menuPlayAll.Image = ((System.Drawing.Image)(resources.GetObject("menuPlayAll.Image")));
            this.menuPlayAll.Name = "menuPlayAll";
            this.menuPlayAll.Size = new System.Drawing.Size(184, 22);
            this.menuPlayAll.Text = "Iniciar todos";
            this.menuPlayAll.Click += new System.EventHandler(this.menuPlayAll_Click);
            // 
            // menuPlayAndRecordAll
            // 
            this.menuPlayAndRecordAll.Image = ((System.Drawing.Image)(resources.GetObject("menuPlayAndRecordAll.Image")));
            this.menuPlayAndRecordAll.Name = "menuPlayAndRecordAll";
            this.menuPlayAndRecordAll.Size = new System.Drawing.Size(184, 22);
            this.menuPlayAndRecordAll.Text = "Iniciar e gravar todos";
            this.menuPlayAndRecordAll.Click += new System.EventHandler(this.menuPlayAndRecordAll_Click);
            // 
            // menuStopAll
            // 
            this.menuStopAll.Image = ((System.Drawing.Image)(resources.GetObject("menuStopAll.Image")));
            this.menuStopAll.Name = "menuStopAll";
            this.menuStopAll.Size = new System.Drawing.Size(184, 22);
            this.menuStopAll.Text = "Parar todos";
            this.menuStopAll.Click += new System.EventHandler(this.menuStopAll_Click);
            // 
            // menuAbout
            // 
            this.menuAbout.Name = "menuAbout";
            this.menuAbout.Size = new System.Drawing.Size(49, 20);
            this.menuAbout.Text = "Sobre";
            this.menuAbout.Click += new System.EventHandler(this.sobreToolStripMenuItem_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 24);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.treeServices);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tabForms);
            this.splitContainer.Size = new System.Drawing.Size(675, 364);
            this.splitContainer.SplitterDistance = 106;
            this.splitContainer.TabIndex = 2;
            // 
            // treeServices
            // 
            this.treeServices.AllowDrop = true;
            this.treeServices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeServices.ImageIndex = 0;
            this.treeServices.ImageList = this.imageList1;
            this.treeServices.Location = new System.Drawing.Point(0, 0);
            this.treeServices.Name = "treeServices";
            this.treeServices.SelectedImageIndex = 0;
            this.treeServices.Size = new System.Drawing.Size(106, 364);
            this.treeServices.TabIndex = 2;
            this.treeServices.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeServices_BeforeCollapse);
            this.treeServices.DoubleClick += new System.EventHandler(this.treeServices_DoubleClick);
            this.treeServices.MouseMove += new System.Windows.Forms.MouseEventHandler(this.treeServices_MouseMove);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "play");
            this.imageList1.Images.SetKeyName(1, "stop");
            this.imageList1.Images.SetKeyName(2, "services");
            this.imageList1.Images.SetKeyName(3, "folder");
            this.imageList1.Images.SetKeyName(4, "edit");
            this.imageList1.Images.SetKeyName(5, "remove");
            this.imageList1.Images.SetKeyName(6, "record");
            this.imageList1.Images.SetKeyName(7, "add");
            this.imageList1.Images.SetKeyName(8, "request");
            this.imageList1.Images.SetKeyName(9, "response");
            this.imageList1.Images.SetKeyName(10, "default");
            this.imageList1.Images.SetKeyName(11, "mock");
            // 
            // tabForms
            // 
            this.tabForms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabForms.Location = new System.Drawing.Point(0, 0);
            this.tabForms.Name = "tabForms";
            this.tabForms.SelectedIndex = 0;
            this.tabForms.Size = new System.Drawing.Size(565, 364);
            this.tabForms.TabIndex = 0;
            // 
            // FormMaster
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(675, 388);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.menuOptions);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuOptions;
            this.Name = "FormMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wiremock UI";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMaster_FormClosing);
            this.Load += new System.EventHandler(this.Master_Load);
            this.menuOptions.ResumeLayout(false);
            this.menuOptions.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuOptions;
        private System.Windows.Forms.ToolStripMenuItem menuServices;
        private System.Windows.Forms.ToolStripMenuItem menuAddMockService;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TreeView treeServices;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TabControl tabForms;
        private System.Windows.Forms.ToolStripMenuItem menuPlayAndRecordAll;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem menuStopAll;
        private System.Windows.Forms.ToolStripMenuItem menuPlayAll;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem menuClose;
        private System.Windows.Forms.ToolStripMenuItem menuAbout;
    }
}