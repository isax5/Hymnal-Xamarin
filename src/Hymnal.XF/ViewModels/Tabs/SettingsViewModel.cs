using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Hymnal.XF.Extensions.i18n;
using Hymnal.XF.Models;
using Hymnal.XF.Services;
using Hymnal.XF.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Essentials;

namespace Hymnal.XF.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly IPreferencesService preferencesService;
        private readonly IPageDialogService dialogService;

        private readonly BrowserLaunchOptions browserLaunchOptions = new BrowserLaunchOptions
        {
            LaunchMode = BrowserLaunchMode.SystemPreferred,
            TitleMode = BrowserTitleMode.Show,
            PreferredToolbarColor = Color.Black,
            PreferredControlColor = Color.White
        };

        public int HymnFontSize
        {
            get => preferencesService.HymnalsFontSize;
            set => preferencesService.HymnalsFontSize = value;
        }
        public int MinimumHymnFontSize => Constants.Constants.MINIMUM_HYMNALS_FONT_SIZE;
        public int MaximumHymnFontSize => Constants.Constants.MAXIMUM_HYMNALS_FONT_SIZE;

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

        public bool KeepScreenOn
        {
            get
            {
                if (DeviceInfo.Platform == DevicePlatform.iOS ||
                    DeviceInfo.Platform == DevicePlatform.Android)
                {
                    return DeviceDisplay.KeepScreenOn;

                }
                return false;
            }
            set
            {
                DeviceDisplay.KeepScreenOn = value;
                preferencesService.KeepScreenOn = value;
                RaisePropertyChanged(nameof(KeepScreenOn));
            }
        }

        public SettingsViewModel(
            INavigationService navigationService,
            IPreferencesService preferencesService,
            IPageDialogService dialogService
            ) : base(navigationService)
        {
            this.preferencesService = preferencesService;
            this.dialogService = dialogService;

            ChooseLanguageCommand = new DelegateCommand(ChooseLanguageExecuteAsync).ObservesCanExecute(() => NotBusy);
            HelpCommand = new DelegateCommand(HelpExecuteAsync).ObservesCanExecute(() => NotBusy);
            OpenGitHubCommand = new DelegateCommand(OpenGitHubExecuteAsync).ObservesCanExecute(() => NotBusy);
            DeveloperCommand = new DelegateCommand(DeveloperExecuteAsync).ObservesCanExecute(() => NotBusy);
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            HymnalLanguage = preferencesService.ConfiguratedHymnalLanguage;

            AppVersionString = AppInfo.VersionString;
            AppBuildString = AppInfo.BuildString;
        }

        //public override void ViewAppeared()
        //{
        //    base.ViewAppeared();

        //    Analytics.TrackEvent(Constants.TrackEv.Navigation, new Dictionary<string, string>
        //    {
        //        { Constants.TrackEv.NavigationReferenceScheme.PageName, nameof(SettingsViewModel) },
        //        { Constants.TrackEv.NavigationReferenceScheme.CultureInfo, Constants.CurrentCultureInfo.Name },
        //        { Constants.TrackEv.NavigationReferenceScheme.HymnalVersion, preferencesService.ConfiguratedHymnalLanguage.Id }
        //    });
        //}

        public DelegateCommand ChooseLanguageCommand;
        private async void ChooseLanguageExecuteAsync()
        {
            var languages = new Dictionary<string, HymnalLanguage>();

            foreach (HymnalLanguage hl in Constants.Constants.HymnsLanguages)
            {
                languages.Add($"({hl.Detail}) {hl.Name}", hl);
            }

            var title = Languages.ChooseYourHymnal;
            var cancelButton = Languages.Cancel;

            var languageKey = await dialogService.DisplayActionSheetAsync(
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

        public DelegateCommand HelpCommand;
        private async void HelpExecuteAsync()
        {
            await NavigationService.NavigateAsync(nameof(HelpPage));
        }

        public DelegateCommand OpenGitHubCommand;
        private async void OpenGitHubExecuteAsync()
        {
            await Browser.OpenAsync(Constants.Constants.WebLinks.GitHubDevelopingLink, browserLaunchOptions);
        }

        public DelegateCommand DeveloperCommand;
        private async void DeveloperExecuteAsync()
        {
            await Browser.OpenAsync(Constants.Constants.WebLinks.DeveloperWebSite, browserLaunchOptions);
        }
    }
}
