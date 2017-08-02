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
    public partial class FormImageFile : Form
    {
        private FormMaster formMaster;
        private object p;
        private string fullPath;

        public FormImageFile()
        {
            InitializeComponent();
        }

        public FormImageFile(FormMaster formMaster, object p, string fullPath)
        {
            this.formMaster = formMaster;
            this.p = p;
            this.fullPath = fullPath;
        }
    }
}
