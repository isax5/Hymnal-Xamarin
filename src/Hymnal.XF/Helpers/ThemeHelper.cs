using Hymnal.XF.Resources.Theme;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Hymnal.XF.Helpers
{
    public class ThemeHelper
    {
        private readonly App app;
        public AppTheme CurrentTheme;

        public ThemeHelper(App app)
        {
            this.app = app;

            // Configure theme
            CurrentTheme = AppInfo.RequestedTheme;
            configureTheme(CurrentTheme);

            app.RequestedThemeChanged += App_RequestedThemeChanged;
        }

        private void App_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            if ((int)e.RequestedTheme == (int)CurrentTheme)
                return;

            CurrentTheme = (AppTheme)(int)e.RequestedTheme;
            configureTheme(CurrentTheme);
        }

        private void configureTheme(AppTheme appTheme)
        {
            switch (appTheme)
            {
                case AppTheme.Dark:
                    manuallyCopyThemesTo(app.Resources, new DarkTheme());
                    break;

                case AppTheme.Light:
                case AppTheme.Unspecified:
                default:
                    manuallyCopyThemesTo(app.Resources, new LightTheme());
                    break;
            }
        }

        //public static void CheckTheme()
        //{
        //    if (!_themeConfigurated)
        //    {
        //        Debug.WriteLine("==================================== CONFIGURATING THEME FOR FIRST TIME ====================================");

        //        switch (AppInfo.RequestedTheme)
        //        {
        //            case AppTheme.Dark:
        //                App.Current.Resources.MergedDictionaries.Add(new DarkTheme());
        //                break;

        //            case AppTheme.Light:
        //            case AppTheme.Unspecified:
        //            default:
        //                App.Current.Resources.MergedDictionaries.Add(new LightTheme());
        //                break;
        //        }

        //        _themeConfigurated = true;
        //    }
        //    else if (_currentTheme != AppInfo.RequestedTheme)
        //    {
        //        Debug.WriteLine("==================================== CONFIGURATING THEME ====================================");

        //        switch (AppInfo.RequestedTheme)
        //        {
        //            case AppTheme.Dark:
        //                ManuallyCopyThemes(new DarkTheme(), App.Current.Resources);
        //                break;

        //            case AppTheme.Light:
        //            case AppTheme.Unspecified:
        //            default:
        //                ManuallyCopyThemes(new LightTheme(), App.Current.Resources);
        //                break;
        //        }
        //    }

        //    _currentTheme = AppInfo.RequestedTheme;
        //}

        private void manuallyCopyThemesTo(ResourceDictionary toResource, ResourceDictionary fromResource)
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
