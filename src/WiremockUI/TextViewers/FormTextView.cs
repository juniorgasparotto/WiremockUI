using System.Windows.Forms;

namespace WiremockUI
{
    public partial class FormTextView : Form
    {
        private FormMaster master;

        public FormTextView(FormMaster master, string title, string content)
        {
            InitializeComponent();

            this.master = master;
            this.TabStop = false;
            this.txtTitle.Text = title;
            this.txtContent.Text = Helper.ResolveBreakLineInCompatibility(content);
        }
    }
}
