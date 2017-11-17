using System;
using System.Windows.Forms;
using WiremockUI.Data;
using WiremockUI.Languages;

namespace WiremockUI
{
    public partial class frmSslSettings : Form
    {
        private FormMaster master;

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

            var settings = SettingsUtils.GetSettings();

            if (settings.TrustStoreDefault == SslHelper.GLOBAL)
                optCacerts.Checked = true;
            else if (settings.TrustStoreDefault == SslHelper.NONE)
                optSslEmptyStore.Checked = true;
            else
                optOther.Checked = true;

            ucKeyStoreView1.SetTrustStore(settings.TrustStoreDefault, settings.TrustStorePwdDefault);
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
                SslHelper.UseTrustStoreCacerts();
            }
            else if (optSslEmptyStore.Checked)
            {
                SslHelper.UseTrustStoreEmpty();
            }
            else if (optOther.Checked)
            {
                SslHelper.SaveTrustStore(ucKeyStoreView1.KeyStorePath, ucKeyStoreView1.Pwd);
            }

            this.Close();
        }

        private void frmSslSettings_Shown(object sender, EventArgs e)
        {
            ucKeyStoreView1.ClearSelection();
        }
    }
}
