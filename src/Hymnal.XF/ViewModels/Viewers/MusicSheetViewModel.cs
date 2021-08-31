using System;
using System.Collections.Generic;
using Hymnal.XF.Constants;
using Hymnal.XF.Extensions;
using Hymnal.XF.Models.Parameters;
using Microsoft.AppCenter.Analytics;
using Prism.Navigation;

namespace Hymnal.XF.ViewModels
{
    public sealed class MusicSheetViewModel : BaseViewModelParameter<HymnIdParameter>
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

        public override void OnAppearing()
        {
            base.OnAppearing();

            Analytics.TrackEvent(TrackingConstants.TrackEv.HymnMusicSheetOpened, new Dictionary<string, string>
            {
                { TrackingConstants.TrackEv.HymnReferenceScheme.Number, Parameter.Number.ToString() },
                { TrackingConstants.TrackEv.HymnReferenceScheme.HymnalVersion, Parameter.HymnalLanguage.Id },
                { TrackingConstants.TrackEv.HymnReferenceScheme.CultureInfo, InfoConstants.CurrentCultureInfo.Name },
                { TrackingConstants.TrackEv.HymnReferenceScheme.Time, DateTime.Now.ToLocalTime().ToString() }
            });
        }
    }
}
