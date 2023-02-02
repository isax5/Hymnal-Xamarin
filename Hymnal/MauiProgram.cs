using CommunityToolkit.Maui;
using Hymnal.Resources.Languages;
using LocalizationResourceManager.Maui;
using Microsoft.Extensions.Logging;

namespace Hymnal;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>()
            .UseLocalizationResourceManager(settings =>
            {
                settings.AddResource(LanguageResources.ResourceManager);
            })
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

        builder.Services.AddSingleton<FilesService>();
        builder.Services.AddSingleton<HymnsService>();
        builder.Services.AddSingleton<PreferencesService>();
        builder.Services.AddSingleton<DatabaseService>();
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
