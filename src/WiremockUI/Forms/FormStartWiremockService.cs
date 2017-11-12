using System;
using System.Windows.Forms;
using System.Diagnostics;
using WiremockUI.Data;
using System.IO;
using WiremockUI.Languages;
using java.security;

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
        private string labelDiff;

        private Server.WiremockProperties wiremockProperties;

        public RichTextBoxLogWriter LogWriter { get => logWriter; set => logWriter = value; }

        public FormStartWiremockService(FormMaster master, Server server, Scenario scenario, Server.PlayType playType)
        {
            InitializeComponent();

            lblUrlTarget.Text = Resource.lblUrlTarget;
            lblUrlServer.Text = Resource.lblUrlServer;
            linkUrlTarget.Text = Resource.viewLink;
            linkUrlServer.Text = Resource.viewLink;
            btnClean.Text = Resource.btnClean;
            linkOpenFolder.Text = Resource.linkOpenFolder;

            chkDisableTextLog.Text = Resource.chkDisableLogText;
            chkDisableTableLog.Text = Resource.chkDisableLogTable;

            chkAutoScroll.Text = Resource.chkAutoScroll;
            tabLogText.Text = Resource.tabLogText;
            tabLogTable.Text = Resource.tabLogTable;
            
            toolStripStatusLabelCount.Text = Resource.toolStripStatusLabelCount;
            toolStripStatusDiff.Text = Resource.toolStripStatusDiff;
            toolStripStatusValue.Text = Resource.toolStripStatusValue;

            this.wiremockProperties = server.GetWiremockProperties();
            chkDisableTextLog.Checked = !wiremockProperties.Verbose;

            // log text
            this.LogWriter = new RichTextBoxLogWriter(rtxtLog);
            this.LogWriter.EnableAutoScroll = chkAutoScroll.Checked;
            this.LogWriter.Enabled = !chkDisableTextLog.Checked;

            // need add this event after checked because the event is called when checkbox 
            // is checked in the code
            this.chkDisableTextLog.CheckedChanged += this.chkDisableTextLog_CheckedChanged;

            // log table
            this.logTable = new GridViewLogRequestResponse(gridLog, master, (row) =>
            {
                if (gridLog.Rows.Count == 0)
                    tabLogs.SelectedTab = tabLogTable;
                toolStripStatusCount.Text = row.Number.ToString();
            });

            this.logTable.EnableAutoScroll = chkAutoScroll.Checked;
            this.logTable.Enabled = !chkDisableTableLog.Checked;

            gridLog.Columns[gridLog.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.labelDiff = toolStripStatusValue.Text;

            this.master = master;
            this.server = server;
            this.scenario = scenario;
            this.playType = playType;
            
            if (!string.IsNullOrWhiteSpace(server.UrlTarget))
            {
                this.txtFrom.TextValue = server.UrlTarget;
                this.linkUrlTarget.Links.Add(0, server.UrlTarget.Length, server.UrlTarget);
            }
            else
            {
                this.linkUrlTarget.Visible = false;
            }

            var urlServer = server.GetServerUrl();
            this.txtTo.TextValue = urlServer;
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
            try
            {
                master.Dashboard.Play(server, scenario, playType, LogWriter, logTable);
            }
            catch (KeyStoreException ex)
            {

            }
        }
        
        private void lblUrlTarget_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(e.Link.LinkData.ToString());
            }
            catch (Exception ex)
            {
                Helper.MessageBoxError(ex.Message);
            }
        }

        private void lblUrlServer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(e.Link.LinkData.ToString());
            }
            catch (Exception ex)
            {
                Helper.MessageBoxError(ex.Message);
            }
        }

        private void lblOpenFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(e.Link.LinkData.ToString());
            }
            catch (Exception ex)
            {
                Helper.MessageBoxError(ex.Message);
            }
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
            this.LogWriter.EnableAutoScroll = chkAutoScroll.Checked;
            this.logTable.EnableAutoScroll = chkAutoScroll.Checked;
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

        private void chkDisableTextLog_CheckedChanged(object sender, EventArgs e)
        {
            this.LogWriter.Enabled = !chkDisableTextLog.Checked;

            this.wiremockProperties.Verbose = this.LogWriter.Enabled;
            server.Save(this.wiremockProperties);
        }

        private void chkDisableTableLog_CheckedChanged(object sender, EventArgs e)
        {
            this.logTable.Enabled = !chkDisableTableLog.Checked;
        }
    }
}
