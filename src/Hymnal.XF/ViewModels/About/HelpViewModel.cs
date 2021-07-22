using Prism.Navigation;

namespace Hymnal.XF.ViewModels
{
    public class HelpViewModel : BaseViewModel
    {
        public HelpViewModel(INavigationService navigationService) : base(navigationService)
        { }

        public override void OnAppearing()
        {
            base.OnAppearing();

            //Analytics.TrackEvent(Constants.TrackEv.Navigation, new Dictionary<string, string>
            //{
            //    { Constants.TrackEv.NavigationReferenceScheme.PageName, nameof(HelpViewModel) },
            //    { Constants.TrackEv.NavigationReferenceScheme.CultureInfo, Constants.CurrentCultureInfo.Name },
            //    { Constants.TrackEv.NavigationReferenceScheme.HymnalVersion, preferencesService.ConfiguratedHymnalLanguage.Id }
            //});
        }
    }
}
