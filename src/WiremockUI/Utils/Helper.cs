using Newtonsoft.Json;
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
        private const string FOLDER = ".app";
        public const string DBFILE = "db.json";

        public static string GetBasePath()
        {
            if (DevelopmentHelper.IsAttached)
                return Path.Combine(DevelopmentHelper.GetProjectDirectory(), FOLDER);
            else
                return Path.Combine(Directory.GetCurrentDirectory(), FOLDER);
        }

        public static string GetDbFilePath()
        {
            return Path.Combine(GetBasePath(), DBFILE);
        }

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

        public static string ReadFileAsString(string fileName)
        {
            try
            {
                return File.ReadAllText(fileName);
            }
            catch (Exception ex)
            {
                MessageBoxError(ex.Message);
                return null;
            }
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

        public static string FormatToXml(string xml, bool showErrorMessage = true)
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
                if (showErrorMessage)
                    MessageBoxError(string.Format(Resource.formatXmlErrorMessage, ex.Message));
                return xml;
            }
        }

        public static string JsonEscape(string text)
        {
            string json = text;
            if (text?.Length > 0)
            {
                try
                {
                    json = JsonConvert.SerializeObject(text);
                    json = json.Remove(0, 1);
                    json = json.Remove(json.Length - 1);
                }
                catch(Exception ex)
                {
                    MessageBoxError(ex.Message);
                }
            }
            return json;
        }

        internal static object FormatToJson(object textValue)
        {
            throw new NotImplementedException();
        }

        public static string JsonUnescape(string text)
        {
            string json = text;
            if (text?.Length > 0)
            {
                try
                {
                    var obj = (JToken)JsonConvert.DeserializeObject("{ value: \"" + text + "\" }");
                    json = obj["value"].ToString();
                }
                catch (Exception ex)
                {
                    MessageBoxError(ex.Message);
                }
            }
            return json;
        }

        public static string JsonMinify(string text)
        {
            string json = text;
            if (text?.Length > 0)
            {
                try
                {
                    var obj = (JToken)JsonConvert.DeserializeObject(text);
                    json = obj.ToString(Newtonsoft.Json.Formatting.None);
                }
                catch (Exception ex)
                {
                    MessageBoxError(ex.Message);
                }
            }
            return json;
        }

        public static string GetJsonValue(string text, out bool hasQuote, out bool hasError)
        {
            string json = text;
            hasQuote = false;
            hasError = false;
            if (text?.Length > 0)
            {
                try
                {
                    var jsonTrim = text.Trim();
                    if (jsonTrim.StartsWith("\"") && jsonTrim.EndsWith("\""))
                    {
                        json = jsonTrim.Remove(0, 1);
                        json = json.Remove(json.Length - 1);
                        hasQuote = true;
                    }

                    var obj = (JToken)JsonConvert.DeserializeObject("{ value: \"" + json + "\" }");
                    json = obj["value"].ToString();
                }
                catch (Exception ex)
                {
                    hasError = true;
                    MessageBoxError(ex.Message);
                }
            }
            return json;
        }

        public static string XmlEscape(string text)
        {
            string xml = text;
            if (text?.Length > 0)
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    XmlNode node = doc.CreateElement("root");
                    node.InnerText = text;
                    xml = node.InnerXml;
                }
                catch (Exception ex)
                {
                    MessageBoxError(ex.Message);
                }
            }
            return xml;
        }

        public static string XmlUnescape(string text)
        {
            string xml = text;
            if (text?.Length > 0)
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    XmlNode node = doc.CreateElement("root");
                    node.InnerXml = text;
                    xml = node.InnerText;
                }
                catch (Exception ex)
                {
                    MessageBoxError(ex.Message);
                }
            }
            return xml;
        }

        public static string XmlMinify(string text)
        {
            string xml = text;
            if (text?.Length > 0)
            {
                try
                {
                    xml = new XMLMinifier(XMLMinifierSettings.Aggressive).Minify(text);
                }
                catch (Exception ex)
                {
                    MessageBoxError(ex.Message);
                }
            }
            return xml;
        }

        public static string GetXmlValue(string text, out bool hasTagInSelection, out bool hasError)
        {
            string xml = text;
            hasTagInSelection = false;
            hasError = false;
            if (text?.Length > 0)
            {
                try
                {
                    var xmlTrim = text.Trim();
                    if (xmlTrim.StartsWith(">") && xmlTrim.EndsWith("</"))
                    {
                        xml = xmlTrim.Remove(0, 1);
                        xml = xml.Remove(xml.Length - 1);
                        xml = xml.Remove(xml.Length - 1);
                        hasTagInSelection = true;
                    }

                    XmlDocument doc = new XmlDocument();
                    XmlNode node = doc.CreateElement("root");
                    node.InnerXml = xml;
                    xml = node.InnerText;
                }
                catch (Exception ex)
                {
                    hasError = true;
                    MessageBoxError(ex.Message);
                }
            }
            return xml;
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

        public static async void AnimateSaveButton(Button button, string originalText)
        {
            var text = button.Text;
            button.Text = "*";
            await Task.Delay(300);
            button.Text = originalText;
        }

        public static string FormatToJson(string content, bool showErrorMessage = true)
        {
            try
            {
                using (var stringReader = new StringReader(content))
                using (var stringWriter = new StringWriter())
                {
                    var jsonReader = new JsonTextReader(stringReader);
                    var jsonWriter = new JsonTextWriter(stringWriter)
                    {
                        Formatting = Newtonsoft.Json.Formatting.Indented,
                        Indentation = 4
                    };
                    jsonWriter.WriteToken(jsonReader);
                    return stringWriter.ToString();
                }

                //return JToken.Parse(content).ToString();
            }
            catch (Exception ex)
            {
                if (showErrorMessage)
                    MessageBoxError(string.Format(Resource.formatJsonErrorMessage, ex.Message));
                return content;
            }
        }

        public static string AddQuote(string value, bool addQuoteInStrings)
        {
            if (addQuoteInStrings)
                return "\"" + value + "\"";
            return value;
        }

        public static void CopyTo(this DirectoryInfo source, DirectoryInfo target, bool overwiteFiles = true)
        {
            if (!source.Exists) return;
            if (!target.Exists)
                target.Create();

            foreach (var sourceChildDirectory in source.GetDirectories())
                CopyTo(sourceChildDirectory, new DirectoryInfo(Path.Combine(target.FullName, sourceChildDirectory.Name)));

            foreach (var sourceFile in source.GetFiles())
                sourceFile.CopyTo(Path.Combine(target.FullName, sourceFile.Name), overwiteFiles);
        }

        public static void CopyTo(this DirectoryInfo source, string target, bool overwiteFiles = true)
        {
            CopyTo(source, new DirectoryInfo(target), overwiteFiles);
        }

        public static string GetExceptionDetails(Exception ex)
        {
            var strBuilder = new StringBuilder();

            strBuilder.AppendLine("--- Exception: " + ex.Message);
            strBuilder.AppendLine("--- StackTrace:");
            strBuilder.AppendLine(ex.StackTrace);

            if (ex.InnerException != null)
            {
                strBuilder.AppendLine("--- Inner exception: " + ex.InnerException.Message);
                strBuilder.AppendLine("--- StackTrace:");
                strBuilder.AppendLine(ex.InnerException.StackTrace);
            }
            
            if (ex.InnerException != null)
            {
                if (ex.InnerException is AggregateException list)
                {
                    foreach (var exInner in list.InnerExceptions)
                    {
                        strBuilder.AppendLine("--- Inner exception: " + exInner.Message);
                        strBuilder.AppendLine("--- StackTrace:" );
                        strBuilder.AppendLine(ex.InnerException.StackTrace);
                    }
                }
            }

            return strBuilder.ToString();
        }
    }
}
