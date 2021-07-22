using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Hymnal.XF.Constants;
using Hymnal.XF.Extensions.i18n;
using Hymnal.XF.Models;
using Hymnal.XF.Services;
using Hymnal.XF.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Essentials;
using Xamarin.Essentials.Interfaces;

namespace Hymnal.XF.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly IPreferencesService preferencesService;
        private readonly IPageDialogService dialogService;
        private readonly IDeviceInfo deviceInfo;
        private readonly IAppInfo appInfo;
        private readonly IBrowser browser;
        private readonly IDeviceDisplay deviceDisplay;

        #region Properties
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
        public int MinimumHymnFontSize => AppConstants.MINIMUM_HYMNALS_FONT_SIZE;
        public int MaximumHymnFontSize => AppConstants.MAXIMUM_HYMNALS_FONT_SIZE;

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
                if (deviceInfo.Platform == DevicePlatform.iOS ||
                    deviceInfo.Platform == DevicePlatform.Android)
                {
                    return deviceDisplay.KeepScreenOn;

                }
                return false;
            }
            set
            {
                deviceDisplay.KeepScreenOn = value;
                preferencesService.KeepScreenOn = value;
                RaisePropertyChanged(nameof(KeepScreenOn));
            }
        }
        #endregion

        #region Commands
        public DelegateCommand ChooseLanguageCommand { get; internal set; }
        public DelegateCommand HelpCommand { get; internal set; }
        public DelegateCommand OpenGitHubCommand { get; internal set; }
        public DelegateCommand DeveloperCommand { get; internal set; }
        #endregion

        public SettingsViewModel(
            INavigationService navigationService,
            IPreferencesService preferencesService,
            IPageDialogService dialogService,
            IDeviceInfo deviceInfo,
            IAppInfo appInfo,
            IBrowser browser,
            IDeviceDisplay deviceDisplay
            ) : base(navigationService)
        {
            this.preferencesService = preferencesService;
            this.dialogService = dialogService;
            this.deviceInfo = deviceInfo;
            this.appInfo = appInfo;
            this.browser = browser;
            this.deviceDisplay = deviceDisplay;

            ChooseLanguageCommand = new DelegateCommand(ChooseLanguageExecuteAsync).ObservesCanExecute(() => NotBusy);
            HelpCommand = new DelegateCommand(HelpExecuteAsync).ObservesCanExecute(() => NotBusy);
            OpenGitHubCommand = new DelegateCommand(OpenGitHubExecuteAsync).ObservesCanExecute(() => NotBusy);
            DeveloperCommand = new DelegateCommand(DeveloperExecuteAsync).ObservesCanExecute(() => NotBusy);
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            HymnalLanguage = preferencesService.ConfiguratedHymnalLanguage;

            AppVersionString = appInfo.VersionString;
            AppBuildString = appInfo.BuildString;
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

        #region Command Actions
        private async void ChooseLanguageExecuteAsync()
        {
            var languages = new Dictionary<string, HymnalLanguage>();

            foreach (HymnalLanguage hl in InfoConstants.HymnsLanguages)
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

        private async void HelpExecuteAsync()
        {
            await NavigationService.NavigateAsync(nameof(HelpPage));
        }

        private async void OpenGitHubExecuteAsync()
        {
            await browser.OpenAsync(AppConstants.WebLinks.GitHubDevelopingLink, browserLaunchOptions);
        }

        private async void DeveloperExecuteAsync()
        {
            await browser.OpenAsync(AppConstants.WebLinks.DeveloperWebSite, browserLaunchOptions);
        }
        #endregion
    }
}
