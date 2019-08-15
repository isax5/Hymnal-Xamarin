using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class AlphabeticalIndexViewModel : MvxViewModel
    {
        private IMvxNavigationService _navigationService;

        public AlphabeticalIndexViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
