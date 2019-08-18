using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class HelpViewModel : MvxViewModel
    {
        private IMvxNavigationService _navigationService;

        public HelpViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
