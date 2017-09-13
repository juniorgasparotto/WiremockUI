using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using Newtonsoft.Json;
using WiremockUI.Languages;

namespace WiremockUI
{
    public partial class UcJsonView : UserControl
    {
        private bool expandAll;

        public Action<string, string, bool> OnJsonVisualizer { get; set; }
        public Action<string, string, bool> OnTextVisualizer { get; set; }

        [Description("The content json"), Category("Data")]
        public string ContentJson
        {
            get => txtContent.Text;
            set => txtContent.Text = value;
        }

        [Description("If true all nodes are expanded"), Category("Data")]
        public bool ExpandAll
        {
            get => expandAll;
            set => expandAll = value;
        }

        public TabPage TabRaw => tabRaw;
        public TabPage TabJsonTree => tabTree;

        public TabControl Tabs => tabs;

        public UcJsonView()
        {
            InitializeComponent();

            tabRaw.Text = Resource.tabRaw;
            tabTree.Text = Resource.tabTree;
            btnFormat.Text = Resource.btnFormat;
        }

        public UcJsonView(string content, bool expandAll = true)
        {
            InitializeComponent();
            this.ContentJson = content;
            this.expandAll = expandAll;
        }

        public void GenerateTree()
        {
            treeRaw.Nodes.Clear();
            string rootName = "root", nodeName = "node";

            JContainer json;
            try
            {
                if (this.ContentJson.StartsWith("["))
                {
                    json = JArray.Parse(this.ContentJson);
                    treeRaw.Nodes.Add(Json2Tree((JArray)json, rootName, nodeName));
                }
                else
                {
                    json = JObject.Parse(this.ContentJson);
                    treeRaw.Nodes.Add(Json2Tree((JObject)json, rootName));
                }

                // aways expand root
                treeRaw.Nodes[0].Expand();

                if (expandAll)
                    treeRaw.ExpandAll();
            }
            catch (Exception ex)
            {
                lblError.Text = string.Format(Resource.jsonLoadError, ex.Message);
                lblError.Visible = true;
            }
        }

        private TreeNode Json2Tree(JArray root, string rootName = "", string nodeName = "")
        {
            TreeNode parent = GetNode(rootName);
            int index = 0;

            foreach (JToken obj in root)
            {
                var child = GetNode(string.Format("[{1}]", nodeName, index++));
                foreach (KeyValuePair<string, JToken> token in (JObject)obj)
                {
                    switch (token.Value.Type)
                    {
                        case JTokenType.Array:
                        case JTokenType.Object:
                            child.Nodes.Add(Json2Tree((JObject)token.Value, token.Key));
                            break;
                        default:
                            child.Nodes.Add(GetChild(token));
                            break;
                    }
                }
                parent.Nodes.Add(child);
            }

            return parent;
        }

        private TreeNode Json2Tree(JObject root, string text = "")
        {
            TreeNode parent = GetNode(text);

            foreach (KeyValuePair<string, JToken> token in root)
            {

                switch (token.Value.Type)
                {
                    case JTokenType.Object:
                        parent.Nodes.Add(Json2Tree((JObject)token.Value, token.Key));
                        break;
                    case JTokenType.Array:
                        int index = 0;
                        foreach (JToken element in (JArray)token.Value)
                        {
                            parent.Nodes.Add(Json2Tree((JObject)element, string.Format("{0}[{1}]", token.Key, index++)));
                        }

                        if (index == 0) parent.Nodes.Add(string.Format("{0}[ ]", token.Key)); //to handle empty arrays
                        break;
                    default:
                        parent.Nodes.Add(GetChild(token));
                        break;
                }
            }

            return parent;
        }

        private TreeNode GetChild(KeyValuePair<string, JToken> token)
        {
            TreeNode child = GetNode(token.Key);
            TreeNode finalNode = GetNode(string.IsNullOrEmpty(token.Value.ToString()) ? "n/a" : token.Value.ToString());
            finalNode.ForeColor = Color.Blue;
            child.Nodes.Add(finalNode);
            return child;
        }

        private TreeNode GetNode(string text)
        {
            var node = new TreeNode(text);
            AddMenuTrip(node);
            return node;
        }

        private void AddMenuTrip(TreeNode node)
        {
            var menu = new ContextMenuStrip();
            var viewJsonMenu = new ToolStripMenuItem();
            var viewTextMenu = new ToolStripMenuItem();
            var expandAllMenu = new ToolStripMenuItem();
            var collapseAllMenu = new ToolStripMenuItem();

            menu.Items.AddRange(new ToolStripMenuItem[]
            {
                viewTextMenu,
                viewJsonMenu,
                expandAllMenu,
                collapseAllMenu
            });

            // add files
            viewTextMenu.Text = Resource.viewTextMenu;
            viewTextMenu.Click += (a, b) =>
            {
                var name = node.Parent?.Text;
                OpenText(name, node.Text);
            };

            // add files
            viewJsonMenu.Text = Resource.viewAsJsonMenu;
            viewJsonMenu.Click += (a, b) =>
            {
                var name = node.Parent?.Text;
                OpenJson(name, node.Text);
            };

            // expand all
            expandAllMenu.Text = Resource.expandAllMenu;
            expandAllMenu.Click += (a, b) =>
            {
                node.ExpandAll();
            };

            // collapse all
            collapseAllMenu.Text = Resource.collapseAllMenu;
            collapseAllMenu.Click += (a, b) =>
            {
                node.Collapse();
            };

            node.ContextMenuStrip = menu;
        }

        private void OpenJson(string name, string body)
        {
            if (OnJsonVisualizer == null)
            {
                var _tabName = "json:";
                if (!string.IsNullOrWhiteSpace(name))
                    _tabName += "." + name;

                var frm = new FormJsonViewer(FormMaster.Current, _tabName, body, true);
                FormMaster.Current.TabMaster.AddTab(frm, null, _tabName);
            }
            else
            {
                OnJsonVisualizer.Invoke(name, body, true);
            }
        }

        private void OpenText(string name, string body)
        {
            if (OnTextVisualizer == null)
            {
                var _tabName = "text:";
                if (!string.IsNullOrWhiteSpace(name))
                    _tabName += "." + name;

                var frm = new FormTextView(FormMaster.Current, _tabName, body);
                FormMaster.Current.TabMaster.AddTab(frm, null, _tabName);
            }
            else
            {
                OnTextVisualizer.Invoke(name, body, true);
            }
        }

        private void tabs_Click(object sender, EventArgs e)
        {
            var selected = tabs.SelectedTab;
            if (selected == tabTree)
                GenerateTree();
        }

        private void btnFormat_Click(object sender, EventArgs e)
        {
            ContentJson = Helper.FormatToJson(ContentJson);
        }

        public bool IsValidJson(string strInput)
        {
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void SetContentFocus()
        {
            txtContent.Focus();
            txtContent.SelectionStart = 0;
            txtContent.SelectionLength = 0;
        }
    }
}
