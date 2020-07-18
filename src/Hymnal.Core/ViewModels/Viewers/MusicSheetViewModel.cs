using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Hymnal.Core.Extensions;
using Hymnal.Core.Models;
using Hymnal.Core.Models.Parameter;
using Microsoft.AppCenter.Analytics;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class MusicSheetViewModel : MvxViewModel<HymnIdParameter>
    {
        private readonly IMvxNavigationService navigationService;
        private readonly IMvxLog log;

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
            IMvxNavigationService navigationService,
            IMvxLog log
            )
        {
            this.navigationService = navigationService;
            this.log = log;
        }

        public override void Prepare(HymnIdParameter parameter)
        {
            HymnId = parameter;
            Language = parameter.HymnalLanguage;
        }

        public override Task Initialize()
        {
            ImageSource = HymnId.HymnalLanguage.GetMusicSheetSource(HymnId.Number);
            return base.Initialize();
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();

            log.Info($"Opening Hymn Sheet: {HymnId} of {Language.Id}");

            Analytics.TrackEvent(Constants.TrackEvents.HymnMusicSheetOpened, new Dictionary<string, string>
            {
                { Constants.TrackEvents.HymnReferenceScheme.Number, HymnId.Number.ToString() },
                { Constants.TrackEvents.HymnReferenceScheme.HymnalVersion, Language.Id },
                { Constants.TrackEvents.HymnReferenceScheme.CultureInfo, Constants.CurrentCultureInfo.Name },
                { Constants.TrackEvents.HymnReferenceScheme.Time, DateTime.Now.ToLocalTime().ToString() }
            });
        }
    }
}
