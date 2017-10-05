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
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuOpenFilesFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFindInFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuLanguages = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuClose = new System.Windows.Forms.ToolStripMenuItem();
            this.menuServices = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddServer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuPlayAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPlayAndRecordAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStopAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.menuWebRequest = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTextCompare = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTextEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.menuJsonVisualizer = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pnlSelectFile = new System.Windows.Forms.Panel();
            this.btnCancelFileSelectiong = new System.Windows.Forms.Button();
            this.lblSelectFileCompare = new System.Windows.Forms.Label();
            this.treeServices = new WiremockUI.TreeViewCustom();
            this.tabForms = new WiremockUI.TabControlCustom();
            this.menuOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.pnlSelectFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuOptions
            // 
            this.menuOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuServices,
            this.menuTools,
            this.menuAbout});
            this.menuOptions.Location = new System.Drawing.Point(0, 0);
            this.menuOptions.Name = "menuOptions";
            this.menuOptions.Size = new System.Drawing.Size(675, 24);
            this.menuOptions.TabIndex = 3;
            this.menuOptions.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuRefresh,
            this.toolStripMenuItem1,
            this.menuOpenFilesFolder,
            this.menuFindInFiles,
            this.toolStripMenuItem4,
            this.mnuLanguages,
            this.toolStripMenuItem3,
            this.menuClose});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(61, 20);
            this.menuFile.Text = "&Arquivo";
            // 
            // menuRefresh
            // 
            this.menuRefresh.Image = ((System.Drawing.Image)(resources.GetObject("menuRefresh.Image")));
            this.menuRefresh.Name = "menuRefresh";
            this.menuRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.menuRefresh.Size = new System.Drawing.Size(273, 22);
            this.menuRefresh.Text = "&Atualizar";
            this.menuRefresh.Click += new System.EventHandler(this.menuRefresh_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(270, 6);
            // 
            // menuOpenFilesFolder
            // 
            this.menuOpenFilesFolder.Name = "menuOpenFilesFolder";
            this.menuOpenFilesFolder.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.E)));
            this.menuOpenFilesFolder.Size = new System.Drawing.Size(273, 22);
            this.menuOpenFilesFolder.Text = "Abrir pasta dos arquivos";
            this.menuOpenFilesFolder.Click += new System.EventHandler(this.menuOpenFilesFolder_Click);
            // 
            // menuFindInFiles
            // 
            this.menuFindInFiles.Name = "menuFindInFiles";
            this.menuFindInFiles.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.F)));
            this.menuFindInFiles.Size = new System.Drawing.Size(273, 22);
            this.menuFindInFiles.Text = "Localizar na pasta...";
            this.menuFindInFiles.Click += new System.EventHandler(this.menuFindInFiles_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(270, 6);
            // 
            // mnuLanguages
            // 
            this.mnuLanguages.Name = "mnuLanguages";
            this.mnuLanguages.Size = new System.Drawing.Size(273, 22);
            this.mnuLanguages.Text = "Idiomas";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(270, 6);
            // 
            // menuClose
            // 
            this.menuClose.Image = ((System.Drawing.Image)(resources.GetObject("menuClose.Image")));
            this.menuClose.Name = "menuClose";
            this.menuClose.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Q)));
            this.menuClose.Size = new System.Drawing.Size(273, 22);
            this.menuClose.Text = "&Sair";
            this.menuClose.Click += new System.EventHandler(this.menuClose_Click);
            // 
            // menuServices
            // 
            this.menuServices.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAddServer,
            this.toolStripMenuItem2,
            this.menuPlayAll,
            this.menuPlayAndRecordAll,
            this.menuStopAll});
            this.menuServices.Name = "menuServices";
            this.menuServices.Size = new System.Drawing.Size(62, 20);
            this.menuServices.Text = "&Serviços";
            // 
            // menuAddServer
            // 
            this.menuAddServer.Image = ((System.Drawing.Image)(resources.GetObject("menuAddServer.Image")));
            this.menuAddServer.Name = "menuAddServer";
            this.menuAddServer.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.A)));
            this.menuAddServer.Size = new System.Drawing.Size(262, 22);
            this.menuAddServer.Text = "&Adicionar";
            this.menuAddServer.Click += new System.EventHandler(this.menuAddServer_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(182, 6);
            // 
            // menuPlayAll
            // 
            this.menuPlayAll.Image = ((System.Drawing.Image)(resources.GetObject("menuPlayAll.Image")));
            this.menuPlayAll.Name = "menuPlayAll";
            this.menuPlayAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.menuPlayAll.Size = new System.Drawing.Size(258, 22);
            this.menuPlayAll.Text = "&Iniciar todos";
            this.menuPlayAll.Click += new System.EventHandler(this.menuPlayAll_Click);
            // 
            // menuPlayAndRecordAll
            // 
            this.menuPlayAndRecordAll.Image = ((System.Drawing.Image)(resources.GetObject("menuPlayAndRecordAll.Image")));
            this.menuPlayAndRecordAll.Name = "menuPlayAndRecordAll";
            this.menuPlayAndRecordAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.W)));
            this.menuPlayAndRecordAll.Size = new System.Drawing.Size(262, 22);
            this.menuPlayAndRecordAll.Text = "Iniciar e &Gravar todos";
            this.menuPlayAndRecordAll.Click += new System.EventHandler(this.menuPlayAndRecordAll_Click);
            // 
            // menuStopAll
            // 
            this.menuStopAll.Image = ((System.Drawing.Image)(resources.GetObject("menuStopAll.Image")));
            this.menuStopAll.Name = "menuStopAll";
            this.menuStopAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.menuStopAll.Size = new System.Drawing.Size(258, 22);
            this.menuStopAll.Text = "&Parar todos";
            this.menuStopAll.Click += new System.EventHandler(this.menuStopAll_Click);
            // 
            // menuTools
            // 
            this.menuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuWebRequest,
            this.menuTextCompare,
            this.menuTextEditor,
            this.menuJsonVisualizer});
            this.menuTools.Name = "menuTools";
            this.menuTools.Size = new System.Drawing.Size(84, 20);
            this.menuTools.Text = "&Ferramentas";
            // 
            // menuWebRequest
            // 
            this.menuWebRequest.Name = "menuWebRequest";
            this.menuWebRequest.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.W)));
            this.menuWebRequest.Size = new System.Drawing.Size(251, 22);
            this.menuWebRequest.Text = "&Web Request";
            this.menuWebRequest.Click += new System.EventHandler(this.webRequestToolStripMenuItem_Click);
            // 
            // menuTextCompare
            // 
            this.menuTextCompare.Name = "menuTextCompare";
            this.menuTextCompare.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.C)));
            this.menuTextCompare.Size = new System.Drawing.Size(251, 22);
            this.menuTextCompare.Text = "&Comparador de texto";
            this.menuTextCompare.Click += new System.EventHandler(this.compareTextToolStripMenuItem_Click);
            // 
            // menuTextEditor
            // 
            this.menuTextEditor.Name = "menuTextEditor";
            this.menuTextEditor.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.E)));
            this.menuTextEditor.Size = new System.Drawing.Size(251, 22);
            this.menuTextEditor.Text = "&Editor de texto";
            this.menuTextEditor.Click += new System.EventHandler(this.textEditorToolStripMenuItem_Click);
            // 
            // menuJsonVisualizer
            // 
            this.menuJsonVisualizer.Name = "menuJsonVisualizer";
            this.menuJsonVisualizer.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.V)));
            this.menuJsonVisualizer.Size = new System.Drawing.Size(251, 22);
            this.menuJsonVisualizer.Text = "&Visualizador de JSON";
            this.menuJsonVisualizer.Click += new System.EventHandler(this.visualizadorDeJSONToolStripMenuItem_Click);
            // 
            // menuAbout
            // 
            this.menuAbout.Name = "menuAbout";
            this.menuAbout.Size = new System.Drawing.Size(49, 20);
            this.menuAbout.Text = "S&obre";
            this.menuAbout.Click += new System.EventHandler(this.aboutMenuItem_Click);
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
            this.splitContainer.SplitterDistance = 200;
            this.splitContainer.TabIndex = 2;
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
            this.imageList1.Images.SetKeyName(11, "check");
            this.imageList1.Images.SetKeyName(12, "disable");
            this.imageList1.Images.SetKeyName(13, "duplicate");
            this.imageList1.Images.SetKeyName(14, "rename");
            this.imageList1.Images.SetKeyName(15, "play-proxy");
            this.imageList1.Images.SetKeyName(16, "check");
            this.imageList1.Images.SetKeyName(17, "scenario");
            // 
            // pnlSelectFile
            // 
            this.pnlSelectFile.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pnlSelectFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSelectFile.Controls.Add(this.btnCancelFileSelectiong);
            this.pnlSelectFile.Controls.Add(this.lblSelectFileCompare);
            this.pnlSelectFile.Location = new System.Drawing.Point(237, 132);
            this.pnlSelectFile.Name = "pnlSelectFile";
            this.pnlSelectFile.Size = new System.Drawing.Size(200, 123);
            this.pnlSelectFile.TabIndex = 4;
            this.pnlSelectFile.Visible = false;
            // 
            // btnCancelFileSelectiong
            // 
            this.btnCancelFileSelectiong.Location = new System.Drawing.Point(61, 84);
            this.btnCancelFileSelectiong.Name = "btnCancelFileSelectiong";
            this.btnCancelFileSelectiong.Size = new System.Drawing.Size(75, 23);
            this.btnCancelFileSelectiong.TabIndex = 1;
            this.btnCancelFileSelectiong.Text = "Cancelar";
            this.btnCancelFileSelectiong.UseVisualStyleBackColor = true;
            this.btnCancelFileSelectiong.Click += new System.EventHandler(this.btnCancelFileSelectiong_Click);
            // 
            // lblSelectFileCompare
            // 
            this.lblSelectFileCompare.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelectFileCompare.Location = new System.Drawing.Point(29, 0);
            this.lblSelectFileCompare.Name = "lblSelectFileCompare";
            this.lblSelectFileCompare.Size = new System.Drawing.Size(147, 81);
            this.lblSelectFileCompare.TabIndex = 0;
            this.lblSelectFileCompare.Text = "Selecione na árvore ao lado  (<) um arquivo para fazer a comparação. Use dois cli" +
    "ques para selecionar.";
            this.lblSelectFileCompare.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // treeServices
            // 
            this.treeServices.AllowDrop = true;
            this.treeServices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeServices.HideSelection = false;
            this.treeServices.ImageIndex = 0;
            this.treeServices.ImageList = this.imageList1;
            this.treeServices.Location = new System.Drawing.Point(0, 0);
            this.treeServices.Name = "treeServices";
            this.treeServices.SelectedImageIndex = 0;
            this.treeServices.Size = new System.Drawing.Size(200, 364);
            this.treeServices.TabIndex = 1;
            this.treeServices.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeServices_BeforeLabelEdit);
            this.treeServices.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeServices_AfterLabelEdit);
            this.treeServices.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeServices_BeforeCollapse);
            this.treeServices.DoubleClick += new System.EventHandler(this.treeServices_DoubleClick);
            this.treeServices.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeServices_KeyDown);
            this.treeServices.MouseMove += new System.Windows.Forms.MouseEventHandler(this.treeServices_MouseMove);
            // 
            // tabForms
            // 
            this.tabForms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabForms.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabForms.Location = new System.Drawing.Point(0, 0);
            this.tabForms.Name = "tabForms";
            this.tabForms.Padding = new System.Drawing.Point(20, 5);
            this.tabForms.SelectedIndex = 0;
            this.tabForms.Size = new System.Drawing.Size(471, 364);
            this.tabForms.TabIndex = 0;
            // 
            // FormMaster
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(675, 388);
            this.Controls.Add(this.pnlSelectFile);
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
            this.pnlSelectFile.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuOptions;
        private System.Windows.Forms.ToolStripMenuItem menuServices;
        private System.Windows.Forms.ToolStripMenuItem menuAddServer;
        private System.Windows.Forms.SplitContainer splitContainer;
        private TreeViewCustom treeServices;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem menuPlayAndRecordAll;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem menuStopAll;
        private System.Windows.Forms.ToolStripMenuItem menuPlayAll;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem menuClose;
        private System.Windows.Forms.ToolStripMenuItem menuAbout;
        private TabControlCustom tabForms;
        private System.Windows.Forms.ToolStripMenuItem menuTools;
        private System.Windows.Forms.ToolStripMenuItem menuWebRequest;
        private System.Windows.Forms.ToolStripMenuItem menuTextCompare;
        private System.Windows.Forms.Panel pnlSelectFile;
        private System.Windows.Forms.Label lblSelectFileCompare;
        private System.Windows.Forms.Button btnCancelFileSelectiong;
        private System.Windows.Forms.ToolStripMenuItem menuTextEditor;
        private System.Windows.Forms.ToolStripMenuItem menuJsonVisualizer;
        private System.Windows.Forms.ToolStripMenuItem mnuLanguages;
        private System.Windows.Forms.ToolStripMenuItem menuOpenFilesFolder;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuFindInFiles;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
    }
}