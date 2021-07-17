using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace Hymnal.XF.ViewModels
{
    public class SimpleViewModel : BaseViewModel
    {
        public DelegateCommand ClickmeCommand { get; private set; }

        public SimpleViewModel(
            INavigationService navigationService,
            IPageDialogService pageDialogService)
            : base(navigationService)
        {
            ClickmeCommand = new DelegateCommand(() =>
            {
                pageDialogService.DisplayAlertAsync("Holi", "Mensaje....", "Chao!");
            }).ObservesCanExecute(() => NotBusy);
        }
    }
}
