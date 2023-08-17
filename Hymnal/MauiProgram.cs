using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Views;
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
            .UseMauiCommunityToolkitMediaElement()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("fa-brands-400.ttf", "FABrands");
                fonts.AddFont("fa-regular-400.ttf", "FARegular");
                fonts.AddFont("fa-solid-900.ttf", "FASolid");
                fonts.AddFont("fa-v4compatibility.ttf", "FAComp");
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        /**
         * Transient objects are always different; a new instance is provided to every controller and every service.
         * Scoped objects are the same within a request, but different across different requests (makes sense in web environments).
         * Singleton objects are the same for every object and every request.
         */

        #region Services
        builder.Services
            .AddSingleton<IPreferences>(e => Preferences.Default)
            .AddSingleton<IDeviceInfo>(e => DeviceInfo.Current)
            .AddSingleton<IBrowser>(e => Browser.Default)
            .AddSingleton<IDeviceDisplay>(e => DeviceDisplay.Current)

            .AddSingleton<FilesService>()
            .AddSingleton<HymnsService>()
            .AddSingleton<PreferencesService>()
            .AddSingleton<DatabaseService>()

        // Setup
            .AddSingleton<Setup>()

            // As a service to share the same instance between all the pages and times it's needed
            .AddSingleton<MediaElement>((_) => new() { IsVisible = false });
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

            .AddTransient<RecordsPage>().AddSingleton<RecordsViewModel>()

        // Thematic
            .AddSingleton<ThematicSubGroupPage>().AddSingleton<ThematicSubGroupViewModel>()
            .AddTransient<ThematicHymnsListPage>().AddSingleton<ThematicHymnsListViewModel>()

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
