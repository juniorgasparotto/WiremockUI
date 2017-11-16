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
            LoadJavaProperties();

            /// Menu AddJavaProperties
            var menu = new ContextMenuStrip();
            var addJavaxNetDebug = new ToolStripMenuItem();
            menu.Items.AddRange(new ToolStripMenuItem[]
            {
                addJavaxNetDebug
            });

            addJavaxNetDebug.Text = "javax.net.debug";
            addJavaxNetDebug.Click += (a, b) =>
            {
                java.lang.System.setProperty("javax.net.debug", "all");
                LoadJavaProperties();
                Helper.MessageBoxExclamation("This change has no effect if you have previously started any server. If you have already started, reopen the application, add this property, and start the server you want.");
            };

            btnAdd.ContextMenuStrip = menu;
        }

        private void LoadJavaProperties()
        {
            var iterator = java.lang.System.getProperties().keys();
            while (iterator.hasMoreElements())
            {
                var key = iterator.nextElement();
                var value = java.lang.System.getProperty(key.ToString());
                dataGridView1.Rows.Add(key, value);
            }

            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
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
