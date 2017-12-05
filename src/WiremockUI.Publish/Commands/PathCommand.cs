using SysCommand.ConsoleApp;
using SysCommand.ConsoleApp.Helpers;
using SysCommand.Mapping;
using System.IO;

namespace WiremockUI.Publish
{
    public class PathCommand : Command
    {
        #region Constants
        private const string _targetProjectAssemblyInfoPath = @"Properties\AssemblyInfo.cs";
        private const string _outputDirectory = "Output";
        private const string _buildDirectory = "Build"; 
        private const string _packDirectory = "Packs";
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

        public static string PackDirectory
        {
            get
            {
                return Path.Combine(OutputDirectory, _packDirectory);
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
        
        public string TargetProjectDirectory
        {
            get
            {
                return Path.GetDirectoryName(TargetProjectPath);
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
