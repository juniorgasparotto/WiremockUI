using com.github.tomakehurst.wiremock.stubbing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WiremockUI.Data;
using WiremockUI.Languages;

namespace WiremockUI
{
    public partial class FormMaster : Form
    {
        delegate void ActionDelegate();
        private int treeX;
        public TabMaster TabMaster { get; private set;}
        public Dashboard Dashboard { get; private set; }

        public bool InSelectingFile { get => actionSelectionFile != null && pnlSelectFile.Visible; }
        private Action<string> actionSelectionFile;
        private Action<string, string> actionSelectionFileAndContent;
        private Control activeControlLast;
        private IButtonControl accessButtonLast;
        private IButtonControl cancelButtonLast;

        public static FormMaster Current { get; set; }

        public FormMaster()
        {
            InitializeComponent();
            Current = this;

            // language feature
            var settings = SettingsUtils.GetSettings();
            if (settings?.Languages?.Count > 0)
            {
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMaster));
                SettingsUtils.ChangeUILanguage(settings.DefaultLanguage);

                foreach (var lang in settings.Languages)
                {
                    var item = new ToolStripMenuItem()
                    {
                        Text = lang.Value,
                    };
                    
                    if (lang.Key == settings.DefaultLanguage)
                        item.Image = imageList1.Images["check"];

                    item.Click += (o, s) =>
                    {
                        if (Helper.MessageBoxQuestion(Resource.confirmRefreshAll) == DialogResult.Yes)
                        {
                            foreach (ToolStripMenuItem m in mnuLanguages.DropDownItems)
                                m.Image = null;
                            item.Image = imageList1.Images["check"];
                            SettingsUtils.SetLanguage(lang.Key);
                            RefreshAll();
                        }
                    };

                    this.mnuLanguages.DropDownItems.Add(item);
                }
            }
            else
            {
                mnuLanguages.Visible = false;
            }

            // set labels
            UpdateLabels();
        }

        private void UpdateLabels()
        {
            Text = Resource.FormMasterTitle;
            menuFile.Text = Resource.menuFile;
            mnuLanguages.Text = Resource.menuLanguages;
            menuAbout.Text = Resource.menuAbout;
            menuAddServer.Text = Resource.menuAddServer;
            menuClose.Text = Resource.menuClose;
            menuRefresh.Text = Resource.menuRefresh;
            menuServices.Text = Resource.menuServices;
            menuPlayAll.Text = Resource.menuPlayAll;
            menuPlayAndRecordAll.Text = Resource.menuPlayAndRecordAll;
            menuStopAll.Text = Resource.menuStopAll;
            menuTools.Text = Resource.menuTools;
            menuWebRequest.Text = Resource.menuWebRequest;
            menuTextCompare.Text = Resource.menuTextCompare;
            menuTextEditor.Text = Resource.menuTextEditor;
            menuJsonVisualizer.Text = Resource.menuJsonVisualizer;
            menuOpenFilesFolder.Text = Resource.menuOpenFilesFolder;
            lblSelectFileCompare.Text = Resource.lblSelectFileCompare;
            btnCancelFileSelectiong.Text = Resource.btnCancelFileSelectiong;
        }

        private void Master_Load(object sender, EventArgs e)
        {
            this.Dashboard = new Dashboard();
            this.TabMaster = new TabMaster(this);

            // permite renomear os nodes
            treeServices.LabelEdit = true;
            LoadServers();
            this.ActiveControl = this.treeServices;
            this.activeControlLast = this.ActiveControl;
            this.accessButtonLast = AcceptButton;
            this.cancelButtonLast = CancelButton;
        }

        internal void LoadServers()
        {
            var topNode = new TreeNode(Resource.treeviewTopNode);
            ChangeTreeNodeImage(topNode, "services");
            treeServices.Nodes.Add(topNode);

            // context menu
            var menu = new ContextMenuStrip();
            var addMenu = new ToolStripMenuItem();
            var startAllMenu = new ToolStripMenuItem();
            var startAndRecordAllMenu = new ToolStripMenuItem();
            var stopAllMenu = new ToolStripMenuItem();

            menu.ImageList = imageList1;
            menu.Items.AddRange(new ToolStripMenuItem[]
            {
                addMenu,
                startAllMenu,
                startAndRecordAllMenu,
                stopAllMenu
            });

            // add server
            addMenu.Text = Resource.menuAddServer;
            addMenu.ImageKey = "add";
            addMenu.Click += (a, b) =>
            {
                OpenAddOrEditServer();
            };

            // start all
            startAllMenu.Text = Resource.menuPlayAll;
            startAllMenu.ImageKey = "play";
            startAllMenu.Click += (a, b) =>
            {
                PlayAll(Server.PlayType.Play);
            };

            // start and record all
            startAndRecordAllMenu.Text = Resource.menuPlayAndRecordAll;
            startAndRecordAllMenu.ImageKey = "record";
            startAndRecordAllMenu.Click += (a, b) =>
            {
                PlayAll(Server.PlayType.PlayAndRecord);
            };

            // stop all
            stopAllMenu.Text = Resource.menuStopAll;
            stopAllMenu.ImageKey = "stop";
            stopAllMenu.Click += (a, b) =>
            {
                StopAll();
            };

            topNode.ContextMenuStrip = menu;

            var db = new UnitOfWork();
            var servers = db.Servers.GetAll();
            foreach (var p in servers)
                SetServer(p);
            topNode.Expand();
        }

        internal void SelectToCompare(Action<string> action, Action<string, string> actionAndContent)
        {
            Cursor = Cursors.Hand;
            pnlSelectFile.Location = new Point(
                this.ClientSize.Width / 2 - pnlSelectFile.Size.Width / 2,
                this.ClientSize.Height / 2 - pnlSelectFile.Size.Height / 2);
            pnlSelectFile.Anchor = AnchorStyles.None;

            this.actionSelectionFile = action;
            this.actionSelectionFileAndContent = actionAndContent;
            pnlSelectFile.Visible = true;
            this.ActiveControl = btnCancelFileSelectiong;
            this.CancelButton = btnCancelFileSelectiong;
        }

        private void CancelFileSelection()
        {
            Cursor = Cursors.Arrow;
            pnlSelectFile.Visible = false;
            actionSelectionFile = null;
            this.ActiveControl = this.activeControlLast;
            this.CancelButton = this.cancelButtonLast;
        }

        internal TreeNode SetServer(Server server, bool expand = true)
        {
            var topNode = treeServices.Nodes[0];

            TreeNode nodeServer = null;
            foreach (TreeNode n in topNode.Nodes)
            {
                if (((Server)n.Tag).Id == server.Id)
                {
                    nodeServer = n;
                    break;
                }
            }

            if (nodeServer == null)
            {
                nodeServer = new TreeNode();
                nodeServer.Text = server.GetFormattedName();
                nodeServer.Tag = server;
                topNode.Nodes.Add(nodeServer);

                LoadScenarios(nodeServer, server);

                // Create the ContextMenuStrip.
                var menu = new ContextMenuStrip();
                var addScenario = new ToolStripMenuItem();
                var duplicateMenu = new ToolStripMenuItem();
                var startAsProxyMenu = new ToolStripMenuItem();
                var startMenu = new ToolStripMenuItem();
                var startAndRecordMenu = new ToolStripMenuItem();
                var restartMenu = new ToolStripMenuItem();
                var stopMenu = new ToolStripMenuItem();
                var openFolderMenu = new ToolStripMenuItem();
                var openUrlTargetMenu = new ToolStripMenuItem();
                var openUrlServerScenarioMenu = new ToolStripMenuItem();
                var editMenu = new ToolStripMenuItem();
                var removeMenu = new ToolStripMenuItem();

                //Add the menu items to the menu.
                menu.Items.AddRange(new ToolStripMenuItem[]
                {
                    addScenario,
                    startMenu,
                    startAsProxyMenu,
                    startAndRecordMenu,
                    restartMenu,
                    stopMenu,
                    openFolderMenu,
                    openUrlTargetMenu,
                    openUrlServerScenarioMenu,
                    duplicateMenu,
                    editMenu,
                    removeMenu
                });

                menu.ImageList = imageList1;
                menu.Opening += (a, b) =>
                {
                    var hasScenario = server.Scenarios.Any();
                    var hasUrl = !string.IsNullOrWhiteSpace(server.UrlTarget);
                    var hasFolder = Directory.Exists(server.GetFullPath());

                    startMenu.Visible = hasScenario && hasFolder;
                    startAsProxyMenu.Visible = hasScenario && hasUrl;
                    startAndRecordMenu.Visible = hasScenario && hasUrl;
                    stopMenu.Visible = hasScenario;
                    restartMenu.Visible = hasScenario;
                    openFolderMenu.Visible = hasScenario && hasFolder;
                    openUrlTargetMenu.Visible = hasScenario && hasUrl;
                    openUrlServerScenarioMenu.Visible = hasScenario;

                    // Enable control
                    if (hasScenario)
                    {
                        var defaultScenario = server.GetDefaultScenario();
                        var isRunning = Dashboard.IsRunning(defaultScenario);

                        startAsProxyMenu.Enabled = !isRunning;
                        startMenu.Enabled = !isRunning;
                        startAndRecordMenu.Enabled = !isRunning;
                        openUrlServerScenarioMenu.Visible = isRunning;
                        editMenu.Enabled = !isRunning;
                        removeMenu.Enabled = !isRunning;

                        stopMenu.Enabled = isRunning;
                        restartMenu.Enabled = isRunning;
                    }
                    else
                    {
                        editMenu.Enabled = true;
                        removeMenu.Enabled = true;
                    }
                };

                // add cenary
                addScenario.Text = Resource.addServerScenario;
                addScenario.ImageKey = "add";
                addScenario.Click += (a, b) =>
                {
                    OpenAddOrEditScenario(nodeServer);
                };

                // duplicate
                duplicateMenu.Text = Resource.duplicateMappingMenu;
                duplicateMenu.ImageKey = "duplicate";
                duplicateMenu.ShortcutKeys = Keys.Control | Keys.D;
                duplicateMenu.Click += (a, b) =>
                {
                    DuplicateServer(nodeServer);
                };

                // show files
                openFolderMenu.Text = Resource.openServerFolderMenu;
                openFolderMenu.ImageKey = "folder";
                openFolderMenu.Click += (a, b) =>
                {
                    Process.Start(server.GetFullPath());
                };

                // open url target
                openUrlTargetMenu.Text = Resource.openServerUrlTargetMenu;
                openUrlTargetMenu.ImageKey = "services";
                openUrlTargetMenu.Click += (a, b) =>
                {
                    Process.Start(server.UrlTarget);
                };

                // open url Scenario
                openUrlServerScenarioMenu.Text = Resource.openServerUrlScenarioMenu;
                openUrlServerScenarioMenu.ImageKey = "services";
                openUrlServerScenarioMenu.Click += (a, b) =>
                {
                    Process.Start(server.GetServerUrl());
                };

                // edit Scenario
                editMenu.Text = Resource.editServerMenu;
                editMenu.ImageKey = "edit";
                editMenu.ShortcutKeys = Keys.F2;
                editMenu.Click += (a, b) =>
                {
                    OpenAddOrEditServer(server);
                };

                // remove Scenario
                removeMenu.Text = Resource.removeServerMenu;
                removeMenu.ImageKey = "remove";
                removeMenu.ShortcutKeys = Keys.Delete;
                removeMenu.Click += (a, b) =>
                {
                    RemoveServer(nodeServer);
                };

                // play
                startMenu.Text = Resource.startServerMenu;
                startMenu.ImageKey = "play";
                startMenu.Click += (a, b) =>
                {
                    var defaultScenario = server.GetDefaultScenario();
                    StartService(defaultScenario, Server.PlayType.Play);
                };

                // play and record
                startAndRecordMenu.Text = Resource.startServerAndRecordMenu;
                startAndRecordMenu.ImageKey = "record";
                startAndRecordMenu.Click += (a, b) =>
                {
                    var defaultScenario = server.GetDefaultScenario();
                    StartService(defaultScenario, Server.PlayType.PlayAndRecord);
                };

                // play
                startAsProxyMenu.Text = Resource.startServerAsProxyMenu;
                startAsProxyMenu.ImageKey = "play-proxy";
                startAsProxyMenu.Click += (a, b) =>
                {
                    var defaultScenario = server.GetDefaultScenario();
                    StartService(defaultScenario, Server.PlayType.PlayAsProxy);
                };

                // stop
                restartMenu.Text = Resource.restartServerMenu;
                restartMenu.ImageKey = "refresh";
                restartMenu.Click += (a, b) =>
                {
                    var defaultScenario = server.GetDefaultScenario();
                    var wiremockServer = Dashboard.GetWireMockServer(defaultScenario);

                    StopService(defaultScenario);
                    if (wiremockServer != null)
                        StartService(defaultScenario, wiremockServer.PlayType);
                };

                // stop
                stopMenu.Text = Resource.stopServerMenu;
                stopMenu.ImageKey = "stop";
                stopMenu.Click += (a, b) =>
                {
                    var defaultScenario = server.GetDefaultScenario();
                    StopService(defaultScenario);
                };

                nodeServer.ContextMenuStrip = menu;
            }
            else
            {
                nodeServer.Text = server.GetFormattedName();
                nodeServer.Tag = server;
            }

            ChangeTreeNodeImage(nodeServer, "stop");
            if (expand)
                topNode.Expand();
            
            return nodeServer;
        }

        private void DuplicateServer(TreeNode nodeServer)
        {
            try
            {
                int sameNameCount = 0;
                var from = (Server)nodeServer.Tag;
                var newServer = from.Copy();

                var db = new UnitOfWork();
                var allServers = db.Servers.GetAll();
                var serverName = Regex.Replace(newServer.Name, @"\d+$", (resut) =>
                {
                    sameNameCount = int.Parse(resut.Value);
                    return "";
                });

                do
                {
                    newServer.Name = serverName + (++sameNameCount);
                }
                while (allServers.Any(f => f.Name == newServer.Name));

                if (from.Scenarios.Any())
                {
                    foreach (var s in from.Scenarios)
                        newServer.AddScenario(s.Copy());
                }

                var fromDir = new DirectoryInfo(from.GetFullPath());
                fromDir.CopyTo(newServer.GetFullPath(), false);

                treeServices.SelectedNode = SetServer(newServer, false);
                treeServices.SelectedNode.Collapse();

                db.Servers.Insert(newServer);
                db.Save();
            }
            catch (Exception ex)
            {
                Helper.MessageBoxError(ex.Message);
            }
        }

        private void DuplicateScenario(TreeNode nodeScenario)
        {
            try
            {
                int sameNameCount = 0;
                var from = (Data.Scenario)nodeScenario.Tag;
                var server = (Server)nodeScenario.Parent.Tag;
                var to = from.Copy();
                to.IsDefault = false;

                var db = new UnitOfWork();
                var all = server.Scenarios;
                var nameWithoutNumber = Regex.Replace(to.Name, @"\d+$", (resut) =>
                {
                    sameNameCount = int.Parse(resut.Value);
                    return "";
                });

                do
                {
                    to.Name = nameWithoutNumber + (++sameNameCount);
                }
                while (all.Any(f => f.Name == to.Name));

                var fromDir = new DirectoryInfo(server.GetFullPath(from));
                fromDir.CopyTo(server.GetFullPath(to), false);
                server.AddScenario(to);
                db.Servers.Update(server);
                db.Save();

                
                treeServices.SelectedNode = SetScenario(nodeScenario.Parent, to);
            }
            catch (Exception ex)
            {
                Helper.MessageBoxError(ex.Message);
            }
        }

        internal TabControlCustom GetTabControl()
        {
            return tabForms;
        }

        private void LoadScenarios(TreeNode nodeServer, Server server)
        {
            var db = new UnitOfWork();
            foreach (var c in server.Scenarios)
                SetScenario(nodeServer, c);
        }

        private void LoadRequestsAndResponses(TreeNode nodeScenario, Data.Scenario scenario)
        {
            var server = (Server)nodeScenario.Parent.Tag;
            var path = server.GetMappingPath(scenario);
            if (!Directory.Exists(path))
                return;

            string[] filePaths = Directory.GetFiles(path);
            
            foreach(var mapFile in filePaths)
            {
                AddMappingNode(nodeScenario, scenario, mapFile);
            }
        }

        internal TreeNode SetScenario(TreeNode nodeServer, Data.Scenario scenario)
        {
            var server = (Server)nodeServer.Tag;
            TreeNode nodeScenario = null;
            foreach (TreeNode n in nodeServer.Nodes)
            {
                if (((Data.Scenario)n.Tag).Id == scenario.Id)
                {
                    nodeScenario = n;
                    break;
                }
            }

            if (nodeScenario == null)
            {
                nodeScenario = new TreeNode();
                nodeScenario.Text = scenario.GetFormattedName();
                nodeScenario.Tag = scenario;
                nodeServer.Nodes.Add(nodeScenario);

                if (scenario.IsDefault)
                    SetNodeScenarioAsDefault(scenario, nodeScenario);

                LoadRequestsAndResponses(nodeScenario, scenario);

                // Create the ContextMenuStrip.
                var menu = new ContextMenuStrip();
                var addMenu = new ToolStripMenuItem();
                var duplicateMenu = new ToolStripMenuItem();
                var setDefaultMenu = new ToolStripMenuItem();
                var openFolderMenu = new ToolStripMenuItem();
                var editMenu = new ToolStripMenuItem();
                var removeMenu = new ToolStripMenuItem();
                var showUrlMenu = new ToolStripMenuItem();
                var showNameMenu = new ToolStripMenuItem();

                //Add the menu items to the menu.
                menu.Items.AddRange(new ToolStripMenuItem[]
                {
                    addMenu,
                    setDefaultMenu,
                    openFolderMenu,
                    duplicateMenu,
                    editMenu,
                    removeMenu,
                    showUrlMenu,
                    showNameMenu
                });

                menu.ImageList = imageList1;
                menu.Opening += (a, b) =>
                {
                    var isRunning = Dashboard.IsRunning(scenario);
                    //setDefaultMenu.Enabled = !Dashboard.IsAnyRunning();
                    //addMenu.Enabled = !isRunning;
                    editMenu.Enabled = !isRunning;
                    removeMenu.Enabled = !isRunning;

                    if (scenario.ShowURL)
                        showUrlMenu.ImageKey = "check";
                    else
                        showUrlMenu.ImageKey = "";

                    if (scenario.ShowName)
                        showNameMenu.ImageKey = "check";
                    else
                        showNameMenu.ImageKey = "";

                    if (!server.AlreadyRecord(scenario))
                    {
                        openFolderMenu.Visible = false;
                    }
                    else
                    {
                        openFolderMenu.Visible = true;
                    }
                };

                // add menu
                addMenu.Text = Resource.addScenarioMenu;
                addMenu.ImageKey = "add";
                addMenu.Click += (a, b) =>
                {
                    AddNewMap(nodeScenario);
                };

                // duplicate
                duplicateMenu.Text = Resource.duplicateMappingMenu;
                duplicateMenu.ImageKey = "duplicate";
                duplicateMenu.ShortcutKeys = Keys.Control | Keys.D;
                duplicateMenu.Click += (a, b) =>
                {
                    DuplicateScenario(nodeScenario);
                };

                // show files
                setDefaultMenu.Text = Resource.setScenarioDefaultMenu;
                setDefaultMenu.ImageKey = "default";
                setDefaultMenu.Click += (a, b) =>
                {
                    if (Dashboard.IsRunning(server.GetDefaultScenario()))
                    {
                        Helper.MessageBoxExclamation(Resource.scenarioStopMessage);
                        return;
                    }

                    server.SetDefault(scenario);
                    SaveServer(server);
                    SetNodeScenarioAsDefault(scenario, nodeScenario);
                };

                // show files
                openFolderMenu.Text = Resource.openScenarioFolderMenu;
                openFolderMenu.ImageKey = "folder";
                openFolderMenu.Click += (a, b) =>
                {
                    Process.Start(server.GetFullPath(scenario));
                };

                // edit Scenario
                editMenu.Text = Resource.editScenarioMenu;
                editMenu.ImageKey = "edit";
                editMenu.ShortcutKeys = Keys.F2;
                editMenu.Click += (a, b) =>
                {
                    OpenAddOrEditScenario(nodeServer, scenario);
                };

                // remove mock
                removeMenu.Text = Resource.removeScenarioMenu;
                removeMenu.ImageKey = "remove";
                removeMenu.ShortcutKeys = Keys.Delete;
                removeMenu.Click += (a, b) =>
                {
                    RemoveScenario(nodeScenario);
                };

                // mostra ou esconde a URL do Scenario

                showUrlMenu.Text = Resource.showScenarioUrlMenu;
                showUrlMenu.Click += (a, b) =>
                {
                    var db = new UnitOfWork();
                    scenario.ShowURL = !scenario.ShowURL;
                    db.Servers.Update(server);
                    db.Save();

                    var nodesMaps = GetAllMappingNodes(scenario);
                   
                    foreach(var nodeMap in nodesMaps)
                    {
                        var model = (TreeNodeMappingModel)nodeMap.Tag;
                        var text = model.Mapping.GetFormattedName(model.File.GetOnlyFileName(), scenario.ShowURL, scenario.ShowName);
                        nodeMap.Text = text;
                    }
                };

                showNameMenu.Text = Resource.showScenarioNameMenu;
                showNameMenu.Click += (a, b) =>
                {
                    var db = new UnitOfWork();
                    scenario.ShowName = !scenario.ShowName;
                    db.Servers.Update(server);
                    db.Save();

                    var nodesMaps = GetAllMappingNodes(scenario);

                    foreach (var nodeMap in nodesMaps)
                    {
                        var model = (TreeNodeMappingModel)nodeMap.Tag;
                        var text = model.Mapping.GetFormattedName(model.File.GetOnlyFileName(), scenario.ShowURL, scenario.ShowName);
                        nodeMap.Text = text;
                    }
                };

                // Set the ContextMenuStrip property to the ContextMenuStrip.
                nodeScenario.ContextMenuStrip = menu;
            }
            else
            {
                nodeScenario.Text = scenario.GetFormattedName();
                nodeScenario.Tag = scenario;
            }

            ChangeTreeNodeImage(nodeScenario, "scenario");
            nodeServer.Expand();

            return nodeScenario;
        }

        private void OpenAddOrEditServer(Server server = null)
        {
            var frmAdd = new FormServer(this, server);
            frmAdd.StartPosition = FormStartPosition.CenterParent;
            frmAdd.ShowDialog();
        }

        private void OpenAddOrEditScenario(TreeNode nodeServer, Data.Scenario scenario = null)
        {
            var server = (Server)nodeServer.Tag;
            if (scenario == null)
            {
                var frmAdd = new FormScenario(this, nodeServer, server, null);
                frmAdd.StartPosition = FormStartPosition.CenterParent;
                frmAdd.ShowDialog();
            }
            else
            {
                var tabExists = TabMaster.GetTabByInternalTag(scenario.Id);
                if (tabExists == null)
                { 
                    var frmEdit = new FormScenario(this, nodeServer, server, scenario.Id);
                    frmEdit.FormBorderStyle = FormBorderStyle.None;
                    TabMaster.AddTab(frmEdit, scenario.Id, frmEdit.Text);
                }
                else
                {
                    TabMaster.SelectTab(tabExists);
                }
            }
        }

        private TreeNode AddMappingNode(TreeNode nodeScenario, Data.Scenario scenario, string mapFile, int index = -1)
        {
            var server = (Server)nodeScenario.Parent.Tag;
            var treeNodeMapping = GetTreeNodeMapping(server, scenario, mapFile);

            var nodeMapping = new TreeNode(treeNodeMapping.Name);

            // context menu
            var menu = new ContextMenuStrip();
            menu.ImageList = imageList1;
            var renameMenu = new ToolStripMenuItem();
            var deleteMenu = new ToolStripMenuItem();
            var toggleMapStateMenu = new ToolStripMenuItem();
            var duplicateMenu = new ToolStripMenuItem();
            var viewInExplorerMenu = new ToolStripMenuItem();
            var viewInWebRequest = new ToolStripMenuItem();

            menu.Opening += (a, b) =>
            {
                var treeNodeMappingActual = (TreeNodeMappingModel)nodeMapping.Tag;
                if (IsMapEnable(treeNodeMappingActual.File.FullPath))
                {
                    toggleMapStateMenu.ImageKey = "check";
                }
                else
                {
                    toggleMapStateMenu.ImageKey = "";
                }

                //var isRunning = Dashboard.IsRunning(scenario);
                //renameMenu.Enabled = !isRunning;
                //deleteMenu.Enabled = !isRunning;
                //toggleMapStateMenu.Enabled = !isRunning;
                //duplicateMenu.Enabled = !isRunning;
            };

            menu.Items.AddRange(new ToolStripMenuItem[]
            {
                renameMenu,
                duplicateMenu,
                deleteMenu,
                toggleMapStateMenu,
                viewInWebRequest,
                viewInExplorerMenu
            });

            // rename
            renameMenu.Text = Resource.renameMappingMenu;
            renameMenu.ShortcutKeys = Keys.F2;
            renameMenu.ImageKey = "rename";
            renameMenu.Click += (a, b) =>
            {
                treeServices.BeginEdit();
            };

            // delete
            deleteMenu.Text = Resource.deleteMappingMenu;
            deleteMenu.ImageKey = "remove";
            deleteMenu.ShortcutKeys = Keys.Delete;
            deleteMenu.Click += (a, b) =>
            {
                DeleteMap(nodeMapping);
            };

            // disable
            toggleMapStateMenu.Text = Resource.toggleMappingStateMenu;
            toggleMapStateMenu.Click += (a, b) =>
            {
                try
                {
                    var treeNodeMappingActual = (TreeNodeMappingModel)nodeMapping.Tag;
                    if (IsMapEnable(treeNodeMappingActual.File.FullPath))
                        DisableMap(nodeMapping, treeNodeMappingActual);
                    else
                        EnableMap(nodeMapping, treeNodeMappingActual);
                }
                catch (Exception ex)
                {
                    Helper.MessageBoxError(string.Format(Resource.removeMappingErrorMessage, ex.Message));
                }
            };

            // duplicate
            duplicateMenu.Text = Resource.duplicateMappingMenu;
            duplicateMenu.ImageKey = "duplicate";
            duplicateMenu.ShortcutKeys = Keys.Control | Keys.D;
            duplicateMenu.Click += (a, b) =>
            {
                DuplicateMap(nodeMapping);
            };

            // view in explorer
            viewInExplorerMenu.Text = Resource.viewMappingInExplorerMenu;
            viewInExplorerMenu.Click += (a, b) =>
            {
                var model = (TreeNodeMappingModel)nodeMapping.Tag;
                ViewFileInExplorer(model.File.FullPath);
            };

            // view in explorer
            viewInWebRequest.Text = Resource.viewMappingInWebRequestMenu;
            viewInWebRequest.Click += (a, b) =>
            {
                try
                {
                    var model = (TreeNodeMappingModel)nodeMapping.Tag;

                    Dictionary<string, string> headers;
                    string body, method, url;
                    TransformUtils.GetRequestElementsByMap(model.Server.GetServerUrl(), model.File.FullPath, out headers, out body, out method, out url);

                    var frmComposer = new FormWebRequest(method, url, headers, body, null, null);
                    TabMaster.AddTab(frmComposer, model, model.File.GetOnlyFileName());
                }
                catch (Exception ex)
                {
                    Helper.MessageBoxError(ex.Message);
                }
            };

            nodeMapping.ContextMenuStrip = menu;
            nodeMapping.Tag = treeNodeMapping;

            if (index >= 0)
                nodeScenario.Nodes.Insert(index, nodeMapping);
            else
                nodeScenario.Nodes.Add(nodeMapping);

            ChangeTreeNodeImage(nodeMapping, "request");
            SetMappingNodeState(mapFile, nodeMapping);

            if (treeNodeMapping.TreeNodeBody != null)
            {
                var nodeResponse = new TreeNode(treeNodeMapping.TreeNodeBody.Name);
                nodeResponse.ContextMenuStrip = GetMenuTripToNodeResponse(nodeMapping); 
                nodeMapping.Nodes.Add(nodeResponse);
                nodeResponse.Tag = treeNodeMapping.TreeNodeBody;
                ChangeTreeNodeImage(nodeResponse, "response");
            }

            return nodeMapping;
        }

        
        private void DeleteMap(TreeNode nodeMapping)
        {
            if (Helper.MessageBoxQuestion(Resource.removeMappingConfirmMessage) == DialogResult.Yes)
            {
                try
                {
                    var treeNodeMappingActual = (TreeNodeMappingModel)nodeMapping.Tag;
                    if (treeNodeMappingActual.Mapping?.HasBodyFile() == true)
                        File.Delete(treeNodeMappingActual.TreeNodeBody.File.FullPath);
                    File.Delete(treeNodeMappingActual.File.FullPath);
                    nodeMapping.Parent.Nodes.Remove(nodeMapping);
                    DeleteMappingTab(nodeMapping);
                    Dashboard.Refresh(treeNodeMappingActual.Scenario);
                }
                catch (Exception ex)
                {
                    Helper.MessageBoxError(string.Format(Resource.removeMappingErrorMessage, ex.Message));
                }
            }
        }

        private void DuplicateMap(TreeNode nodeMapping)
        {
            var model = (TreeNodeMappingModel)nodeMapping.Tag;
            var content = model.File.GetContent(out _);
            var newMapFile = GetNewFileName(model.File.FullPath);
            var newBodyFile = "";
            if (model.Mapping != null)
            {
                if (model.Mapping.HasBodyFile())
                {
                    newBodyFile = GetNewFileName(model.TreeNodeBody.File.FullPath);
                    content = MappingModel.RenameBodyName(content, newBodyFile);
                    File.WriteAllText(newBodyFile, model.TreeNodeBody.File.GetContent(out _));
                }
            }

            File.WriteAllText(newMapFile, content);
            treeServices.SelectedNode = AddMappingNode(GetNodeScenarioById(model.Scenario.Id), model.Scenario, newMapFile, nodeMapping.Index + 1);
            Dashboard.Refresh(model.Scenario);
        }

        private void AddNewMap(TreeNode nodeScenario)
        {
            var contentMap = "";
            var contentBody = "";
            var templateMapFileName = Resource.templateMapFileName;
            var templateBodyFileName = Resource.templateBodyFileName;
            var mapName = Resource.newMappingFileName;
            var mapBodyName = Resource.newBodyFileName;
            var scenario = (Data.Scenario)nodeScenario.Tag;
            var server = (Server)nodeScenario.Parent.Tag;
            var newMapFileName = GetNewFileName(Path.Combine(server.GetMappingPath(scenario), mapName));
            var newBodyFileName = GetNewFileName(Path.Combine(server.GetBodyFilesPath(scenario), mapBodyName));

            if (File.Exists(templateMapFileName))
            {
                contentMap = File.ReadAllText(templateMapFileName);
                contentMap = MappingModel.RenameBodyName(contentMap, Path.GetFileName(newBodyFileName));
            }

            if (File.Exists(templateBodyFileName))
                contentBody = File.ReadAllText(templateBodyFileName);

            CreateFolderIfNeeded(newMapFileName);
            CreateFolderIfNeeded(newBodyFileName);

            File.WriteAllText(newMapFileName, contentMap);
            File.WriteAllText(newBodyFileName, contentBody);

            var nodeMap = AddMappingNode(nodeScenario, scenario, newMapFileName);
            treeServices.SelectedNode = nodeMap;
            Dashboard.Refresh(scenario);
        }

        private string GetNewFileName(string fileName)
        {
            var fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
            var ext = Path.GetExtension(fileName);
            var directory = Path.GetDirectoryName(fileName);

            var fileCount = 0;
            fileNameWithoutExt = Regex.Replace(fileNameWithoutExt, @"\d+$", (resut) =>
            {
                fileCount = int.Parse(resut.Value);
                return "";
            });

            var newFileName = fileName;
            while (File.Exists(newFileName))
            {
                fileCount++;
                newFileName = Path.Combine(directory, fileNameWithoutExt + fileCount + ext);
            }

            return newFileName;
        }

        public void CreateFolderIfNeeded(string filename)
        {
            string folder = Path.GetDirectoryName(filename);
            if (!string.IsNullOrEmpty(folder) && !Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }

        private ContextMenuStrip GetMenuTripToNodeResponse(TreeNode nodeMapping)
        {
            // context menu
            var menuResponse = new ContextMenuStrip();
            var viewInExplorerResponseMenu = new ToolStripMenuItem();
            menuResponse.ImageList = imageList1;
            menuResponse.Items.AddRange(new ToolStripMenuItem[]
            {
                    viewInExplorerResponseMenu
            });

            // view in explorer
            viewInExplorerResponseMenu.Text = Resource.viewMappingBodyInExplorerResponseMenu;
            viewInExplorerResponseMenu.Click += (a, b) =>
            {
                var treeNodeMappingActual = (TreeNodeMappingModel)nodeMapping.Tag;
                ViewFileInExplorer(treeNodeMappingActual.TreeNodeBody.File.FullPath);
            };
            return menuResponse;
        }

        private static void ViewFileInExplorer(string fileName)
        {
            string args = string.Format("/e, /select, \"{0}\"", fileName);
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "explorer";
            info.Arguments = args;
            Process.Start(info);
        }

        private void SetMappingNodeState(string mapFile, TreeNode nodeMapping)
        {
            if (IsMapEnable(mapFile))
                nodeMapping.NodeFont = new Font(treeServices.Font.FontFamily, treeServices.Font.Size, treeServices.Font.Style);
            else
                nodeMapping.NodeFont = new Font(treeServices.Font.FontFamily, treeServices.Font.Size, FontStyle.Strikeout);
        }

        private void UpdateMappingTab(TreeNode nodeMapping, TreeNode oldNodeBody)
        {
            var tab = TabMaster.GetTabByInternalTag(nodeMapping);
            if (tab != null)
            {
                var model = (TreeNodeMappingModel)nodeMapping.Tag;
                var form = TabMaster.GetForm(tab);
                if (form is IFormFileUpdate formUpdate)
                    formUpdate.Update(model.File.FullPath);
                
                tab.Text = model.File.GetOnlyFileName();
                TabMaster.GetTag(tab).InternalTag = nodeMapping;
            }

            if (nodeMapping.Nodes.Count > 0)
            {
                var nodeBody = nodeMapping.Nodes[0];
                var model = (TreeNodeBodyModel)nodeBody.Tag;
                var tabResponse = TabMaster.GetTabByInternalTag(oldNodeBody);

                if (tabResponse != null)
                {
                    var form = TabMaster.GetForm(tabResponse);
                    if (form is IFormFileUpdate formUpdate)
                        formUpdate.Update(model.File.FullPath);
                    tabResponse.Text = model.File.GetOnlyFileName();
                    TabMaster.GetTag(tabResponse).InternalTag = nodeBody;
                }
            }
        }

        private void DeleteMappingTab(TreeNode nodeMapping)
        {
            var tab = TabMaster.GetTabByInternalTag(nodeMapping);
            if (tab != null)
                TabMaster.CloseTab(tab);

            if (nodeMapping.Nodes.Count > 0)
            {
                var nodeBody = nodeMapping.Nodes[0];
                var tabResponse = TabMaster.GetTabByInternalTag(nodeBody);

                if (tabResponse != null)
                    TabMaster.CloseTab(tabResponse);
            }
        }

        private void UpdateMappingNode(TreeNode nodeMapping, TreeNodeMappingModel newTreeNodeMapping)
        {
            nodeMapping.Text = newTreeNodeMapping.Name;
            nodeMapping.Tag = newTreeNodeMapping;
            nodeMapping.Nodes.Clear();
            ChangeTreeNodeImage(nodeMapping, "request");
            SetMappingNodeState(newTreeNodeMapping.File.FullPath, nodeMapping);

            if (newTreeNodeMapping.TreeNodeBody != null)
            {
                var nodeResponse = new TreeNode(newTreeNodeMapping.TreeNodeBody.Name);
                nodeResponse.ContextMenuStrip = GetMenuTripToNodeResponse(nodeMapping);
                nodeMapping.Nodes.Add(nodeResponse);
                nodeResponse.Tag = newTreeNodeMapping.TreeNodeBody;
                ChangeTreeNodeImage(nodeResponse, "response");
            }
        }

        private TreeNodeMappingModel GetTreeNodeMapping(Server server, Data.Scenario scenario, string mapFile)
        {
            var fileModelMapping = FileModel.Create(mapFile);

            var content = fileModelMapping.GetContent(out var ex);
            var mapName = fileModelMapping.GetOnlyFileName();
            MappingModel mapping = null;
            Exception exMap = null;

            if (content != null)
                mapping = MappingModel.Create(server, scenario, content, out exMap);

            if (mapping != null)
                mapName = mapping.GetFormattedName(mapName, scenario.ShowURL, scenario.ShowName);

            var treeNodeMapping = new TreeNodeMappingModel
            {
                Name = mapName,
                File = fileModelMapping,
                Mapping = mapping,
                Server = server,
                Scenario = scenario
            };

            if (mapping?.HasBodyFile() == true)
            {
                treeNodeMapping.TreeNodeBody = new TreeNodeBodyModel
                {
                    File = FileModel.Create(mapping?.GetBodyFullPath()),
                    TreeNodeMapping = treeNodeMapping
                };

                treeNodeMapping.TreeNodeBody.Name = treeNodeMapping.TreeNodeBody.File.GetOnlyFileName();
            }

            return treeNodeMapping;
        }

        private void StartWatcherFolder(TreeNode nodeScenario, Data.Scenario scenario)
        {
            var watcher = new FileSystemWatcher();
            var server = (Server)nodeScenario.Parent.Tag;

            watcher.Path = server.GetMappingPath(scenario);
            watcher.Filter = "*.*";
            watcher.IncludeSubdirectories = true;

            watcher.Created += (sender, e) =>
            {
                var d = new ActionDelegate(() =>
                {
                    AddMappingNode(nodeScenario, scenario, e.FullPath);
                    nodeScenario.Expand();
                });

                this.Invoke(d);
            };

            watcher.EnableRaisingEvents = true;

            Dashboard.AddWatchers(scenario, watcher);
        }

        private void RemoveServer(TreeNode nodeServer)
        {
            if (Helper.MessageBoxQuestion(Resource.removeServerConfirmMessage) == DialogResult.Yes)
            {
                var server = (Server)nodeServer.Tag;
                var path = server.GetFullPath();

                if (Directory.Exists(path))
                {
                    try
                    {
                        Helper.ClearFolder(path);
                        Directory.Delete(path, true);
                    }
                    catch
                    {
                        Helper.MessageBoxError(Resource.removeServerError);
                        return;
                    }
                }

                nodeServer.Parent.Nodes.Remove(nodeServer);

                var db = new UnitOfWork();
                db.Servers.Delete(server.Id);
                db.Save();
            }
        }


        private void RemoveScenario(TreeNode nodeScenario)
        {
            if (Helper.MessageBoxQuestion(Resource.scenarioConfirmRemoveMessage) == DialogResult.Yes)
            {
                var nodeServer = nodeScenario.Parent;
                var server = (Server)nodeServer.Tag;
                var scenario = (Data.Scenario)nodeScenario.Tag;
                var path = server.GetFullPath(scenario);

                if (Directory.Exists(path))
                {
                    try
                    {
                        Helper.ClearFolder(path);
                        Directory.Delete(path, true);
                    }
                    catch
                    {
                        Helper.MessageBoxError(Resource.scenarioRemoveError);
                        return;
                    }
                }

                nodeScenario.Parent.Nodes.Remove(nodeScenario);
                server.RemoveScenario(scenario.Id);

                if (scenario.IsDefault && nodeServer.Nodes.Count > 0)
                {
                    var nodeScenarioFirst = nodeServer.Nodes[0];
                    var scenarioFirst = (Data.Scenario)nodeScenarioFirst.Tag;
                    server.SetDefault(scenarioFirst);
                    SetNodeScenarioAsDefault(scenarioFirst, nodeScenarioFirst);
                }

                SaveServer(server);
            }
        }

        private void SetNodeScenarioAsDefault(Data.Scenario scenario, TreeNode nodeScenario)
        {
            // change front
            var font = new Font(treeServices.Font.FontFamily, treeServices.Font.Size, FontStyle.Bold);
            nodeScenario.NodeFont = font;

            // FIX: Added to fix the treenode size that is cut when changed to bold
            nodeScenario.Text = nodeScenario.Text;

            foreach (TreeNode n in nodeScenario.Parent.Nodes)
                if (n != nodeScenario)
                    n.NodeFont = new Font(treeServices.Font.FontFamily, treeServices.Font.Size, treeServices.Font.Style);
        }

        private static void SaveServer(Server server)
        {
            var db = new UnitOfWork();
            db.Servers.Update(server);
            db.Save();
        }

        private static void ChangeTreeNodeImage(TreeNode nodeService, string imageKey)
        {
            nodeService.ImageKey = imageKey;
            nodeService.SelectedImageKey = nodeService.ImageKey;
            nodeService.StateImageKey = nodeService.ImageKey;
        }

        private void StartService(Data.Scenario scenario, Server.PlayType playType)
        {
            var nodeScenario = GetNodeScenarioById(scenario.Id);
            var nodeServer = nodeScenario.Parent;
            var server = (Server)nodeServer.Tag;

            var frmStart = new FormStartWiremockService(this, server, scenario, playType);
            
            try
            {
                frmStart.Play();
            }
            catch (Exception ex)
            {
                StopService(scenario);
                Helper.MessageBoxError(string.Format(Resource.startServerError, ex.GetType().Name, ex.Message));
                return;
            }

            var recordText = "";

            if (playType == Server.PlayType.PlayAndRecord)
                recordText = " " + Resource.startServerRecordText;
            else if (playType == Server.PlayType.PlayAsProxy)
                recordText = " " + Resource.startServerAsProxyText;
            else
                recordText = " " + Resource.startServerText;

            TabMaster.AddTab(frmStart, scenario.Id, scenario.Name + recordText)
                .CanClose = () => {
                    if (Helper.MessageBoxQuestion(Resource.stopServerConfirmMessage) == DialogResult.Yes)
                        StopService(scenario);

                    return false;
                };

            var icon = "start";
            if (playType == Server.PlayType.PlayAndRecord)
                icon = "record";
            else if (playType == Server.PlayType.PlayAsProxy)
                icon = "play-proxy";

            ChangeTreeNodeImage(nodeServer, icon);

            if (playType == Server.PlayType.PlayAndRecord)
                StartWatcherFolder(nodeScenario, scenario);
        }

        private void StopService(Data.Scenario scenario)
        {
            var nodeScenario = GetNodeScenarioById(scenario.Id);
            var nodeServer = nodeScenario.Parent;

            Dashboard.Stop(scenario);
            ChangeTreeNodeImage(nodeServer, "stop");

            TabMaster.CloseTab(scenario.Id);
        }

        private IEnumerable<TreeNode> GetAllServersNodes()
        {
            foreach (TreeNode nodeServer in treeServices.Nodes[0].Nodes)
                yield return nodeServer;
        }

        private IEnumerable<TreeNode> GetAllMappingNodes(Data.Scenario scenario)
        {
            foreach (TreeNode nodeServer in GetAllServersNodes())
            {
                foreach (TreeNode nodeScenario in nodeServer.Nodes)
                    if (nodeScenario.Tag is Data.Scenario nScenario && nScenario == scenario)
                        foreach (TreeNode nodeMap in nodeScenario.Nodes)
                            yield return nodeMap;
            }
        }

        private TreeNode GetNodeScenarioById(Guid id)
        {
            foreach (TreeNode n in GetAllServersNodes())
            {
                foreach (TreeNode m in n.Nodes)
                    if (((Data.Scenario)m.Tag).Id == id)
                        return m;
            }

            return null;
        }

        private void StopAll()
        {
            foreach(TreeNode n in GetAllServersNodes())
            {
                var server = (Server)n.Tag;
                if (server.Scenarios.Any())
                    StopService(server.GetDefaultScenario());
            }

            foreach (var w in Dashboard.Watchers)
                w.Value.EnableRaisingEvents = false;

            Dashboard.Watchers.Clear();
        }

        private void PlayAll(Server.PlayType playType)
        {
            foreach (TreeNode n in GetAllServersNodes())
            {
                var server = (Server)n.Tag;
                if (server.Scenarios.Any())
                {
                    var scenario = server.GetDefaultScenario();
                    if (Dashboard.IsRunning(scenario))
                        continue;

                    // inicia gravando caso ainda não tenha sido feito
                    if (!server.AlreadyRecord(scenario))
                        playType = Server.PlayType.PlayAsProxy;

                    StartService(scenario, playType);
                }
                else
                {
                    Helper.MessageBoxError(string.Format(Resource.serverWithoutScenarioError, server.Name));
                }
            }
        }

        private void RefreshAll()
        {
            UpdateLabels();
            StopAll();
            treeServices.Nodes.Clear();
            tabForms.TabPages.Clear();
            LoadServers();
        }

        private void menuAddServer_Click(object sender, EventArgs e)
        {
            OpenAddOrEditServer();
        }

        private void treeServices_DoubleClick(object sender, EventArgs e)
        {
            var selected = ((TreeView)sender).SelectedNode;
            
            if (selected == null)
                return;

            if (selected.Tag != null && selected.Tag is Server)
            {
                OpenAddOrEditServer((Server)selected.Tag);
            }
            else if (selected.Tag != null && selected.Tag is Data.Scenario)
            {
                OpenAddOrEditScenario(selected.Parent, (Data.Scenario)selected.Tag);
            }
            else if (selected.Tag != null && selected.Tag is TreeNodeMappingModel treeNodeMapping)
            {
                if (InSelectingFile)
                {
                    try
                    {
                        Dictionary<string, string> headers;
                        string body, method, url;
                        TransformUtils.GetRequestElementsByMap(treeNodeMapping.Server.GetServerUrl(), treeNodeMapping.File.FullPath, out headers, out body, out method, out url);

                        var strBuilder = new StringBuilder();
                        strBuilder.Append($"{method} {url}");
                        if (headers.Count > 0)
                        {
                            strBuilder.AppendLine();
                            strBuilder.AppendLine();
                            strBuilder.Append(HttpUtils.GetHeadersAsString(headers));
                        }

                        if (body?.Length > 0)
                        {
                            strBuilder.AppendLine();
                            strBuilder.AppendLine();
                            strBuilder.Append(body);
                        }

                        actionSelectionFileAndContent("", strBuilder.ToString());
                    }
                    catch(Exception ex)
                    {
                        Helper.MessageBoxError(string.Format(Resource.selectTextErrorMessage, ex.Message));
                    }
                    finally
                    {
                        CancelFileSelection();
                    }

                    return;
                }

                var currentTab = TabMaster.GetTabByInternalTag(selected);
                if (currentTab != null)
                {
                    tabForms.SelectedTab = currentTab;
                    return;
                }

                var frmStart = new FormJsonFile(this, null, treeNodeMapping.File.FullPath);
                frmStart.OnSave = () =>
                {
                    TreeNodeMappingModel findMap = null;
                    var nodes = GetAllMappingNodes(treeNodeMapping.Scenario);
                    foreach(var n in nodes)
                    {
                        if (n.Tag is TreeNodeMappingModel model)
                        {
                            if (model.File.FullPath == frmStart.FilePath)
                            {
                                findMap = model;
                                break;
                            }
                        }
                    }

                    if (findMap != null)
                    {
                        var newTreeNodeMapping = GetTreeNodeMapping(findMap.Server, findMap.Scenario, findMap.File.FullPath);
                        UpdateMappingNode(selected, newTreeNodeMapping);
                        Dashboard.Refresh(findMap.Scenario);
                    }
                };

                TabMaster.AddTab(frmStart, selected, treeNodeMapping.File.GetOnlyFileName());
            }
            else if (selected.Tag != null && selected.Tag is TreeNodeBodyModel treeNodeBody)
            {
                if (InSelectingFile)
                {
                    try
                    {
                        actionSelectionFile(treeNodeBody.File.FullPath);
                    }
                    catch (Exception ex)
                    {
                        Helper.MessageBoxError(string.Format(Resource.selectTextErrorMessage, ex.Message));
                    }
                    finally
                    {
                        CancelFileSelection();
                    }

                    return;
                }

                var currentTab = TabMaster.GetTabByInternalTag(selected);
                if (currentTab != null)
                {
                    tabForms.SelectedTab = currentTab;
                    return;
                }

                if (treeNodeBody.TreeNodeMapping.Mapping.Response.IsJson())
                {
                    var frmStart = new FormJsonFile(this, null, treeNodeBody.File.FullPath);
                    frmStart.ExpandAll = false;
                    TabMaster.AddTab(frmStart, selected, treeNodeBody.File.GetOnlyFileName());
                }
                else if (treeNodeBody.TreeNodeMapping.Mapping.Response.IsImage())
                {
                    var frmStart = new FormImageFile(this, null, treeNodeBody.File.FullPath);
                    TabMaster.AddTab(frmStart, selected, treeNodeBody.File.GetOnlyFileName());
                }
                else if (treeNodeBody.TreeNodeMapping.Mapping.Response.IsXml())
                {
                    var frmStart = new FormXmlFile(this, null, treeNodeBody.File.FullPath);
                    TabMaster.AddTab(frmStart, selected, treeNodeBody.File.GetOnlyFileName());
                }
                else
                {
                    var frmStart = new FormTextFile(this, null, treeNodeBody.File.FullPath);
                    TabMaster.AddTab(frmStart, selected, treeNodeBody.File.GetOnlyFileName());
                }
            }
        }

        private void menuClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormMaster_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopAll();
        }

        private void menuStopAll_Click(object sender, EventArgs e)
        {
            StopAll();
        }

        private void menuPlayAll_Click(object sender, EventArgs e)
        {
            PlayAll(Server.PlayType.Play);
        }

        private void menuPlayAndRecordAll_Click(object sender, EventArgs e)
        {
            PlayAll(Server.PlayType.PlayAndRecord);
        }

        private void menuRefresh_Click(object sender, EventArgs e)
        {
            if (Helper.MessageBoxQuestion(Resource.confirmRefreshAll) == DialogResult.Yes)
                RefreshAll();
        }

        private void treeServices_MouseMove(object sender, MouseEventArgs e)
        {
            treeX = e.X;
        }

        private void treeServices_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (treeX > e.Node.Bounds.Left) e.Cancel = true;
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            new FormAbout().ShowDialog();
        }

        private bool IsMapEnable(string fileNameWithExtension)
        {
            var ext = Path.GetExtension(fileNameWithExtension);
            return !string.IsNullOrEmpty(ext);
        }

        private void DisableMap(TreeNode nodeMap, TreeNodeMappingModel model)
        {
            if (IsMapEnable(model.File.FullPath))
            {
                var filaName = Path.GetFileName(model.File.FullPath).Split('.').First();
                RenameMap(filaName, "", nodeMap, model, false);
                Dashboard.Refresh(model.Scenario);
            }
        }

        private void EnableMap(TreeNode nodeMap, TreeNodeMappingModel model)
        {
            if (!IsMapEnable(model.File.FullPath))
            {
                var filaName = Path.GetFileName(model.File.FullPath).Split('.').First();
                RenameMap(filaName, ".json", nodeMap, model, false);
                Dashboard.Refresh(model.Scenario);
            }
        }

        private void RenameMap(string newName, string extension, TreeNode nodeMap, TreeNodeMappingModel model, bool renameResponse = true)
        {
            string newNameMap = null;
            string newNameBody = null;
            
            newNameMap = GetNewName(newName, model.File, extension);

            if (File.Exists(newNameMap))
            {
                Helper.MessageBoxError(Resource.mappingFileAlreadyExistsMessage);
                return;
            }

            if (renameResponse && model.Mapping != null && model.Mapping.HasBodyFile())
            {
                newNameBody = GetNewName(newName, model.TreeNodeBody.File, null);

                if (File.Exists(newNameBody))
                {
                    Helper.MessageBoxError(Resource.mappingBodyFileAlreadExistsMessage);
                    return;
                }
            }

            if (newNameBody != null)
            {
                File.Move(model.TreeNodeBody.File.FullPath, newNameBody);
                var newContent = MappingModel.RenameBodyName(model.File.GetContent(out _), newNameBody);
                File.WriteAllText(model.File.FullPath, newContent);
                File.Move(model.File.FullPath, newNameMap);
            }
            else
            {
                File.Move(model.File.FullPath, newNameMap);
            }

            var oldNodeBody = nodeMap.Nodes.Count > 0 ? nodeMap.Nodes[0] : null;
            var newModel = GetTreeNodeMapping(model.Server, model.Scenario, newNameMap);
            UpdateMappingNode(nodeMap, newModel);
            UpdateMappingTab(nodeMap, oldNodeBody);
            Dashboard.Refresh(model.Scenario);
        }

        private string GetNewName(string newName, FileModel file, string extension)
        {
            var ext = extension ?? Path.GetExtension(file.FullPath);
            var directory = Path.GetDirectoryName(file.FullPath);
            return Path.Combine(directory, Path.GetFileNameWithoutExtension(newName) + ext);
        }


        private void treeServices_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node.Tag is TreeNodeMappingModel map)
            {
                e.Node.Text = map.File.GetOnlyFileName();
            }
            else
            {
                e.CancelEdit = true;
            }
        }

        private void treeServices_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            var selected = e.Node;
            if (selected.Tag is TreeNodeMappingModel model)
            {
                if (e.Label != null && e.Label.Length > 0)
                {
                    // cancela a edição, pois o novo nome será renderizado de forma diferente
                    e.CancelEdit = true;

                    try
                    {
                        RenameMap(e.Label, null, selected, model);
                    }
                    catch (Exception ex)
                    {
                        Helper.MessageBoxError(string.Format(Resource.renameFileError, ex.Message));
                    }
                }
                else
                {
                    var mapName = model.File.GetOnlyFileName();
                    if (model.Mapping != null)
                        mapName = model.Mapping.GetFormattedName(mapName, model.Scenario.ShowURL, model.Scenario.ShowName);
                    e.Node.Text = mapName;
                    e.CancelEdit = true;
                }
            }
            else
            {
                e.CancelEdit = true;
            }
        }

        private void treeServices_KeyDown(object sender, KeyEventArgs e)
        {
            if (treeServices.SelectedNode?.Tag is TreeNodeMappingModel model)
            {
                if (e.KeyCode == Keys.Delete)
                {
                    DeleteMap(treeServices.SelectedNode);
                }
                if (e.KeyCode == Keys.F2)
                {
                    treeServices.BeginEdit();
                }
                else if (e.Control && e.KeyCode == Keys.D)
                {
                    DuplicateMap(treeServices.SelectedNode);
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
                // Isso não funciona por que o código que previne o double click 
                // de expandir os nodes está influenciando nessa lógica.
                //else if (e.KeyCode == Keys.Space)
                //{
                //    treeServices.SelectedNode.Toggle();
                //    e.Handled = true;
                //    e.SuppressKeyPress = true;
                //}
            }

            if (treeServices.SelectedNode?.Tag is Server server)
            {
                var isRunning = Dashboard.IsRunning(server.GetDefaultScenario());
                if (e.KeyCode == Keys.Delete && !isRunning)
                {
                    RemoveServer(treeServices.SelectedNode);
                }
                if (e.KeyCode == Keys.F2 && !isRunning)
                {
                    OpenAddOrEditServer(server);
                }
                else if (e.Control && e.KeyCode == Keys.D)
                {
                    DuplicateServer(treeServices.SelectedNode);
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            }

            if (treeServices.SelectedNode?.Tag is Data.Scenario scenario)
            {
                var isRunning = Dashboard.IsRunning(scenario);
                if (e.KeyCode == Keys.Delete && !isRunning)
                {
                    RemoveScenario(treeServices.SelectedNode);
                }
                if (e.KeyCode == Keys.F2 && !isRunning)
                {
                    OpenAddOrEditScenario(treeServices.SelectedNode.Parent, scenario);
                }
                else if (e.Control && e.KeyCode == Keys.D)
                {
                    DuplicateScenario(treeServices.SelectedNode);
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            }
        }

        private void webRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmComposer = new FormWebRequest();
            TabMaster.AddTab(frmComposer, null, GetMenuNameAsTabName(menuWebRequest.Text));
        }

        private void compareTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new FormCompare(this);
            TabMaster.AddTab(frm, null, GetMenuNameAsTabName(menuTextCompare.Text));
        }

        private void btnCancelFileSelectiong_Click(object sender, EventArgs e)
        {
            CancelFileSelection();
        }

        private void textEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new FormTextView(this, null, null);
            TabMaster.AddTab(frm, null, GetMenuNameAsTabName(menuTextEditor.Text));
        }

        private void visualizadorDeJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var title = GetMenuNameAsTabName(menuJsonVisualizer.Text);
            var frm = new FormJsonViewer(this, title, null, true);
            TabMaster.AddTab(frm, null, title);
        }

        private string GetMenuNameAsTabName(string text)
        {
            return text.Replace("&", "");
        }

        private void menuOpenFilesFolder_Click(object sender, EventArgs e)
        {
            ViewFileInExplorer(Helper.GetDbFilePath());
        }
    }
}
