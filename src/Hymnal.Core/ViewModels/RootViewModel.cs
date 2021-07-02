using System.Collections.Generic;
using Hymnal.Core.Services;
using Microsoft.AppCenter.Analytics;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Essentials;

namespace Hymnal.Core.ViewModels
{
    public class RootViewModel : MvxNavigationViewModel
    {
        private readonly IPreferencesService preferencesService;

        public RootViewModel(
            IMvxLogProvider logProvider,
            IMvxNavigationService navigationService,
            IMvxLog log,
            IPreferencesService preferencesService
            ) : base(logProvider, navigationService)
        {
            this.preferencesService = preferencesService;
        }

        private bool loaded = false;
        public override async void ViewAppearing()
        {
            if (loaded)
                return;

            loaded = true;

            // KeepScreenOn
            if (DeviceInfo.Platform == DevicePlatform.iOS ||
                DeviceInfo.Platform == DevicePlatform.Android ||
                DeviceInfo.Platform == DevicePlatform.UWP)
            {
                DeviceDisplay.KeepScreenOn = preferencesService.KeepScreenOn;
            }


            if (DeviceInfo.Platform == DevicePlatform.iOS ||
                DeviceInfo.Platform == DevicePlatform.Android)
            {
                await NavigationService.Navigate<NumberViewModel>();
                await NavigationService.Navigate<IndexViewModel>();
                await NavigationService.Navigate<FavoritesViewModel>();
                await NavigationService.Navigate<SettingsViewModel>();
            }
            else if (DeviceInfo.Platform == DevicePlatform.tvOS)
            {
                await NavigationService.Navigate<NumberViewModel>();
                await NavigationService.Navigate<SearchViewModel>();
                await NavigationService.Navigate<NumericalIndexViewModel>();
                await NavigationService.Navigate<SettingsViewModel>();
            }
            else if (DeviceInfo.Platform == DevicePlatform.Tizen)
            {
                await NavigationService.Navigate<NumberViewModel>();
                await NavigationService.Navigate<SearchViewModel>();
                //await navigationService.Navigate<SettingsViewModel>();
            }
            else if (DeviceInfo.Platform == DevicePlatform.UWP)
            {
                await NavigationService.Navigate<NumberViewModel>();
            }
            else
            {
                await NavigationService.Navigate<SimpleViewModel>();
            }

            base.ViewAppearing();
        }

        // LifeCycle implemented in RootViewModel
        #region LifeCycle
        public override void Start()
        {
            Log.Debug("App Started");

            if (DeviceInfo.Platform == DevicePlatform.iOS ||
                DeviceInfo.Platform == DevicePlatform.Android)
            {
                Analytics.TrackEvent(Constants.TrackEv.AppOpened, new Dictionary<string, string>
                {
                    { Constants.TrackEv.AppOpenedScheme.CultureInfo, Constants.CurrentCultureInfo.Name },
                    { Constants.TrackEv.AppOpenedScheme.HymnalVersion, preferencesService.ConfiguratedHymnalLanguage.Id },
                    { Constants.TrackEv.AppOpenedScheme.ThemeConfigurated, AppInfo.RequestedTheme.ToString() }
                });
            }

            base.Start();
        }
        #endregion
    }
}
