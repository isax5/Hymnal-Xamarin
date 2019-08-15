using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class FavoritesViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;

        public FavoritesViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
    }
}
