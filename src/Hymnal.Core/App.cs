using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Hymnal.Core.Models;
using Hymnal.Core.Models.Parameter;
using Hymnal.Core.Resources;
using Hymnal.Core.Services;
using Hymnal.Core.ViewModels;
using MediaManager;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core
{
    public class App : MvxApplication
    {
        public static App Current;

        public App()
            : base()
        {
            Current = this;
        }


        public override void Initialize()
        {
#if __ANDROID__ || __IOS__
            SetUp();
#endif

            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

#if __ANDROID__ || __IOS__
            Mvx.IoCProvider.RegisterSingleton<IMediaManager>(CrossMediaManager.Current);

            RegisterAppStart<RootViewModel>();
#else
            RegisterAppStart<SimpleViewModel>();
#endif
        }

        private void SetUp()
        {
            // XF configuration
            // Doc: https://docs.microsoft.com/en-us/appcenter/sdk/getting-started/xamarin#423-xamarinforms
#if RELEASE
            AppCenter.Start("ios=d636d723-86a7-4d3a-8f02-cfdd454df9af;android=2ded5d95-4218-4a32-893f-1db17c0004a6;uwp={YourAppSecret}", typeof(Analytics), typeof(Crashes));
#elif DEBUG
            AppCenter.Start("ios=b3d6dce3-971c-40cf-aa5f-e40979e7fb7a;android=d3f0ef03-acc8-450b-b028-6fb74ddd98c5;uwp={YourAppSecret}", typeof(Analytics), typeof(Crashes));
#endif

            IMultilingualService multilingualService = Mvx.IoCProvider.Resolve<IMultilingualService>();
            IPreferencesService preferencesService = Mvx.IoCProvider.Resolve<IPreferencesService>();
            IAppInformationService appInformationService = Mvx.IoCProvider.Resolve<IAppInformationService>();

            // Configurating language of the device
            Constants.CurrentCultureInfo = multilingualService.DeviceCultureInfo;
            AppResources.Culture = Constants.CurrentCultureInfo;

            // Configurating language of the hymnals
            if (preferencesService.ConfiguratedHymnalLanguage == null)
            {
                List<HymnalLanguage> lngs = Constants.HymnsLanguages.FindAll(l => l.TwoLetterISOLanguageName == Constants.CurrentCultureInfo.TwoLetterISOLanguageName);

                if (lngs.Count == 0)
                {
                    preferencesService.ConfiguratedHymnalLanguage = Constants.HymnsLanguages.First();
                }
                else
                {
                    preferencesService.ConfiguratedHymnalLanguage = lngs.First();
                }
            }

            // Configurate First Time opening this version
            if (preferencesService.LastVersionOpened != appInformationService.VersionString)
            {
                preferencesService.LastVersionOpened = appInformationService.VersionString;

                // Do somethink for new version

            }
        }

#region Open Page as
        public void LaunchPage<TViewModel>() where TViewModel : IMvxViewModel
        {
            IMvxNavigationService navigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
            navigationService.Navigate<TViewModel>();
        }

        public void LaunchPage<TViewModel, TParameter>(TParameter parameter) where TViewModel : IMvxViewModel<TParameter>
        {
            IMvxNavigationService navigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
            navigationService.Navigate<TViewModel, TParameter>(parameter);
        }

        public void PerformAppLinkRequest(Uri uri)
        {
            var request = uri.ToString().Replace(Constants.AppLink.UriBase, string.Empty);

            if (!string.IsNullOrEmpty(request))
            {
                if (request.Equals(PageRequest.Search.ToString()))
                    LaunchPage<SearchViewModel>();

                if (request.Equals(PageRequest.Records.ToString()))
                    LaunchPage<RecordsViewModel>();

                if (request.Contains(PageRequest.Hymn.ToString()))
                {
                    IPreferencesService preferencesService = Mvx.IoCProvider.Resolve<IPreferencesService>();

                    LaunchPage<HymnViewModel, HymnIdParameter>(new HymnIdParameter
                    {
                        Number = 22,
                        HymnalLanguage = preferencesService.ConfiguratedHymnalLanguage
                    });

                }
            }
        }

        public void PerformPageRequest(PageRequest pageRequest)
        {
            switch (pageRequest)
            {
                case PageRequest.Records:
                    LaunchPage<RecordsViewModel>();
                    break;

                case PageRequest.Search:
                    LaunchPage<SearchViewModel>();
                    break;

                default:
                    Debug.Write($"Imposible to perform: {pageRequest}");
                    break;
            }
        }
#endregion
    }
}
