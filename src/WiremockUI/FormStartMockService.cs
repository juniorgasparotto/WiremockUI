using com.github.tomakehurst.wiremock.standalone;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Text;
using System.Diagnostics;
using WiremockUI.Data;

namespace WiremockUI
{
    public partial class FormStartMockService : Form
    {
        delegate void SetTextCallback(string text);
        private TextBoxWriter log;
        private FormMaster master;
        private Mock mockService;
        private bool record;
        private Proxy proxy;

        public FormStartMockService(FormMaster master, Proxy proxy, Mock mock, bool record)
        {
            InitializeComponent();

            this.log = new TextBoxWriter(txtLog);
            Console.SetOut(log);
            TextWriterTraceListener myWriter = new TextWriterTraceListener(log);
            Debug.Listeners.Add(myWriter);
            this.master = master;
            this.proxy = proxy;
            this.mockService = mock;
            this.record = record;

            this.lblUrlOriginal.Text = proxy.UrlOriginal;
            this.lblUrlOriginal.Links.Add(0, this.lblUrlOriginal.Text.Length, this.lblUrlOriginal.Text);

            this.lblUrlProxy.Text = proxy.GetUrlProxy();
            this.lblUrlProxy.Links.Add(0, this.lblUrlProxy.Text.Length, this.lblUrlProxy.Text);

            var folderPath = proxy.GetFullPath(mock);
            this.lblOpenFolder.Links.Add(0, folderPath.Length, folderPath);
        }

        public void Play()
        {
            master.Dashboard.Play(proxy, mockService, record, log);
        }

        public class TextBoxWriter : TextWriter
        {
            TextBox _output = null;

            public TextBoxWriter(TextBox output)
            {
                _output = output;
            }

            public override void Write(string value)
            {
                if (_output.InvokeRequired)
                {
                    SetTextCallback d = new SetTextCallback(f => _output.AppendText(f));
                    _output.Invoke(d, new object[] { value });
                }
                else
                {
                    this._output.AppendText(value);
                }
            }

            public override void Write(char[] buffer, int index, int count)
            {
                base.Write(buffer, index, count);
            }

            public override void Write(char value)
            {
                base.Write(value);
            }

            public override Encoding Encoding
            {
                get { return System.Text.Encoding.UTF8; }
            }
        }

        private void lblUrlOriginal_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData.ToString());
        }

        private void lblUrlProxy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData.ToString());
        }

        private void lblOpenFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData.ToString());
        }

        private void lblOpenFolder_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData.ToString());
        }

    }
}
