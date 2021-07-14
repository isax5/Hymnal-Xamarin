using Hymnal.XF.Extensions;
using Hymnal.XF.Models;
using Hymnal.XF.Models.Parameters;
using Prism.Navigation;

namespace Hymnal.XF.ViewModels
{
    public class MusicSheetViewModel : BaseViewModelParameter<HymnIdParameter>
    {
        private HymnIdParameter hymn;
        public HymnIdParameter HymnId
        {
            get => hymn;
            set => SetProperty(ref hymn, value);
        }

        private string imageSource;
        public string ImageSource
        {
            get => imageSource;
            set => SetProperty(ref imageSource, value);
        }

        private HymnalLanguage Language;


        public MusicSheetViewModel(
            INavigationService navigationService
            ) : base(navigationService)
        { }

        public override void OnNavigatedTo(INavigationParameters parameters, HymnIdParameter parameter)
        {
            base.OnNavigatedTo(parameters, parameter);
            HymnId = parameter;
            Language = parameter.HymnalLanguage;
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            ImageSource = HymnId.HymnalLanguage.GetMusicSheetSource(HymnId.Number);
        }

        //public override void ViewAppeared()
        //{
        //    base.ViewAppeared();

        //    logger.LogInformation($"Opening Hymn Sheet: {HymnId} of {Language.Id}");

        //    Analytics.TrackEvent(Constants.TrackEv.HymnMusicSheetOpened, new Dictionary<string, string>
        //    {
        //        { Constants.TrackEv.HymnReferenceScheme.Number, HymnId.Number.ToString() },
        //        { Constants.TrackEv.HymnReferenceScheme.HymnalVersion, Language.Id },
        //        { Constants.TrackEv.HymnReferenceScheme.CultureInfo, Constants.CurrentCultureInfo.Name },
        //        { Constants.TrackEv.HymnReferenceScheme.Time, DateTime.Now.ToLocalTime().ToString() }
        //    });
        //}
    }
}
