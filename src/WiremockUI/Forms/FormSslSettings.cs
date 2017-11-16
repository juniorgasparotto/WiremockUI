using System;
using System.Windows.Forms;
using WiremockUI.Data;
using WiremockUI.Languages;

namespace WiremockUI
{
    public partial class FormSslSettings : Form
    {
        private FormMaster master;
        private Settings settings;

        public FormSslSettings(FormMaster master)
        {
            this.master = master;
            InitializeComponent();

            Text = Resource.formServerTitle;
            btnSave.Text = Resource.btnSaveFile;
            btnCancel.Text = Resource.btnCancel;

            this.settings = SettingsUtils.GetSettings();
            ucKeyStoreView1.SetTrustStore(settings.TrustStoreDefault);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormServer_Load(object sender, EventArgs e)
        {
            //this.ActiveControl = txtName;
        }

        private void optSslEmptyStore_CheckedChanged(object sender, EventArgs e)
        {
            if (optSslEmptyStore.Checked)
            {
                ucKeyStoreView1.SetTrustStore(SslHelper.NONE);
            }
        }

        private void optCacerts_CheckedChanged(object sender, EventArgs e)
        {
            if (optCacerts.Checked)
            {
                ucKeyStoreView1.SetTrustStore(SslHelper.GLOBAL);
            }
        }

        private void optOther_CheckedChanged(object sender, EventArgs e)
        {
            if (optOther.Checked)
            {
                ucKeyStoreView1.SetTrustStore("");
            }
        }
    }
}
