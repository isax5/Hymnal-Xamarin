using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Hymnal.XF.Resources.Theme
{
    public static class ThemeHelper
    {
        private static AppTheme _currentTheme = AppTheme.Unspecified;
        private static bool _themeConfigurated = false;

        public static void CheckTheme()
        {
            if (!_themeConfigurated)
            {
                Debug.WriteLine("==================================== CONFIGURATING THEME FOR FIRST TIME ====================================");

                switch (AppInfo.RequestedTheme)
                {
                    case AppTheme.Dark:
                        App.Current.Resources.MergedDictionaries.Add(new DarkTheme());
                        break;

                    case AppTheme.Light:
                    case AppTheme.Unspecified:
                    default:
                        App.Current.Resources.MergedDictionaries.Add(new LightTheme());
                        break;
                }

                _themeConfigurated = true;
            }
            else if (_currentTheme != AppInfo.RequestedTheme)
            {
                Debug.WriteLine("==================================== CONFIGURATING THEME ====================================");

                switch (AppInfo.RequestedTheme)
                {
                    case AppTheme.Dark:
                        ManuallyCopyThemes(new DarkTheme(), App.Current.Resources);
                        break;

                    case AppTheme.Light:
                    case AppTheme.Unspecified:
                    default:
                        ManuallyCopyThemes(new LightTheme(), App.Current.Resources);
                        break;
                }
            }

            _currentTheme = AppInfo.RequestedTheme;
        }

        public static void ManuallyCopyThemes(ResourceDictionary fromResource, ResourceDictionary toResource)
        {
            lock (toResource)
            {
                foreach (var item in fromResource.Keys)
                {
                    toResource[item] = fromResource[item];
                }
            }
        }
    }
}
