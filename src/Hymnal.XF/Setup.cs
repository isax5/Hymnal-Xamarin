using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Hymnal.AzureFunctions.Client;
using Hymnal.AzureFunctions.Models;
using Hymnal.XF.Constants;
using Hymnal.XF.Extensions;
using Hymnal.XF.Helpers;
using Hymnal.XF.Models;
using Hymnal.XF.Services;
using MediaManager;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Prism.Ioc;
using Xamarin.Essentials;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;

namespace Hymnal.XF
{
    public sealed class Setup
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

            #region Restore preferences

            var preferencesService = app.Container.Resolve(typeof(IPreferencesService)) as IPreferencesService;

            // Update Last AppVersion Opened
            bool firstVersionOpened = preferencesService.LastVersionOpened.Equals(AppInfo.VersionString);
            preferencesService.LastVersionOpened = AppInfo.VersionString;

            // Last hymnbook opened
            if (preferencesService is { ConfiguredHymnalLanguage: null })
            {
                List<HymnalLanguage> lngs = InfoConstants.HymnsLanguages.FindAll(l =>
                    l.TwoLetterIsoLanguageName == InfoConstants.CurrentCultureInfo.TwoLetterISOLanguageName);
                preferencesService.ConfiguredHymnalLanguage =
                    lngs.Count == 0 ? InfoConstants.HymnsLanguages.First() : lngs.First();
            }

            // Not needed for startup
            Task.Run(() => MainThread.BeginInvokeOnMainThread(delegate
            {
                var dataStorageService = app.Container.Resolve(typeof(IDataStorageService)) as IDataStorageService;
                var azureHymnService = app.Container.Resolve(typeof(IAzureHymnService)) as IAzureHymnService;

                // Keep Screen on
                if (DeviceInfo.Platform == DevicePlatform.Android
                    || DeviceInfo.Platform == DevicePlatform.iOS
                    || DeviceInfo.Platform == DevicePlatform.tvOS
                    || DeviceInfo.Platform == DevicePlatform.UWP
                    || DeviceInfo.Platform == DevicePlatform.watchOS)
                {
                    DeviceDisplay.KeepScreenOn = preferencesService.KeepScreenOn;
                }

                // Last Azure values downloaded
                if (preferencesService.OpeningCounter % 5 != 0 &&
                    dataStorageService.GetItems<HymnSettingsResponse>() is IList<HymnSettingsResponse> data &&
                    data.Any())
                {
                    azureHymnService.SetNextValues(data);
                }
                else
                {
                    var hymnSettings = new List<HymnSettingsResponse>();
                    azureHymnService.ObserveSettings()
                        .Finally(() =>
                        {
                            if (hymnSettings.Any())
                                dataStorageService.SetItems(hymnSettings);
                        })
                        .Subscribe(hymnSetting => hymnSettings.Add(hymnSetting));
                }

                // Increase opening counter
                preferencesService.OpeningCounter += 1;
            }));

            #endregion

            #region AppCenter Tracking

            // Doc: https://docs.microsoft.com/en-us/appcenter/sdk/getting-started/xamarin#423-xamarinforms
            if (DeviceInfo.Platform == DevicePlatform.iOS ||
                DeviceInfo.Platform == DevicePlatform.Android)
            {
#if RELEASE
                AppCenter.Start("ios=d636d723-86a7-4d3a-8f02-cfdd454df9af;android=2ded5d95-4218-4a32-893f-1db17c0004a6;uwp={YourAppSecret}",
                    typeof(Analytics), typeof(Crashes));
#elif DEBUG
                AppCenter.Start(
                    "ios=b3d6dce3-971c-40cf-aa5f-e40979e7fb7a;android=d3f0ef03-acc8-450b-b028-6fb74ddd98c5;uwp={YourAppSecret}",
                    typeof(Analytics), typeof(Crashes));
#endif
            }
            else
            {
                // Disable all AppCenter Services at runtime
                // https://docs.microsoft.com/en-us/appcenter/sdk/other-apis/xamarin#disable-all-services-at-runtime
                AppCenter.SetEnabledAsync(false);
            }

            #endregion
        }

        public void RegisterDependencies(IContainerRegistry containerRegistry)
        {
            containerRegistry
                .RegisterSingleton<IMainThread, MainThreadImplementation>()
                .RegisterSingleton<IAppInfo, AppInfoImplementation>()
                .RegisterSingleton<IDeviceInfo, DeviceInfoImplementation>()
                .RegisterSingleton<IDeviceDisplay, DeviceDisplayImplementation>()
                .RegisterSingleton<IConnectivity, ConnectivityImplementation>()
                .RegisterSingleton<IPreferences, PreferencesImplementation>()
                .RegisterSingleton<IShare, ShareImplementation>()
                .RegisterSingleton<IBrowser, BrowserImplementation>()
                .RegisterInstance<IMediaManager>(CrossMediaManager.Current)
                .RegisterInstance<IAzureHymnService>(AzureHymnService.Current)
                .RegisterSingleton<IDataStorageService, DataStorageService>()
                .RegisterSingleton<IStorageManagerService, StorageManagerService>()
                .RegisterSingleton<IAssetsService, AssetsService>()
                .RegisterSingleton<IFilesService, FilesService>()
                .RegisterSingleton<IHymnsService, HymnsService>()
                .RegisterSingleton<IPreferencesService, PreferencesService>()
                .RegisterSingleton<IDataStorageService, DataStorageService>()
                ;
        }
    }
}
