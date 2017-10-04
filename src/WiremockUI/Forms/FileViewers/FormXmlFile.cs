using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using WiremockUI.Languages;

namespace WiremockUI
{
    public partial class FormXmlFile : Form, IFormFileUpdate
    {
        private object tabPage;
        private object master;

        public Action OnSave { get; set; }

        public FormXmlFile(FormMaster master, TabPageCustom tabPage, string fileName)
        {
            InitializeComponent();

            this.TabStop = false;

            this.tabPage = tabPage;
            this.master = master;
            LoadForm(fileName);

            this.btnOpen.Text = Resource.btnOpenExplorer;
            this.btnClose.Text = Resource.btnCloseTab;
            this.btnSave.Text = Resource.btnSaveFile;
            this.btnFormat.Text = Resource.btnXmlFormat;
        }

        private void LoadForm(string fileName)
        {
            var content = "";
            if (fileName != null)
            {
                txtPath.TextValue = fileName;
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
                txtContent.TextValue = content;
            }
        }

        private void Save()
        {
            try
            {
                File.WriteAllText(txtPath.TextValue, txtContent.TextValue);
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
            Process.Start(txtPath.TextValue);
        }

        private void FormTextFile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                Save();
                Helper.AnimateSaveButton(btnSave);
                e.SuppressKeyPress = true;
            }
        }

        private void btnFormat_Click(object sender, EventArgs e)
        {
            try
            {
                txtContent.TextValue = Helper.FormatToXml(txtContent.TextValue);
            }
            catch(Exception ex)
            {
                Helper.MessageBoxError(string.Format(Resource.formatXmlErrorMessage, ex.Message));
            }
        }

        public void Update(string fileName)
        {
            LoadForm(fileName);
        }

        private void FormXmlFile_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtContent;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            FormMaster.Current.TabMaster.CloseTab(this);
        }
    }
}
