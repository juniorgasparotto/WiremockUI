using com.github.tomakehurst.wiremock.stubbing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
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
                        item.Image = ((System.Drawing.Image)(resources.GetObject("check.Image")));

                    item.Click += (o, s) =>
                    {
                        foreach (ToolStripMenuItem m in mnuLanguages.DropDownItems)
                            m.Image = null;
                        item.Image = ((System.Drawing.Image)(resources.GetObject("check.Image")));
                        SettingsUtils.SetLanguage(lang.Key);
                        RefreshAll();
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
            menuAddMockService.Text = Resource.menuAddMockService;
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
            lblSelectFileCompare.Text = Resource.lblSelectFileCompare;
            btnCancelFileSelectiong.Text = Resource.btnCancelFileSelectiong;
        }

        private void Master_Load(object sender, EventArgs e)
        {
            this.Dashboard = new Dashboard();
            this.TabMaster = new TabMaster(this);

            // permite renomear os nodes
            treeServices.LabelEdit = true;
            LoadProxies();
            this.ActiveControl = this.treeServices;
            this.activeControlLast = this.ActiveControl;
            this.accessButtonLast = AcceptButton;
            this.cancelButtonLast = CancelButton;
        }

        internal void LoadProxies()
        {
            var topNode = new TreeNode(Resource.treeviewTopNode);
            ChangeTreeNodeImage(topNode, "services");
            treeServices.Nodes.Add(topNode);

            // context menu
            var menu = new ContextMenuStrip();
            menu.ImageList = imageList1;
            var addMenu = new ToolStripMenuItem();
            var startAllMenu = new ToolStripMenuItem();
            var startAndRecordAllMenu = new ToolStripMenuItem();
            var stopAllMenu = new ToolStripMenuItem();

            menu.Items.AddRange(new ToolStripMenuItem[]
            {
                addMenu,
                startAllMenu,
                startAndRecordAllMenu,
                stopAllMenu
            });

            // add proxy
            addMenu.Text = Resource.menuAddMockService;
            addMenu.ImageKey = "add";
            addMenu.Click += (a, b) =>
            {
                OpenAddOrEditProxy();
            };

            // start all
            startAllMenu.Text = Resource.menuPlayAll;
            startAllMenu.ImageKey = "play";
            startAllMenu.Click += (a, b) =>
            {
                PlayAll(Proxy.PlayType.Play);
            };

            // start and record all
            startAndRecordAllMenu.Text = Resource.menuPlayAndRecordAll;
            startAndRecordAllMenu.ImageKey = "record";
            startAndRecordAllMenu.Click += (a, b) =>
            {
                PlayAll(Proxy.PlayType.PlayAndRecord);
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
            var proxies = db.Proxies.GetAll();
            foreach (var p in proxies)
                SetProxy(p);
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

        internal void SetProxy(Proxy proxy, bool expand = true)
        {
            var topNode = treeServices.Nodes[0];

            TreeNode nodeProxy = null;
            foreach (TreeNode n in topNode.Nodes)
            {
                if (((Proxy)n.Tag).Id == proxy.Id)
                {
                    nodeProxy = n;
                    break;
                }
            }

            if (nodeProxy == null)
            {
                nodeProxy = new TreeNode();
                nodeProxy.Text = proxy.GetFormattedName();
                nodeProxy.Tag = proxy;
                topNode.Nodes.Add(nodeProxy);

                LoadScenarios(nodeProxy, proxy);

                // Create the ContextMenuStrip.
                var menu = new ContextMenuStrip();
                var addScenario = new ToolStripMenuItem();
                var startAsProxyMenu = new ToolStripMenuItem();
                var startMenu = new ToolStripMenuItem();
                var startAndRecordMenu = new ToolStripMenuItem();
                var stopMenu = new ToolStripMenuItem();
                var openFolderMenu = new ToolStripMenuItem();
                var openUrlTargetMenu = new ToolStripMenuItem();
                var openUrlProxyScenarioMenu = new ToolStripMenuItem();
                var editMenu = new ToolStripMenuItem();
                var removeMenu = new ToolStripMenuItem();

                //Add the menu items to the menu.
                menu.Items.AddRange(new ToolStripMenuItem[]
                {
                    addScenario,
                    startMenu,
                    startAsProxyMenu,
                    startAndRecordMenu,
                    stopMenu,
                    openFolderMenu,
                    openUrlTargetMenu,
                    openUrlProxyScenarioMenu,
                    editMenu,
                    removeMenu
                });

                menu.ImageList = imageList1;
                menu.Opening += (a, b) =>
                {
                    var hasScenario = proxy.Scenarios.Any();
                    var hasUrl = !string.IsNullOrWhiteSpace(proxy.UrlTarget);
                    var hasFolder = Directory.Exists(proxy.GetFullPath());

                    startMenu.Visible = hasScenario && hasFolder;
                    startAsProxyMenu.Visible = hasScenario && hasUrl;
                    startAndRecordMenu.Visible = hasScenario && hasUrl;
                    stopMenu.Visible = hasScenario;
                    openFolderMenu.Visible = hasScenario && hasFolder;
                    openUrlTargetMenu.Visible = hasScenario && hasUrl;
                    openUrlProxyScenarioMenu.Visible = hasScenario;

                    // Enable control
                    if (hasScenario)
                    {
                        var defaultScenario = proxy.GetDefaultScenario();
                        var isRunning = Dashboard.IsRunning(defaultScenario);

                        startAsProxyMenu.Enabled = !isRunning;
                        startMenu.Enabled = !isRunning;
                        startAndRecordMenu.Enabled = !isRunning;
                        stopMenu.Enabled = isRunning;
                        openUrlProxyScenarioMenu.Visible = isRunning;
                        editMenu.Enabled = !isRunning;
                        removeMenu.Enabled = !isRunning;
                    }
                    else
                    {
                        editMenu.Enabled = true;
                        removeMenu.Enabled = true;
                    }
                };

                // add cenary
                addScenario.Text = Resource.addProxyScenario;
                addScenario.ImageKey = "add";
                addScenario.Click += (a, b) =>
                {
                    OpenAddOrEditScenario(nodeProxy);
                };

                // show files
                openFolderMenu.Text = Resource.openProxyFolderMenu;
                openFolderMenu.ImageKey = "folder";
                openFolderMenu.Click += (a, b) =>
                {
                    Process.Start(proxy.GetFullPath());
                };

                // open url target
                openUrlTargetMenu.Text = Resource.openProxyUrlTargetMenu;
                openUrlTargetMenu.ImageKey = "services";
                openUrlTargetMenu.Click += (a, b) =>
                {
                    Process.Start(proxy.UrlTarget);
                };

                // open url Scenario
                openUrlProxyScenarioMenu.Text = Resource.openProxyUrlProxyScenarioMenu;
                openUrlProxyScenarioMenu.ImageKey = "services";
                openUrlProxyScenarioMenu.Click += (a, b) =>
                {
                    Process.Start(proxy.GetUrlProxy());
                };

                // edit Scenario
                editMenu.Text = Resource.editProxyMenu;
                editMenu.ImageKey = "edit";
                editMenu.ShortcutKeys = Keys.F2;
                editMenu.Click += (a, b) =>
                {
                    OpenAddOrEditProxy(proxy);
                };

                // remove Scenario
                removeMenu.Text = Resource.removeProxyMenu;
                removeMenu.ImageKey = "remove";
                removeMenu.ShortcutKeys = Keys.Delete;
                removeMenu.Click += (a, b) =>
                {
                    RemoveProxy(nodeProxy);
                };

                // play
                startMenu.Text = Resource.startProxyMenu;
                startMenu.ImageKey = "play";
                startMenu.Click += (a, b) =>
                {
                    var defaultScenario = proxy.GetDefaultScenario();
                    StartService(defaultScenario, Proxy.PlayType.Play);
                };

                // play and record
                startAndRecordMenu.Text = Resource.startProxyAndRecordMenu;
                startAndRecordMenu.ImageKey = "record";
                startAndRecordMenu.Click += (a, b) =>
                {
                    var defaultScenario = proxy.GetDefaultScenario();
                    StartService(defaultScenario, Proxy.PlayType.PlayAndRecord);
                };

                // play
                startAsProxyMenu.Text = Resource.startProxyAsProxyMenu;
                startAsProxyMenu.ImageKey = "play-proxy";
                startAsProxyMenu.Click += (a, b) =>
                {
                    var defaultScenario = proxy.GetDefaultScenario();
                    StartService(defaultScenario, Proxy.PlayType.PlayAsProxy);
                };

                // stop
                stopMenu.Text = Resource.stopProxyMenu;
                stopMenu.ImageKey = "stop";
                stopMenu.Click += (a, b) =>
                {
                    var defaultScenario = proxy.GetDefaultScenario();
                    StopService(defaultScenario);
                };

                nodeProxy.ContextMenuStrip = menu;
            }
            else
            {
                nodeProxy.Text = proxy.GetFormattedName();
                nodeProxy.Tag = proxy;
            }

            ChangeTreeNodeImage(nodeProxy, "stop");
            if (expand)
                topNode.Expand();
        }

        internal TabControlCustom GetTabControl()
        {
            return tabForms;
        }

        private void LoadScenarios(TreeNode nodeProxy, Proxy proxy)
        {
            var db = new UnitOfWork();
            foreach (var c in proxy.Scenarios)
                SetScenario(nodeProxy, c);
        }

        private void LoadRequestsAndResponses(TreeNode nodeScenario, Data.Scenario scenario)
        {
            var proxy = (Proxy)nodeScenario.Parent.Tag;
            var path = proxy.GetMappingPath(scenario);
            if (!Directory.Exists(path))
                return;

            string[] filePaths = Directory.GetFiles(path);
            
            foreach(var mapFile in filePaths)
            {
                AddMappingNode(nodeScenario, scenario, mapFile);
            }
        }

        internal void SetScenario(TreeNode nodeProxy, Data.Scenario scenario)
        {
            var proxy = (Proxy)nodeProxy.Tag;
            TreeNode nodeScenario = null;
            foreach (TreeNode n in nodeProxy.Nodes)
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
                nodeProxy.Nodes.Add(nodeScenario);

                if (scenario.IsDefault)
                    SetNodeScenarioAsDefault(scenario, nodeScenario);

                LoadRequestsAndResponses(nodeScenario, scenario);

                // Create the ContextMenuStrip.
                var menu = new ContextMenuStrip();
                var addMenu = new ToolStripMenuItem();
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
                    addMenu.Enabled = !isRunning;
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

                    if (!proxy.AlreadyRecord(scenario))
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

                // show files
                setDefaultMenu.Text = Resource.setScenarioDefaultMenu;
                setDefaultMenu.ImageKey = "default";
                setDefaultMenu.Click += (a, b) =>
                {
                    if (Dashboard.IsRunning(proxy.GetDefaultScenario()))
                    {
                        Helper.MessageBoxExclamation(Resource.scenarioStopMessage);
                        return;
                    }

                    proxy.SetDefault(scenario);
                    SaveProxy(proxy);
                    SetNodeScenarioAsDefault(scenario, nodeScenario);
                };

                // show files
                openFolderMenu.Text = Resource.openScenarioFolderMenu;
                openFolderMenu.ImageKey = "folder";
                openFolderMenu.Click += (a, b) =>
                {
                    Process.Start(proxy.GetFullPath(scenario));
                };

                // edit Scenario
                editMenu.Text = Resource.editScenarioMenu;
                editMenu.ImageKey = "edit";
                editMenu.ShortcutKeys = Keys.F2;
                editMenu.Click += (a, b) =>
                {
                    OpenAddOrEditScenario(nodeProxy, scenario);
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
                    db.Proxies.Update(proxy);
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
                    db.Proxies.Update(proxy);
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

            ChangeTreeNodeImage(nodeScenario, "mock");
            nodeProxy.Expand();
        }

        private void OpenAddOrEditProxy(Proxy proxy = null)
        {
            var frmAdd = new FormAddProxy(this, proxy);
            frmAdd.StartPosition = FormStartPosition.CenterParent;
            frmAdd.ShowDialog();
        }

        private void OpenAddOrEditScenario(TreeNode nodeProxy, Data.Scenario scenario = null)
        {
            var proxy = (Proxy)nodeProxy.Tag;
            if (scenario == null)
            {
                var frmAdd = new FormAddScenario(this, nodeProxy, proxy, null);
                frmAdd.StartPosition = FormStartPosition.CenterParent;
                frmAdd.ShowDialog();
            }
            else
            {
                var tabExists = TabMaster.GetTabByInternalTag(scenario.Id);
                if (tabExists == null)
                { 
                    var frmEdit = new FormAddScenario(this, nodeProxy, proxy, scenario.Id);
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
            var proxy = (Proxy)nodeScenario.Parent.Tag;
            var treeNodeMapping = GetTreeNodeMapping(proxy, scenario, mapFile);

            var nodeMapping = new TreeNode(treeNodeMapping.Name);

            // context menu
            var menu = new ContextMenuStrip();
            menu.ImageList = imageList1;
            var renameMenu = new ToolStripMenuItem();
            var deleteMenu = new ToolStripMenuItem();
            var toggleMapStateMenu = new ToolStripMenuItem();
            var duplicateMenu = new ToolStripMenuItem();
            var viewInExplorerMenu = new ToolStripMenuItem();

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

                var isRunning = Dashboard.IsRunning(scenario);
                renameMenu.Enabled = !isRunning;
                deleteMenu.Enabled = !isRunning;
                toggleMapStateMenu.Enabled = !isRunning;
                duplicateMenu.Enabled = !isRunning;
            };

            menu.Items.AddRange(new ToolStripMenuItem[]
            {
                renameMenu,
                duplicateMenu,
                toggleMapStateMenu,
                deleteMenu,
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
        }

        private void AddNewMap(TreeNode nodeScenario)
        {
            var contentMap = "";
            var contentBody = "";
            var templateMapFileName = Resource.templateMapFileName;
            var templateBodyFileName = Resource.templateBodyFileName;
            var mockName = Resource.newMappingFileName;
            var mockBodyName = Resource.newBodyFileName;
            var scenario = (Data.Scenario)nodeScenario.Tag;
            var proxy = (Proxy)nodeScenario.Parent.Tag;
            var newMapFileName = GetNewFileName(Path.Combine(proxy.GetMappingPath(scenario), mockName));
            var newBodyFileName = GetNewFileName(Path.Combine(proxy.GetBodyFilesPath(scenario), mockBodyName));

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
        }

        private string GetNewFileName(string fileName)
        {
            var fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
            var ext = Path.GetExtension(fileName);
            var directory = Path.GetDirectoryName(fileName);

            var fileCount = 0;
            var newFileName = fileName;
            while (File.Exists(newFileName))
            {
                fileNameWithoutExt = Regex.Replace(fileNameWithoutExt, @"\d+$", (resut) =>
                {
                    fileCount = int.Parse(resut.Value);
                    return "";
                });

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

        private TreeNodeMappingModel GetTreeNodeMapping(Proxy proxy, Data.Scenario scenario, string mapFile)
        {
            var fileModelMapping = FileModel.Create(mapFile);

            var content = fileModelMapping.GetContent(out var ex);
            var mapName = fileModelMapping.GetOnlyFileName();
            MappingModel mapping = null;
            Exception exMap = null;

            if (content != null)
                mapping = MappingModel.Create(proxy, scenario, content, out exMap);

            if (mapping != null)
                mapName = mapping.GetFormattedName(mapName, scenario.ShowURL, scenario.ShowName);

            var treeNodeMapping = new TreeNodeMappingModel
            {
                Name = mapName,
                File = fileModelMapping,
                Mapping = mapping,
                Proxy = proxy,
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
            var proxy = (Proxy)nodeScenario.Parent.Tag;

            watcher.Path = proxy.GetMappingPath(scenario);
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

        private void RemoveProxy(TreeNode nodeProxy)
        {
            if (Helper.MessageBoxQuestion(Resource.removeProxyConfirmMessage) == DialogResult.Yes)
            {
                var proxy = (Proxy)nodeProxy.Tag;
                var path = proxy.GetFullPath();

                if (Directory.Exists(path))
                {
                    try
                    {
                        Helper.ClearFolder(path);
                        Directory.Delete(path, true);
                    }
                    catch
                    {
                        Helper.MessageBoxError(Resource.removeProxyError);
                        return;
                    }
                }

                nodeProxy.Parent.Nodes.Remove(nodeProxy);

                var db = new UnitOfWork();
                db.Proxies.Delete(proxy.Id);
                db.Save();
            }
        }


        private void RemoveScenario(TreeNode nodeScenario)
        {
            if (Helper.MessageBoxQuestion(Resource.scenarioConfirmRemoveMessage) == DialogResult.Yes)
            {
                var nodeProxy = nodeScenario.Parent;
                var proxy = (Proxy)nodeScenario.Parent.Tag;
                var scenario = (Data.Scenario)nodeScenario.Tag;
                var path = proxy.GetFullPath(scenario);

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
                proxy.RemoveScenario(scenario.Id);

                if (scenario.IsDefault && nodeProxy.Nodes.Count > 0)
                {
                    var nodeScenarioFirst = nodeProxy.Nodes[0];
                    var scenarioFirst = (Data.Scenario)nodeScenarioFirst.Tag;
                    proxy.SetDefault(scenarioFirst);
                    SetNodeScenarioAsDefault(scenarioFirst, nodeScenarioFirst);
                }

                SaveProxy(proxy);
            }
        }

        private void SetNodeScenarioAsDefault(Data.Scenario scenario, TreeNode nodeScenario)
        {
            // change front
            var font = new Font(treeServices.Font.FontFamily, treeServices.Font.Size, FontStyle.Bold);
            nodeScenario.NodeFont = font;

            foreach (TreeNode n in nodeScenario.Parent.Nodes)
                if (n != nodeScenario)
                    n.NodeFont = new Font(treeServices.Font.FontFamily, treeServices.Font.Size, treeServices.Font.Style);
        }

        private static void SaveProxy(Proxy proxy)
        {
            var db = new UnitOfWork();
            db.Proxies.Update(proxy);
            db.Save();
        }

        private static void ChangeTreeNodeImage(TreeNode nodeService, string imageKey)
        {
            nodeService.ImageKey = imageKey;
            nodeService.SelectedImageKey = nodeService.ImageKey;
            nodeService.StateImageKey = nodeService.ImageKey;
        }

        private void StartService(Data.Scenario scenario, Proxy.PlayType playType)
        {
            var nodeScenario = GetNodeScenarioById(scenario.Id);
            var nodeProxy = nodeScenario.Parent;
            var proxy = (Proxy)nodeProxy.Tag;

            var frmStart = new FormStartWiremockService(this, proxy, scenario, playType);
            
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

            if (playType == Proxy.PlayType.PlayAndRecord)
                recordText = " " + Resource.startServerRecordText;
            else if (playType == Proxy.PlayType.PlayAsProxy)
                recordText = " " + Resource.startServerAsProxyText;

            TabMaster.AddTab(frmStart, scenario.Id, scenario.Name + recordText)
                .CanClose = () => {
                    if (Helper.MessageBoxQuestion(Resource.stopServerConfirmMessage) == DialogResult.Yes)
                        StopService(scenario);

                    return false;
                };

            var icon = "start";
            if (playType == Proxy.PlayType.PlayAndRecord)
                icon = "record";
            else if (playType == Proxy.PlayType.PlayAsProxy)
                icon = "play-proxy";

            ChangeTreeNodeImage(nodeProxy, icon);

            if (playType == Proxy.PlayType.PlayAndRecord)
                StartWatcherFolder(nodeScenario, scenario);
        }

        private void StopService(Data.Scenario scenario)
        {
            var nodeScenario = GetNodeScenarioById(scenario.Id);
            var nodeProxy = nodeScenario.Parent;

            Dashboard.Stop(scenario);
            ChangeTreeNodeImage(nodeProxy, "stop");

            TabMaster.CloseTab(scenario.Id);
        }

        private IEnumerable<TreeNode> GetAllProxiesNodes()
        {
            foreach (TreeNode nodeProxy in treeServices.Nodes[0].Nodes)
                yield return nodeProxy;
        }

        private IEnumerable<TreeNode> GetAllMappingNodes(Data.Scenario scenario)
        {
            foreach (TreeNode nodeProxy in GetAllProxiesNodes())
            {
                foreach (TreeNode nodeScenario in nodeProxy.Nodes)
                    if (nodeScenario.Tag is Data.Scenario nScenario && nScenario == scenario)
                        foreach (TreeNode nodeMap in nodeScenario.Nodes)
                            yield return nodeMap;
            }
        }

        private TreeNode GetNodeScenarioById(Guid id)
        {
            foreach (TreeNode n in GetAllProxiesNodes())
            {
                foreach (TreeNode m in n.Nodes)
                    if (((Data.Scenario)m.Tag).Id == id)
                        return m;
            }

            return null;
        }

        private void StopAll()
        {
            foreach(TreeNode n in GetAllProxiesNodes())
            {
                var proxy = (Proxy)n.Tag;
                if (proxy.Scenarios.Any())
                    StopService(proxy.GetDefaultScenario());
            }

            foreach (var w in Dashboard.Watchers)
                w.Value.EnableRaisingEvents = false;

            Dashboard.Watchers.Clear();
        }

        private void PlayAll(Proxy.PlayType playType)
        {
            foreach (TreeNode n in GetAllProxiesNodes())
            {
                var proxy = (Proxy)n.Tag;
                if (proxy.Scenarios.Any())
                {
                    var scenario = proxy.GetDefaultScenario();
                    if (Dashboard.IsRunning(scenario))
                        continue;

                    // inicia gravando caso ainda não tenha sido feito
                    if (!proxy.AlreadyRecord(scenario))
                        playType = Proxy.PlayType.PlayAsProxy;

                    StartService(scenario, playType);
                }
            }
        }

        private void RefreshAll()
        {
            UpdateLabels();
            StopAll();
            treeServices.Nodes.Clear();
            tabForms.TabPages.Clear();
            LoadProxies();
        }

        private void menuAddMockService_Click(object sender, EventArgs e)
        {
            OpenAddOrEditProxy();
        }

        private void treeServices_DoubleClick(object sender, EventArgs e)
        {
            var selected = ((TreeView)sender).SelectedNode;
            
            if (selected == null)
                return;

            if (selected.Tag != null && selected.Tag is Proxy)
            {
                OpenAddOrEditProxy((Proxy)selected.Tag);
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
                        var strBuilder = new StringBuilder();
                        var headers = new Dictionary<string, string>();
                        string body = null;

                        var fileContent = File.ReadAllText(treeNodeMapping.File.FullPath);
                        var stub = StubMapping.buildFrom(fileContent);
                        var method = stub.getRequest().getMethod().ToString();
                        var url = stub.getRequest().getUrl();

                        var iterator = stub.getRequest().getHeaders()?.keySet()?.iterator();
                        if (iterator != null)
                        {
                            while (iterator.hasNext())
                            {
                                var name = (string)iterator.next();
                                var value = (com.github.tomakehurst.wiremock.matching.MultiValuePattern)stub.getRequest().getHeaders().get(name);
                                var compareName = value.getName();
                                headers[name] = value.getExpected();
                            }
                        }

                        var iteratorCookie = stub.getRequest().getCookies()?.keySet()?.iterator();
                        if (iteratorCookie != null && !headers.Any((KeyValuePair<string, string> f) => f.Key.ToLower() == "cookies"))
                        {
                            headers["Cookies"] = stub.getRequest().getCookies().ToString();
                        }

                        var bodyPatterns = stub.getRequest().getBodyPatterns()?.toArray();
                        if (bodyPatterns != null)
                        {
                            foreach (var bodyPattern in bodyPatterns)
                            {
                                if (bodyPattern is com.github.tomakehurst.wiremock.matching.StringValuePattern converted)
                                {
                                    body = converted.getExpected();
                                    break;
                                }
                            }
                        }

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
                    var newTreeNodeMapping = GetTreeNodeMapping(treeNodeMapping.Proxy, treeNodeMapping.Scenario, treeNodeMapping.File.FullPath);
                    UpdateMappingNode(selected, newTreeNodeMapping);
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
            PlayAll(Proxy.PlayType.Play);
        }

        private void menuPlayAndRecordAll_Click(object sender, EventArgs e)
        {
            PlayAll(Proxy.PlayType.PlayAndRecord);
        }

        private void menuRefresh_Click(object sender, EventArgs e)
        {
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
            }
        }

        private void EnableMap(TreeNode nodeMap, TreeNodeMappingModel model)
        {
            if (!IsMapEnable(model.File.FullPath))
            {
                var filaName = Path.GetFileName(model.File.FullPath).Split('.').First();
                RenameMap(filaName, ".json", nodeMap, model, false);
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
            var newModel = GetTreeNodeMapping(model.Proxy, model.Scenario, newNameMap);
            UpdateMappingNode(nodeMap, newModel);
            UpdateMappingTab(nodeMap, oldNodeBody);
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
            if (treeServices.SelectedNode?.Tag is TreeNodeMappingModel model
                && !Dashboard.IsRunning(model.Scenario))
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

            if (treeServices.SelectedNode?.Tag is Proxy proxy
                && !Dashboard.IsRunning(proxy.GetDefaultScenario()))
            {
                if (e.KeyCode == Keys.Delete)
                {
                    RemoveProxy(treeServices.SelectedNode);
                }
                if (e.KeyCode == Keys.F2)
                {
                    OpenAddOrEditProxy(proxy);
                }
            }

            if (treeServices.SelectedNode?.Tag is Data.Scenario scenario
                && !Dashboard.IsRunning(scenario))
            {
                if (e.KeyCode == Keys.Delete)
                {
                    RemoveScenario(treeServices.SelectedNode);
                }
                if (e.KeyCode == Keys.F2)
                {
                    OpenAddOrEditScenario(treeServices.SelectedNode.Parent, scenario);
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
    }
}
