using System.Globalization;

namespace WiremockUI
{
    public static class LanguageUtils
    {
        public static void ChangeLanguage(string code)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo(code);
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo(code);
        }
    }
}
