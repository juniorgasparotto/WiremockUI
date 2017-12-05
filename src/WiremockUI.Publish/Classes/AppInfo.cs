using SysCommand.ConsoleApp.Files;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WiremockUI.Publish
{
    public class AppInfo
    {
        public static AppInfo _appInfo;
        public List<ConfigurationInfo> Configurations;
        public string MsBuildPath { get; set; }
        public string GitHubUrl { get; set; }
        public TimeSpan? GitHubTimeout { get; set; }
        public string TargetProjectPath { get; set; }
        public string GitHubUserName { get; set; }
        public string GitHubRepositoryName { get; set; }

        public AppInfo()
        {
            Configurations = new List<ConfigurationInfo>();
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

        //public class VersionInfo
        //{
        //    public int Major { get; set; }
        //    public int Minor { get; set; }
        //    public int Build { get; set; }
        //    public int? Revision { get; set; }

        //    public string GetVersion()
        //    {
        //        var major = Major.ToString();
        //        var minor = Minor.ToString();
        //        var build = Build.ToString();
        //        var revision = Revision.ToString();
        //        var version = string.Format("v{0}.{1}.{2}", major, minor, build);
        //        return version;
        //    }

        //    public void Set(int? major = null, int? minor = null, int? build = null, int? revision = null)
        //    {
        //        Major = major ?? Major;
        //        Minor = minor ?? Minor;
        //        Build = build ?? Build;
        //        Revision = revision ?? Revision;
        //    }
        //}
    }
}