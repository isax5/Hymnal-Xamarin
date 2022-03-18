using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Hymnal.XF.Constants;
using Hymnal.XF.Extensions;
using Hymnal.XF.Models;
using Hymnal.XF.Models.Parameters;
using Hymnal.XF.Services;
using Microsoft.AppCenter.Analytics;
using Prism.Navigation;

namespace Hymnal.XF.ViewModels
{
    public sealed partial class MusicSheetViewModel : BaseViewModelParameter<HymnIdParameter>
    {
        private readonly IHymnsService hymnsService;

        [ObservableProperty]
        private string imageSource;

        [ObservableProperty]
        private Hymn hymn;

        public MusicSheetViewModel(
            INavigationService navigationService,
            IHymnsService hymnsService
            ) : base(navigationService)
        {
            this.hymnsService = hymnsService;
        }

        public override void Initialize(INavigationParameters parameters, HymnIdParameter parameter)
        {
            base.Initialize(parameters, parameter);
            ImageSource = parameter.HymnalLanguage.GetMusicSheetSource(parameter.Number);
        }

        public override async Task InitializeAsync(INavigationParameters parameters, HymnIdParameter parameter)
        {
            await base.InitializeAsync(parameters, parameter);
            Hymn = await hymnsService.GetHymnAsync(parameter.Number, parameter.HymnalLanguage);
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
