using System;
using System.Windows.Forms;
using WiremockUI.Languages;

namespace WiremockUI
{
    public partial class FormEditValue : Form
    {
        private Action<string> onReplace;

        public FormEditValue()
        {
            InitializeComponent();

            Text = Resource.formEditValue;
            btnCancel.Text = Resource.btnCancel;
            btnReplace.Text = Resource.btnReplace;
        }

        public FormEditValue(string content, Action<string> onReplace) :
            this()
        {
            this.txtContent.Text = content;
            this.onReplace = onReplace;
        }

        private void btnReplace_Click(object sender, System.EventArgs e)
        {
            onReplace(txtContent.Text);
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
