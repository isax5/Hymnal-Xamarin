using Hymnal.Resources.Styles;

namespace Hymnal.Helpers;

public sealed class ThemeHelper
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
        if (e.RequestedTheme == CurrentTheme)
            return;

        CurrentTheme = e.RequestedTheme;

        performThemeActions(CurrentTheme);
    }

    private void performThemeActions(AppTheme appTheme)
    {
        switch (appTheme)
        {
            case AppTheme.Dark:
                ManuallyCopyThemesTo(app.Resources, DarkTheme);
                break;

            case AppTheme.Light:
            case AppTheme.Unspecified:
            default:
                ManuallyCopyThemesTo(app.Resources, LightTheme);
                break;
        }
    }

    private static void ManuallyCopyThemesTo(ResourceDictionary toResource, ResourceDictionary fromResource)
    {
        lock (toResource)
        {
            foreach (var item in fromResource.Keys)
                toResource[item] = fromResource[item];
        }
    }
}
