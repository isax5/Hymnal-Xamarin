using System.Collections.Generic;
using System.Linq;
using Hymnal.Core.Models;
using Hymnal.Core.Models.Parameter;
using Hymnal.Core.Services;
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

        public MvxCommand OpenRecordsCommand => new MvxCommand(() => navigationService.Navigate<RecordsViewModel>());
        public MvxCommand OpenSearchCommand => new MvxCommand(() => navigationService.Navigate<SearchViewModel>());
    }
}
