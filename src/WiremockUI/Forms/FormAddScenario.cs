using System;
using System.IO;
using System.Windows.Forms;
using WiremockUI.Data;
using System.Linq;
using WiremockUI.Languages;

namespace WiremockUI
{
    public partial class FormAddScenario : Form
    {
        private FormMaster master;
        private Scenario scenario;
        private string oldPath;
        private TreeNode parent;
        private Proxy proxy;

        public FormAddScenario(FormMaster master, TreeNode parent, Proxy proxy, Guid? id)
        {
            this.master = master;
            this.scenario = proxy.GetScenarioById(id);
            this.parent = parent;
            this.proxy = proxy;
            InitializeComponent();

            Text = Resource.formAddScenarioTitle;
            btnAdd.Text = Resource.btnAdd;
            btnCancel.Text = Resource.btnCancel;
            lblScenarioName.Text = Resource.lblScenarioName;
            lblScenarioDescription.Text = Resource.lblScenarioDescription;

            if (this.scenario != null)
            {
                Text = Resource.formAddScenarioInEditModeTitle;
                this.txtName.Text = scenario.Name;
                this.txtDesc.Text = scenario.Description;
                this.btnAdd.Text = Resource.btnEdit;
                this.oldPath = proxy.GetFullPath(scenario);

                if (master.Dashboard.IsRunning(scenario))
                    btnAdd.Enabled = false;

                this.Text = this.scenario.Name;
            }

            ResizeTexts();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (InTab())
            {
                FormMaster.Current.TabMaster.CloseTab(this);
            }
            else
            {
                this.Close();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var name = txtName.Text.Trim();
            var idExists = scenario?.Id;

            if (string.IsNullOrWhiteSpace(name))
            {
                Helper.MessageBoxError(Resource.addScenarioRequiredNameMessage);
                txtName.Focus();
                return;
            }

            var db = new UnitOfWork();
            var existsName = (from s in proxy.Scenarios
                              where s.Name.ToLower() == name.ToLower() &&
                                    s.Id != idExists
                              select 1).Any();

            if (existsName)
            {
                Helper.MessageBoxError(Resource.addScenarioDuplicateNameMessage);
                txtName.Focus();
                return;
            }

            if (scenario == null)
                scenario = new Scenario();

            scenario.Name = name;
            scenario.Description = this.txtDesc.Text;

            var newPath = proxy.GetFullPath(scenario);
            if (!string.IsNullOrWhiteSpace(this.oldPath) && Directory.Exists(oldPath) && oldPath != newPath)
            {
                Directory.Move(this.oldPath, newPath);
            }

            if (scenario.Id == Guid.Empty)
                proxy.AddScenario(scenario);

            db.Save();

            if (!InTab())
                this.Close();

            master.SetScenario(this.parent, scenario);
        }

        private bool InTab()
        {
            return this.Parent is TabPage;
        }

        private void FormAddScenario_Resize(object sender, EventArgs e)
        {
            ResizeTexts();
        }

        private void ResizeTexts()
        {
            txtDesc.Width = this.ClientSize.Width - 15;
            txtName.Width = this.ClientSize.Width - 15;
            txtDesc.Height = this.ClientSize.Height - 150;
        }

        private void FormAddScenario_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtName;
        }
    }
}
