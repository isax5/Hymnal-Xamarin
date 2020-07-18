using System.Threading.Tasks;
using Hymnal.Core.Extensions;
using Hymnal.Core.Models;
using Hymnal.Core.Models.Parameter;
using Hymnal.Core.Services;
using Microsoft.AppCenter.Analytics;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class PlayerViewModel : BaseViewModel<HymnIdParameter>
    {
        private readonly IMvxNavigationService navigationService;
        private readonly IHymnsService hymnsService;
        private readonly IPreferencesService preferencesService;
        private readonly IMvxLog log;

        private HymnIdParameter hymn;
        public HymnIdParameter HymnId
        {
            get => hymn;
            set => SetProperty(ref hymn, value);
        }

        private HymnalLanguage Language;


        public PlayerViewModel(
            IMvxNavigationService navigationService,
            IHymnsService hymnsService,
            IPreferencesService preferencesService,
            IMvxLog log
            )
        {
            this.navigationService = navigationService;
            this.hymnsService = hymnsService;
            this.preferencesService = preferencesService;
            this.log = log;
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

            log.Info($"Player open: {HymnId} of {Language.Id}");

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
        public MvxCommand CloseCommand => new MvxCommand(Close);
        private void Close()
        {
            navigationService.Close(this);
        }
#endregion
    }
}
