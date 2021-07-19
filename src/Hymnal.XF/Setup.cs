using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Hymnal.XF.Constants;
using Hymnal.XF.Extensions.i18n;
using Hymnal.XF.Helpers;
using Hymnal.XF.Models;
using Hymnal.XF.Services;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Prism.Ioc;
using Xamarin.Essentials;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;

namespace Hymnal.XF
{
    public class Setup
    {
        public void InitializeFirstChance(App app)
        {
            TranslateExtension.CurrentCultureInfo = CultureInfo.InstalledUICulture;
            InfoConstants.CurrentCultureInfo = CultureInfo.InstalledUICulture;
        }

        public void InitializeLastChance(App app)
        {
            // App Theme
            app.ThemeHelper = new ThemeHelper(app);

            // Hymnals Language
            var preferencesService = app.Container.Resolve(typeof(IPreferencesService)) as IPreferencesService;
            if (preferencesService.ConfiguratedHymnalLanguage == null)
            {
                List<HymnalLanguage> lngs = InfoConstants.HymnsLanguages.FindAll(l => l.TwoLetterISOLanguageName == InfoConstants.CurrentCultureInfo.TwoLetterISOLanguageName);
                preferencesService.ConfiguratedHymnalLanguage = lngs.Count == 0 ? InfoConstants.HymnsLanguages.First() : lngs.First();
            }

            // AppCenter Tracking
            // Doc: https://docs.microsoft.com/en-us/appcenter/sdk/getting-started/xamarin#423-xamarinforms
            if (DeviceInfo.Platform == DevicePlatform.iOS ||
                DeviceInfo.Platform == DevicePlatform.Android)
            {
#if RELEASE
                AppCenter.Start("ios=d636d723-86a7-4d3a-8f02-cfdd454df9af;android=2ded5d95-4218-4a32-893f-1db17c0004a6;uwp={YourAppSecret}", typeof(Analytics), typeof(Crashes));
#elif DEBUG
                AppCenter.Start("ios=b3d6dce3-971c-40cf-aa5f-e40979e7fb7a;android=d3f0ef03-acc8-450b-b028-6fb74ddd98c5;uwp={YourAppSecret}", typeof(Analytics), typeof(Crashes));
#endif
            }
            else
            {
                AppCenter.SetEnabledAsync(false);
            }
        }

        public void RegisterDependencies(IContainerRegistry containerRegistry)
        {
            containerRegistry
                .RegisterSingleton<IAppInfo, AppInfoImplementation>()
                .RegisterSingleton<IConnectivity, ConnectivityImplementation>()
                .RegisterSingleton<IPreferences, PreferencesImplementation>()
                .RegisterSingleton<IDeviceInfo, DeviceInfoImplementation>()

                .RegisterSingleton<IDataStorageService, DataStorageService>()
                .RegisterSingleton<IStorageManagerService, StorageManagerService>()
                .RegisterSingleton<IAssetsService, AssetsService>()
                .RegisterSingleton<IFilesService, FilesService>()
                .RegisterSingleton<IHymnsService, HymnsService>()
                .RegisterSingleton<IPreferencesService, PreferencesService>();
        }
    }
}
