using Publisher;
using Publisher.Core;
using SysCommand.ConsoleApp;
using System;
using System.Collections.Generic;
using System.IO;
using WiremockUI.Data;

namespace Publisher
{
    public class PackWiremockUICommand : Command
    {
        public void Pack()
        {
            var configs = AppInfo.GetAppInfo().Configurations;
            foreach (var c in configs)
                Pack(c.Name);
        }

        public void Pack(string config)
        {
            try
            {
                App.Console.Write($"Start packing '{config}': {DateTime.Now}");

                #region 1) Create configs files
                CreateDbConfigDefault(config);
                #endregion

                #region 2) Clear unused files
                ClearUnused(config);
                #endregion

                #region 3) Zip
                Zip(config);
                #endregion
            }
            catch (Exception ex)
            {
                App.Console.Error(ex.Message);
            }
            finally
            {
                App.Console.Write($"End: {DateTime.Now}");
            }
        }

        #region

        private void CreateDbConfigDefault(string config)
        {
            const string _projectConfigFileName = @"db.json";
            const string _projectConfigSettingId = "b712d615-d1d1-4f7f-ac1c-c6c72e037c5e";
            const string _projectConfigDefaultLang = "en-us";

            UnitOfWork.ClearCache();
            var dbConfig = Path.Combine(PathCommand.AppDirectory, _projectConfigFileName);
            if (File.Exists(dbConfig))
                File.Delete(dbConfig);

            var db = new UnitOfWork();
            var settingid = new Guid(_projectConfigSettingId);
            var settings = new Settings()
            {
                DefaultLanguage = _projectConfigDefaultLang,
                Id = settingid,
                Languages = new Dictionary<string, string>
                {
                    {  "pt-br", "Portuguese" },
                    {  "en-us", "English" },
                }
            };
            db.Settings.Insert(settings);
            db.Save();

            var dbConfigOutput = Path.Combine(PathCommand.GetOutputAppDirectory(config), _projectConfigFileName);
            if (File.Exists(dbConfigOutput))
                File.Delete(dbConfigOutput);

            Utils.CreateFolderIfNeeded(dbConfigOutput);
            File.Move(dbConfig, dbConfigOutput);
        }

        private void ClearUnused(string config)
        {
            var listUnused = new string[]
            {
                "WiremockUI.application",
                "WiremockUI.exe.config",
                "WiremockUI.exe.manifest",
                "FastColoredTextBox.xml",
                "Newtonsoft.Json.xml",
                "System.ValueTuple.xml",
                "WiremockUI.pdb",
                ".old",
                ".old-test",
            };

            foreach (var name in listUnused)
            {
                var path = Path.Combine(PathCommand.BuildDirectory, config, name);
                if (File.Exists(path))
                    File.Delete(path);
                else if (Directory.Exists(path))
                    Directory.Delete(path, true);
            }
        }

        private void Zip(string config)
        {
            var appInfo = AppInfo.GetAppInfo();
            var configuration = appInfo.GetConfiguration(config, true);

            var source = Path.Combine(PathCommand.BuildDirectory, config);
            var destination = Path.Combine(PathCommand.GitHubPackDirectory, configuration.PackName);

            Utils.CreateFolderIfNeeded(destination);

            if (File.Exists(destination))
                File.Delete(destination);

            System.IO.Compression.ZipFile.CreateFromDirectory(source, destination);
        }

        #endregion
    }
}
