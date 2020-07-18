using System.Diagnostics;
using Hymnal.Core.Models;
using Hymnal.Core.Services;
using MvvmCross;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Hymnal.XF.UI.Resources
{
    public static class ThemeHelper
    {
        public static AppTheme CurrentTheme = AppTheme.Unspecified;
        private static bool themeConfigurated = false;

        public static void CheckTheme()
        {
            if (!themeConfigurated)
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

                themeConfigurated = true;
            }
            else if (CurrentTheme != AppInfo.RequestedTheme)
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

            CurrentTheme = AppInfo.RequestedTheme;
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
