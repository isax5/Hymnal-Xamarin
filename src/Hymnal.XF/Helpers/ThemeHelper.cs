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
            switch (AppInfo.RequestedTheme)
            {
                case AppTheme.Dark:
                    break;

                case AppTheme.Light:
                case AppTheme.Unspecified:
                default:
                    break;
            }

            app.RequestedThemeChanged += App_RequestedThemeChanged;
        }

        private void App_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            app.MainPage.DisplayAlert("Tema cambiado", $"Tema cambiado a {e.RequestedTheme}", "Enterado");
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

        //public static void ManuallyCopyThemes(ResourceDictionary fromResource, ResourceDictionary toResource)
        //{
        //    lock (toResource)
        //    {
        //        foreach (var item in fromResource.Keys)
        //        {
        //            toResource[item] = fromResource[item];
        //        }
        //    }
        //}
    }
}
