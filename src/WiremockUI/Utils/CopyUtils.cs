using System.Linq;
using System.IO;
using WiremockUI.Data;

namespace WiremockUI
{
    public class CopyUtils
    {
        private static string jarFile;
        public static string JarFile
        {
            get
            {
                if (jarFile == null)
                {
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), "Jar");
                    DirectoryInfo di = new DirectoryInfo(dir);
                    FileInfo[] files = di.GetFiles("*.jar");
                    if (files.Length > 0)
                        jarFile = files.OrderByDescending(f => f.CreationTime).FirstOrDefault().FullName;
                }

                return jarFile;
            }
        }

        public static string GetAsJavaCommand(Server server, Scenario scenario, Server.PlayType type)
        {
            var argsWithQuote = server.GetArguments(scenario, type, true);
            return $@"java -jar ""{JarFile}"" {string.Join(" ", argsWithQuote)}";
        }
    }
}
