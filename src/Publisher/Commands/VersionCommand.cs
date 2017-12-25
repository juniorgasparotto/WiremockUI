using Publisher.Core;
using SysCommand.ConsoleApp;
using System;

namespace Publisher
{
    public class VersionCommand : Command
    {
        public VersionCommand()
        {
            this.UsePrefixInAllMethods = true;
        }

        public void Next()
        {
            var pathCommand = App.Commands.Get<PathCommand>();
            var curVersion = Utils.GetCurrentVersion(pathCommand.TargetProjectAssemblyInfoPath);

            App.Console.Write("QUESTION: Choose the type of novelty that exists in this release.");
            App.Console.Write("    1 - Are there any significant new features.");
            App.Console.Write("    2 - Are there only improvements, refactorings, and evolutions.");
            App.Console.Write("    3 - There are only bug fixes (DEFAULT)");
            App.Console.Write("    4 - Set manually");
            App.Console.Write("    5 - Not set");
            App.Console.Write($"Current Version: {Utils.GetVersionToString(curVersion)}");

            var opt = App.Console.Read("Choose: ");

            int.TryParse(opt, out int value);
            value = string.IsNullOrWhiteSpace(opt) ? 3 : value;

            Version newVersion;
            if (value == 5)
            {
                return;
            }
            else if (value == 4)
            {
                int major, minor, build;

                while (!int.TryParse(App.Console.Read("Version.Major: "), out major));
                while (!int.TryParse(App.Console.Read("Version.Minor: "), out minor));
                while (!int.TryParse(App.Console.Read("Version.Build: "), out build));
                newVersion = new Version(major, minor, build);
            }
            else
            {
                newVersion = Utils.GetNewVersion(pathCommand.TargetProjectAssemblyInfoPath, value);
            }

            if (newVersion == null)
                throw new Exception("Invalid option");

            App.Console.Write($"New Version    : {Utils.GetVersionToString(newVersion)}\r\n");
            SetInternal(newVersion.Major, newVersion.Minor, newVersion.Build);
        }

        public void Set(int? major = null, int? minor = null, int? build = null)
        {
            SetInternal(major, minor, build);
        }

        public void Show()
        {
            var pathCommand = App.Commands.Get<PathCommand>();
            App.Console.Write(AssemblyInfoUpdater.GetAttrLine(pathCommand.TargetProjectAssemblyInfoPath, PathCommand.AssemblyVersionAttr, out _, out _));
            App.Console.Write(AssemblyInfoUpdater.GetAttrLine(pathCommand.TargetProjectAssemblyInfoPath, PathCommand.AssemblyFileVersionAttr, out _, out _));
            App.Console.Write(AssemblyInfoUpdater.GetAttrLine(pathCommand.TargetProjectAssemblyInfoPath, PathCommand.AssemblyInformationalVersionAttr, out _, out _));
        }

        private void SetInternal(int? major, int? minor, int? build)
        {
            var appInfo = AppInfo.GetAppInfo();
            var pathCommand = App.Commands.Get<PathCommand>();
            AssemblyInfoUpdater.Update(pathCommand.TargetProjectAssemblyInfoPath, PathCommand.AssemblyVersionAttr, major.ToString(), minor.ToString(), "*", null);
            AssemblyInfoUpdater.Update(pathCommand.TargetProjectAssemblyInfoPath, PathCommand.AssemblyFileVersionAttr, major.ToString(), minor.ToString(), build.ToString(), "");
            AssemblyInfoUpdater.Update(pathCommand.TargetProjectAssemblyInfoPath, PathCommand.AssemblyInformationalVersionAttr, major.ToString(), minor.ToString(), build.ToString(), "");
            AppInfo.SaveAppInfo(appInfo);
        }
    }
}
