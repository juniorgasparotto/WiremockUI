using System;
using System.IO;
using System.Windows.Forms;
using WiremockUI.Data;
using System.Linq;
using WiremockUI.Languages;

namespace WiremockUI
{
    public partial class FormServer : Form
    {
        private FormMaster master;
        private Server server;
        private string oldPath;
        private Server.WiremockProperties properties;

        public FormServer(FormMaster master, Server server)
        {
            this.master = master;
            this.server = server;
            InitializeComponent();

            Text = Resource.formServerTitle;
            lblServerNew.Text = Resource.lblServerNew;
            lblServerTargetUrl.Text = Resource.lblServerTargetUrl;
            lblServerPort.Text = Resource.lblServerPort;
            btnAdd.Text = Resource.btnAdd;
            btnCancel.Text = Resource.btnCancel;
            tabServerBasic.Text = Resource.tabServerBasic;
            tabServerAdvance.Text = Resource.tabServerAdvance;

            if (this.server == null)
            {
                this.txtPort.Text = Server.GetAutoPort().ToString();
                this.properties = new Server.WiremockProperties();
            }
            else
            {
                Text = Resource.formServerInEditModeTitle;

                this.txtName.Text = server.Name;
                this.txtUrlTarget.Text = server.UrlTarget;
                this.txtPort.Text = server.Port.ToString();
                this.btnAdd.Text = Resource.btnEdit;
                this.oldPath = this.server.GetFullPath();
                this.properties = server.GetWiremockProperties();

                if (master.Dashboard.IsRunning(server.GetDefaultScenario()))
                    btnAdd.Enabled = false;
            }

            this.propertyGrid1.SelectedObject = properties;
        }

        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var name = txtName.Text.Trim();
            var urlTarget = txtUrlTarget.Text.Trim();
            var port = txtPort.Text.Trim();
            var idExists = server?.Id;

            if (string.IsNullOrWhiteSpace(name))
            {
                Helper.MessageBoxError(Resource.addServerRequiredNameMessage);
                txtName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(urlTarget))
                urlTarget = null;

            if (string.IsNullOrWhiteSpace(port))
            {
                Helper.MessageBoxError(Resource.addServerRequiredPortMessage);
                txtPort.Focus();
                return;
            }

            if (!int.TryParse(port, out int portNumber))
            {
                Helper.MessageBoxError(Resource.addServerInvalidPortMessage);
                txtPort.Focus();
                return;
            }

            var db = new UnitOfWork();
            var existsName = (from s in db.Servers.AsQueryable()
                              where s.Name.ToLower() == name.ToLower() &&
                                    s.Id != idExists
                              select 1).Any();

            var existsPort = (from s in db.Servers.AsQueryable()
                              where s.Port == portNumber &&
                              s.Id != idExists
                              select 1).Any();

            if (existsName)
            {
                Helper.MessageBoxError(Resource.addServerDuplicateNameMessage);
                txtName.Focus();
                return;
            }

            if (existsPort)
            {
                Helper.MessageBoxError(Resource.addServerDuplicatePortMessage);
                txtPort.Focus();
                return;
            }

            if (server == null)
                server = new Server();

            server.Name = name;
            server.UrlTarget = urlTarget;
            server.Port = portNumber;

            var newPath = server.GetFullPath();
            if (!string.IsNullOrWhiteSpace(this.oldPath) && Directory.Exists(oldPath) && oldPath != newPath)
            {
                Directory.Move(this.oldPath, newPath);
            }

            server.Save(properties);
            this.Close();
            master.SetServer(server);
        }

        private void FormServer_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtName;
        }
    }
}
