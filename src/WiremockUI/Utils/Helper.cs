using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using WiremockUI.Languages;

namespace WiremockUI
{
    public static class Helper
    {
        public static DialogResult MessageBoxError(string message, string title = null)
        {
            title = title ?? Resource.messageBoxErrorTitle;
            return MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult MessageBoxExclamation(string message, string title = null)
        {
            title = title ?? Resource.messageBoxAlertTitle;
            return MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static DialogResult MessageBoxQuestion(string message, string title = null)
        {
            title = title ?? Resource.messageBoxQuestionTitle;
            return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static void ClearFolder(string folderName)
        {
            DirectoryInfo dir = new DirectoryInfo(folderName);

            foreach (FileInfo fi in dir.GetFiles())
                fi.Delete();

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                ClearFolder(di.FullName);
                di.Delete(true);
            }
        }

        public static string FormatToXml(String xml)
        {
            try
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
                //xmlWriterSettings.OmitXmlDeclaration = true;

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
            catch(Exception ex)
            {
                Helper.MessageBoxError(string.Format(Resource.formatXmlErrorMessage, ex.Message));
                return xml;
            }
        }


        public static string ResolveBreakLineInCompatibility(string text)
        {
            if (text == null)
                return text;

            var strbuilder = new StringBuilder();
            char last = default(char);
            foreach(var b in text)
            {
                if (last != '\r' && b == '\n')
                    strbuilder.Append("\r\n");
                else
                    strbuilder.Append(b);

                last = b;
            }

            return strbuilder.ToString();
        }

        public static string ReadAllText(string fileName)
        {
            while (IsFileLocked(fileName))
            { }

            using (var lockFileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                using (var reader = new StreamReader(lockFileStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static bool IsFileLocked(string filePath)
        {
            try
            {
                using (File.Open(filePath, FileMode.Open)) { }
            }
            catch (IOException e)
            {
                var errorCode = Marshal.GetHRForException(e) & ((1 << 16) - 1);

                return errorCode == 32 || errorCode == 33;
            }

            return false;
        }

        public static async void AnimateSaveButton(Button button)
        {
            var text = button.Text;
            button.Text = "*";
            await Task.Delay(300);
            button.Text = text;
        }

        public static string FormatToJson(string content)
        {
            try
            {
                return JToken.Parse(content).ToString();
            }
            catch (Exception ex)
            {
                Helper.MessageBoxError(string.Format(Resource.formatJsonErrorMessage, ex.Message));
                return content;
            }
        }

        public static string AddQuote(string value, bool addQuoteInStrings)
        {
            if (addQuoteInStrings)
                return "\"" + value + "\"";
            return value;
        }
    }
}
