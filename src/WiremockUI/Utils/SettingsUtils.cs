using System.Globalization;
using System.Linq;
using WiremockUI.Data;

namespace WiremockUI
{
    public static class SettingsUtils
    {
        public static Settings GetSettings()
        {
            return new UnitOfWork()
                .Settings
                .GetAll()
                .FirstOrDefault();
        }

        public static void SaveSettings(Settings settings)
        {
            var db = new UnitOfWork();
            db.Settings.Update(settings);
            db.Save();
        }

        public static void SetLanguage(string code)
        {
            var s = GetSettings();
            s.DefaultLanguage = code;
            SaveSettings(s);
            ChangeUILanguage(code);
        }

        public static void ChangeUILanguage(string code)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo(code);
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo(code);
        }
    }
}
