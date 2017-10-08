using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using WiremockUI.Languages;

namespace WiremockUI
{
    public partial class FormTextFile : Form, IFormFileUpdate
    {
        private object tabPage;
        private object master;
        private string originalText;

        public Action OnSave { get; set; }

        public FormTextFile(FormMaster master, TabPageCustom tabPage, string fileName)
        {
            InitializeComponent();

            this.TabStop = false;

            this.tabPage = tabPage;
            this.master = master;
            LoadForm(fileName);

            this.btnOpen.Text = Resource.btnOpenExplorer;
            this.btnClose.Text = Resource.btnCloseTab;
            this.btnSave.Text = Resource.btnSaveFile;

            this.originalText = this.btnSave.Text;
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
                this.txtContent.IsEdited = false;
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
                Helper.AnimateSaveButton(btnSave, this.originalText);
                e.SuppressKeyPress = true;
            }
        }

        #region IFormFileUpdate

        public void Update(string fileName)
        {
            LoadForm(fileName);
        }

        public bool CanClose()
        {
            if (this.txtContent.IsEdited
                && Helper.MessageBoxQuestion(string.Format(Resource.unsavedFileMessage, Path.GetFileName(txtPath.TextValue))) == DialogResult.No)
            {
                return false;
            }

            return true;
        }

        #endregion

        private void FormTextFile_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtContent;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            FormMaster.Current.TabMaster.CloseTab(this);
        }
    }
}
