using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.XF.ViewModels
{
    public class IndexViewModel : MvxViewModel
    {
        private readonly INavigationService navigationService;

        public IndexViewModel(INavigationService navigationService)
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
