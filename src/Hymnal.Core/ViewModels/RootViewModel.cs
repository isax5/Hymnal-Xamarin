using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class RootViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        public RootViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private bool loaded = false;
        public override async void ViewAppearing()
        {
            base.ViewAppearing();

            if (loaded)
                return;

            loaded = true;

            await _navigationService.Navigate<NumberViewModel>();
            await _navigationService.Navigate<IndexViewModel>();
            await _navigationService.Navigate<FavoritesViewModel>();
            await _navigationService.Navigate<SettingsViewModel>();
        }
    }
}
