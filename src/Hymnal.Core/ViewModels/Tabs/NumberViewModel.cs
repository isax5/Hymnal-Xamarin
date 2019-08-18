using System.Collections.Generic;
using System.Linq;
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

        public NumberViewModel(IMvxNavigationService navigationService, IHymnsService hymnsService)
        {
            this.navigationService = navigationService;
            this.hymnsService = hymnsService;
        }

        public MvxCommand<string> OpenHymnCommand => new MvxCommand<string>(OpenHymnAsync);

        private async void OpenHymnAsync(string text)
        {
            IEnumerable<Models.Hymn> hymns = await hymnsService.GetHymnListAsync();

            if (int.TryParse(text, out var number))
            {
                if (number < 0 || number > hymns.Count())
                    return;

                await navigationService.Navigate<HymnViewModel, HymnId>(new HymnId { Number = number });
            }
        }

        public MvxCommand OpenRecordsCommand => new MvxCommand(() => navigationService.Navigate<RecordsViewModel>());
        public MvxCommand OpenSearchCommand => new MvxCommand(() => navigationService.Navigate<SearchViewModel>());
    }
}
