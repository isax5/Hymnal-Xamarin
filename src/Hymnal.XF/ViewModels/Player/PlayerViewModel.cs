using Hymnal.XF.Models;
using Hymnal.XF.Models.Parameters;
using Hymnal.XF.Services;
using Prism.Commands;
using Prism.Navigation;

namespace Hymnal.XF.ViewModels
{
    public class PlayerViewModel : BaseViewModelParameter<HymnIdParameter>
    {
        private readonly IHymnsService hymnsService;
        private readonly IPreferencesService preferencesService;

        private HymnIdParameter hymn;
        public HymnIdParameter HymnId
        {
            get => hymn;
            set => SetProperty(ref hymn, value);
        }

        private HymnalLanguage Language;


        public PlayerViewModel(
            INavigationService navigationService,
            IHymnsService hymnsService,
            IPreferencesService preferencesService
            ) : base(navigationService)
        {
            this.hymnsService = hymnsService;
            this.preferencesService = preferencesService;

            CloseCommand = new DelegateCommand(CloseAsync).ObservesCanExecute(() => NotBusy);
        }

        public override void OnNavigatedTo(INavigationParameters parameters, HymnIdParameter parameter)
        {
            base.OnNavigatedTo(parameters, parameter);
            HymnId = parameter;
            Language = parameter.HymnalLanguage;
        }

        //public override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    //TODO: Check Track event player (Try to no make differences in the sended data for this log related to the previous one)
        //    Analytics.TrackEvent(Constants.TrackEvents.HymnMusicPlayed, new Dictionary<string, string>
        //    {
        //        { Constants.TrackEvents.HymnReferenceScheme.Number, HymnId.Number.ToString()
        //        },
        //        { Constants.TrackEvents.HymnReferenceScheme.HymnalVersion, Language.Id
        //        },
        //        { Constants.TrackEvents.HymnReferenceScheme.CultureInfo, Constants.CurrentCultureInfo.Name },
        //        { Constants.TrackEvents.HymnReferenceScheme.Time, DateTime.Now.ToLocalTime().ToString() }
        //    });
        //}


        #region Commands
        public DelegateCommand CloseCommand;
        private async void CloseAsync()
        {
            await NavigationService.GoBackAsync();
        }
        #endregion
    }
}
