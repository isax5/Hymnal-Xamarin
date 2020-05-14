using System.Diagnostics;
using Hymnal.Core.Models;
using Hymnal.Core.Services;
using MvvmCross;
using Xamarin.Forms;

namespace Hymnal.XF.UI.Resources
{
    public static class ThemeHelper
    {
        public static Theme CurrentTheme = Theme.Unspecified;
        private static bool themeConfigurated = false;

        public static void CheckTheme()
        {
            IAppInformationService appInformationService = Mvx.IoCProvider.Resolve<IAppInformationService>();

            if (!themeConfigurated)
            {
                Debug.WriteLine("==================================== CONFIGURATING THEME FOR FIRST TIME ====================================");

                switch (appInformationService.RequestedTheme)
                {
                    case Theme.Dark:
                        App.Current.Resources.MergedDictionaries.Add(new DarkTheme());
                        break;

                    case Theme.Light:
                    case Theme.Unspecified:
                    default:
                        App.Current.Resources.MergedDictionaries.Add(new LightTheme());
                        break;
                }

                themeConfigurated = true;
            }
            else if (CurrentTheme != appInformationService.RequestedTheme)
            {
                Debug.WriteLine("==================================== CONFIGURATING THEME ====================================");

                switch (appInformationService.RequestedTheme)
                {
                    case Theme.Dark:
                        ManuallyCopyThemes(new DarkTheme(), App.Current.Resources);
                        break;

                    case Theme.Light:
                    case Theme.Unspecified:
                    default:
                        ManuallyCopyThemes(new LightTheme(), App.Current.Resources);
                        break;
                }
            }

            CurrentTheme = appInformationService.RequestedTheme;
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
