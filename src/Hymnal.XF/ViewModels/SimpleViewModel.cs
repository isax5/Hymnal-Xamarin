using Hymnal.XF.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace Hymnal.XF.ViewModels
{
    public class SimpleViewModel : BaseViewModel
    {
        public DelegateCommand ClickmeCommand { get; private set; }
        public DelegateCommand SampleModalNavigationCommand { get; private set; }
        public DelegateCommand GoBackCommand { get; private set; }

        public SimpleViewModel(
            INavigationService navigationService,
            IPageDialogService pageDialogService)
            : base(navigationService)
        {
            ClickmeCommand = new DelegateCommand(() =>
            {
                pageDialogService.DisplayAlertAsync("Holi", "Mensaje....", "Chao!");
            }).ObservesCanExecute(() => NotBusy);

            SampleModalNavigationCommand = new DelegateCommand(() =>
            {
                NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(SimplePage)}", null, true, true);
            }).ObservesCanExecute(() => NotBusy);

            GoBackCommand = new DelegateCommand(() =>
            {
                navigationService.GoBackAsync();
            }).ObservesCanExecute(() => NotBusy);
        }
    }
}
