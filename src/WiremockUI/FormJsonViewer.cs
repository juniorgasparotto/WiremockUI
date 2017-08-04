using System.Windows.Forms;

namespace WiremockUI
{
    public partial class FormJsonViewer : Form
    {
        public FormJsonViewer(FormMaster master, string tabName, string json, bool expandAll)
        {
            InitializeComponent();
            this.ucJsonView.ContentJson = json;
            this.ucJsonView.ExpandAll = expandAll;
            this.ucJsonView.OnJsonVisualizer = (_elementName, _elementValue, _expandAll) =>
            {
                var _tabName = tabName;
                if (!string.IsNullOrWhiteSpace(_elementName))
                    _tabName += "." + _elementName;

                var frm = new FormJsonViewer(master, _tabName, _elementValue, true);
                master.TabMaster.AddTab(frm, null, _tabName);
            };
        }
    }
}
