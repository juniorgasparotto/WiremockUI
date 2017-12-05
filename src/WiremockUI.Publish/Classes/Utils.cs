using Octokit;
using SysCommand.ConsoleApp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace WiremockUI.Publish
{
    public static class Utils
    {
        public static string GetExceptionDetails(Exception ex)
        {
            if (ex == null)
                return null;

            var strBuilder = new StringBuilder();

            if (ex.GetBaseException() is ApiValidationException exBase)
            {
                var response = exBase.HttpResponse != null
                       && !exBase.HttpResponse.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase)
                       && exBase.HttpResponse.Body is string
                    ? (string)exBase.HttpResponse.Body : string.Empty;

                strBuilder.AppendLine("Error: " + exBase.Message);
                strBuilder.AppendLine("Respose: " + response);
                return strBuilder.ToString();
            }

            return ex.ToString();
        }

        public static SecureString ReadPassword(Command command, string msg)
        {
            var pass = new SecureString();
            var colorOriginal = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;

            if (command.App.Console.BreakLineInNextWrite)
                Console.WriteLine();

            Console.Write(msg);
            Console.ForegroundColor = colorOriginal;
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                // Backspace Should Not Work
                if (!char.IsControl(key.KeyChar))
                {
                    pass.AppendChar(key.KeyChar);
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass.RemoveAt(pass.Length - 1);
                        Console.Write("\b \b");
                    }
                }
            }
            // Stops Receving Keys Once Enter is Pressed
            while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            command.App.Console.BreakLineInNextWrite = false;
            return pass;
        }

        public static string SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }

        public static Version GetCurrentVersion(string assemblyInfoPath)
        {
            return AssemblyInfoUpdater.GetCurrentVersion(assemblyInfoPath, PathCommand.AssemblyFileVersionAttr);
        }

        public static Version GetNewVersion(string assemblyInfoPath, int posAddNew)
        {
            var curVersion = AssemblyInfoUpdater.GetCurrentVersion(assemblyInfoPath, PathCommand.AssemblyFileVersionAttr);

            if (posAddNew == 1)
                return new Version(curVersion.Major + 1, 0, 0, 0);
            else if (posAddNew == 2)
                return new Version(curVersion.Major, curVersion.Minor + 1, 0, 0);
            else if (posAddNew == 3)
                return new Version(curVersion.Major, curVersion.Minor, curVersion.Build + 1, 0);

            return null;
        }

        public static string GetVersionToString(Version version, string prefix = null, string suffix = null)
        {
            return $"{prefix} {version.Major}.{version.Minor}.{version.Build} {suffix}".Trim();
        }

        public static T Get<T>(this IEnumerable<Command> collection) where T : Command
        {
            return collection.Where(f => f is T).Cast<T>().FirstOrDefault();
        }

        public static void ClearFolder(string folderName)
        {
            DirectoryInfo dir = new DirectoryInfo(folderName);

            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.IsReadOnly = false;
                fi.Delete();
            }

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                ClearFolder(di.FullName);
                di.Delete();
            }
        }

        /// <summary>
        /// Create the folder if not existing for a full file name
        /// </summary>
        /// <param name="filename">full path of the file</param>
        public static void CreateFolderIfNeeded(string filename)
        {
            string folder = Path.GetDirectoryName(filename);
            if (!string.IsNullOrEmpty(folder) && !Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }

        public static bool Continue(Command command, string message)
        {
            var result = command.App.Console.Read(message + " [Yes/No]: ")?.ToLower();
            return string.IsNullOrWhiteSpace(result) || result == "y" || result == "yes";
        }
    }
}