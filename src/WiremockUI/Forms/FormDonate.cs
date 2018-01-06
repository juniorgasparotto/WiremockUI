using System;
using System.Windows.Forms;
using WiremockUI.Languages;

namespace WiremockUI
{
    public partial class FormDonate : Form
    {
        public FormDonate()
        {
            InitializeComponent();
            btnClose.Text = Resource.btnClose;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Helper.Process("https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=BJKBZXZJFVNUL&lc=US&item_name=wiremockui&item_number=wiremockui&currency_code=BRL&bn=PP%2dDonationsBF%3abtn_donate_LG%2egif%3aNonHosted");
        }
    }
}
