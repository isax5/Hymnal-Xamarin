using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class SettingsViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;

        public SettingsViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public MvxCommand HolaCommand => new MvxCommand(Hola);

        private void Hola()
        {
            navigationService.Navigate<HymnViewModel>();
        }
    }
}
