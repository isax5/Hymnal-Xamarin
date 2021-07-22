using Prism.Navigation;

namespace Hymnal.XF.ViewModels
{
    public class RootViewModel : BaseViewModel
    {
        //private readonly IPreferencesService preferencesService;

        public RootViewModel(
            INavigationService navigationService
            //IPreferencesService preferencesService
            ) : base(navigationService)
        {
            //this.preferencesService = preferencesService;
        }

        // LifeCycle implemented in RootViewModel
        //public override void Start()
        //{
        //    Log.LogInformation("App Started");

        //    if (DeviceInfo.Platform == DevicePlatform.iOS ||
        //        DeviceInfo.Platform == DevicePlatform.Android)
        //    {
        //        Analytics.TrackEvent(Constants.TrackEv.AppOpened, new Dictionary<string, string>
        //        {
        //            { Constants.TrackEv.AppOpenedScheme.CultureInfo, Constants.CurrentCultureInfo.Name },
        //            { Constants.TrackEv.AppOpenedScheme.HymnalVersion, preferencesService.ConfiguratedHymnalLanguage.Id },
        //            { Constants.TrackEv.AppOpenedScheme.ThemeConfigurated, AppInfo.RequestedTheme.ToString() }
        //        });
        //    }

        //    base.Start();
        //}
    }
}
