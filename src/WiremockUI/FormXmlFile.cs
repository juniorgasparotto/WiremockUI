using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WiremockUI
{
    public partial class FormXmlFile : Form
    {
        private FormMaster formMaster;
        private object p;
        private string fullPath;

        public FormXmlFile()
        {
            InitializeComponent();
        }

        public FormXmlFile(FormMaster formMaster, object p, string fullPath)
        {
            this.formMaster = formMaster;
            this.p = p;
            this.fullPath = fullPath;
        }
    }
}
