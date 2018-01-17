using SysCommand.ConsoleApp.Files;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Publisher.Core
{
    public class AppInfo
    {
        private static AppInfo _appInfo;

        public List<ConfigurationInfo> Configurations;
        public List<FileReplace> FilesReplace;
        public string TargetProjectPath { get; set; }

        public string MsBuildPath { get; set; }

        public string GitHubUrl { get; set; }
        public TimeSpan? GitHubTimeout { get; set; }
        public string GitHubUserName { get; set; }
        public string GitHubRepositoryName { get; set; }

        public string ChocolateyPath { get; set; }
        public string ChocolateyExeName { get; set; }
        public string ChocolateyPackageName { get; set; }
        public string ChocolateyTitle { get; set; }
        public string ChocolateyUrlDownload { get; set; }
        public string ChocolateyProjectUrl { get; set; }
        public string ChocolateyLicenceUrl { get; set; }
        public string ChocolateyIconUrl { get; set; }
        public string ChocolateyTags { get; set; }
        public string ChocolateyDescription { get; set; }
        public string ChocolateyAuthor { get; set; }
        public string ChocolateyPackageSourceUrl { get; set; }
        public string ChocolateyDocsUrl { get; set; }
        public string ChocolateyBugTrackerUrl { get; set; }

        public string GitPath { get; set; }
        public string ChecksumToolPath { get; set; }

        public AppInfo()
        {
            Configurations = new List<ConfigurationInfo>();
            FilesReplace = new List<FileReplace>();
        }

        public ConfigurationInfo GetConfiguration(string name, bool throwEx = false)
        {
            var config = Configurations.FirstOrDefault(f => f.Name.ToLower() == name.ToLower());
            if (config == null && throwEx)
                throw new NullReferenceException($"The configuration '{name}' doesn't exists.");
            return config;
        }

        public static AppInfo GetAppInfo(bool refresh = false)
        {
            if (_appInfo == null || refresh)
            {
                var fileManager = new JsonFileManager();
                _appInfo = fileManager.GetOrCreate<AppInfo>();
            }
            return _appInfo;
        }

        public static void SaveAppInfo(AppInfo appInfo)
        {
            var fileManager = new JsonFileManager();
            fileManager.Save(appInfo);
        }

        public class ConfigurationInfo
        {
            public string Name { get; set; }
            public string PackName { get; set; }
        }

        public class FileReplace
        {
            public string Path { get; set; }
            public string Pattern { get; set; }
            public string Replacement { get; set; }
            public string GroupName { get; set; }
        }
    }
}