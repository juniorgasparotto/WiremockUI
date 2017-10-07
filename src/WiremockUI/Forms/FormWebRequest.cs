using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WiremockUI.Languages;

namespace WiremockUI
{
    public partial class FormWebRequest : Form
    {
        public FormWebRequest()
        {
            InitializeComponent();
            txtUrl.Multiline = false;

            Text = Resource.formWebRequest;
            btnExecute.Text = Resource.btnExecute;
            tabRequestHeaders.Text = Resource.tabRequestHeaders;
            tabRequestBody.Text = Resource.tabRequestBody;
            tabRequestHeadersReal.Text = Resource.tabRequestHeadersReal;
            tabRequestOptions.Text = Resource.tabRequestOptions;
            tabResponseBody.Text = Resource.tabResponseBody;
            tabResponseHeaders.Text = Resource.tabResponseHeaders;
            stsTime.Text = Resource.stsTime;
            stsStatus.Text = Resource.stsStatus;
            lblTimeout.Text = Resource.lblTimeout;
            lblMiliseconds.Text = Resource.lblMiliseconds;
            chkAutoContentLength.Text = Resource.chkAutoContentLength;
            chk100Expect.Text = Resource.chk100Expect;
            chkAutoRedirect.Text = Resource.chkAutoRedirect;
        }

        public FormWebRequest(string method, string urlAbsolute, Dictionary<string, string> requestHeaders, string requestBody, Dictionary<string, string> responseHeaders, string responseBody)
            : this()
        {
            this.cmbVerb.Text = method;
            this.txtUrl.TextValue = urlAbsolute;
            this.txtRequestHeaders.TextValue = HttpUtils.GetHeadersAsString(requestHeaders);
            this.txtRequestBody.TextValue = Helper.ResolveBreakLineInCompatibility(requestBody);
            this.txtResponseHeaders.TextValue = HttpUtils.GetHeadersAsString(responseHeaders);
            this.txtResponseBody.TextValue = Helper.ResolveBreakLineInCompatibility(responseBody);
            if (string.IsNullOrWhiteSpace(requestBody) && !string.IsNullOrWhiteSpace(this.txtRequestHeaders.TextValue))
                tabRequest.SelectedTab = tabRequestHeaders;

            if (string.IsNullOrWhiteSpace(responseBody) && !string.IsNullOrWhiteSpace(this.txtResponseHeaders.TextValue))
                tabResponse.SelectedTab = tabResponseHeaders;

            var contentType = HttpUtils.GetHeaderValue(requestHeaders, "content-type");
            if (contentType != null)
            {
                if (contentType.Contains("xml"))
                    txtRequestBody.Language = AdvancedEditor.LanguageSupported.XML;
                else if (contentType.Contains("json"))
                    txtRequestBody.Language = AdvancedEditor.LanguageSupported.Json;
                else if (contentType.Contains("javascript"))
                    txtRequestBody.Language = AdvancedEditor.LanguageSupported.JS;
            }

            if (!string.IsNullOrWhiteSpace(this.txtRequestBody.TextValue))
                tabRequest.SelectedTab = tabRequestBody;
        }

        private async void WebRequest()
        {
            btnExecute.Enabled = false;
            CleanResponses();

            HttpWebRequest webRequest = null;
            var start = DateTime.Now;

            try
            {
                var headers = HttpUtils.GetHeaders(txtRequestHeaders.TextValue, false, false);
                webRequest = (HttpWebRequest)System.Net.WebRequest.Create(txtUrl.TextValue);
                webRequest.AllowAutoRedirect = this.chkAutoRedirect.Checked;
                webRequest.KeepAlive = this.chkKeepAlive.Checked;

                headers.Remove("method");
                headers.Remove("url");
                headers.Remove("protocol");
                headers.Remove("protocol-version");

                if (webRequest != null)
                {
                    webRequest.Method = cmbVerb.Text;
                    webRequest.Timeout = (int)txtTimeout.Value;
                    if (headers.Any(f => f.Key.ToLower() == "accept-encoding"))
                        webRequest.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

                    var contentTypeHeader = headers.FirstOrDefault(f => f.Key.ToLower() == "content-type");
                    if (contentTypeHeader.Key != null)
                        webRequest.ContentType = contentTypeHeader.Value;

                    webRequest.ServicePoint.Expect100Continue = chk100Expect.Checked;

                    // Obrigatory before body
                    foreach (var h in headers)
                    {
                        try
                        {
                            var nameLower = h.Key.ToLower();
                            var valueLower = h.Value.ToLower();
                            switch (nameLower)
                            {
                                case "host":
                                    break;
                                case "content-length":
                                    if (!this.chkAutoContentLength.Checked)
                                        webRequest.ContentLength = Convert.ToInt64(h.Value);
                                    break;
                                case "accept-encoding":
                                    break;
                                case "proxy-connection":
                                    break;
                                case "content-type":
                                    webRequest.ContentType = h.Value;
                                    break;
                                case "expect":
                                    if (h.Value?.ToLower() == "100-expect")
                                        webRequest.ServicePoint.Expect100Continue = true;
                                    break;
                                case "user-agent":
                                    webRequest.UserAgent = h.Value;
                                    break;
                                case "accept":
                                    webRequest.Accept = h.Value;
                                    break;
                                case "referer":
                                    webRequest.Referer = h.Value;
                                    break;
                                case "connection":
                                    if (valueLower == "keep-alive")
                                        webRequest.KeepAlive = true;
                                    break;
                                default:
                                    webRequest.Headers.Add(h.Key, h.Value);
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            Helper.MessageBoxError(ex.Message);
                        }
                    }

                    // Obrigatory after headers
                    if (!string.IsNullOrWhiteSpace(this.txtRequestBody.TextValue))
                    {
                        var data = Encoding.ASCII.GetBytes(this.txtRequestBody.TextValue);
                        if (this.chkAutoContentLength.Checked)
                            webRequest.ContentLength = data.Length;
                        var newStream = webRequest.GetRequestStream();
                        newStream.Write(data, 0, data.Length);
                        newStream.Close();
                    }

                    start = DateTime.Now;
                    txtRequestHeadersFinal.TextValue = HttpUtils.GetHeadersAsString(HttpUtils.GetHeaders(webRequest));
                    var response = await webRequest.GetResponseAsync();
                    ShowResponse(start, DateTime.Now, webRequest, (HttpWebResponse)response);
                }
            }
            catch(WebException ex)
            {
                if (ex.Response != null)
                    ShowResponse(start, DateTime.Now, webRequest, (HttpWebResponse)ex.Response);
                else
                    Helper.MessageBoxError(ex.Message);
                btnExecute.Enabled = true;
            }
            catch (Exception ex)
            {
                Helper.MessageBoxError(ex.Message);
                btnExecute.Enabled = true;
            }
        }

        private async void ShowResponse(DateTime t1, DateTime t2, HttpWebRequest request, HttpWebResponse response)
        {
            using (var s = response.GetResponseStream())
            {
                using (var sr = new StreamReader(s, Encoding.UTF8))
                {
                    var content = await sr.ReadToEndAsync();
                    txtResponseBody.TextValue = Helper.ResolveBreakLineInCompatibility(content);
                }
            }

            var headers = HttpUtils.GetHeaders(response);
            stsTimeValue.Text = (t2 - t1).ToString();
            stsStatusValue.Text = $"{(int)response.StatusCode} ({response.StatusDescription})";
            txtRequestHeadersFinal.TextValue = HttpUtils.GetHeadersAsString(headers);
            txtResponseHeaders.TextValue = $"{(int)response.StatusCode} {response.StatusDescription}\r\n";
            txtResponseHeaders.TextValue += HttpUtils.GetHeadersAsString(HttpUtils.GetHeaders(response));
            btnExecute.Enabled = true;

            var contentType = HttpUtils.GetHeaderValue(headers, "content-type");
            if (contentType != null)
            {
                if (contentType.Contains("xml"))
                    txtResponseBody.Language = AdvancedEditor.LanguageSupported.XML;
                else if (contentType.Contains("json"))
                    txtResponseBody.Language = AdvancedEditor.LanguageSupported.Json;
                else if (contentType.Contains("javascript"))
                    txtResponseBody.Language = AdvancedEditor.LanguageSupported.JS;
            }
        }

        private void CleanResponses()
        {
            txtResponseBody.TextValue = "";
            txtResponseHeaders.TextValue = "";
            stsStatusValue.Text = "-";
            stsTimeValue.Text = "-";
        }

        private void btnExecute_Click(object sender, System.EventArgs e)
        {
            WebRequest();
        }

        private void panel4_Resize(object sender, EventArgs e)
        {
            txtUrl.Width = panel4.Width;
        }

        private void txtUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                WebRequest();
        }

        private void FormWebRequest_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtUrl;
            SetFocus();
        }

        public async void SetFocus()
        {
            await Task.Delay(200);
            txtUrl.Focus();
        }
    }
}
