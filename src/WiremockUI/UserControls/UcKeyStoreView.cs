using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace WiremockUI
{
    public partial class UcKeyStoreView : UserControl
    {
        private string KeyStorePath
        {
            get
            {
                return this.txtKeyStoreFile.Text;
            }
            set
            {
                if (value == null)
                    panelSelectFile.Visible = false;
                else
                    panelSelectFile.Visible = true;

                this.txtKeyStoreFile.Text = value;
            }
        }

        public UcKeyStoreView()
        {
            InitializeComponent();
            this.KeyStorePath = null;
        }

        public void SetTrustStore(string trustStore)
        {
            gridCertificates.Rows.Clear();

            if (trustStore == SslHelper.GLOBAL)
            {
                var certificatesValidateds = SslHelper.CheckInvalidCertificateInCacertsKeyStore();
                KeyStorePath = null;
                Populate(certificatesValidateds);
            }
            else if (trustStore == SslHelper.NONE)
            {
                KeyStorePath = null;
            }
            else
            {
                KeyStorePath = trustStore;
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
    }
}
