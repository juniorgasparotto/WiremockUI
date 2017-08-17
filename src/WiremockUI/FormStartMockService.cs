using System;
using System.Windows.Forms;
using System.Diagnostics;
using WiremockUI.Data;
using System.Drawing;

namespace WiremockUI
{
    public partial class FormStartMockService : Form
    {
        private RichTextBoxLogWriter logWriter;
        private FormMaster master;
        private Mock mockService;
        private Dashboard.PlayType playType;
        private Proxy proxy;
        private int start;
        private int indexOfSearchText;

        public FormStartMockService(FormMaster master, Proxy proxy, Mock mock, Dashboard.PlayType playType)
        {
            InitializeComponent();
            this.txtSearchValue.Multiline = false;

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

            if (mock != null)
            {
                var folderPath = proxy.GetFullPath(mock);
                this.lblOpenFolder.Links.Add(0, folderPath.Length, folderPath);
            }
            else
            {
                this.lblOpenFolder.Visible = false;
            }
        }

        public void Play()
        {
            master.Dashboard.Play(proxy, mockService, playType, logWriter);
        }

        private void Search()
        {
            int startindex = 0;

            if (txtSearchValue.Text.Length > 0)
            {
                startindex = FindMyText(txtSearchValue.Text.Trim(), start, rtxtLog.Text.Length);

                if (startindex == -1 && start >= 0) // Not found string and not searching from beginning
                {
                    // Wrap search
                    // int oldStart = start;
                    start = 0;
                    startindex = FindMyText(txtSearchValue.Text.Trim(), start, rtxtLog.Text.Length);
                }
            }

            // If string was found in the RichTextBox, highlight it
            if (startindex >= 0)
            {
                int endindex = txtSearchValue.Text.Length;
                rtxtLog.Select(startindex, endindex);
                start = startindex + endindex;
            }
        }

        public int FindMyText(string txtToSearch, int searchStart, int searchEnd)
        {
            // Unselect the previously searched string
            if (searchStart > 0 && searchEnd > 0 && indexOfSearchText >= 0)
            {
                rtxtLog.Undo();
            }

            // Set the return value to -1 by default.
            int retVal = -1;

            // A valid starting index should be specified.
            // if indexOfSearchText = -1, the end of search
            if (searchStart >= 0 && indexOfSearchText >= 0)
            {
                // A valid ending index
                if (searchEnd > searchStart || searchEnd == -1)
                {
                    // Find the position of search string in RichTextBox
                    indexOfSearchText = rtxtLog.Find(txtToSearch, searchStart, searchEnd, RichTextBoxFinds.None);
                    // Determine whether the text was found in richTextBox1.
                    if (indexOfSearchText != -1)
                    {
                        // Return the index to the specified search text.
                        retVal = indexOfSearchText;
                    }
                    else
                    {
                        start = 0;
                        indexOfSearchText = 0;
                    }
                }
            }
            return retVal;
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void txtSearchValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            pnlSearch.Visible = false;
            rtxtLog.DeselectAll();
        }

        private void FormStartMockService_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {
                pnlSearch.Visible = !pnlSearch.Visible;
                rtxtLog.DeselectAll();
                txtSearchValue.Focus();
            }
        }

    }
}
