using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WiremockUI.Data;

namespace WiremockUI
{
    public partial class FormMaster : Form
    {
        delegate void ActionDelegate();
        private int treeX;
        public TabMaster TabMaster { get; private set;}
        public Dashboard Dashboard { get; private set; }

        public FormMaster()
        {
            InitializeComponent();
        }

        private void Master_Load(object sender, EventArgs e)
        {
            this.Dashboard = new Dashboard();
            this.TabMaster = new TabMaster(this);

            // permite renomear os nodes
            treeServices.LabelEdit = true;
            LoadProxies();
        }

        internal void LoadProxies()
        {
            var topNode = new TreeNode("Serviços");
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

            // add files
            addMenu.Text = "Adicionar";
            addMenu.ImageKey = "add";
            addMenu.Click += (a, b) =>
            {
                OpenAddOrEditProxy();
            };

            // start all
            startAllMenu.Text = "Iniciar todos";
            startAllMenu.ImageKey = "play";
            startAllMenu.Click += (a, b) =>
            {
                PlayAll(Dashboard.PlayType.Play);
            };

            // start and record all
            startAndRecordAllMenu.Text = "Iniciar e gravar todos";
            startAndRecordAllMenu.ImageKey = "record";
            startAndRecordAllMenu.Click += (a, b) =>
            {
                PlayAll(Dashboard.PlayType.PlayAndRecord);
            };

            // stop all
            stopAllMenu.Text = "Parar todos";
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

                LoadMocks(nodeProxy, proxy);

                // Create the ContextMenuStrip.
                var menu = new ContextMenuStrip();
                var addMock = new ToolStripMenuItem();
                var startAsProxyMenu = new ToolStripMenuItem();
                var startMenu = new ToolStripMenuItem();
                var startAndRecordMenu = new ToolStripMenuItem();
                var stopMenu = new ToolStripMenuItem();
                var openFolderMenu = new ToolStripMenuItem();
                var openUrlTargetMenu = new ToolStripMenuItem();
                var openUrlProxyMockMenu = new ToolStripMenuItem();
                var editMenu = new ToolStripMenuItem();
                var removeMenu = new ToolStripMenuItem();

                //Add the menu items to the menu.
                menu.Items.AddRange(new ToolStripMenuItem[]
                {
                    addMock,
                    startMenu,
                    startAsProxyMenu,
                    startAndRecordMenu,
                    stopMenu,
                    openFolderMenu,
                    openUrlTargetMenu,
                    openUrlProxyMockMenu,
                    editMenu,
                    removeMenu
                });

                menu.ImageList = imageList1;
                menu.Opening += (a, b) =>
                {
                    var hasMock = proxy.Mocks.Any();

                    startMenu.Visible = hasMock;
                    startAndRecordMenu.Visible = hasMock;
                    stopMenu.Visible = hasMock;
                    openFolderMenu.Visible = hasMock;
                    openUrlTargetMenu.Visible = hasMock;
                    openUrlProxyMockMenu.Visible = hasMock;

                    if (hasMock)
                    {
                        var defaultMock = proxy.GetDefaultMock();
                        var isRunning = Dashboard.IsRunning(defaultMock);
                        startAsProxyMenu.Enabled = !isRunning;
                        startMenu.Enabled = !isRunning;
                        startAndRecordMenu.Enabled = !isRunning;
                        stopMenu.Enabled = isRunning;
                        openUrlProxyMockMenu.Visible = isRunning;
                        editMenu.Enabled = !isRunning;
                        removeMenu.Enabled = !isRunning;

                        if (!Directory.Exists(proxy.GetFullPath()))
                        {
                            startMenu.Visible = false;
                            openFolderMenu.Visible = false;
                        }
                        else
                        {
                            startMenu.Visible = true;
                            openFolderMenu.Visible = true;
                        }
                    }
                    else
                    {
                        editMenu.Enabled = true;
                        removeMenu.Enabled = true;
                    }
                };

                // add cenary
                addMock.Text = "Adicionar mock";
                addMock.ImageKey = "add";
                addMock.Click += (a, b) =>
                {
                    OpenAddOrEditMock(nodeProxy);
                };

                // show files
                openFolderMenu.Text = "Abrir pasta do proxy";
                openFolderMenu.ImageKey = "folder";
                openFolderMenu.Click += (a, b) =>
                {
                    Process.Start(proxy.GetFullPath());
                };

                // open url target
                openUrlTargetMenu.Text = "Abrir URL destino no browser";
                openUrlTargetMenu.ImageKey = "services";
                openUrlTargetMenu.Click += (a, b) =>
                {
                    Process.Start(proxy.UrlTarget);
                };

                // open url mock
                openUrlProxyMockMenu.Text = "Abrir URL do proxy no browser";
                openUrlProxyMockMenu.ImageKey = "services";
                openUrlProxyMockMenu.Click += (a, b) =>
                {
                    Process.Start(proxy.GetUrlProxy());
                };

                // edit mock
                editMenu.Text = "Editar";
                editMenu.ImageKey = "edit";
                editMenu.Click += (a, b) =>
                {
                    OpenAddOrEditProxy(proxy);
                };

                // remove mock
                removeMenu.Text = "Remover";
                removeMenu.ImageKey = "remove";
                removeMenu.Click += (a, b) =>
                {
                    RemoveProxy(nodeProxy);
                };

                // play
                startMenu.Text = "Iniciar";
                startMenu.ImageKey = "play";
                startMenu.Click += (a, b) =>
                {
                    var defaultMock = proxy.GetDefaultMock();
                    StartService(defaultMock, Dashboard.PlayType.Play);
                };

                // play and record
                startAndRecordMenu.Text = "Iniciar e Gravar";
                startAndRecordMenu.ImageKey = "record";
                startAndRecordMenu.Click += (a, b) =>
                {
                    var defaultMock = proxy.GetDefaultMock();
                    StartService(defaultMock, Dashboard.PlayType.PlayAndRecord);
                };

                // play
                startAsProxyMenu.Text = "Iniciar (Somente Proxy)";
                startAsProxyMenu.ImageKey = "play-proxy";
                startAsProxyMenu.Click += (a, b) =>
                {
                    var defaultMock = proxy.GetDefaultMock();
                    StartService(defaultMock, Dashboard.PlayType.PlayAsProxy);
                };

                // stop
                stopMenu.Text = "Parar";
                stopMenu.ImageKey = "stop";
                stopMenu.Click += (a, b) =>
                {
                    var defaultMock = proxy.GetDefaultMock();
                    StopService(defaultMock);
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

        internal TabControl GetTabControl()
        {
            return tabForms;
        }

        private void LoadMocks(TreeNode nodeProxy, Proxy proxy)
        {
            var db = new UnitOfWork();
            foreach (var c in proxy.Mocks)
                SetMock(nodeProxy, c);
        }

        private void LoadRequestsAndResponses(TreeNode nodeMock, Mock mock)
        {
            var proxy = (Proxy)nodeMock.Parent.Tag;
            var path = proxy.GetMappingPath(mock);
            if (!Directory.Exists(path))
                return;

            string[] filePaths = Directory.GetFiles(path);
            
            foreach(var mapFile in filePaths)
            {
                AddMappingNode(nodeMock, mock, mapFile);
            }
        }

        internal void SetMock(TreeNode nodeProxy, Mock mock)
        {
            var proxy = (Proxy)nodeProxy.Tag;
            TreeNode nodeMock = null;
            foreach (TreeNode n in nodeProxy.Nodes)
            {
                if (((Mock)n.Tag).Id == mock.Id)
                {
                    nodeMock = n;
                    break;
                }
            }

            if (nodeMock == null)
            {
                nodeMock = new TreeNode();
                nodeMock.Text = mock.GetFormattedName();
                nodeMock.Tag = mock;
                nodeProxy.Nodes.Add(nodeMock);

                if (mock.IsDefault)
                    SetNodeMockAsDefault(mock, nodeMock);

                LoadRequestsAndResponses(nodeMock, mock);

                // Create the ContextMenuStrip.
                var menu = new ContextMenuStrip();
                var addMenu = new ToolStripMenuItem();
                var setDefaultMenu = new ToolStripMenuItem();
                var openFolderMenu = new ToolStripMenuItem();
                var editMenu = new ToolStripMenuItem();
                var removeMenu = new ToolStripMenuItem();
                var showUrlMenu = new ToolStripMenuItem();

                //Add the menu items to the menu.
                menu.Items.AddRange(new ToolStripMenuItem[]
                {
                    addMenu,
                    setDefaultMenu,
                    openFolderMenu,
                    editMenu,
                    removeMenu,
                    showUrlMenu
                });

                menu.ImageList = imageList1;
                menu.Opening += (a, b) =>
                {
                    var isRunning = Dashboard.IsRunning(mock);
                    //setDefaultMenu.Enabled = !Dashboard.IsAnyRunning();
                    addMenu.Enabled = !isRunning;
                    editMenu.Enabled = !isRunning;
                    removeMenu.Enabled = !isRunning;

                    if (mock.ShowURL)
                        showUrlMenu.ImageKey = "check";
                    else
                        showUrlMenu.ImageKey = "";

                    if (!proxy.AlreadyRecord(mock))
                    {
                        openFolderMenu.Visible = false;
                    }
                    else
                    {
                        openFolderMenu.Visible = true;
                    }
                };

                // add menu
                addMenu.Text = "Adicionar";
                addMenu.ImageKey = "add";
                addMenu.Click += (a, b) =>
                {
                    AddNewMap(nodeMock);
                };

                // show files
                setDefaultMenu.Text = "Definir como padrão";
                setDefaultMenu.ImageKey = "default";
                setDefaultMenu.Click += (a, b) =>
                {
                    if (Dashboard.IsRunning(proxy.GetDefaultMock()))
                    {
                        Helper.MessageBoxExclamation("Para continuar é necessário parar a execução desse proxy.");
                        return;
                    }

                    SetNodeMockAsDefault(mock, nodeMock);
                };

                // show files
                openFolderMenu.Text = "Abrir pasta da mock";
                openFolderMenu.ImageKey = "folder";
                openFolderMenu.Click += (a, b) =>
                {
                    Process.Start(proxy.GetFullPath(mock));
                };

                // edit mock
                editMenu.Text = "Editar";
                editMenu.ImageKey = "edit";
                editMenu.Click += (a, b) =>
                {
                    OpenAddOrEditMock(nodeProxy, mock);
                };

                // remove mock
                removeMenu.Text = "Remover";
                removeMenu.ImageKey = "remove";
                removeMenu.Click += (a, b) =>
                {
                    RemoveMock(nodeMock, mock);
                };

                // mostra ou esconde a URL do mock

                showUrlMenu.Text = "Visualizar URL";
                showUrlMenu.Click += (a, b) =>
                {
                    var db = new UnitOfWork();
                    mock.ShowURL = !mock.ShowURL;
                    db.Proxies.Update(proxy);
                    db.Save();

                    var nodesMaps = GetAllMappingNodes(mock);
                   
                    foreach(var nodeMap in nodesMaps)
                    {
                        var model = (TreeNodeMappingModel)nodeMap.Tag;
                        var text = model.Mapping.GetFormattedName(model.File.GetOnlyFileName(), mock.ShowURL);
                        nodeMap.Text = text;
                    }
                };

                // Set the ContextMenuStrip property to the ContextMenuStrip.
                nodeMock.ContextMenuStrip = menu;
            }
            else
            {
                nodeMock.Text = mock.GetFormattedName();
                nodeMock.Tag = mock;
            }

            ChangeTreeNodeImage(nodeMock, "mock");
            nodeProxy.Expand();
        }

        private void OpenAddOrEditProxy(Proxy proxy = null)
        {
            var frmAdd = new FormAddProxy(this, proxy);
            frmAdd.StartPosition = FormStartPosition.CenterParent;
            frmAdd.ShowDialog();
        }

        private void OpenAddOrEditMock(TreeNode nodeProxy, Mock mockService = null)
        {
            var proxy = (Proxy)nodeProxy.Tag;
            var frmAdd = new FormAddMock(this, nodeProxy, proxy, mockService?.Id);
            frmAdd.StartPosition = FormStartPosition.CenterParent;
            frmAdd.ShowDialog();
        }

        private TreeNode AddMappingNode(TreeNode nodeMock, Mock mock, string mapFile, int index = -1)
        {
            var proxy = (Proxy)nodeMock.Parent.Tag;
            var treeNodeMapping = GetTreeNodeMapping(proxy, mock, mapFile);

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

                var isRunning = Dashboard.IsRunning(mock);
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
            renameMenu.Text = "Renomear";
            renameMenu.ShortcutKeys = Keys.F2;
            renameMenu.ImageKey = "rename";
            renameMenu.Click += (a, b) =>
            {
                nodeMapping.BeginEdit();
            };

            // delete
            deleteMenu.Text = "Remover";
            deleteMenu.ImageKey = "remove";
            deleteMenu.ShortcutKeys = Keys.Delete;
            deleteMenu.Click += (a, b) =>
            {
                DeleteMap(nodeMapping);
            };

            // disable
            toggleMapStateMenu.Text = "Habilitado";
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
                    Helper.MessageBoxError("Ocorreu um erro ao tentar excluir esse arquivo: " + ex.Message);
                }
            };

            // duplicate
            duplicateMenu.Text = "Duplicar";
            duplicateMenu.ImageKey = "duplicate";
            duplicateMenu.ShortcutKeys = Keys.Control | Keys.D;
            duplicateMenu.Click += (a, b) =>
            {
                DuplicateMap(nodeMapping);
            };

            // view in explorer
            viewInExplorerMenu.Text = "Visualizar no explorer";
            viewInExplorerMenu.Click += (a, b) =>
            {
                var model = (TreeNodeMappingModel)nodeMapping.Tag;
                ViewFileInExplorer(model.File.FullPath);
            };

            nodeMapping.ContextMenuStrip = menu;
            nodeMapping.Tag = treeNodeMapping;

            if (index >= 0)
                nodeMock.Nodes.Insert(index, nodeMapping);
            else
                nodeMock.Nodes.Add(nodeMapping);

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
            if (Helper.MessageBoxQuestion("Deseja realmente remover esse arquivo?") == DialogResult.Yes)
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
                    Helper.MessageBoxError("Ocorreu um erro ao tentar excluir esse arquivo: " + ex.Message);
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
            treeServices.SelectedNode = AddMappingNode(GetNodeMockById(model.Mock.Id), model.Mock, newMapFile, nodeMapping.Index + 1);
        }

        private void AddNewMap(TreeNode nodeMock)
        {
            var contentMap = "";
            var contentBody = "";
            var templateMapFileName = "Templates/Mappings/New.json";
            var templateBodyFileName = "Templates/Files/New.txt";
            var mockName = "Mock.json";
            var mockBodyName = "Mock.txt";
            var mock = (Mock)nodeMock.Tag;
            var proxy = (Proxy)nodeMock.Parent.Tag;
            var newMapFileName = GetNewFileName(Path.Combine(proxy.GetMappingPath(mock), mockName));
            var newBodyFileName = GetNewFileName(Path.Combine(proxy.GetBodyFilesPath(mock), mockBodyName));

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

            var nodeMap = AddMappingNode(nodeMock, mock, newMapFileName);
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
            viewInExplorerResponseMenu.Text = "Visualizar no explorer";
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

        private TreeNodeMappingModel GetTreeNodeMapping(Proxy proxy, Mock mock, string mapFile)
        {
            var fileModelMapping = FileModel.Create(mapFile);

            var content = fileModelMapping.GetContent(out var ex);
            var mapName = fileModelMapping.GetOnlyFileName();
            MappingModel mapping = null;
            Exception exMap = null;

            if (content != null)
                mapping = MappingModel.Create(proxy, mock, content, out exMap);

            if (mapping != null)
                mapName = mapping.GetFormattedName(mapName, mock.ShowURL);

            var treeNodeMapping = new TreeNodeMappingModel
            {
                Name = mapName,
                File = fileModelMapping,
                Mapping = mapping,
                Proxy = proxy,
                Mock = mock
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

        private void StartWatcherFolder(TreeNode nodeMock, Mock mock)
        {
            var watcher = new FileSystemWatcher();
            var proxy = (Proxy)nodeMock.Parent.Tag;

            watcher.Path = proxy.GetMappingPath(mock);
            watcher.Filter = "*.*";
            watcher.IncludeSubdirectories = true;

            watcher.Created += (sender, e) =>
            {
                var d = new ActionDelegate(() =>
                {
                    AddMappingNode(nodeMock, mock, e.FullPath);
                    nodeMock.Expand();
                });

                this.Invoke(d);
            };

            watcher.EnableRaisingEvents = true;

            Dashboard.AddWatchers(mock, watcher);
        }

        private void RemoveProxy(TreeNode nodeProxy)
        {
            if (MessageBox.Show("Deseja realmente excluir esse proxy de mock?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                        Helper.MessageBoxError("Ocorreu um problema ao tentar excluir a pasta. Tente fazer esse processo manualmente.");
                        return;
                    }
                }

                nodeProxy.Parent.Nodes.Remove(nodeProxy);

                var db = new UnitOfWork();
                db.Proxies.Delete(proxy.Id);
                db.Save();
            }
        }


        private void RemoveMock(TreeNode nodeMock, Mock mock)
        {
            if (MessageBox.Show("Deseja realmente excluir esse mock?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var nodeProxy = nodeMock.Parent;
                var proxy = (Proxy)nodeMock.Parent.Tag;
                var path = proxy.GetFullPath(mock);

                if (Directory.Exists(path))
                {
                    try
                    {
                        Helper.ClearFolder(path);
                        Directory.Delete(path, true);
                    }
                    catch
                    {
                        Helper.MessageBoxError("Ocorreu um problema ao tentar excluir a pasta. Tente fazer esse processo manualmente.");
                        return;
                    }
                }

                nodeMock.Parent.Nodes.Remove(nodeMock);
                proxy.RemoveMock(mock.Id);

                if (mock.IsDefault)
                    SetFirstMockAsDefault(nodeProxy);

                var db = new UnitOfWork();
                db.Proxies.Update(proxy);
                db.Save();
            }
        }

        private void SetFirstMockAsDefault(TreeNode nodeProxy)
        {
            if (nodeProxy.Nodes.Count > 0)
            {
                var nodeMock = nodeProxy.Nodes[0];
                SetNodeMockAsDefault((Mock)nodeMock.Tag, nodeMock);
            }
        }

        private void SetNodeMockAsDefault(Mock mock, TreeNode nodeMock)
        {
            // change database
            var proxy = (Proxy)nodeMock.Parent.Tag;
            proxy.SetDefault(mock);

            var db = new UnitOfWork();
            db.Proxies.Update(proxy);
            db.Save();

            // change front
            var font = new Font(treeServices.Font.FontFamily, treeServices.Font.Size, FontStyle.Bold);
            nodeMock.NodeFont = font;

            foreach(TreeNode n in nodeMock.Parent.Nodes)
                if (n != nodeMock)
                    n.NodeFont = new Font(treeServices.Font.FontFamily, treeServices.Font.Size, treeServices.Font.Style);
        }

        private static void ChangeTreeNodeImage(TreeNode nodeService, string imageKey)
        {
            nodeService.ImageKey = imageKey;
            nodeService.SelectedImageKey = nodeService.ImageKey;
            nodeService.StateImageKey = nodeService.ImageKey;
        }

        private void StartService(Mock mock, Dashboard.PlayType playType)
        {
            var nodeMock = GetNodeMockById(mock.Id);
            var nodeProxy = nodeMock.Parent;
            var proxy = (Proxy)nodeProxy.Tag;

            var frmStart = new FormStartMockService(this, proxy, mock, playType);
            
            try
            {
                frmStart.Play();
            }
            catch (Exception ex)
            {
                StopService(mock);
                Helper.MessageBoxError($"Ocorreu um erro ao tentar iniciar: {ex.GetType().Name}: {ex.Message}");
                return;
            }

            var recordText = "";

            if (playType == Dashboard.PlayType.PlayAndRecord)
                recordText = " (Gravando)";
            else if (playType == Dashboard.PlayType.PlayAsProxy)
                recordText = " (Proxy)";

            TabMaster.AddTab(frmStart, mock.Id, mock.Name + recordText)
                .CanClose = () => {
                    if (Helper.MessageBoxQuestion("Deseja realmente parar o serviço?") == DialogResult.Yes)
                        StopService(mock);

                    return false;
                };

            var icon = "start";
            if (playType == Dashboard.PlayType.PlayAndRecord)
                icon = "record";
            else if (playType == Dashboard.PlayType.PlayAsProxy)
                icon = "play-proxy";

            ChangeTreeNodeImage(nodeProxy, icon);

            if (playType == Dashboard.PlayType.PlayAndRecord)
                StartWatcherFolder(nodeMock, mock);
        }

        private void StopService(Mock mock)
        {
            var nodeMock = GetNodeMockById(mock.Id);
            var nodeProxy = nodeMock.Parent;

            Dashboard.Stop(mock);
            ChangeTreeNodeImage(nodeProxy, "stop");

            TabMaster.CloseTab(mock.Id);
        }

        private IEnumerable<TreeNode> GetAllProxiesNodes()
        {
            foreach (TreeNode nodeProxy in treeServices.Nodes[0].Nodes)
                yield return nodeProxy;
        }

        private IEnumerable<TreeNode> GetAllMappingNodes(Mock mock)
        {
            foreach (TreeNode nodeProxy in GetAllProxiesNodes())
            {
                foreach (TreeNode nodeMock in nodeProxy.Nodes)
                    if (nodeMock.Tag is Mock nMock && nMock == mock)
                        foreach (TreeNode nodeMap in nodeMock.Nodes)
                            yield return nodeMap;
            }
        }

        private TreeNode GetNodeMockById(Guid id)
        {
            foreach (TreeNode n in GetAllProxiesNodes())
            {
                foreach (TreeNode m in n.Nodes)
                    if (((Mock)m.Tag).Id == id)
                        return m;
            }

            return null;
        }

        private void StopAll()
        {
            foreach(TreeNode n in GetAllProxiesNodes())
            {
                var proxy = (Proxy)n.Tag;
                if (proxy.Mocks.Any())
                    StopService(proxy.GetDefaultMock());
            }

            foreach (var w in Dashboard.Watchers)
                w.Value.EnableRaisingEvents = false;

            Dashboard.Watchers.Clear();
        }

        private void PlayAll(Dashboard.PlayType playType)
        {
            foreach (TreeNode n in GetAllProxiesNodes())
            {
                var proxy = (Proxy)n.Tag;
                if (proxy.Mocks.Any())
                {
                    var mock = proxy.GetDefaultMock();
                    if (Dashboard.IsRunning(mock))
                        continue;

                    // inicia gravando caso ainda não tenha sido feito
                    if (!proxy.AlreadyRecord(mock))
                        playType = Dashboard.PlayType.PlayAsProxy;

                    StartService(mock, playType);
                }
            }
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
            else if (selected.Tag != null && selected.Tag is Mock)
            {
                OpenAddOrEditMock(selected.Parent, (Mock)selected.Tag);
            }
            else if (selected.Tag != null && selected.Tag is TreeNodeMappingModel treeNodeMapping)
            {
                var currentTab = TabMaster.GetTabByInternalTag(selected);
                if (currentTab != null)
                {
                    tabForms.SelectedTab = currentTab;
                    return;
                }

                var frmStart = new FormJsonFile(this, null, treeNodeMapping.File.FullPath);
                frmStart.OnSave = () =>
                {
                    var newTreeNodeMapping = GetTreeNodeMapping(treeNodeMapping.Proxy, treeNodeMapping.Mock, treeNodeMapping.File.FullPath);
                    UpdateMappingNode(selected, newTreeNodeMapping);
                };

                TabMaster.AddTab(frmStart, selected, treeNodeMapping.File.GetOnlyFileName());
            }
            else if (selected.Tag != null && selected.Tag is TreeNodeBodyModel treeNodeBody)
            {
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
            PlayAll(Dashboard.PlayType.Play);
        }

        private void menuPlayAndRecordAll_Click(object sender, EventArgs e)
        {
            PlayAll(Dashboard.PlayType.PlayAndRecord);
        }

        private void menuRefresh_Click(object sender, EventArgs e)
        {
            StopAll();
            treeServices.Nodes.Clear();
            tabForms.TabPages.Clear();
            LoadProxies();
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
                Helper.MessageBoxError("Esse arquivo de mapa já existe");
                return;
            }

            if (renameResponse && model.Mapping != null && model.Mapping.HasBodyFile())
            {
                newNameBody = GetNewName(newName, model.TreeNodeBody.File, null);

                if (File.Exists(newNameBody))
                {
                    Helper.MessageBoxError("Esse arquivo de resposta já existe");
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
            var newModel = GetTreeNodeMapping(model.Proxy, model.Mock, newNameMap);
            UpdateMappingNode(nodeMap, newModel);
            UpdateMappingTab(nodeMap, oldNodeBody);
        }

        private string GetNewName(string newName, FileModel file, string extension)
        {
            var ext = extension ?? Path.GetExtension(file.FullPath);
            var directory = Path.GetDirectoryName(file.FullPath);
            return Path.Combine(directory, Path.GetFileNameWithoutExtension(newName) + ext);
        }
        
        private void treeServices_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                if (e.Label.Length > 0)
                {
                    // cancela a edição, pois o novo nome será renderizado de forma diferente
                    e.CancelEdit = true;

                    try
                    {
                        var selected = e.Node;
                        if (selected.Tag is TreeNodeMappingModel model)
                        {
                            RenameMap(e.Label, null, selected, model);
                        }
                    }
                    catch(Exception ex)
                    {
                        Helper.MessageBoxError("Erro ao renomear arquivo: " + ex.Message);
                    }
                }
                else
                {                    
                    e.CancelEdit = true;                    
                }
            }
        }

        private void treeServices_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (!(e.Node.Tag is TreeNodeMappingModel))
                e.CancelEdit = true;
        }

        private void treeServices_KeyDown(object sender, KeyEventArgs e)
        {
            if (treeServices.SelectedNode?.Tag is TreeNodeMappingModel model
                && !Dashboard.IsRunning(model.Mock))
            {
                if (e.KeyCode == Keys.Delete)
                {
                    DeleteMap(treeServices.SelectedNode);
                }
                if (e.KeyCode == Keys.F2)
                {
                    treeServices.SelectedNode.BeginEdit();
                }
                else if (e.Control && e.KeyCode == Keys.D)
                {
                    DuplicateMap(treeServices.SelectedNode);
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            }
        }
    }
}
