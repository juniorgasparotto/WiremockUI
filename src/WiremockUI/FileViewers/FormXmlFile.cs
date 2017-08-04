using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Text;

namespace WiremockUI
{
    public partial class FormXmlFile : Form, IFormFileUpdate
    {
        private object tabPage;
        private object master;

        public Action OnSave { get; set; }

        public FormXmlFile(FormMaster master, TabPageCustom tabPage, string fileName)
        {
            InitializeComponent();

            this.TabStop = false;

            this.tabPage = tabPage;
            this.master = master;
            LoadForm(fileName);
        }

        private void LoadForm(string fileName)
        {
            var content = "";
            if (fileName != null)
            {
                txtPath.Text = fileName;
                content = File.ReadAllText(fileName);
            }
            else
            {
                txtPath.Enabled = false;
                btnOpen.Enabled = false;
            }

            var text = Helper.ResolveBreakLineIncompatibility(content);
            if (text != null)
            {
                txtContent.Text = content;
            }
        }

        public static String FormatXml(String xml)
        {
            string strRetValue = null;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            var enc = Encoding.UTF8;
            var xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Encoding = enc;
            xmlWriterSettings.Indent = true;
            xmlWriterSettings.IndentChars = "    ";
            xmlWriterSettings.NewLineChars = "\r\n";
            xmlWriterSettings.NewLineHandling = NewLineHandling.Replace;
            xmlWriterSettings.ConformanceLevel = ConformanceLevel.Document;

            using (var ms = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(ms, xmlWriterSettings))
                {
                    doc.Save(writer);
                    writer.Flush();
                    ms.Flush();

                    writer.Close();
                } // End Using writer

                ms.Position = 0;
                using (var sr = new StreamReader(ms, enc))
                {
                    // Extract the text from the StreamReader.
                    strRetValue = sr.ReadToEnd();

                    sr.Close();
                } // End Using sr

                ms.Close();
            } // End Using ms

            xmlWriterSettings = null;
            return strRetValue;
        }

        private void Save()
        {
            try
            {
                File.WriteAllText(txtPath.Text, txtContent.Text);
                OnSave?.Invoke();
            }
            catch (Exception ex)
            {
                Helper.MessageBoxError("Ocorreu um erro ao tentar salvar o arquivo: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            Process.Start(txtPath.Text);
        }

        private void FormTextFile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                Save();
                Helper.AnimateSaveButton(btnSave);
                e.SuppressKeyPress = true;
            }
        }

        private void btnFormat_Click(object sender, EventArgs e)
        {
            try
            {
                txtContent.Text = FormatXml(txtContent.Text);
            }
            catch(Exception ex)
            {
                Helper.MessageBoxError("Esse XML está inválido: " + ex.Message);
            }
        }

        public void Update(string fileName)
        {
            LoadForm(fileName);
        }
    }
}
