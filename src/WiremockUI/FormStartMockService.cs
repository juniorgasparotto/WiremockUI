using System;
using System.Windows.Forms;
using System.Diagnostics;
using WiremockUI.Data;

namespace WiremockUI
{
    public partial class FormStartMockService : Form
    {
        private RichTextBoxLogWriter logWriter;
        private FormMaster master;
        private Mock mockService;
        private Dashboard.PlayType playType;
        private Proxy proxy;

        public FormStartMockService(FormMaster master, Proxy proxy, Mock mock, Dashboard.PlayType playType)
        {
            InitializeComponent();
            this.logWriter = new RichTextBoxLogWriter(rtxtLog);
            this.logWriter.EnableAutoScroll = chkAutoScroll.Checked;
            this.logWriter.Enabled = !chkDisable.Checked;

            //Console.SetOut(logWriter);
            //TextWriterTraceListener myWriter = new TextWriterTraceListener(logWriter);
            //Debug.Listeners.Add(myWriter);

            this.master = master;
            this.proxy = proxy;
            this.mockService = mock;
            this.playType = playType;

            this.txtFrom.Text = proxy.UrlTarget;
            this.lblUrlTarget.Links.Add(0, proxy.UrlTarget.Length, proxy.UrlTarget);

            var urlProxy = proxy.GetUrlProxy();
            this.txtTo.Text = urlProxy;
            this.lblUrlProxy.Links.Add(0, urlProxy.Length, urlProxy);

            var folderPath = proxy.GetFullPath(mock);
            this.lblOpenFolder.Links.Add(0, folderPath.Length, folderPath);
        }

        public void Play()
        {
            master.Dashboard.Play(proxy, mockService, playType, logWriter);
        }

        private void lblUrlTarget_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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

        private void btnClean_Click(object sender, EventArgs e)
        {
            rtxtLog.Clear();
        }

        private void chkAutoScroll_CheckedChanged(object sender, EventArgs e)
        {
            this.logWriter.EnableAutoScroll = chkAutoScroll.Checked;
        }

        private void chkDisable_CheckedChanged(object sender, EventArgs e)
        {
            this.logWriter.Enabled = !chkDisable.Checked;
        }
    }
}
