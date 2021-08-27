using System.Collections.Generic;
using Hymnal.XF.Constants;
using Hymnal.XF.Services;
using Microsoft.AppCenter.Analytics;
using Prism.Navigation;
using Xamarin.Essentials;

namespace Hymnal.XF.ViewModels
{
    public sealed class RootViewModel : BaseViewModel
    {
        private readonly IPreferencesService preferencesService;

        public RootViewModel(
            INavigationService navigationService,
            IPreferencesService preferencesService
            ) : base(navigationService)
        {
            this.preferencesService = preferencesService;
        }

        public override void OnAppearing()
        {
            base.OnAppearing();

            Analytics.TrackEvent(TrackingConstants.TrackEv.AppOpened, new Dictionary<string, string>
            {
                { TrackingConstants.TrackEv.AppOpenedScheme.CultureInfo, InfoConstants.CurrentCultureInfo.Name },
                { TrackingConstants.TrackEv.AppOpenedScheme.HymnalVersion, preferencesService.ConfiguratedHymnalLanguage.Id },
                { TrackingConstants.TrackEv.AppOpenedScheme.ThemeConfigurated, AppInfo.RequestedTheme.ToString() }
            });
        }
    }
}
