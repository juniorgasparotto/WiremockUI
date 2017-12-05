using System;
using System.Diagnostics;
using System.Windows.Forms;
using WiremockUI.Languages;

namespace WiremockUI
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
            txtAboutText.Text = Resource.txtAboutText;
            lblEmail.Text = Resource.email;
            lblUrl.Text = Resource.gitHubProjectUrl;
            btnClose.Text = Resource.btnClose;
            var versions = Helper.GetBuildVersion();
            lblVersion.Text = versions.InformationalVersion.ToString();
            lblBuild.Text = $"{versions.Version.Build}.{versions.Version.Revision}";
            lblDate.Text = versions.BuildDate.ToString();
        }

        private void lblEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("mail://" + lblEmail.Text);
        }

        private void lblUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(lblUrl.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            var form = new FormDevTools();
            form.ShowDialog();
        }
    }
}
