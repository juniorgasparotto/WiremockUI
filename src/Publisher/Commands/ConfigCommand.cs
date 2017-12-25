using Publisher.Core;
using SysCommand.ConsoleApp;
using SysCommand.ConsoleApp.Files;

namespace Publisher
{
    public class ConfigCommand : Command
    {
        public ConfigCommand()
        {
            this.UsePrefixInAllMethods = true;
        }

        public void Set(string name, string packName)
        {
            var appInfo = AppInfo.GetAppInfo();
            var exists = appInfo.GetConfiguration(name);

            if (exists == null)
            { 
                appInfo.Configurations.Add(new AppInfo.ConfigurationInfo
                {
                    Name = name,
                    PackName = packName
                });
            }
            else
            {
                exists.Name = name;
                exists.PackName = packName;
            }

            AppInfo.SaveAppInfo(appInfo);
        }

        public void Delete(string name)
        {
            var appInfo = AppInfo.GetAppInfo();
            var exists = appInfo.GetConfiguration(name);

            if (exists != null)
            {
                appInfo.Configurations.Remove(exists);
                AppInfo.SaveAppInfo(appInfo);
            }
        }

        public void Remove(string name)
        {
            var appInfo = AppInfo.GetAppInfo();
            appInfo.Configurations.Remove(appInfo.GetConfiguration(name, true));
            AppInfo.SaveAppInfo(appInfo);
        }

        public string Show()
        {
            return JsonFileManager.GetContentJsonFromObject(AppInfo.GetAppInfo().Configurations);
        }

        public string Show(string name)
        {
            return JsonFileManager.GetContentJsonFromObject(AppInfo.GetAppInfo().GetConfiguration(name));
        }
    }
}
