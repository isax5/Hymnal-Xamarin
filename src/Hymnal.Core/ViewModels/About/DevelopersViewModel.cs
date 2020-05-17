using System.Collections.Generic;
using Hymnal.Core.Services;
using Microsoft.AppCenter.Analytics;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class DevelopersViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;
        private readonly IPreferencesService preferencesService;

        public DevelopersViewModel(
            IMvxNavigationService navigationService,
            IPreferencesService preferencesService
            )
        {
            this.navigationService = navigationService;
            this.preferencesService = preferencesService;
        }
        public override void ViewAppeared()
        {
            base.ViewAppeared();

            Analytics.TrackEvent(Constants.TrackEvents.Navigation, new Dictionary<string, string>
            {
                { Constants.TrackEvents.NavigationReferenceScheme.PageName, nameof(DevelopersViewModel) },
                { Constants.TrackEvents.NavigationReferenceScheme.CultureInfo, Constants.CurrentCultureInfo.Name },
                { Constants.TrackEvents.NavigationReferenceScheme.HymnalVersion, preferencesService.ConfiguratedHymnalLanguage.Id }
            });
        }
    }
}
