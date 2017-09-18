using System;
using System.Windows.Forms;
using System.Diagnostics;
using WiremockUI.Data;
using System.IO;
using WiremockUI.Languages;

namespace WiremockUI
{
    public partial class FormStartWiremockService : Form
    {
        private RichTextBoxLogWriter logWriter;
        private GridViewLogRequestResponse logTable;
        private FormMaster master;
        private Scenario scenario;
        private Server.PlayType playType;
        private Server server;
        private int start;
        private int indexOfSearchText;
        private string labelDiff;

        public FormStartWiremockService(FormMaster master, Server server, Scenario scenario, Server.PlayType playType)
        {
            InitializeComponent();

            lblUrlTarget.Text = Resource.lblUrlTarget;
            lblUrlServer.Text = Resource.lblUrlServer;
            linkUrlTarget.Text = Resource.viewLink;
            linkUrlServer.Text = Resource.viewLink;
            btnClean.Text = Resource.btnClean;
            linkOpenFolder.Text = Resource.linkOpenFolder;
            chkDisable.Text = Resource.chkDisable;
            chkAutoScroll.Text = Resource.chkAutoScroll;
            tabLogText.Text = Resource.tabLogText;
            tabLogTable.Text = Resource.tabLogTable;
            btnSearch.Text = Resource.btnSearch;
            btnClose.Text = Resource.btnClose;
            toolStripStatusLabelCount.Text = Resource.toolStripStatusLabelCount;
            toolStripStatusDiff.Text = Resource.toolStripStatusDiff;
            toolStripStatusValue.Text = Resource.toolStripStatusValue;

            this.txtSearchValue.Multiline = false;

            // log text
            this.logWriter = new RichTextBoxLogWriter(rtxtLog);
            this.logWriter.EnableAutoScroll = chkAutoScroll.Checked;
            this.logWriter.Enabled = !chkDisable.Checked;

            // log table
            this.logTable = new GridViewLogRequestResponse(gridLog, master, (row) =>
            {
                if (gridLog.Rows.Count == 0)
                    tabLogs.SelectedTab = tabLogTable;
                toolStripStatusCount.Text = row.Number.ToString();
            });

            this.logTable.EnableAutoScroll = chkAutoScroll.Checked;
            this.logTable.Enabled = !chkDisable.Checked;

            gridLog.Columns[gridLog.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.labelDiff = toolStripStatusValue.Text;

            this.master = master;
            this.server = server;
            this.scenario = scenario;
            this.playType = playType;
            
            if (!string.IsNullOrWhiteSpace(server.UrlTarget))
            {
                this.txtFrom.Text = server.UrlTarget;
                this.linkUrlTarget.Links.Add(0, server.UrlTarget.Length, server.UrlTarget);
            }
            else
            {
                this.linkUrlTarget.Visible = false;
            }

            var urlServer = server.GetServerUrl();
            this.txtTo.Text = urlServer;
            this.linkUrlServer.Links.Add(0, urlServer.Length, urlServer);

            var folderPath = server.GetFullPath(scenario);
            if (scenario != null && File.Exists(folderPath))
            {
                this.linkOpenFolder.Links.Add(0, folderPath.Length, folderPath);
            }
            else
            {
                this.linkOpenFolder.Visible = false;
            }
        }
        
        public void Play()
        {
            master.Dashboard.Play(server, scenario, playType, logWriter, logTable);
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

        private void lblUrlServer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData.ToString());
        }

        private void lblOpenFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData.ToString());
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            if (tabLogs.SelectedTab == tabLogText)
                rtxtLog.Clear();
            else
                gridLog.Rows.Clear();
        }

        private void chkAutoScroll_CheckedChanged(object sender, EventArgs e)
        {
            this.logWriter.EnableAutoScroll = chkAutoScroll.Checked;
            this.logTable.EnableAutoScroll = chkAutoScroll.Checked;
        }

        private void chkDisable_CheckedChanged(object sender, EventArgs e)
        {
            this.logWriter.Enabled = !chkDisable.Checked;
            this.logTable.Enabled = !chkDisable.Checked;
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

        private void gridLog_SelectionChanged(object sender, EventArgs e)
        {
            toolStripStatusValue.Text = this.labelDiff;
            if (gridLog.SelectedCells.Count == 2)
            {
                var sel1 = gridLog.SelectedCells[0];
                var sel2 = gridLog.SelectedCells[1];
                if (sel1.Tag is TimeSpan t1 && sel2.Tag is TimeSpan t2)
                {
                    toolStripStatusValue.Text = (t1 - t2).ToString();
                }
            }
        }
    }
}
