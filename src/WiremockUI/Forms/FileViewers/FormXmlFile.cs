using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Text;

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
                txtContent.Text = content;
            }
        }

        private void Save()
        {
            try
            {
                File.WriteAllText(txtPath.Text, txtContent.Text);
                OnSave?.Invoke();
            }
            catch (Exception ex)
            {
                Helper.MessageBoxError("Ocorreu um erro ao tentar salvar o arquivo: " + ex.Message);
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
                txtContent.Text = Helper.FormatToXml(txtContent.Text);
            }
            catch(Exception ex)
            {
                Helper.MessageBoxError("Esse XML está inválido: " + ex.Message);
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
