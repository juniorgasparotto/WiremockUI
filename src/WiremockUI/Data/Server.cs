using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using WiremockUI.Languages;

namespace WiremockUI.Data
{
    public class Server
    {
        public Guid Id { get; set; }
        public string UrlTarget { get; set; }
        public int Port { get; set; }
        public string Name { get; set; }
        private List<Scenario> scenario = new List<Scenario>();

        public IEnumerable<Scenario> Scenarios => scenario;
        public IEnumerable<Argument> Arguments { get; set; }

        public string GetFormattedName()
        {
            if (!string.IsNullOrWhiteSpace(UrlTarget))
                return $"{Name} (http://localhost:{Port} <- {UrlTarget})";

            return $"{Name} (http://localhost:{Port})";
        }

        public string GetFolderName()
        {
            return Port.ToString();
        }

        public string GetServerUrl()
        {
            return $"http://localhost:{Port}";
        }

        public string GetFullPath()
        {
            return Path.Combine(Helper.GetBasePath(), GetFolderName());
        }

        public string GetFullPath(Scenario scenario)
        {
            return Path.Combine(GetFullPath(), scenario.GetFolderName());
        }

        public string GetMappingPath(Scenario scenario)
        {
            return Path.Combine(GetFullPath(scenario), "mappings");
        }

        public string GetBodyFilesPath(Scenario scenario)
        {
            return Path.Combine(GetFullPath(scenario), "__files");
        }

        public bool AlreadyRecord(Scenario scenario)
        {
            if (Directory.Exists(GetMappingPath(scenario)))
                return true;
            return false;
        }

        public Scenario GetDefaultScenario()
        {
            var d = scenario.FirstOrDefault(f => f.IsDefault);
            return d;
        }

        public void SetDefault(Scenario scenario)
        {
            scenario.IsDefault = true;
            foreach (var m in this.scenario)
                if (m.Id != scenario.Id)
                    m.IsDefault = false;
        }

        public Scenario GetScenarioById(Guid? id)
        {
            return scenario.FirstOrDefault(f => f.Id == id);
        }

        public void AddScenario(Scenario scenario)
        {
            if (scenario.Id == Guid.Empty)
                scenario.Id = Guid.NewGuid();

            if (this.scenario.Count == 0)
                scenario.IsDefault = true;
            this.scenario.Add(scenario);
        }

        public void RemoveScenario(Guid id)
        {
            scenario.RemoveAll(f => f.Id == id);
        }

        public string[] GetArguments(Scenario scenario, PlayType type, bool addQuoteInStrings = false)
        {
            string[] args;
            var relativeFolder = GetFullPath(scenario);

            if (type == PlayType.PlayAndRecord)
            {
                args = new string[]
                {
                    "--port", Port.ToString(),
                    "--proxy-all", Helper.AddQuote(UrlTarget, addQuoteInStrings),
                    "--record-mappings",
                    "--root-dir", Helper.AddQuote(relativeFolder, addQuoteInStrings)
                };
            }
            else if (type == PlayType.PlayAsProxy)
            {
                args = new string[]
                {
                    "--port", Port.ToString(),
                    "--proxy-all", Helper.AddQuote(UrlTarget, addQuoteInStrings),
                };
            }
            else
            {
                args = new string[]
                {
                    "--port", Port.ToString(),
                    "--root-dir",  Helper.AddQuote(relativeFolder, addQuoteInStrings)
                };
            }

            var argsAux = GetArguments();
            argsAux.InsertRange(0, args);
            return argsAux.ToArray();
        }

        public List<string> GetArguments(bool addQuoteInStrings = false)
        {
            var lst = new List<string>();
            foreach (var arg in Arguments)
            {
                if (!string.IsNullOrWhiteSpace(arg.Value))
                {
                    var type = arg.Type.ToLower();
                    if (type == "boolean" && bool.TryParse(arg.Value, out var b))
                    {
                        if (b)
                            lst.Add(arg.ArgName);
                    }
                    else
                    {
                        lst.Add(arg.ArgName);
                        if (arg.ArgName != arg.Value)
                        {
                            var value = arg.Value;
                            if (type == "string")
                                value = Helper.AddQuote(value, addQuoteInStrings);
                            lst.Add(value);
                        }
                    }
                }
            }
            return lst;
        }

        public Server Copy()
        {
            return new Server()
            {
                Id = Guid.NewGuid(),
                Arguments = Arguments,
                Name = Name,
                Port = GetAutoPort(),
                UrlTarget = UrlTarget
            };
        }

        public static int GetAutoPort()
        {
            var db = new UnitOfWork();
            var maxPort = db.Servers.AsQueryable().Select(f => f.Port).DefaultIfEmpty().Max();
            if (maxPort == 0)
                return 5500;
            return maxPort + 1;
        }

        public enum PlayType
        {
            Play,
            PlayAsProxy,
            PlayAndRecord
        }

        public class Argument
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public string ArgName { get; set; }
            public string Type { get; set; }
        }

        #region Save

        public void Save(WiremockProperties properties = null)
        {
            var db = new UnitOfWork();
            if (properties != null)
                Arguments = ToArguments(properties);

            if (Id == Guid.Empty)
            {
                AddScenario(new Scenario()
                {
                    IsDefault = true,
                    Name = Resource.defaultScenarioName
                });
                db.Servers.Insert(this);
            }
            else
            {
                db.Servers.Update(this);
            }

            db.Servers.Update(this);
            db.Save();
        }

        #endregion

        #region WiremockProperties

        public WiremockProperties GetWiremockProperties()
        {
            var properties = new WiremockProperties();
            if (Arguments != null)
            {
                var type = properties.GetType();
                foreach (var arg in Arguments)
                {
                    var property = type.GetProperty(arg.Name);
                    if (property == null)
                        continue;
                    var convertedValue = Convert.ChangeType(arg.Value, property.PropertyType);
                    property.SetValue(properties, convertedValue);
                }
            }
            return properties;
        }

        public static IEnumerable<Argument> ToArguments(WiremockProperties wiremockProperties)
        {
            var props = wiremockProperties.GetType().GetProperties();
            foreach (var prop in props)
            {
                var value = prop.GetValue(wiremockProperties);
                if (value != null && value.ToString() != "")
                {
                    var arg = new Argument()
                    {
                        Name = prop.Name,
                        Value = value.ToString(),
                        Type = prop.PropertyType.Name
                    };

                    var attrs = prop.GetCustomAttributes(true).Where(f => f is WiremockProperties.ArgumentAttribute).FirstOrDefault();
                    if (attrs is WiremockProperties.ArgumentAttribute argAttr)
                        arg.ArgName = argAttr.ArgName;

                    yield return arg;
                }
            }
        }

        public class WiremockProperties
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
            [DefaultValue(false)]
            public bool Verbose
            {
                get; set;
            } = false;

            [Description("When in record mode, capture request headers with the keys specified. See record-playback.")]
            [Category("Argumentos")]
            [Argument("--match-headers")]
            public string MatchHeaders
            {
                get; set;
            } = "Content-Type,SOAPAction";

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
            [DefaultValue(false)]
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
        #endregion
    }
}
