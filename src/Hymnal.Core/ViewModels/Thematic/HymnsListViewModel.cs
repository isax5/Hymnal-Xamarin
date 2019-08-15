using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class HymnsListViewModel : MvxViewModel
    {
        private IMvxNavigationService _navigationService;

        public HymnsListViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
