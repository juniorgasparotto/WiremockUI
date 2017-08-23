using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace WiremockUI
{
    public partial class FormWebRequest : Form
    {
        public FormWebRequest()
        {
            InitializeComponent();
            txtUrl.Multiline = false;
        }

        public FormWebRequest(string urlAbsolute, Dictionary<string, string> requestHeaders, string requestBody, Dictionary<string, string> responseHeaders, string responseBody)
        {
            InitializeComponent();

            this.txtUrl.Text = urlAbsolute;
            this.txtRequestHeaders.Text = HttpUtils.GetHeadersAsString(requestHeaders);
            this.txtRequestBody.Text = requestBody;
            this.txtResponseHeaders.Text = HttpUtils.GetHeadersAsString(responseHeaders);
            this.txtResponseBody.Text = responseBody;
            if (string.IsNullOrWhiteSpace(requestBody) && !string.IsNullOrWhiteSpace(this.txtRequestHeaders.Text))
                tabRequest.SelectedTab = tabRequestHeaders;

            if (string.IsNullOrWhiteSpace(responseBody) && !string.IsNullOrWhiteSpace(this.txtResponseHeaders.Text))
                tabResponse.SelectedTab = tabResponseHeaders;
        }

        private async void WebRequest()
        {
            try
            {
                var headers = HttpUtils.GetHeaders(txtRequestHeaders.Text, false, false);
                var webRequest = (HttpWebRequest)System.Net.WebRequest.Create(txtUrl.Text);

                headers.Remove("method");
                headers.Remove("url");
                headers.Remove("protocol");
                headers.Remove("protocol-version");

                if (webRequest != null)
                {
                    webRequest.Method = cmbVerb.Text;
                    webRequest.Timeout = (int)txtTimeout.Value;
                    webRequest.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

                    foreach (var h in headers)
                    {
                        var valueLower = h.Key.ToLower();
                        switch (valueLower)
                        {
                            case "host":
                                continue;
                            case "content-type":
                                webRequest.ContentType = h.Value;
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

                    var newHeaders = HttpUtils.GetHeaders(webRequest);
                    foreach(var h in newHeaders)
                    {
                        if (!headers.Keys.Any(f => f.ToLower() == h.Key.ToLower()))
                            headers.Add(h.Key, h.Value);
                    }

                    txtRequestHeaders.Text = HttpUtils.GetHeadersAsString(headers);
                    var response = await webRequest.GetResponseAsync();
                    ShowResponse((HttpWebResponse)response);
                }
            }
            catch(WebException ex)
            {
                if (ex.Response != null)
                    ShowResponse((HttpWebResponse)ex.Response);
                else
                    Helper.MessageBoxError(ex.Message);
            }
            catch (Exception ex)
            {
                Helper.MessageBoxError(ex.Message);
            }
            finally
            {
                btnExecute.Enabled = true;
            }
        }

        private async void ShowResponse(HttpWebResponse response)
        {
            txtResponseBody.Text = "";
            txtResponseHeaders.Text = "";

            using (var s = response.GetResponseStream())
            {
                using (var sr = new StreamReader(s, Encoding.UTF8))
                {
                    var content = await sr.ReadToEndAsync();
                    txtResponseBody.Text = Helper.ResolveBreakLineInCompatibility(content);
                }
            }

            txtResponseHeaders.Text = $"{(int)response.StatusCode} {response.StatusDescription}\r\n";
            txtResponseHeaders.Text += HttpUtils.GetHeadersAsString(HttpUtils.GetHeaders(response));
        }

        private void btnExecute_Click(object sender, System.EventArgs e)
        {
            btnExecute.Enabled = false;
            txtResponseBody.Text = "";
            txtResponseHeaders.Text = "";
            WebRequest();
        }

        private void btnExecute_Click_1(object sender, EventArgs e)
        {

        }

        private void panel4_Resize(object sender, EventArgs e)
        {
            txtUrl.Width = panel4.Width;
        }
    }
}
