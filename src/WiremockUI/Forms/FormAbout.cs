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
    }
}
