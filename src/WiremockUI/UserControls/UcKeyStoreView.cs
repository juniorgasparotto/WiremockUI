using java.io;
using java.security;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using WiremockUI.Languages;

namespace WiremockUI
{
    public partial class UcKeyStoreView : UserControl
    {
        public string KeyStorePath
        {
            get
            {
                return this.txtKeyStoreFile.Text;
            }
            private set
            {
                if (value == null)
                    panelSelectFile.Visible = false;
                else
                    panelSelectFile.Visible = true;

                this.txtKeyStoreFile.Text = value;
            }
        }

        public string Pwd
        {
            get
            {
                return this.txtPwd.Text;
            }
            private set
            {
                this.txtPwd.Text = value;
            }
        }

        public UcKeyStoreView()
        {
            InitializeComponent();
            this.KeyStorePath = null;
            groupSelectFile.Text = Resource.certificatesGroupSelectFile;
            lblFile.Text = Resource.certificatesLblFile;
            lblPwd.Text = Resource.certificatesLblPwd;
            btnOpen.Text = Resource.certificatesBtnOpen;
            btnLoad.Text = Resource.certificatesBtnLoad;
            groupCertificates.Text = Resource.certificatesGroupCertificates;
        }

        public void SetTrustStore(string trustStore, string pwd = null)
        {
            gridCertificates.Rows.Clear();

            if (trustStore == SslHelper.GLOBAL)
            {
                var certificatesValidateds = SslHelper.ValidateCacerts();
                KeyStorePath = null;
                txtPwd.Text = "";
                Populate(certificatesValidateds);
            }
            else if (trustStore == SslHelper.NONE)
            {
                KeyStorePath = null;
                txtPwd.Text = "";
            }
            else
            {
                KeyStorePath = trustStore;
                txtPwd.Text = pwd;
            }
        }

        private void Populate(Dictionary<X509Certificate2, Exception> certificatesValidateds)
        {
            if (certificatesValidateds != null)
            {
                foreach (var c in certificatesValidateds.OrderBy(f => f.Value == null))
                {
                    var isValid = (c.Value == null ? true : false);
                    gridCertificates.Rows.Add(c.Key.Subject, isValid, Helper.GetExceptionDetails(c.Value));

                    var last = gridCertificates.Rows.Count - 1;
                    if (!isValid)
                        gridCertificates.Rows[last].DefaultCellStyle.ForeColor = Color.Red;
                }
            }
        }

        internal void ClearSelection()
        {
            gridCertificates.ClearSelection();
        }

        private void Populate(Dictionary<java.security.cert.Certificate, Exception> certificatesValidateds)
        {
            if (certificatesValidateds != null)
            {
                foreach (var c in certificatesValidateds.OrderBy(f => f.Value == null))
                {
                    var isValid = (c.Value == null ? true : false);
                    gridCertificates.Rows.Add(c.Key, isValid, Helper.GetExceptionDetails(c.Value));

                    var last = gridCertificates.Rows.Count - 1;
                    if (!isValid)
                        gridCertificates.Rows[last].DefaultCellStyle.ForeColor = Color.Red;
                }
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            fileOpen.ShowDialog();
        }

        private void fileOpen_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.txtKeyStoreFile.Text = fileOpen.FileName;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                gridCertificates.Rows.Clear();
                var certificatesValidateds = new Dictionary<java.security.cert.Certificate, Exception>();
                var file = this.txtKeyStoreFile.Text;
                KeyStore ks = KeyStore.getInstance(KeyStore.getDefaultType());
                var instream = new FileInputStream(file);
                ks.load(instream, txtPwd.Text?.ToCharArray());
                var en = ks.aliases();

                while (en.hasMoreElements())
                {
                    var aliasKey = (String)en.nextElement();
                    var c = ks.getCertificate(aliasKey);
                    certificatesValidateds.Add(c, null);
                }
                Populate(certificatesValidateds);
            }
            catch (Exception ex)
            {
                Helper.MessageBoxError(ex.Message);
            }
        }
    }
}
