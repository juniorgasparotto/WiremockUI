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
                this.txtDesc.Text = mock.Description;
                this.btnAdd.Text = "Salvar";
                this.oldPath = proxy.GetFullPath(mock);

                if (master.Dashboard.IsRunning(mock))
                    btnAdd.Enabled = false;
            }

            ResizeTexts();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (InTab())
            {
                var tabPage = (TabPage)this.Parent;
                var tab = (TabControl)tabPage.Parent;
                tab.TabPages.Remove(tabPage);
            }

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
            mock.Description = this.txtDesc.Text;

            var newPath = proxy.GetFullPath(mock);
            if (!string.IsNullOrWhiteSpace(this.oldPath) && Directory.Exists(oldPath) && oldPath != newPath)
            {
                Directory.Move(this.oldPath, newPath);
            }

            if (mock.Id == Guid.Empty)
                proxy.AddMock(mock);

            db.Save();

            if (!InTab())
                this.Close();

            master.SetMock(this.parent, mock);
        }

        private bool InTab()
        {
            return this.Parent is TabPage;
        }

        private void FormAddMock_Resize(object sender, EventArgs e)
        {
            ResizeTexts();
        }

        private void ResizeTexts()
        {
            txtDesc.Width = this.ClientSize.Width - 15;
            txtName.Width = this.ClientSize.Width - 15;
            txtDesc.Height = this.ClientSize.Height - 150;
        }
    }
}
