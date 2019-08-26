using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hymnal.Core.Helpers;
using Hymnal.Core.Models;
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
        private readonly IDialogService dialogService;

        public int HymnFontSize
        {
            get => preferencesService.HymnalsFontSize;
            set => preferencesService.HymnalsFontSize = value;
        }
        public int MinimumHymnFontSize => Constants.MINIMUM_HYMNALS_FONT_SIZE;
        public int MaximumHymnFontSize => Constants.MAXIMUM_HYMNALS_FONT_SIZE;

        private HymnalLanguage hymnalLanguage;
        public HymnalLanguage HymnalLanguage
        {
            get => hymnalLanguage;
            set => SetProperty(ref hymnalLanguage, value);
        }

        public SettingsViewModel(
            IMvxNavigationService navigationService,
            IPreferencesService preferencesService,
            IDialogService dialogService
            )
        {
            this.navigationService = navigationService;
            this.preferencesService = preferencesService;
            this.dialogService = dialogService;
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            HymnalLanguage = preferencesService.ConfiguratedHymnalLanguage;
        }


        public MvxCommand ChooseLanguageCommand => new MvxCommand(ChooseLanguageExecuteAsync);
        private async void ChooseLanguageExecuteAsync()
        {
            var languages = new Dictionary<string, HymnalLanguage>();

            foreach (HymnalLanguage hl in Constants.HymnsLanguages)
            {
                languages.Add($"({hl.Detail}) {hl.Name}", hl);
            }

            var title = Languages.ChooseYourHymnal;
            var cancelButton = Languages.Cancel;

            var languageKey = await dialogService.DisplayActionSheet(
                title,
                cancelButton,
                null,
                languages.Select(hld => hld.Key).ToArray()
                );

            if (string.IsNullOrEmpty(languageKey) || languageKey.Equals(cancelButton))
                return;

            HymnalLanguage = languages[languageKey];
            preferencesService.ConfiguratedHymnalLanguage = HymnalLanguage;
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
