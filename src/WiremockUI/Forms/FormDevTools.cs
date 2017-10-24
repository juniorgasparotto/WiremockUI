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
    public partial class FormDevTools : Form
    {
        public FormDevTools()
        {
            InitializeComponent();

            var iterator = java.lang.System.getProperties().keys();
            while(iterator.hasMoreElements())
            {
                var key = iterator.nextElement();
                var value = java.lang.System.getProperty(key.ToString());
                dataGridView1.Rows.Add(key, value);
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var row = dataGridView1.Rows[e.RowIndex];
            var key = row.Cells[0].Value?.ToString();
            var value = row.Cells[1].Value?.ToString();

            if (!string.IsNullOrEmpty(key))
                java.lang.System.setProperty(key, value ?? "");
        }

        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            var key = e.Row.Cells[0].Value?.ToString();

            if (!string.IsNullOrEmpty(key))
                java.lang.System.getProperties().remove(key);
        }
    }
}
