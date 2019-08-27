using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class DevelopersViewModel : MvxViewModel
    {
        private IMvxNavigationService _navigationService;


        public DevelopersViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
