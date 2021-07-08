using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            HymnNumber = $"{1}";
#endif
        }


        public override void ViewAppeared()
        {
            base.ViewAppeared();

            Analytics.TrackEvent(Constants.TrackEv.Navigation, new Dictionary<string, string>
            {
                { Constants.TrackEv.NavigationReferenceScheme.PageName, nameof(NumberViewModel) },
                { Constants.TrackEv.NavigationReferenceScheme.CultureInfo, Constants.CurrentCultureInfo.Name },
                { Constants.TrackEv.NavigationReferenceScheme.HymnalVersion, preferencesService.ConfiguratedHymnalLanguage.Id }
            });
        }

        public MvxAsyncCommand<string> OpenHymnCommand => new MvxAsyncCommand<string>(OpenHymnAsync);

        private async Task OpenHymnAsync(string text)
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

        public MvxAsyncCommand OpenSimplePageCommand => new MvxAsyncCommand(OpenSimplePageExecuteAsync);

        private async Task OpenSimplePageExecuteAsync()
        {
            await navigationService.Navigate<SimpleViewModel>();
        }

        public MvxCommand OpenRecordsCommand => new MvxCommand(() => navigationService.Navigate<RecordsViewModel>());
        public MvxCommand OpenSearchCommand => new MvxCommand(() => navigationService.Navigate<SearchViewModel>());
    }
}
