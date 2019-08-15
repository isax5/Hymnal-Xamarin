using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class SearchViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;

        public SearchViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
    }
}
