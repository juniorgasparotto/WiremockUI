using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WiremockUI
{
    public partial class FormComposer : Form
    {
        private Dictionary<string, string> requestHeaders;
        private string requestBody;
        private Dictionary<string, string> responseHeaders;
        private string responseBody;

        public FormComposer()
        {
            InitializeComponent();
        }

        public FormComposer(string urlAbsolute, Dictionary<string, string> requestHeaders, string requestBody, Dictionary<string, string> responseHeaders, string responseBody)
        {
            InitializeComponent();

            this.txtUrl.Text = urlAbsolute;
            this.txtRequestHeaders.Text = GetHeaders(requestHeaders);
            this.txtRequestBody.Text = requestBody;
            this.txtResponseHeaders.Text = GetHeaders(responseHeaders);
            this.txtResponseBody.Text = responseBody;
        }

        private string GetHeaders(Dictionary<string, string> dic)
        {
            return string.Join("\r\n", dic.Select(x => x.Key + ": " + x.Value).ToArray());
        }
    }
}
