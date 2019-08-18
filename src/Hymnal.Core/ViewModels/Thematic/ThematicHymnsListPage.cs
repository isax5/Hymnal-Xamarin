using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class ThematicHymnsListViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;

        public ThematicHymnsListViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
    }
}
