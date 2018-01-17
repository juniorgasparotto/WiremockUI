using Publisher.Core;
using SysCommand.ConsoleApp;
using SysCommand.ConsoleApp.Helpers;
using SysCommand.Mapping;
using System.IO;

namespace Publisher
{
    public class PathCommand : Command
    {
        #region Constants
        private const string _targetProjectAssemblyInfoPath = @"Properties\AssemblyInfo.cs";
        
        private const string _outputDirectory = "Output";
        private const string _buildDirectory = "Build"; 
        private const string _gitHubPackDirectory = "PacksGitHub";
        private const string _chocoPackDirectory = "PacksChocolatey";
        private const string _commitFiles = "commits.txt";
        private const string _appDirectory = ".app";
        public const string AssemblyFileVersionAttr = "AssemblyFileVersion";
        public const string AssemblyVersionAttr = "AssemblyVersion";
        public const string AssemblyInformationalVersionAttr = "AssemblyInformationalVersion";
        #endregion

        #region Properties (statics)

        public static string RootDirectory
        {
            get
            {
                return Development.GetProjectDirectory();
            }
        }

        public static string AppDirectory
        {
            get
            {
                return Path.Combine(RootDirectory, _appDirectory);
            }
        }

        public static string OutputDirectory
        {
            get
            {
                return Path.Combine(RootDirectory, _outputDirectory);
            }
        }

        public static string BuildDirectory
        {
            get
            {
                return Path.Combine(OutputDirectory, _buildDirectory);
            }
        }

        public static string GitHubPackDirectory
        {
            get
            {
                return Path.Combine(OutputDirectory, _gitHubPackDirectory);
            }
        }

        public static string ChocolateyPackDirectory
        {
            get
            {
                return Path.Combine(OutputDirectory, _chocoPackDirectory);
            }
        }

        public static string CommitFilePath
        {
            get
            {
                return Path.Combine(OutputDirectory, _commitFiles);
            }
        }

        #endregion

        #region Properties (arguments)

        [Argument]
        public string MsBuildPath
        {
            get
            {
                var appInfo = AppInfo.GetAppInfo();
                if (string.IsNullOrWhiteSpace(appInfo.MsBuildPath))
                {
                    appInfo.MsBuildPath = App.Console.Read("MsBuild Path: ");
                    AppInfo.SaveAppInfo(appInfo);
                }
                return appInfo.MsBuildPath;
            }
            set
            {
                var appInfo = AppInfo.GetAppInfo();
                appInfo.MsBuildPath = value;
                AppInfo.SaveAppInfo(appInfo);
            }
        }

        [Argument]
        public string GitPath
        {
            get
            {
                var appInfo = AppInfo.GetAppInfo();
                if (string.IsNullOrWhiteSpace(appInfo.GitPath))
                {
                    appInfo.GitPath = App.Console.Read("Git Path: ");
                    AppInfo.SaveAppInfo(appInfo);
                }
                return appInfo.GitPath;
            }
            set
            {
                var appInfo = AppInfo.GetAppInfo();
                appInfo.GitPath = value;
                AppInfo.SaveAppInfo(appInfo);
            }
        }

        [Argument]
        public string ChocolateyPath
        {
            get
            {
                var appInfo = AppInfo.GetAppInfo();
                if (string.IsNullOrWhiteSpace(appInfo.ChocolateyPath))
                {
                    appInfo.ChocolateyPath = App.Console.Read("Chocolatey Path: ");
                    AppInfo.SaveAppInfo(appInfo);
                }
                return appInfo.ChocolateyPath;
            }
            set
            {
                var appInfo = AppInfo.GetAppInfo();
                appInfo.ChocolateyPath = value;
                AppInfo.SaveAppInfo(appInfo);
            }
        }

        [Argument]
        public string TargetProjectPath
        {
            get
            {
                var appInfo = AppInfo.GetAppInfo();
                if (string.IsNullOrWhiteSpace(appInfo.TargetProjectPath))
                {
                    appInfo.TargetProjectPath = App.Console.Read("Project path (.csproj): ");
                    AppInfo.SaveAppInfo(appInfo);
                }
                return appInfo.TargetProjectPath;
            }
            set
            {
                var appInfo = AppInfo.GetAppInfo();
                appInfo.TargetProjectPath = value;
                AppInfo.SaveAppInfo(appInfo);
            }
        }
        
        [Argument]
        public string ChecksumToolPath
        {
            get
            {
                var appInfo = AppInfo.GetAppInfo();
                if (string.IsNullOrWhiteSpace(appInfo.ChecksumToolPath))
                {
                    appInfo.ChecksumToolPath = App.Console.Read("Checksum path: ");
                    AppInfo.SaveAppInfo(appInfo);
                }
                return appInfo.ChecksumToolPath;
            }
            set
            {
                var appInfo = AppInfo.GetAppInfo();
                appInfo.ChecksumToolPath = value;
                AppInfo.SaveAppInfo(appInfo);
            }
        }

        #endregion

        #region Properties (normal)

        public string TargetProjectDirectory
        {
            get
            {
                return Path.GetDirectoryName(Utils.GetFullPath(this, TargetProjectPath));
            }
        }

        public string TargetProjectAssemblyInfoPath
        {
            get
            {
                return Path.Combine(TargetProjectDirectory, _targetProjectAssemblyInfoPath);
            }
        }

        #endregion

        public PathCommand()
        {
            this.OnlyPropertiesWithAttribute = true;
            this.OnlyMethodsWithAttribute = true;
        }

        public static string GetOutputAppDirectory(string config)
        {
            return Path.Combine(BuildDirectory, config, _appDirectory);
        }
    }
}
