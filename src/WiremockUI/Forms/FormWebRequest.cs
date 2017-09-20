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
            this.txtRequestBody.EnableFormatter = true;
            this.txtResponseBody.EnableFormatter = true;

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
            this.txtUrl.Text = urlAbsolute;
            this.txtRequestHeaders.Text = HttpUtils.GetHeadersAsString(requestHeaders);
            this.txtRequestBody.Text = Helper.ResolveBreakLineInCompatibility(requestBody);
            this.txtResponseHeaders.Text = HttpUtils.GetHeadersAsString(responseHeaders);
            this.txtResponseBody.Text = Helper.ResolveBreakLineInCompatibility(responseBody);
            if (string.IsNullOrWhiteSpace(requestBody) && !string.IsNullOrWhiteSpace(this.txtRequestHeaders.Text))
                tabRequest.SelectedTab = tabRequestHeaders;

            if (string.IsNullOrWhiteSpace(responseBody) && !string.IsNullOrWhiteSpace(this.txtResponseHeaders.Text))
                tabResponse.SelectedTab = tabResponseHeaders;
        }

        private async void WebRequest()
        {
            btnExecute.Enabled = false;
            CleanResponses();

            HttpWebRequest webRequest = null;
            var start = DateTime.Now;

            try
            {
                var headers = HttpUtils.GetHeaders(txtRequestHeaders.Text, false, false);
                webRequest = (HttpWebRequest)System.Net.WebRequest.Create(txtUrl.Text);
                webRequest.AllowAutoRedirect = this.chkAutoRedirect.Checked;

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

                    if (!string.IsNullOrWhiteSpace(this.txtRequestBody.Text))
                    {
                        var data = Encoding.ASCII.GetBytes(this.txtRequestBody.Text);
                        if (this.chkAutoContentLength.Checked)
                            webRequest.ContentLength = data.Length;
                        var newStream = webRequest.GetRequestStream();
                        newStream.Write(data, 0, data.Length);
                        newStream.Close();
                    }

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

                    start = DateTime.Now;
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
                    txtResponseBody.Text = Helper.ResolveBreakLineInCompatibility(content);
                }
            }

            stsTimeValue.Text = (t2 - t1).ToString();
            stsStatusValue.Text = $"{(int)response.StatusCode} ({response.StatusDescription})";
            txtResponseHeadersFinal.Text = HttpUtils.GetHeadersAsString(HttpUtils.GetHeaders(request));
            txtResponseHeaders.Text = $"{(int)response.StatusCode} {response.StatusDescription}\r\n";
            txtResponseHeaders.Text += HttpUtils.GetHeadersAsString(HttpUtils.GetHeaders(response));
            btnExecute.Enabled = true;
        }

        private void CleanResponses()
        {
            txtResponseBody.Text = "";
            txtResponseHeaders.Text = "";
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
