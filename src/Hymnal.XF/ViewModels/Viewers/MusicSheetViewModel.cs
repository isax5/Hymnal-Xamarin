using Hymnal.XF.Extensions;
using Hymnal.XF.Models.Parameters;
using Prism.Navigation;

namespace Hymnal.XF.ViewModels
{
    public class MusicSheetViewModel : BaseViewModelParameter<HymnIdParameter>
    {
        private string imageSource;
        public string ImageSource
        {
            get => imageSource;
            set => SetProperty(ref imageSource, value);
        }

        public MusicSheetViewModel(
            INavigationService navigationService
            ) : base(navigationService)
        { }

        public override void Initialize(INavigationParameters parameters, HymnIdParameter parameter)
        {
            base.Initialize(parameters, parameter);
            ImageSource = parameter.HymnalLanguage.GetMusicSheetSource(parameter.Number);
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
