using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class ThematicIndexViewModel : MvxViewModel
    {
        private IMvxNavigationService _navigationService;

        public ThematicIndexViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
