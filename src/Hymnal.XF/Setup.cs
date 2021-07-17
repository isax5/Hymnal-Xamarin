using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Hymnal.XF.Extensions.i18n;
using Hymnal.XF.Helpers;
using Hymnal.XF.Models;
using Hymnal.XF.Services;
using Prism.Ioc;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;

namespace Hymnal.XF
{
    public class Setup
    {
        public void InitializeFirstChance(App app)
        {
            TranslateExtension.CurrentCultureInfo = CultureInfo.InstalledUICulture;
        }

        public void InitializeLastChance(App app)
        {
            app.ThemeHelper = new ThemeHelper(app);
            // cosas de configuración de los himnos, etc...

            // Configurating language of the hymnals
            var preferencesService = app.Container.Resolve(typeof(IPreferencesService)) as IPreferencesService;
            if (preferencesService.ConfiguratedHymnalLanguage == null)
            {
                List<HymnalLanguage> lngs = Constants.Constants.HymnsLanguages.FindAll(l => l.TwoLetterISOLanguageName == Constants.Constants.CurrentCultureInfo.TwoLetterISOLanguageName);
                preferencesService.ConfiguratedHymnalLanguage = lngs.Count == 0 ? Constants.Constants.HymnsLanguages.First() : lngs.First();
            }
        }

        public void RegisterDependencies(IContainerRegistry containerRegistry)
        {
            containerRegistry
                .RegisterSingleton<IAppInfo, AppInfoImplementation>()
                .RegisterSingleton<IConnectivity, ConnectivityImplementation>()
                .RegisterSingleton<IPreferences, PreferencesImplementation>()

                .RegisterSingleton<IDataStorageService, DataStorageService>()
                .RegisterSingleton<IStorageManagerService, StorageManagerService>()
                .RegisterSingleton<IAssetsService, AssetsService>()
                .RegisterSingleton<IFilesService, FilesService>()
                .RegisterSingleton<IHymnsService, HymnsService>()
                .RegisterSingleton<IPreferencesService, PreferencesService>();
        }
    }
}
