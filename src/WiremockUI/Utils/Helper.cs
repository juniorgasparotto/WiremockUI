using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WiremockUI
{
    public static class Helper
    {
        public static DialogResult MessageBoxError(string message, string title = null)
        {
            title = title ?? "Error";
            return MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult MessageBoxExclamation(string message, string title = null)
        {
            title = title ?? "Alerta";
            return MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static DialogResult MessageBoxQuestion(string message, string title = null)
        {
            title = title ?? "Pergunta";
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
    }
}
