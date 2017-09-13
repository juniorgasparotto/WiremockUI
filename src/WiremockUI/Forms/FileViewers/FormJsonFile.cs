using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using WiremockUI.Languages;

namespace WiremockUI
{
    public partial class FormJsonFile : Form, IFormFileUpdate
    {
        private TabPageCustom tabPage;
        private FormMaster master;

        public bool ExpandAll
        {
            get => ucJsonView.ExpandAll;
            set => ucJsonView.ExpandAll = value;
        }

        public Action OnSave { get; set; }

        public FormJsonFile(FormMaster master, TabPageCustom tabPage, string fileName)
        {
            InitializeComponent();
            this.TabStop = false;

            // to work Ctrl+S
            this.KeyPreview = true;

            this.tabPage = tabPage;
            this.master = master;
            LoadForm(fileName);

            this.btnOpen.Text = Resource.btnOpenExplorer;
            this.btnClose.Text = Resource.btnCloseTab;
            this.btnSave.Text = Resource.btnSaveFile;
        }

        private void LoadForm(string fileName)
        {
            var content = "";
            if (fileName != null)
            {
                txtPath.Text = fileName;
                content = File.ReadAllText(fileName);
            }
            else
            {
                txtPath.Enabled = false;
                btnOpen.Enabled = false;
            }

            var text = Helper.ResolveBreakLineInCompatibility(content);
            if (text != null)
            {
                ucJsonView.ContentJson = content;
                ucJsonView.OnJsonVisualizer = (_elementName, _elementValue, _expandAll) =>
                {
                    var tabName = Path.GetFileName(fileName);
                    if (!string.IsNullOrWhiteSpace(_elementName))
                        tabName += "." + _elementName;

                    var frm = new FormJsonViewer(master, tabName, _elementValue, true);
                    master.TabMaster.AddTab(frm, null, tabName);
                };
            }
        }

        private void Save()
        {
            try
            {
                if (!ucJsonView.IsValidJson(ucJsonView.ContentJson))
                {
                    Helper.MessageBoxError(Resource.invalidJsonMessage);
                    return;
                }
                
                File.WriteAllText(txtPath.Text, ucJsonView.ContentJson);
                OnSave?.Invoke();
            }
            catch (Exception ex)
            {
                Helper.MessageBoxError(string.Format(Resource.saveFileErrorMessage, ex.Message));
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            Process.Start(txtPath.Text);
        }

        private void FormJsonFile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                Save();
                Helper.AnimateSaveButton(btnSave);
                e.SuppressKeyPress = true;  
            }
        }

        public void Update(string fileName)
        {
            LoadForm(fileName);

            if (ucJsonView.Tabs.SelectedTab == ucJsonView.TabJsonTree)
                ucJsonView.GenerateTree();
        }

        private void FormJsonFile_Load(object sender, EventArgs e)
        {
            this.ActiveControl = ucJsonView;
            ucJsonView.SetContentFocus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            FormMaster.Current.TabMaster.CloseTab(this);
        }
    }
}