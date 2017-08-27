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
    public partial class FormCompare : Form
    {
        private string v;

        public FormCompare()
        {
            InitializeComponent();
        }

        public FormCompare(string content1) 
            : this()
        {
            this.txtContent1.Text = content1;
        }
    }
}
