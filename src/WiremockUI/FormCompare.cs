using WiremockUI;
using System;
using System.Collections;
using System.Windows.Forms;

namespace WiremockUI
{
    public partial class FormCompare : Form
    {
		private DiffEngineLevel _level;

        public FormCompare()
        {
            InitializeComponent();
            this.txtContent1.EnableFormatter = true;
            this.txtContent2.EnableFormatter = true;
        }

        public FormCompare(string content1)
            : this()
        {
            this.txtContent1.Text = content1;
        }

        private void TextDiff(ArrayList lines1, ArrayList lines2)
        {
            this.Cursor = Cursors.WaitCursor;

            TextFileDiff sLF = null;
            TextFileDiff dLF = null;
            try
            {
                sLF = new TextFileDiff(lines1);
                dLF = new TextFileDiff(lines2);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "File Error");
                return;
            }

            try
            {
                double time = 0;
                DiffEngine de = new DiffEngine();
                time = de.ProcessDiff(sLF, dLF, _level);

                var rep = de.DiffReport();
                var dlg = new FormCompareResult(sLF, dLF, rep, time);
                dlg.ShowDialog();
                dlg.Dispose();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                string tmp = string.Format("{0}{1}{1}***STACK***{1}{2}",
                    ex.Message,
                    Environment.NewLine,
                    ex.StackTrace);
                Helper.MessageBoxError(tmp, "Compare Error");
                return;
            }
            this.Cursor = Cursors.Default;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var lines1 = TextFileDiff.GetByText(this.txtContent1.Text);
            var lines2 = TextFileDiff.GetByText(this.txtContent2.Text);
            TextDiff(lines1, lines2);
        }
    }
}
