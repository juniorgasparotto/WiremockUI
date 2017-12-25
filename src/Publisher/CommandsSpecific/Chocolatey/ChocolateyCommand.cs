using Publisher.Core;
using SysCommand.ConsoleApp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Publisher.CommandSpecific.Chocolatey
{
    public class ChocolateyCommand : Command
    {
        private static string appKey;

        private DirectoryInfo ToolsPath
        {
            get
            {
                return new DirectoryInfo(Path.Combine(PathCommand.RootDirectory, "CommandsSpecific", "Chocolatey", "Nuspec", "tools"));
            }
        }

        public string ChocolateyPackageName
        {
            get
            {
                var appInfo = AppInfo.GetAppInfo();
                if (string.IsNullOrWhiteSpace(appInfo.ChocolateyPackageName))
                {
                    appInfo.ChocolateyPackageName = App.Console.Read("Chocolatey.PackageName: ");
                    AppInfo.SaveAppInfo(appInfo);
                }
                return appInfo.ChocolateyPackageName;
            }
            set
            {
                var appInfo = AppInfo.GetAppInfo();
                appInfo.ChocolateyPackageName = value;
                AppInfo.SaveAppInfo(appInfo);
            }
        }

        public string ChocolateyTitle
        {
            get
            {
                var appInfo = AppInfo.GetAppInfo();
                if (string.IsNullOrWhiteSpace(appInfo.ChocolateyTitle))
                {
                    appInfo.ChocolateyTitle = App.Console.Read("Chocolatey.Title: ");
                    AppInfo.SaveAppInfo(appInfo);
                }
                return appInfo.ChocolateyTitle;
            }
            set
            {
                var appInfo = AppInfo.GetAppInfo();
                appInfo.ChocolateyTitle = value;
                AppInfo.SaveAppInfo(appInfo);
            }
        }
        
        public string ChocolateyExeName
        {
            get
            {
                var appInfo = AppInfo.GetAppInfo();
                if (string.IsNullOrWhiteSpace(appInfo.ChocolateyExeName))
                {
                    appInfo.ChocolateyExeName = App.Console.Read("Chocolatey.ExeName: ");
                    AppInfo.SaveAppInfo(appInfo);
                }
                return appInfo.ChocolateyExeName;
            }
            set
            {
                var appInfo = AppInfo.GetAppInfo();
                appInfo.ChocolateyExeName = value;
                AppInfo.SaveAppInfo(appInfo);
            }
        }

        public string ChocolateyUrlDownload
        {
            get
            {
                var appInfo = AppInfo.GetAppInfo();
                if (string.IsNullOrWhiteSpace(appInfo.ChocolateyUrlDownload))
                {
                    appInfo.ChocolateyUrlDownload = App.Console.Read("Chocolatey.UrlDownload (use '{version}' placeholder): ");
                    AppInfo.SaveAppInfo(appInfo);
                }
                return appInfo.ChocolateyUrlDownload;
            }
            set
            {
                var appInfo = AppInfo.GetAppInfo();
                appInfo.ChocolateyUrlDownload = value;
                AppInfo.SaveAppInfo(appInfo);
            }
        }

        public string ChocolateyProjectUrl
        {
            get
            {
                var appInfo = AppInfo.GetAppInfo();
                if (string.IsNullOrWhiteSpace(appInfo.ChocolateyProjectUrl))
                {
                    appInfo.ChocolateyProjectUrl = App.Console.Read("Chocolatey.ProjectUrl: ");
                    AppInfo.SaveAppInfo(appInfo);
                }
                return appInfo.ChocolateyProjectUrl;
            }
            set
            {
                var appInfo = AppInfo.GetAppInfo();
                appInfo.ChocolateyProjectUrl = value;
                AppInfo.SaveAppInfo(appInfo);
            }
        }

        public string ChocolateyLicenceUrl
        {
            get
            {
                var appInfo = AppInfo.GetAppInfo();
                if (string.IsNullOrWhiteSpace(appInfo.ChocolateyLicenceUrl))
                {
                    appInfo.ChocolateyLicenceUrl = App.Console.Read("Chocolatey.LicenceUrl: ");
                    AppInfo.SaveAppInfo(appInfo);
                }
                return appInfo.ChocolateyLicenceUrl;
            }
            set
            {
                var appInfo = AppInfo.GetAppInfo();
                appInfo.ChocolateyLicenceUrl = value;
                AppInfo.SaveAppInfo(appInfo);
            }
        }

        public string ChocolateyIconUrl
        {
            get
            {
                var appInfo = AppInfo.GetAppInfo();
                if (string.IsNullOrWhiteSpace(appInfo.ChocolateyIconUrl))
                {
                    appInfo.ChocolateyIconUrl = App.Console.Read("Chocolatey.IconUrl: ");
                    AppInfo.SaveAppInfo(appInfo);
                }
                return appInfo.ChocolateyIconUrl;
            }
            set
            {
                var appInfo = AppInfo.GetAppInfo();
                appInfo.ChocolateyIconUrl = value;
                AppInfo.SaveAppInfo(appInfo);
            }
        }

        public string ChocolateyTags
        {
            get
            {
                var appInfo = AppInfo.GetAppInfo();
                if (string.IsNullOrWhiteSpace(appInfo.ChocolateyTags))
                {
                    appInfo.ChocolateyTags = App.Console.Read("Chocolatey.Tags: ");
                    AppInfo.SaveAppInfo(appInfo);
                }
                return appInfo.ChocolateyTags;
            }
            set
            {
                var appInfo = AppInfo.GetAppInfo();
                appInfo.ChocolateyTags = value;
                AppInfo.SaveAppInfo(appInfo);
            }
        }

        public string ChocolateyDescription
        {
            get
            {
                var appInfo = AppInfo.GetAppInfo();
                if (string.IsNullOrWhiteSpace(appInfo.ChocolateyDescription))
                {
                    appInfo.ChocolateyDescription = App.Console.Read("Chocolatey.Description: ");
                    AppInfo.SaveAppInfo(appInfo);
                }
                return appInfo.ChocolateyDescription;
            }
            set
            {
                var appInfo = AppInfo.GetAppInfo();
                appInfo.ChocolateyDescription = value;
                AppInfo.SaveAppInfo(appInfo);
            }
        }

        public string ChocolateyAuthor
        {
            get
            {
                var appInfo = AppInfo.GetAppInfo();
                if (string.IsNullOrWhiteSpace(appInfo.ChocolateyAuthor))
                {
                    appInfo.ChocolateyAuthor = App.Console.Read("Chocolatey.Author: ");
                    AppInfo.SaveAppInfo(appInfo);
                }
                return appInfo.ChocolateyAuthor;
            }
            set
            {
                var appInfo = AppInfo.GetAppInfo();
                appInfo.ChocolateyAuthor = value;
                AppInfo.SaveAppInfo(appInfo);
            }
        }

        public string ChocolateyAppKey
        {
            get
            {
                if (appKey == null)
                    appKey = App.Console.Read("Chocolatey.AppKey (https://push.chocolatey.org/account): ");
                return appKey;
            }
            set => appKey = value;
        }

        public ChocolateyCommand()
        {
            this.UsePrefixInAllMethods = true;
        }

        public void Build()
        {
            try
            {
                App.Console.Write($"Start chocolatey build: {DateTime.Now}");
                Utils.ClearFolder(PathCommand.ChocolateyPackDirectory);

                var pathCommand = App.Commands.Get<PathCommand>();
                var newVersion = Utils.GetVersionToString(Utils.GetCurrentVersion(pathCommand.TargetProjectAssemblyInfoPath));

                var vars = new Dictionary<string, string>();
                vars.Add("exeName", ChocolateyExeName);
                vars.Add("packageName", ChocolateyPackageName);                
                vars.Add("title", ChocolateyTitle);
                vars.Add("version", newVersion);
                vars.Add("url", ChocolateyUrlDownload.Replace("{version}", newVersion));
                vars.Add("iconUrl", ChocolateyIconUrl);
                vars.Add("projectUrl", ChocolateyProjectUrl);
                vars.Add("licenceUrl", ChocolateyLicenceUrl);
                vars.Add("author", ChocolateyAuthor);
                vars.Add("tags", ChocolateyTags);
                vars.Add("description", ChocolateyDescription);

                // file1: Modify
                var modify = new CommandsSpecific.Chocolatey.Nuspec.tools_t4.chocolateybeforemodify
                {
                    Session = new Dictionary<string, object> { { "vars", vars } }
                };
                modify.Initialize();

                // file2: Install
                var install = new CommandsSpecific.Chocolatey.Nuspec.tools_t4.chocolateyinstall
                {
                    Session = new Dictionary<string, object> { { "vars", vars } }
                };
                install.Initialize();

                // file3: Uninstall
                var uninstall = new CommandsSpecific.Chocolatey.Nuspec.tools_t4.chocolateyuninstall
                {
                    Session = new Dictionary<string, object> { { "vars", vars } }
                };
                uninstall.Initialize();

                // file4: nuspec
                var nuspec = new CommandsSpecific.Chocolatey.Nuspec.nuspec
                {
                    Session = new Dictionary<string, object> { { "vars", vars } }
                };
                nuspec.Initialize();

                var modifystr = modify.TransformText().Trim();
                var installstr = install.TransformText().Trim();
                var uninstallstr = uninstall.TransformText().Trim();
                var nuspecstr = nuspec.TransformText().Trim();

                var toolsFolder = Path.Combine(PathCommand.ChocolateyPackDirectory, ToolsPath.Name);
                ToolsPath.CopyTo(toolsFolder);

                File.WriteAllText(Path.Combine(toolsFolder, "chocolateybeforemodify.ps1"), modifystr);
                File.WriteAllText(Path.Combine(toolsFolder, "chocolateyinstall.ps1"), installstr);
                File.WriteAllText(Path.Combine(toolsFolder, "chocolateyuninstall.ps1"), uninstallstr);
                File.WriteAllText(Path.Combine(PathCommand.ChocolateyPackDirectory, $"{ChocolateyPackageName}.nuspec"), nuspecstr);
            }
            catch 
            {
                throw;
            }
            finally
            {
                App.Console.Write($"End: {DateTime.Now}");
            }
        }

        public void Pack()
        {
            try
            {
                App.Console.Write($"Start chocolatey pack '{PathCommand.ChocolateyPackDirectory}': {DateTime.Now}");
                Run("pack");
            }
            catch (Exception ex)
            {
                App.Console.Error(ex.Message);
                App.Console.Warning("retry: chocolatey-pack");
            }
            finally
            {
                App.Console.Write($"End: {DateTime.Now}");
            }
        }

        public void Push()
        {
            try
            {
                App.Console.Write($"Start chocolatey push: {DateTime.Now}");
                var nupkg = Directory.GetFiles(PathCommand.ChocolateyPackDirectory, "*.nupkg").FirstOrDefault();
                if (nupkg != null)
                {
                    App.Console.Write(Path.GetFileName(nupkg));
                    Run("push", $"--api-key {ChocolateyAppKey} -v");
                }
                else
                {
                    App.Console.Warning($"Not found '.nupkg' file in folder '{PathCommand.ChocolateyPackDirectory}'");
                }
            }
            catch (Exception ex)
            {
                App.Console.Error(ex.Message);
                App.Console.Warning("retry: chocolatey-push");
            }
            finally
            {
                App.Console.Write($"End: {DateTime.Now}");
            }
        }

        private void Run(string subCommand, string args = null)
        {
            if (!Directory.Exists(PathCommand.ChocolateyPackDirectory))
                Directory.CreateDirectory(PathCommand.ChocolateyPackDirectory);

            App.Console.Write("");
            var pathCommand = App.Commands.Get<PathCommand>();
            var startInfo = new ProcessStartInfo
            {
                CreateNoWindow = false,
                UseShellExecute = false,
                FileName = pathCommand.ChocolateyPath,
                WorkingDirectory = PathCommand.ChocolateyPackDirectory,
                WindowStyle = ProcessWindowStyle.Normal,
                Arguments = $"{subCommand} {args}".Trim()
            };

            using (Process exeProcess = Process.Start(startInfo))
            {
                exeProcess.WaitForExit();
            }
        }
    }
}
