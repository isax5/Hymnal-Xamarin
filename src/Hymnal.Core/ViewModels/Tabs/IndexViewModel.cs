using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class IndexViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;

        public IndexViewModel(IMvxNavigationService navigationService)
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

            await navigationService.Navigate<AlphabeticalIndexViewModel>();
            await navigationService.Navigate<NumericalIndexViewModel>();
            await navigationService.Navigate<ThematicIndexViewModel>();
        }
    }
}
