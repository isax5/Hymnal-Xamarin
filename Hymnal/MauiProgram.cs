using CommunityToolkit.Maui;
using Hymnal.Resources.Languages;
using Microsoft.Extensions.Logging;

namespace Hymnal;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        TranslateExtension.Configure(LanguageResources.ResourceManager);

        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        /**
         * Transient objects are always different; a new instance is provided to every controller and every service.
         * Scoped objects are the same within a request, but different across different requests.
         * Singleton objects are the same for every object and every request.
         */

        #region Services
        builder.Services.AddSingleton<IPreferences>(e => Preferences.Default);

        builder.Services.AddSingleton<PreferencesService>();
        #endregion

        #region Views and ViewModels
        // Tabs
        builder.Services
            .AddSingleton<NumberViewModel>().AddSingleton<NumberPage>()
            .AddSingleton<AlphabeticalIndexPage>().AddSingleton<AlphabeticalIndexViewModel>()
            .AddSingleton<NumericalIndexPage>().AddSingleton<NumericalIndexViewModel>()
            .AddSingleton<ThematicIndexPage>().AddSingleton<ThematicIndexViewModel>()
            .AddSingleton<FavoritesPage>().AddSingleton<FavoritesViewModel>()
            .AddSingleton<SettingsPage>().AddSingleton<SettingsViewModel>()

        // Viewers
            .AddTransient<HymnPage>().AddTransient<HymnViewModel>()
            .AddTransient<MusicSheetPage>().AddTransient<MusicSheetViewModel>();
        #endregion

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
