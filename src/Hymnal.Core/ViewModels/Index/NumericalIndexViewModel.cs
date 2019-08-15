using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class NumericalIndexViewModel : MvxViewModel
    {
        private IMvxNavigationService _navigationService;

        public NumericalIndexViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
