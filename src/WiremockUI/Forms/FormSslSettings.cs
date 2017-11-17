using System;
using System.Windows.Forms;
using WiremockUI.Data;
using WiremockUI.Languages;

namespace WiremockUI
{
    public partial class frmSslSettings : Form
    {
        private FormMaster master;
        private Settings settings;

        public frmSslSettings(FormMaster master)
        {
            this.master = master;
            InitializeComponent();

            Text = Resource.certificatesFrmSslSettings;
            btnSave.Text = Resource.certificatesBtnSave;
            btnCancel.Text = Resource.certificatesBtnCancel;
            groupOptions.Text = Resource.certificatesGroupOptions;
            optCacerts.Text = Resource.certificatesOptCacerts;
            optSslEmptyStore.Text = Resource.certificatesOptSslEmptyStore;
            optOther.Text = Resource.certificatesOptOther;

            this.settings = SettingsUtils.GetSettings();

            if (settings.TrustStoreDefault == SslHelper.GLOBAL)
                optCacerts.Checked = true;
            else if (settings.TrustStoreDefault == SslHelper.NONE)
                optSslEmptyStore.Checked = true;
            else
                optOther.Checked = true;

            ucKeyStoreView1.SetTrustStore(settings.TrustStoreDefault, this.settings.TrustStorePwdDefault);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (optCacerts.Checked)
            {
                this.settings.TrustStoreDefault = SslHelper.GLOBAL;
                this.settings.TrustStorePwdDefault = null;
            }
            else if (optSslEmptyStore.Checked)
            {
                this.settings.TrustStoreDefault = SslHelper.NONE;
                this.settings.TrustStorePwdDefault = null;
            }
            else if (optOther.Checked)
            {
                this.settings.TrustStoreDefault = ucKeyStoreView1.KeyStorePath;
                this.settings.TrustStorePwdDefault = ucKeyStoreView1.Pwd;
            }

            SettingsUtils.SaveSettings(this.settings);
            SslHelper.SetTrustStore();
            this.Close();
        }
    }
}
