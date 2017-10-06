using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WiremockUI.Languages;

namespace WiremockUI
{
    public partial class FormFindInFiles : Form
    {
        private FormMaster master;
        private int searchPanelHeight;
        private int replacePanelHeight;

        private class FileFound
        {
            public int Sequence { get; set; }
            public string Path { get; set; }
            public string Content { get; set; }
        }

        public bool EnableReplace
        {
            get
            {
                return pnlReplace.Visible;
            }
            set
            {
                var down = Properties.Resources.arrow_down;
                var up = Properties.Resources.arrow_up;
                pnlReplace.Visible = value;

                if (value)
                {
                    this.imgToggleReplace.Image = up;
                    this.pnlSearch.Height = this.replacePanelHeight;
                }
                else
                {
                    this.imgToggleReplace.Image = down;
                    this.pnlSearch.Height = this.searchPanelHeight;
                }
            }
        }

        public FormFindInFiles(FormMaster master, string folderStart)
        {
            InitializeComponent();

            this.TabStop = false;

            this.txtFolder.Text = Path.GetDirectoryName(folderStart);
            this.master = master;

            this.searchPanelHeight = this.pnlSearch.Height - this.pnlReplace.Height;
            this.replacePanelHeight = this.pnlSearch.Height;

            this.txtSearchValue.Multiline = false;
            this.btnSearch.Text = Resource.btnSearch;
            this.btnReplaceAll.Text = Resource.btnReplaceAll;
            this.lblFolder.Text = Resource.lblFolder;
            this.pnlSearch.BackColor = Color.FromArgb(207, 214, 229);
            this.EnableReplace = false;
        }

        private List<FileFound> Search(string text)
        {
            var files = new List<FileFound>();

            if (string.IsNullOrWhiteSpace(text))
                return files;

            gridFiles.Rows.Clear();

            DirSearch(this.txtFolder.Text, text, files);
            if (files.Count > 0)
            {
                foreach (var f in files)
                    AddNewRow(f);
            }
            else
            {
                Helper.MessageBoxExclamation(Resource.noFilesFoundMessage);
            }

            return files;
        }

        private void Replace(string search, string replace)
        {
            var files = Search(search);
            if (files.Count > 0)
            {
                if (Helper.MessageBoxQuestion(string.Format(Resource.confirmReplaceMessage, files.Count)) == DialogResult.Yes)
                {
                    foreach(var f in files)
                    {
                        try
                        {
                            var content = File.ReadAllText(f.Path);
                            string result = Regex.Replace(content, search, replace, RegexOptions.IgnoreCase);
                            File.WriteAllText(f.Path, result);
                        }
                        catch (Exception ex)
                        {
                            Helper.MessageBoxError(string.Format(Resource.saveFileErrorMessage, ex.Message));
                        }
                    }
                }
            }
        }

        private void DirSearch(string sDir, string searchText, List<FileFound> filesFound)
        {
            try
            {
                var maxGetContent = 309;
                var i = 1;
                foreach (var directory in Directory.GetDirectories(sDir))
                {
                    foreach (var filename in Directory.GetFiles(directory))
                    {
                        using (var streamReader = new StreamReader(filename))
                        {
                            var contents = streamReader.ReadToEnd().ToLower();
                            var index = contents.IndexOf(searchText.ToLower());
                            if (index != -1)
                            {
                                var maxGet = (index + maxGetContent > contents.Length) ? contents.Length - index : maxGetContent;
                                filesFound.Add(new FileFound
                                {
                                    Sequence = i++,
                                    Content = SafeSubstring(contents, index, maxGetContent),
                                    Path = filename
                                });
                            }
                        }
                    }

                    DirSearch(directory, searchText, filesFound);
                }
            }
            catch (Exception ex)
            {
                Helper.MessageBoxError(ex.Message);
            }
        }

        private void AddNewRow(FileFound file)
        {
            var row = new DataGridViewRow
            {
                Tag = file
            };

            foreach (DataGridViewColumn col in gridFiles.Columns)
            {
                switch (col.Name)
                {
                    case "colSequence":
                        row.Cells.Add(GetCell(file.Sequence));
                        break;
                    case "colFilePath":
                        row.Cells.Add(GetCell(file.Path));
                        break;
                    case "colContent":
                        row.Cells.Add(GetCell(file.Content));
                        break;
                }
            }

            this.gridFiles.Rows.Add(row);
        }

        private DataGridViewTextBoxCell GetCell(object value, string toolTip = null)
        {
            var cell = new DataGridViewTextBoxCell();
            cell.Value = value;
            cell.ToolTipText = toolTip;
            cell.Tag = value;
            return cell;
        }

        private string SafeSubstring(string value, int startIndex, int length)
        {
            return new string((value ?? string.Empty).Skip(startIndex).Take(length).ToArray());
        }

        private void imgToggleReplace_Click(object sender, EventArgs e)
        {
            EnableReplace = !EnableReplace;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search(this.txtSearchValue.Text);
        }

        private void btnReplaceAll_Click(object sender, EventArgs e)
        {
            Replace(this.txtSearchValue.Text, this.txtReplaceText.Text);
        }

        private void FormFindInFiles_Resize(object sender, EventArgs e)
        {
            this.txtFolder.Width = this.Width - 60;
        }

        private void gridFiles_DoubleClick(object sender, EventArgs e)
        {
            var selected = gridFiles.CurrentRow;

            if (selected.Tag != null && selected.Tag is FileFound file)
            {
                var frmStart = new FormTextFile(master, null, file.Path);
                master.TabMaster.AddTab(frmStart, selected, Path.GetFileName(file.Path));
            }
        }
    }
}
