using System;
using System.IO;
using System.Windows.Forms;
using WiremockUI.Data;
using System.Linq;

namespace WiremockUI
{
    public partial class FormAddMock : Form
    {
        private FormMaster master;
        private Mock mock;
        private string oldPath;
        private TreeNode parent;
        private Proxy proxy;

        public FormAddMock(FormMaster master, TreeNode parent, Proxy proxy, Guid? id)
        {
            this.master = master;
            this.mock = proxy.GetMockById(id);
            this.parent = parent;
            this.proxy = proxy;
            InitializeComponent();
            
            if (this.mock != null)
            {
                this.txtName.Text = mock.Name;
                this.btnAdd.Text = "Editar";
                this.oldPath = proxy.GetFullPath(mock);

                if (master.Dashboard.IsRunning(mock))
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
            var idExists = mock?.Id;

            if (string.IsNullOrWhiteSpace(name))
            {
                Helper.MessageBoxError("O campo 'Nome' é obrigatório.");
                txtName.Focus();
                return;
            }

            var db = new UnitOfWork();
            var existsName = (from s in proxy.Mocks
                              where s.Name.ToLower() == name.ToLower() &&
                                    s.Id != idExists
                              select 1).Any();

            if (existsName)
            {
                Helper.MessageBoxError("Esse 'Nome' já está em uso");
                txtName.Focus();
                return;
            }

            if (mock == null)
                mock = new Mock();

            mock.Name = name;

            var newPath = proxy.GetFullPath(mock);
            if (!string.IsNullOrWhiteSpace(this.oldPath) && Directory.Exists(oldPath) && oldPath != newPath)
            {
                Directory.Move(this.oldPath, newPath);
            }

            if (mock.Id == Guid.Empty)
                proxy.AddMock(mock);

            db.Save();
            this.Close();

            master.SetMock(this.parent, mock);
        }
    }
}
