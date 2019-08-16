using Hymnal.Core.Models.Parameter;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class NumberViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;

        public NumberViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public MvxCommand<string> OpenHymnCommand => new MvxCommand<string>(OpenHymn);

        private void OpenHymn(string text)
        {
            if (int.TryParse(text, out var number))
            {
                if (number < 0 || number > 613)
                    return;

                navigationService.Navigate<HymnViewModel, HymnId>(new HymnId { Number = number });
            }
        }

        public MvxCommand OpenRecordsCommand => new MvxCommand(() => navigationService.Navigate<RecordsViewModel>());
        public MvxCommand OpenSearchCommand => new MvxCommand(() => navigationService.Navigate<SearchViewModel>());
    }
}
