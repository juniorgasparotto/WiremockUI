using System;
using System.IO;
using System.Windows.Forms;
using WiremockUI.Data;
using System.Linq;

namespace WiremockUI
{
    public partial class FormAddProxy : Form
    {
        private FormMaster master;
        private Proxy proxy;
        private string oldPath;

        public FormAddProxy(FormMaster master, Proxy proxy)
        {
            this.master = master;
            this.proxy = proxy;
            InitializeComponent();
            
            if (this.proxy == null)
            {
                this.txtMockPort.Text = GetAutoPort().ToString();
            }
            else
            {
                this.txtName.Text = proxy.Name;
                this.txtUrlOriginal.Text = proxy.UrlOriginal;
                this.txtMockPort.Text = proxy.PortProxy.ToString();
                this.txtArguments = proxy.Arguments;
                this.btnAdd.Text = "Editar";
                this.oldPath = this.proxy.GetFullPath();

                if (master.Dashboard.IsRunning(proxy.GetDefaultMock()))
                    btnAdd.Enabled = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var name = txtName.Text.Trim();
            var urlOriginal = txtUrlOriginal.Text.Trim();
            var port = txtMockPort.Text.Trim();
            var idExists = proxy?.Id;

            if (string.IsNullOrWhiteSpace(name))
            {
                Helper.MessageBoxError("O campo 'Nome' é obrigatório.");
                txtName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(urlOriginal))
            {
                Helper.MessageBoxError("O campo 'Url Original' é obrigatório.");
                txtUrlOriginal.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(port))
            {
                Helper.MessageBoxError("O campo 'Porta' é obrigatório.");
                txtMockPort.Focus();
                return;
            }

            if (!int.TryParse(port, out int portNumber))
            {
                Helper.MessageBoxError("O campo 'Porta' está inválido.");
                txtMockPort.Focus();
                return;
            }

            var db = new UnitOfWork();
            var existsName = (from s in db.Proxies.AsQueryable()
                              where s.Name.ToLower() == name.ToLower() &&
                                    s.Id != idExists
                              select 1).Any();

            //var existsUrlOriginal = (from s in db.Proxies.AsQueryable()
            //                         where s.UrlOriginal.ToLower() == urlOriginal.ToLower() &&
            //                         s.Id != idExists
            //                         select 1).Any();

            var existsPort = (from s in db.Proxies.AsQueryable()
                              where s.PortProxy == portNumber &&
                              s.Id != idExists
                              select 1).Any();

            if (existsName)
            {
                Helper.MessageBoxError("Esse 'Nome' já está em uso");
                txtName.Focus();
                return;
            }

            //if (existsUrlOriginal)
            //{
            //    Helper.MessageBoxError("Esse 'Url original' já está em uso");
            //    txtUrlOriginal.Focus();
            //    return;
            //}

            if (existsPort)
            {
                Helper.MessageBoxError("Essa 'Porta' já está em uso");
                txtMockPort.Focus();
                return;
            }

            if (proxy == null)
                proxy = new Proxy();

            proxy.Name = name;
            proxy.UrlOriginal = urlOriginal;
            proxy.PortProxy = portNumber;
            proxy.Arguments = txtArguments.Text;

            var newPath = proxy.GetFullPath();

            if (!string.IsNullOrWhiteSpace(this.oldPath) && Directory.Exists(oldPath) && oldPath != newPath)
            {
                Directory.Move(this.oldPath, newPath);
            }

            if (proxy.Id == Guid.Empty)
                db.Proxies.Insert(proxy);
            else
                db.Proxies.Update(proxy);

            db.Proxies.Update(proxy);
            db.Save();
            this.Close();

            master.SetProxy(proxy);
        }

        public int GetAutoPort()
        {
            var db = new UnitOfWork();
            var maxPort = db.Proxies.AsQueryable().Select(f => f.PortProxy).DefaultIfEmpty().Max();
            if (maxPort == 0)
                return 5500;
            return maxPort + 1;
        }
    }
}
