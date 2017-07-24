using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace WiremockUI
{
    public partial class FormViewFile : Form
    {
        private TabPage tabPage;
        private FormMaster master;

        public FormViewFile(FormMaster master, TabPage tabPage, string fileName, string content)
        {
            InitializeComponent();
            this.tabPage = tabPage;
            this.master = master;
            if (fileName != null)
            {
                txtPath.Text = fileName;
            }
            else
            {
                txtPath.Enabled = false;
                btnOpen.Enabled = false;
            }

            var text = Helper.ResolveBreakLineIncompatibility(content);
            if (text != null)
            {
                txtContent.AppendText(text);
            }
        }

        private void LoadJsonExplorer()
        {
            jsonExplorer.Nodes.Clear();
            var js = new JavaScriptSerializer();

            try
            {
                try
                {
                    var dic = js.Deserialize<List<Dictionary<string, object>>>(txtContent.Text);
                    if (dic.Count == 0)
                        throw new Exception();

                    var i = 0;
                    TreeNode rootNode = new TreeNode("Root");
                    jsonExplorer.Nodes.Add(rootNode);

                    foreach (var d in dic)
                    {
                        TreeNode node = new TreeNode(i++.ToString());
                        rootNode.Nodes.Add(node);
                        BuildTree(d, node);                        
                    }

                    rootNode.Expand();
                }
                catch
                {
                    var dic = js.Deserialize<Dictionary<string, object>>(txtContent.Text);

                    TreeNode rootNode = new TreeNode("Root");
                    jsonExplorer.Nodes.Add(rootNode);
                    BuildTree(dic, rootNode);
                    rootNode.ExpandAll();
                }
            }
            catch 
            {
                lblError.Visible = true;
            }
        }

        public void BuildTree(Dictionary<string, object> dictionary, TreeNode node)
        {
            foreach (KeyValuePair<string, object> item in dictionary)
            {
                TreeNode parentNode = new TreeNode(item.Key);
                node.Nodes.Add(parentNode);

                try
                {
                    dictionary = (Dictionary<string, object>)item.Value;
                    BuildTree(dictionary, parentNode);
                }
                catch (InvalidCastException dicE)
                {
                    try
                    {
                        ArrayList list = (ArrayList)item.Value;

                        if (list.Count > 0 && list[0] is Dictionary<string, object> dic)
                        {
                            BuildTree(dic, parentNode);
                        }
                        else
                        {
                            foreach (string value in list)
                            {
                                TreeNode finalNode = new TreeNode(value);
                                finalNode.ForeColor = Color.Blue;
                                parentNode.Nodes.Add(finalNode);
                            }
                        }
                    }
                    catch (InvalidCastException ex)
                    {
                        TreeNode finalNode = new TreeNode(item.Value.ToString());
                        finalNode.ForeColor = Color.Blue;
                        parentNode.Nodes.Add(finalNode);
                        AddMenuTrip(finalNode);
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            master.CloseTab(tabPage);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            Process.Start(txtPath.Text);
        }

        private void tabResponse_Click(object sender, EventArgs e)
        {
            var seleceted = tabResponse.SelectedTab;
            if (seleceted.Tag == null)
            {
                seleceted.Tag = true;
                if (seleceted == tabJson)
                {
                    LoadJsonExplorer();
                }
            }
        }

        private void AddMenuTrip(TreeNode node)
        {
            var menu = new ContextMenuStrip();
            var viewJsonMenu = new ToolStripMenuItem();
            
            menu.Items.AddRange(new ToolStripMenuItem[]
            {
                viewJsonMenu
            });

            // add files
            viewJsonMenu.Text = "Visualizar como Json";
            viewJsonMenu.Click += (a, b) =>
            {
                var name = Path.GetFileNameWithoutExtension(txtPath.Text);
                if (node.Parent != null)
                    name = name + "(" + node.Parent.Text + ")";

                OpenJson(name, node.Text);
            };

            node.ContextMenuStrip = menu;
        }

        private void OpenJson(string name, string body)
        {
            var tabpage = new TabPage { Text = name };
            var frmStart = new FormViewFile(master, tabpage, null, body);
            tabpage.BorderStyle = BorderStyle.Fixed3D;
            master.GetTabControl().TabPages.Add(tabpage);            
            frmStart.TopLevel = false;
            frmStart.Parent = tabpage;
            frmStart.Show();
            frmStart.Dock = DockStyle.Fill;
            master.GetTabControl().SelectedTab = tabpage;
        }
    }
}
