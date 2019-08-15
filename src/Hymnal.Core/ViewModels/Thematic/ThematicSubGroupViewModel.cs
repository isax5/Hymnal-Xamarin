using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class ThematicSubGroupViewModel : MvxViewModel
    {
        private IMvxNavigationService _navigationService;

        public ThematicSubGroupViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
