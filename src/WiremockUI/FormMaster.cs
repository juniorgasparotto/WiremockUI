using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
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
                PlayAll(false);
            };

            // start and record all
            startAndRecordAllMenu.Text = "Iniciar e gravar todos";
            startAndRecordAllMenu.ImageKey = "record";
            startAndRecordAllMenu.Click += (a, b) =>
            {
                PlayAll(true);
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
                var startMenu = new ToolStripMenuItem();
                var startAndRecordMenu = new ToolStripMenuItem();
                var stopMenu = new ToolStripMenuItem();
                var openFolderMenu = new ToolStripMenuItem();
                var openUrlOriginalMenu = new ToolStripMenuItem();
                var openUrlProxyMockMenu = new ToolStripMenuItem();
                var editMenu = new ToolStripMenuItem();
                var removeMenu = new ToolStripMenuItem();

                //Add the menu items to the menu.
                menu.Items.AddRange(new ToolStripMenuItem[]
                {
                    addMock,
                    startMenu,
                    startAndRecordMenu,
                    stopMenu,
                    openFolderMenu,
                    openUrlOriginalMenu,
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
                    openUrlOriginalMenu.Visible = hasMock;
                    openUrlProxyMockMenu.Visible = hasMock;

                    if (hasMock)
                    {
                        var defaultMock = proxy.GetDefaultMock();
                        var isRunning = Dashboard.IsRunning(defaultMock);
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

                // open url original
                openUrlOriginalMenu.Text = "Abrir URL original no browser";
                openUrlOriginalMenu.ImageKey = "services";
                openUrlOriginalMenu.Click += (a, b) =>
                {
                    Process.Start(proxy.UrlOriginal);
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
                    StartService(defaultMock, false);
                };

                // play and record
                startAndRecordMenu.Text = "Iniciar e Gravar";
                startAndRecordMenu.ImageKey = "record";
                startAndRecordMenu.Click += (a, b) =>
                {
                    var defaultMock = proxy.GetDefaultMock();
                    StartService(defaultMock, true);
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
                AddRequestResponseNode(nodeMock, mock, mapFile);
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
                var setDefaultMenu = new ToolStripMenuItem();
                var openFolderMenu = new ToolStripMenuItem();
                var editMenu = new ToolStripMenuItem();
                var removeMenu = new ToolStripMenuItem();

                //Add the menu items to the menu.
                menu.Items.AddRange(new ToolStripMenuItem[]
                {
                    setDefaultMenu,
                    openFolderMenu,
                    editMenu,
                    removeMenu
                });

                menu.ImageList = imageList1;
                menu.Opening += (a, b) =>
                {
                    var isRunning = Dashboard.IsRunning(mock);
                    //setDefaultMenu.Enabled = !Dashboard.IsAnyRunning();
                    editMenu.Enabled = !isRunning;
                    removeMenu.Enabled = !isRunning;

                    if (!proxy.AlreadyRecord(mock))
                    {
                        openFolderMenu.Visible = false;
                    }
                    else
                    {
                        openFolderMenu.Visible = true;
                    }
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

        private void AddRequestResponseNode(TreeNode nodeMock, Mock mock, string mapFile)
        {
            var proxy = (Proxy)nodeMock.Parent.Tag;
            var requestResponseFile = new RequestResponse(proxy, mock, mapFile);
            var nodeRequest = new TreeNode(requestResponseFile.GetFormattedName());
            var nodeResponse = new TreeNode(requestResponseFile.GetResponseName());
            nodeRequest.Nodes.Add(nodeResponse);
            nodeMock.Nodes.Add(nodeRequest);

            nodeRequest.Tag = requestResponseFile.Request;
            nodeResponse.Tag = requestResponseFile.Response;

            ChangeTreeNodeImage(nodeRequest, "request");
            ChangeTreeNodeImage(nodeResponse, "response");
        }

        private void StartWatcherFolder(TreeNode nodeMock, Mock mock)
        {
            var watcher = new FileSystemWatcher();
            var proxy = (Proxy)nodeMock.Parent.Tag;

            watcher.Path = proxy.GetMappingPath(mock);
            //watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Filter = "*.*";
            watcher.IncludeSubdirectories = true;

            watcher.Created += (sender, e) =>
            {
                //nodeService.Expand();
                var d = new ActionDelegate(() =>
                {
                    AddRequestResponseNode(nodeMock, mock, e.FullPath);
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

        private void StartService(Mock mock, bool record)
        {
            var nodeMock = GetNodeMockById(mock.Id);
            var nodeProxy = nodeMock.Parent;
            var proxy = (Proxy)nodeProxy.Tag;

            var frmStart = new FormStartMockService(this, proxy, mock, record);
            frmStart.Play();
            var recordText = record ? " (Gravando)" : "";
            TabMaster.AddTab(frmStart, mock.Id, mock.Name + recordText)
                .CanClose = () => {
                    if (Helper.MessageBoxQuestion("Deseja realmente parar o serviço?") == DialogResult.Yes)
                        StopService(mock);

                    return false;
                };

            //var tabpage = new TabPageCustom { Text = mock.Name + recordText };
            //tabpage.Tag = mock.Id;
            //tabpage.BorderStyle = BorderStyle.Fixed3D;
            //tabForms.TabPages.Add(tabpage);
            //frmStart.TopLevel = false;
            //frmStart.Parent = tabpage;
            //frmStart.Show();
            //frmStart.Dock = DockStyle.Fill;
            //tabForms.SelectedTab = tabpage;

            ChangeTreeNodeImage(nodeProxy, (record ? "record" : "start"));

            if (record)
                StartWatcherFolder(nodeMock, mock);
        }

        private void StopService(Mock mock)
        {
            var nodeMock = GetNodeMockById(mock.Id);
            var nodeProxy = nodeMock.Parent;

            Dashboard.Stop(mock);
            ChangeTreeNodeImage(nodeProxy, "stop");

            TabMaster.CloseTab(mock.Id);

            //foreach (var t in tabForms.TabPages)
            //{
            //    if (t.Tag != null && t.Tag is Guid && (Guid)t.Tag == mock.Id)
            //    {
            //        tabForms.TabPages.Remove(t);
            //        break;
            //    }
            //}
        }

        private IEnumerable<TreeNode> GetAllProxiesNodes()
        {
            foreach (TreeNode nodeProxy in treeServices.Nodes[0].Nodes)
                yield return nodeProxy;
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

        private void PlayAll(bool record)
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
                        record = true;

                    StartService(mock, record);
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
            if (selected.Tag != null && selected.Tag is Proxy)
            {
                OpenAddOrEditProxy((Proxy)selected.Tag);
            }
            else if (selected.Tag != null && selected.Tag is Mock)
            {
                OpenAddOrEditMock(selected.Parent, (Mock)selected.Tag);
            }
            else if (selected.Tag != null && selected.Tag is RequestResponse.RequestFile)
            {
                var currentTab = TabMaster.GetTabByTag(selected.Tag);
                if (currentTab != null)
                {
                    tabForms.SelectedTab = currentTab;
                    return;
                }

                var request = (RequestResponse.RequestFile)selected.Tag;

                var frmStart = new FormViewFile(this, null, request.FileName, request.Body);
                TabMaster.AddTab(frmStart, request.RequestResponse.Request, request.RequestResponse.GetRequestName());

                //var tabpage = new TabPageCustom { Text = request.RequestResponse.GetRequestName() };
                //tabpage.Tag = selected.Tag;

                //tabpage.BorderStyle = BorderStyle.Fixed3D;
                //tabForms.TabPages.Add(tabpage);
                //frmStart.TopLevel = false;
                //frmStart.Parent = tabpage;
                //frmStart.Show();
                //frmStart.Dock = DockStyle.Fill;
                //tabForms.SelectedTab = tabpage;
            }
            else if (selected.Tag != null && selected.Tag is RequestResponse.ResponseFile)
            {
                var currentTab = TabMaster.GetTabByTag(selected.Tag);
                if (currentTab != null)
                {
                    tabForms.SelectedTab = currentTab;
                    return;
                }

                var request = (RequestResponse.ResponseFile)selected.Tag;

                var frmStart = new FormViewFile(this, null, request.FileName, request.Body);
                TabMaster.AddTab(frmStart, request.RequestResponse.Response, request.RequestResponse.GetResponseName());

                //var tabpage = new TabPageCustom { Text = request.RequestResponse.GetResponseName() };
                //tabpage.Tag = selected.Tag;

                //tabpage.BorderStyle = BorderStyle.Fixed3D;
                //tabForms.TabPages.Add(tabpage);
                //frmStart.TopLevel = false;
                //frmStart.Parent = tabpage;
                //frmStart.Show();
                //frmStart.Dock = DockStyle.Fill;
                //tabForms.SelectedTab = tabpage;
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
            PlayAll(false);
        }

        private void menuPlayAndRecordAll_Click(object sender, EventArgs e)
        {
            PlayAll(true);
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
    }
}
