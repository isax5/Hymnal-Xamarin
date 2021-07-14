using System.Threading.Tasks;
using Hymnal.XF.Extensions;
using Hymnal.XF.Models;
using Hymnal.XF.Models.Parameter;
using Hymnal.XF.Services;
using Microsoft.AppCenter.Analytics;
using Microsoft.Extensions.Logging;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.XF.ViewModels
{
    public class PlayerViewModel : BaseViewModel<HymnIdParameter>
    {
        private readonly INavigationService navigationService;
        private readonly IHymnsService hymnsService;
        private readonly IPreferencesService preferencesService;
        private readonly ILogger<PlayerViewModel> logger;

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
            IPreferencesService preferencesService,
            ILogger<PlayerViewModel> logger
            )
        {
            this.navigationService = navigationService;
            this.hymnsService = hymnsService;
            this.preferencesService = preferencesService;
            this.logger = logger;
        }

        public override void Prepare(HymnIdParameter parameter)
        {
            HymnId = parameter;
            Language = parameter.HymnalLanguage;
        }

        public override Task Initialize()
        {
            return base.Initialize();
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();

            logger.Log(LogLevel.Information, $"Player open: {HymnId} of {Language.Id}");

            // TODO: Check Track event player (Try to no make differences in the sended data for this log related to the previous one)
            //Analytics.TrackEvent(Constants.TrackEvents.HymnMusicPlayed, new Dictionary<string, string>
            //{
            //    { Constants.TrackEvents.HymnReferenceScheme.Number, HymnId.Number.ToString() },
            //    { Constants.TrackEvents.HymnReferenceScheme.HymnalVersion, Language.Id },
            //    { Constants.TrackEvents.HymnReferenceScheme.CultureInfo, Constants.CurrentCultureInfo.Name },
            //    { Constants.TrackEvents.HymnReferenceScheme.Time, DateTime.Now.ToLocalTime().ToString() }
            //});
        }


#region Commands
        public MvxAsyncCommand CloseCommand => new MvxAsyncCommand(CloseAsync);
        private async Task CloseAsync()
        {
            await navigationService.Close(this);
        }
#endregion
    }
}
