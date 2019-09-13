using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hymnal.Core.Extensions;
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
        private readonly IAppInformationService appInformationService;
        private readonly IBrowserService browserService;

        public int HymnFontSize
        {
            get => preferencesService.HymnalsFontSize;
            set => preferencesService.HymnalsFontSize = value;
        }
        public int MinimumHymnFontSize => Constants.MINIMUM_HYMNALS_FONT_SIZE;
        public int MaximumHymnFontSize => Constants.MAXIMUM_HYMNALS_FONT_SIZE;

        private string appVersionString;
        public string AppVersionString
        {
            get => appVersionString;
            set => SetProperty(ref appVersionString, value);
        }

        private string appBuildString;
        public string AppBuildString
        {
            get => appBuildString;
            set => SetProperty(ref appBuildString, value);
        }

        private HymnalLanguage hymnalLanguage;
        public HymnalLanguage HymnalLanguage
        {
            get => hymnalLanguage;
            set => SetProperty(ref hymnalLanguage, value);
        }

        public SettingsViewModel(
            IMvxNavigationService navigationService,
            IPreferencesService preferencesService,
            IDialogService dialogService,
            IAppInformationService appInformationService,
            IBrowserService browserService
            )
        {
            this.navigationService = navigationService;
            this.preferencesService = preferencesService;
            this.dialogService = dialogService;
            this.appInformationService = appInformationService;
            this.browserService = browserService;
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            HymnalLanguage = preferencesService.ConfiguratedHymnalLanguage.Configuration();

            AppVersionString = appInformationService.VersionString;
            AppBuildString = appInformationService.BuildString;
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
            browserService.OpenBrowserAsync(@"https://storage.googleapis.com/hymn-music/about/index.html");
            //navigationService.Navigate<DevelopersViewModel>();
        }
    }
}
