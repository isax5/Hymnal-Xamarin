using Prism.Navigation;

namespace Hymnal.XF.ViewModels
{
    public class IndexViewModel : BaseViewModel
    {

        public IndexViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

        //private bool loaded = false;
        //public override async void ViewAppearing()
        //{
        //    base.ViewAppearing();

        //    if (loaded)
        //        return;

        //    loaded = true;

        //    await navigationService.Navigate<AlphabeticalIndexViewModel>();
        //    await navigationService.Navigate<NumericalIndexViewModel>();
        //    await navigationService.Navigate<ThematicIndexViewModel>();
        //}
    }
}
