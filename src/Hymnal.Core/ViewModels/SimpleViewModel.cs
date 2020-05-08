using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class SimpleViewModel : MvxViewModel
    {
        private IMvxNavigationService _navigationService;

        public SimpleViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
