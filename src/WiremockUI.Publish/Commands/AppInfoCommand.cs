using SysCommand.ConsoleApp;
using SysCommand.ConsoleApp.Files;

namespace WiremockUI.Publish
{
    public class AppInfoCommand : Command
    {
        public string AppInfo()
        {
            var appInfo = Publish.AppInfo.GetAppInfo();
            return JsonFileManager.GetContentJsonFromObject(appInfo);
        }
    }
}
