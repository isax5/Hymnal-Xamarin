using System;
using Helpers;
using Hymnal.XF.Models.Events;
using Hymnal.XF.Resources.Theme;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Hymnal.XF.Helpers
{
    public class ThemeHelper
    {
        private readonly App app;

        public AppTheme CurrentTheme;

        public ResourceDictionary CurrentResourceDictionaryTheme => CurrentTheme switch
        {
            AppTheme.Dark => DarkTheme,
            _ => LightTheme,
        };

        private DarkTheme darkTheme;
        public DarkTheme DarkTheme
        {
            get
            {
                if (darkTheme is not null)
                    return darkTheme;

                darkTheme = new();
                return darkTheme;
            }
        }

        private LightTheme lightTheme;
        public LightTheme LightTheme
        {
            get
            {
                if (lightTheme is not null)
                    return lightTheme;

                lightTheme = new();
                return lightTheme;
            }
        }

        #region Events
        public event EventHandler<AppThemeRequestedEventArgs> RequestedThemeChanged;
        public ObservableValue<AppThemeRequestedEventArgs> ObservableThemeChange = new(false);
        #endregion

        public ThemeHelper(App app)
        {
            this.app = app;

            // Configure theme
            CurrentTheme = AppInfo.RequestedTheme;
            performThemeActions(CurrentTheme);

            app.RequestedThemeChanged += App_RequestedThemeChanged;
        }

        private void App_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            if ((int)e.RequestedTheme == (int)CurrentTheme)
                return;

            CurrentTheme = (AppTheme)(int)e.RequestedTheme;

            performThemeActions(CurrentTheme);

            var eventParam = new AppThemeRequestedEventArgs(e.RequestedTheme, CurrentResourceDictionaryTheme);
            RequestedThemeChanged?.Invoke(this, eventParam);
            ObservableThemeChange.NextValue(eventParam);
        }

        private void performThemeActions(AppTheme appTheme)
        {
            switch (appTheme)
            {
                case AppTheme.Dark:
                    manuallyCopyThemesTo(app.Resources, DarkTheme);

                    break;

                case AppTheme.Light:
                case AppTheme.Unspecified:
                default:
                    manuallyCopyThemesTo(app.Resources, LightTheme);
                    break;
            }
        }

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
