using System.Drawing;
using System.Windows.Forms;
using WiremockUI.Languages;

namespace WiremockUI
{
    public partial class FormSslFix : Form
    {
        public enum UserFixOption
        {
            None,
            DisableSSLTrustStore,
            OpenSSLTrustStoreSettings,
            OpenServerSettings
        }

        public UserFixOption Result { get; private set; }

        public FormSslFix(System.Exception ex)
        {
            InitializeComponent();
            pictureBox1.Image = SystemIcons.Warning.ToBitmap();
            txtError.Text = Helper.GetExceptionDetails(ex);

            this.lblError.Text = Resource.fixSslLblError;
            this.groupFixOptions.Text = Resource.fixSslGroupFixOptions;
            this.optDisableCacerts.Text = Resource.fixSslOptDisableCacerts;
            this.optOpenSslSettings.Text = Resource.fixSslOptOpenSslSettings;
            this.optOpenServerSettings.Text = Resource.fixSslOptOpenServerSettings;
            this.btnFix.Text = Resource.fixSslBtnFix;
            this.btnCancel.Text = Resource.fixSslBtnCancel;
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void btnFix_Click(object sender, System.EventArgs e)
        {
            if (optDisableCacerts.Checked)
                Result = UserFixOption.DisableSSLTrustStore;
            else if (optOpenSslSettings.Checked)
                Result = UserFixOption.OpenSSLTrustStoreSettings;
            else if (optOpenServerSettings.Checked)
                Result = UserFixOption.OpenServerSettings;

            Close();
        }
    }
}
