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

        public MvxCommand HelpCommand => new MvxCommand(HelpExecute);

        private void HelpExecute()
        {
            navigationService.Navigate<HelpViewModel>();
        }

        public MvxCommand DeveloperCommand => new MvxCommand(DeveloperExecute);

        private void DeveloperExecute()
        {
            navigationService.Navigate<DevelopersViewModel>();
        }
    }
}
