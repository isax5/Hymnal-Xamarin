using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class RootViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;
        public RootViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        private bool loaded = false;
        public override async void ViewAppearing()
        {
            base.ViewAppearing();

            if (loaded)
                return;

            loaded = true;

            await navigationService.Navigate<NumberViewModel>();
            await navigationService.Navigate<IndexViewModel>();
            await navigationService.Navigate<FavoritesViewModel>();
            await navigationService.Navigate<SettingsViewModel>();

            //await navigationService.Navigate<SimpleViewModel>();
        }
    }
}
