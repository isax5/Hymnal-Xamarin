using System.Collections.Generic;
using Hymnal.Core.Services;
using Microsoft.AppCenter.Analytics;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Essentials;

namespace Hymnal.Core.ViewModels
{
    public class RootViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;
        private readonly IMvxLog log;
        private readonly IPreferencesService preferencesService;

        public RootViewModel(
            IMvxNavigationService navigationService,
            IMvxLog log,
            IPreferencesService preferencesService
            )
        {
            this.navigationService = navigationService;
            this.log = log;
            this.preferencesService = preferencesService;
        }

        private bool loaded = false;
        public override async void ViewAppearing()
        {
            base.ViewAppearing();

            if (loaded)
                return;

            loaded = true;

            // KeepScreenOn
            DeviceDisplay.KeepScreenOn = preferencesService.KeepScreenOn;

#if __IOS__ || __ANDROID__
            await navigationService.Navigate<NumberViewModel>();
            await navigationService.Navigate<IndexViewModel>();
            await navigationService.Navigate<FavoritesViewModel>();
            await navigationService.Navigate<SettingsViewModel>();
#elif __TVOS__
            // Native project, RootViewController
            await navigationService.Navigate<NumberViewModel>();
            await navigationService.Navigate<SearchViewModel>();
            await navigationService.Navigate<NumericalIndexViewModel>();
            await navigationService.Navigate<SettingsViewModel>();
#elif TIZEN
            await navigationService.Navigate<NumberViewModel>();
            await navigationService.Navigate<SearchViewModel>();
            await navigationService.Navigate<SettingsViewModel>();
#else
            await navigationService.Navigate<SimpleViewModel>();
#endif
        }

        // LifeCycle implemented in RootViewModel
        #region LifeCycle
        public override void Start()
        {
            log.Debug("App Started");

#if __IOS__ || __ANDROID__
            Analytics.TrackEvent(Constants.TrackEvents.AppOpened, new Dictionary<string, string>
            {
                { Constants.TrackEvents.AppOpenedScheme.CultureInfo, Constants.CurrentCultureInfo.Name },
                { Constants.TrackEvents.AppOpenedScheme.HymnalVersion, preferencesService.ConfiguratedHymnalLanguage.Id },
                { Constants.TrackEvents.AppOpenedScheme.ThemeConfigurated, AppInfo.RequestedTheme.ToString() }
            });
#endif

            base.Start();
        }
#endregion
    }
}
