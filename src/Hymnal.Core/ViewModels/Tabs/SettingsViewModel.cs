using Hymnal.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class SettingsViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;
        private readonly IPreferencesService preferencesService;

        public int HymnFontSize
        {
            get => preferencesService.HymnalsFontSize;
            set => preferencesService.HymnalsFontSize = value;
        }

        public SettingsViewModel(
            IMvxNavigationService navigationService,
            IPreferencesService preferencesService
            )
        {
            this.navigationService = navigationService;
            this.preferencesService = preferencesService;
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
