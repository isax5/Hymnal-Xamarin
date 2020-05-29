using System;
using System.Collections.Generic;
using System.Linq;
using Hymnal.Core.Models;
using Hymnal.Core.Models.Parameter;
using Hymnal.Core.Services;
using Microsoft.AppCenter.Analytics;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class NumberViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;
        private readonly IHymnsService hymnsService;
        private readonly IPreferencesService preferencesService;
        
        private string hymnNumber;
        public string HymnNumber
        {
            get => hymnNumber;
            set => SetProperty(ref hymnNumber, value);
        }

        public NumberViewModel(
            IMvxNavigationService navigationService,
            IHymnsService hymnsService,
            IPreferencesService preferencesService)
        {
            this.navigationService = navigationService;
            this.hymnsService = hymnsService;
            this.preferencesService = preferencesService;

#if DEBUG
            // A long hymn
            //HymnNumber = $"{255}";
            HymnNumber = $"{584}";
#endif
        }


        public override void ViewAppeared()
        {
            base.ViewAppeared();

#if __IOS__ || __ANDROID__
            Analytics.TrackEvent(Constants.TrackEvents.Navigation, new Dictionary<string, string>
            {
                { Constants.TrackEvents.NavigationReferenceScheme.PageName, nameof(NumberViewModel) },
                { Constants.TrackEvents.NavigationReferenceScheme.CultureInfo, Constants.CurrentCultureInfo.Name },
                { Constants.TrackEvents.NavigationReferenceScheme.HymnalVersion, preferencesService.ConfiguratedHymnalLanguage.Id }
            });
#endif
        }

        public MvxCommand<string> OpenHymnCommand => new MvxCommand<string>(OpenHymnAsync);

        private async void OpenHymnAsync(string text)
        {
            var num = text ?? HymnNumber;

            HymnalLanguage language = preferencesService.ConfiguratedHymnalLanguage;
            IEnumerable<Hymn> hymns = await hymnsService.GetHymnListAsync(language);

            if (int.TryParse(num, out var number))
            {
                if (number < 0 || number > hymns.Count())
                    return;

                await navigationService.Navigate<HymnViewModel, HymnIdParameter>(new HymnIdParameter
                {
                    Number = number,
                    HymnalLanguage = language
                });
            }
        }

        public MvxCommand OpenSimplePageCommand => new MvxCommand(OpenSimplePageExecute);

        private void OpenSimplePageExecute()
        {
            navigationService.Navigate<SimpleViewModel>();
        }

        public MvxCommand OpenRecordsCommand => new MvxCommand(() => navigationService.Navigate<RecordsViewModel>());
        public MvxCommand OpenSearchCommand => new MvxCommand(() => navigationService.Navigate<SearchViewModel>());
    }
}
