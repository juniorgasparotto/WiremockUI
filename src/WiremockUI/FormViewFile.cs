using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            txtPath.Text = fileName;
            var text = Helper.ResolveBreakLineIncompatibility(content);
            if (text != null)
                txtContent.AppendText(text);
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
    }
}
