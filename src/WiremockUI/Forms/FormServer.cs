using System;
using System.IO;
using System.Windows.Forms;
using WiremockUI.Data;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using WiremockUI.Languages;

namespace WiremockUI
{
    public partial class FormServer : Form
    {
        private FormMaster master;
        private Server server;
        private string oldPath;
        private WMProperties properties;

        public FormServer(FormMaster master, Server server)
        {
            this.master = master;
            this.server = server;
            InitializeComponent();

            Text = Resource.formServerTitle;
            lblServerNew.Text = Resource.lblServerNew;
            lblServerTargetUrl.Text = Resource.lblServerTargetUrl;
            lblServerPort.Text = Resource.lblServerPort;
            btnAdd.Text = Resource.btnAdd;
            btnCancel.Text = Resource.btnCancel;
            tabServerBasic.Text = Resource.tabServerBasic;
            tabServerAdvance.Text = Resource.tabServerAdvance;

            this.properties = new WMProperties();

            if (this.server == null)
            {
                this.txtPort.Text = GetAutoPort().ToString();
            }
            else
            {
                Text = Resource.formServerInEditModeTitle;

                this.txtName.Text = server.Name;
                this.txtUrlTarget.Text = server.UrlTarget;
                this.txtPort.Text = server.Port.ToString();
                this.btnAdd.Text = Resource.btnEdit;
                this.oldPath = this.server.GetFullPath();
                SetProperties(server);

                if (master.Dashboard.IsRunning(server.GetDefaultScenario()))
                    btnAdd.Enabled = false;
            }

            this.propertyGrid1.SelectedObject = properties;
        }

        private void SetProperties(Server server)
        {
            var type = properties.GetType();
            if (server.Arguments == null)
                return;

            foreach (var arg in server.Arguments)
            {
                var property = type.GetProperty(arg.Name);
                if (property == null)
                    continue;
                var convertedValue = Convert.ChangeType(arg.Value, property.PropertyType);
                property.SetValue(properties, convertedValue);
            }
        }

        private IEnumerable<Server.Argument> GetProperties()
        {
            var props = properties.GetType().GetProperties();
            foreach (var prop in props)
            {
                var value = prop.GetValue(properties);
                if (value != null && value.ToString() != "")
                {
                    var arg = new Server.Argument()
                    {
                        Name = prop.Name,
                        Value = value.ToString(),
                        Type = prop.PropertyType.Name
                    };

                    var attrs = prop.GetCustomAttributes(true).Where(f => f is WMProperties.ArgumentAttribute).FirstOrDefault();
                    if (attrs is WMProperties.ArgumentAttribute argAttr)
                        arg.ArgName = argAttr.ArgName;

                    yield return arg;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var name = txtName.Text.Trim();
            var urlTarget = txtUrlTarget.Text.Trim();
            var port = txtPort.Text.Trim();
            var idExists = server?.Id;

            if (string.IsNullOrWhiteSpace(name))
            {
                Helper.MessageBoxError(Resource.addServerRequiredNameMessage);
                txtName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(urlTarget))
            {
                urlTarget = null;
                //Helper.MessageBoxError(Resource.addServerRequiredUrlTargetMessage);
                //txtUrlTarget.Focus();
                //return;
            }

            if (string.IsNullOrWhiteSpace(port))
            {
                Helper.MessageBoxError(Resource.addServerRequiredPortMessage);
                txtPort.Focus();
                return;
            }

            if (!int.TryParse(port, out int portNumber))
            {
                Helper.MessageBoxError(Resource.addServerInvalidPortMessage);
                txtPort.Focus();
                return;
            }

            var db = new UnitOfWork();
            var existsName = (from s in db.Servers.AsQueryable()
                              where s.Name.ToLower() == name.ToLower() &&
                                    s.Id != idExists
                              select 1).Any();

            var existsPort = (from s in db.Servers.AsQueryable()
                              where s.Port == portNumber &&
                              s.Id != idExists
                              select 1).Any();

            if (existsName)
            {
                Helper.MessageBoxError(Resource.addServerDuplicateNameMessage);
                txtName.Focus();
                return;
            }

            if (existsPort)
            {
                Helper.MessageBoxError(Resource.addServerDuplicatePortMessage);
                txtPort.Focus();
                return;
            }

            if (server == null)
                server = new Server();

            server.Name = name;
            server.UrlTarget = urlTarget;
            server.Port = portNumber;
            server.Arguments = GetProperties();

            var newPath = server.GetFullPath();

            if (!string.IsNullOrWhiteSpace(this.oldPath) && Directory.Exists(oldPath) && oldPath != newPath)
            {
                Directory.Move(this.oldPath, newPath);
            }

            if (server.Id == Guid.Empty)
                db.Servers.Insert(server);
            else
                db.Servers.Update(server);

            db.Servers.Update(server);
            db.Save();
            this.Close();

            master.SetServer(server);
        }

        public int GetAutoPort()
        {
            var db = new UnitOfWork();
            var maxPort = db.Servers.AsQueryable().Select(f => f.Port).DefaultIfEmpty().Max();
            if (maxPort == 0)
                return 5500;
            return maxPort + 1;
        }

        public class WMProperties
        {
            [Description("If specified, enables HTTPS on the supplied port.")]
            [Category("Argumentos")]
            [Argument("--https-port")]
            public string HttpsPort
            {
                get; set;
            }

            [Description("The IP address the WireMock server should serve from. Binds to all local network adapters if unspecified.")]
            [Category("Argumentos")]
            [Argument("--bind-address")]
            public string BindAddress
            {
                get; set;
            }

            [Description("Path to a keystore file containing an SSL certificate to use with HTTPS. The keystore must have a password of “password”. This option will only work if --https-port is specified. If this option isn’t used WireMock will default to its own self-signed certificate.")]
            [Category("Argumentos")]
            [Argument("--https-keystore")]
            public string HttpsKeyStore
            {
                get; set;
            }

            [Description("Password to the keystore, if something other than 'password'.")]
            [Category("Argumentos")]
            [Argument("--keystore-password")]
            public string KeyStorePassoword
            {
                get; set;
            }

            [Description("Path to a keystore file containing client certificates. See https and proxy-client-certs for details.")]
            [Category("Argumentos")]
            [Argument("--https-truststore")]
            public string HttpsTrustStore
            {
                get; set;
            }

            [Description("Optional password to the trust store. Defaults to 'password' if not specified.")]
            [Category("Argumentos")]
            [Argument("--truststore-password")]
            public string TrustStorePassword
            {
                get; set;
            }

            [Description("Force clients to authenticate with a client certificate. See https for details.")]
            [Category("Argumentos")]
            [Argument("--https-require-client-cert")]
            public string HttpsRequireClientCert
            {
                get; set;
            }

            [Description("Turn on verbose logging to stdout")]
            [Category("Argumentos")]
            [Argument("--verbose")]
            [DefaultValue(true)]
            public bool Verbose
            {
                get; set;
            } = true;

            [Description("When in record mode, capture request headers with the keys specified. See record-playback.")]
            [Category("Argumentos")]
            [Argument("--match-headers")]
            public string MatchHeaders
            {
                get; set;
            }

            //[Description("Proxy all requests through to another base URL e.g. --proxy-all='http://api.someservice.com' Typically used in conjunction with --record-mappings such that a session on another service can be recorded.")]
            //[Category("Argumentos")]
            //[Argument("--proxy-all")]
            //public string ProxyAll
            //{
            //    get; set;
            //}

            [Description("When in proxy mode, it passes the Host header as it comes from the client through to the proxied service. When this option is not present, the Host header value is deducted from the proxy URL. This option is only available if the --proxy-all option is specified.")]
            [Category("Argumentos")]
            [Argument("--preserve-host-header")]
            public string PreserveHostHeader
            {
                get; set;
            }

            [Description("When proxying requests (either by using –proxy-all or by creating stub mappings that proxy to other hosts), route via another proxy server (useful when inside a corporate network that only permits internet access via an opaque proxy). e.g. --proxy-via webproxy.mycorp.com (defaults to port 80) or --proxy-via webproxy.mycorp.com:8080")]
            [Category("Argumentos")]
            [Argument("--proxy-via")]
            public string ProxyVia
            {
                get; set;
            }

            [Description("Run as a browser proxy. See browser-proxying.")]
            [Category("Argumentos")]
            [Argument("--enable-browser-proxying")]
            public string EnableBrowserProxying
            {
                get; set;
            }

            [Description("Disable the request journal, which records incoming requests for later verification. This allows WireMock to be run (and serve stubs) for long periods (without resetting) without exhausting the heap. The --record-mappings option isn’t available if this one is specified.")]
            [Category("Argumentos")]
            [Argument("--no-request-journal")]
            public string NoRequestJournal
            {
                get; set;
            }

            [Description("The number of threads created for incoming requests. Defaults to 10.")]
            [Category("Argumentos")]
            [Argument("--container-threads")]
            public string ContainerThreads
            {
                get; set;
            }

            [Description("Set maximum number of entries in request journal (if enabled). When this limit is reached oldest entries will be discarded.")]
            [Category("Argumentos")]
            [Argument("--max-request-journal-entries")]
            public string MaxRequestJournalEntries
            {
                get; set;
            }

            [Description("The number of threads Jetty uses for accepting requests.")]
            [Category("Argumentos")]
            [Argument("--jetty-acceptor-threads")]
            public string JettyAcceptorThreads
            {
                get; set;
            }

            [Description("The Jetty queue size for accepted requests.")]
            [Category("Argumentos")]
            [Argument("--jetty-accept-queue-size")]
            public string JettyAcceptorQueueSize
            {
                get; set;
            }

            [Description("The Jetty buffer size for request headers, e.g. --jetty-header-buffer-size 16384, defaults to 8192K.")]
            [Category("Argumentos")]
            [Argument("--jetty-header-buffer-size")]
            public string JettyHeaderBufferSize
            {
                get; set;
            }

            [Description("Extension class names e.g. com.mycorp.HeaderTransformer,com.mycorp.BodyTransformer. See extending-wiremock.")]
            [Category("Argumentos")]
            [Argument("--extensions")]
            public string Extensions
            {
                get; set;
            }

            [Description("Print all raw incoming and outgoing network traffic to console.")]
            [Category("Argumentos")]
            [Argument("--print-all-network-traffic")]
            public bool PrintAllNetworkTraffic
            {
                get; set;
            }

            [Description("Render all response definitions using Handlebars templates. --local-response-templating: Enable rendering of response definitions using Handlebars templates for specific stub mappings.")]
            [Category("Argumentos")]
            [Argument("--global-response-templating")]
            public string GlobalResponseTemplating
            {
                get; set;
            }

            [Description("Show command line help")]
            [Category("Argumentos")]
            [Argument("--help")]
            public string Help
            {
                get; set;
            }

            public class ArgumentAttribute : Attribute
            {
                public string ArgName { get; set; }

                public ArgumentAttribute(string argName)
                {
                    this.ArgName = argName;
                }
            }
        }

        private void FormServer_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtName;
        }
    }
}
