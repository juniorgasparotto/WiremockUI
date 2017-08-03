using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace WiremockUI
{
    public partial class FormImageFile : Form
    {
        private object tabPage;
        private object master;

        public Action OnSave { get; set; }

        public FormImageFile(FormMaster master, TabPageCustom tabPage, string fileName)
        {
            InitializeComponent();

            this.tabPage = tabPage;
            this.master = master;

            if (fileName != null)
            {
                txtPath.Text = fileName;
                imgFile.ImageLocation = fileName;
            }
            else
            {
                txtPath.Enabled = false;
                btnOpen.Enabled = false;
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            Process.Start(txtPath.Text);
        }
    }
}
