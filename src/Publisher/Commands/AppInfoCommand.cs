using SysCommand.ConsoleApp;
using SysCommand.ConsoleApp.Files;

namespace Publisher
{
    public class AppInfoCommand : Command
    {
        public string AppInfo()
        {
            var appInfo = Core.AppInfo.GetAppInfo();
            return JsonFileManager.GetContentJsonFromObject(appInfo);
        }
    }
}
