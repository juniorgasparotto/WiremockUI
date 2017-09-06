using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace WiremockUI
{
    public partial class FormCompare : Form
    {
		private DiffEngineLevel _level;
        private FormMaster master;

        public FormCompare(FormMaster master)
        {
            InitializeComponent();
            this.txtContent1.EnableFormatter = true;
            this.txtContent2.EnableFormatter = true;
            this.master = master;
            btnOpen1.ContextMenuStrip = GetOptionsMenu(txtFile1, txtContent1);
            btnOpen2.ContextMenuStrip = GetOptionsMenu(txtFile2, txtContent2);
        }

        public FormCompare(FormMaster master, string content1)
            : this(master)
        {
            this.txtContent1.Text = content1;
        }

        private ContextMenuStrip GetOptionsMenu(TextBox txtFile, RichTextBox txtContent)
        {
            // context menu
            var menu = new ContextMenuStrip();
            var openFileMenu = new ToolStripMenuItem();
            var openFileFromTreeViewMenu = new ToolStripMenuItem();
            var saveMenu = new ToolStripMenuItem();

            menu.Opening += (o, e) =>
            {
                if (!File.Exists(txtFile.Text))
                    saveMenu.Enabled = false;
                else
                    saveMenu.Enabled = true;
            };

            menu.Items.AddRange(new ToolStripMenuItem[]
            {
                openFileMenu,
                openFileFromTreeViewMenu,
                saveMenu
            });

            // open file
            openFileMenu.Text = "Abrir arquivo";
            openFileMenu.Click += (a, b) =>
            {
                var openFileDialog1 = new OpenFileDialog();
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    OpenFile(txtFile, txtContent, openFileDialog1.FileName, openFileDialog1.OpenFile());
                }
            };

            // select file
            openFileFromTreeViewMenu.Text = "Selecionar na árvore";
            openFileFromTreeViewMenu.Click += (a, b) =>
            {
                master.SelectToCompare((filename) =>
                {
                    OpenFile(txtFile, txtContent, filename);
                },
                (filename, content) =>
                {
                    OpenFile(txtFile, txtContent, filename, content);
                });
            };

            // save file
            saveMenu.Text = "Salvar";
            saveMenu.Click += (a, b) =>
            {
                try
                {
                    File.WriteAllText(txtFile.Text, txtContent.Text);
                }
                catch (Exception ex)
                {
                    Helper.MessageBoxError("Ocorreu um erro ao tentar salvar o arquivo: " + ex.Message);
                }
            };

            return menu;
        }

        private void OpenFile(TextBox txtFile, RichTextBox txtContent, string fileName)
        {
            OpenFile(txtFile, txtContent, fileName, new FileStream(fileName, FileMode.Open));
        }

        private void OpenFile(TextBox txtFile, RichTextBox txtContent, string fileName, Stream stream)
        {
            using (stream)
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    OpenFile(txtFile, txtContent, fileName, sr.ReadToEnd());
                }
            }
        }

        private void OpenFile(TextBox txtFile, RichTextBox txtContent, string fileName, string content)
        {
            txtFile.Text = fileName;
            txtContent.Text = content;
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

        private void btnCompare_Click(object sender, EventArgs e)
        {
            var lines1 = TextFileDiff.GetByText(this.txtContent1.Text);
            var lines2 = TextFileDiff.GetByText(this.txtContent2.Text);
            TextDiff(lines1, lines2);
        }

        private void txtFile1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (File.Exists(txtFile1.Text))
                    txtContent1.Text = File.ReadAllText(txtFile1.Text);
                else
                    Helper.MessageBoxError("Arquivo inexistente");
            }
        }

        private void txtFile2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (File.Exists(txtFile2.Text))
                    txtContent2.Text = File.ReadAllText(txtFile2.Text);
                else
                    Helper.MessageBoxError("Arquivo inexistente");
            }
        }

        private void FormCompare_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtContent1;
        }
    }
}
