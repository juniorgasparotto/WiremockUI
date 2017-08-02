using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WiremockUI
{
    public partial class FormTextFile : Form
    {
        private object tabPage;
        private object master;

        public Action OnSave { get; set; }

        public FormTextFile(FormMaster master, TabPageCustom tabPage, string fileName)
        {
            InitializeComponent();

            this.TabStop = false;

            var content = "";
            this.tabPage = tabPage;
            this.master = master;

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

            var text = Helper.ResolveBreakLineIncompatibility(content);
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
    }
}
