using CommunityToolkit.Mvvm.Input;
using Hymnal.Resources.Languages;

namespace Hymnal.ViewModels;

public sealed partial class SettingsViewModel : BaseViewModel
{
    private readonly PreferencesService preferencesService;
    private readonly IBrowser browser;
    private readonly IDeviceInfo deviceInfo;
    private readonly IDeviceDisplay deviceDisplay;


    #region Properties
    private readonly BrowserLaunchOptions browserLaunchOptions = new()
    {
        LaunchMode = BrowserLaunchMode.External,
        TitleMode = BrowserTitleMode.Show,
        PreferredToolbarColor = Colors.Black,
        PreferredControlColor = Colors.White,
    };

    [ObservableProperty]
    private HymnalLanguage configuredHymnalLanguage;

    public bool KeepScreenOn
    {
        get
        {
            if (deviceInfo.Platform == DevicePlatform.iOS || deviceInfo.Platform == DevicePlatform.Android)
                return preferencesService.KeepScreenOn;

            return false;
        }
        set
        {
            deviceDisplay.KeepScreenOn = value;
            preferencesService.KeepScreenOn = value;
            OnPropertyChanged(nameof(KeepScreenOn));
        }
    }

    public bool BackgroundImageAppearance
    {
        get => preferencesService.BackgroundImageAppearance;
        set
        {
            preferencesService.BackgroundImageAppearance = value;
            OnPropertyChanged(nameof(BackgroundImageAppearance));
        }
    }

    public double HymnalsFontSize
    {
        get => preferencesService.HymnalsFontSize;
        set
        {
            preferencesService.HymnalsFontSize = (int)value;
            OnPropertyChanged(nameof(HymnalsFontSize));
        }
    }
    #endregion


    public SettingsViewModel(PreferencesService preferencesService,
        IBrowser browser,
        IDeviceInfo deviceInfo,
        IDeviceDisplay deviceDisplay)
    {
        this.preferencesService = preferencesService;
        this.browser = browser;
        this.deviceInfo = deviceInfo;
        this.deviceDisplay = deviceDisplay;

        configuredHymnalLanguage = preferencesService.ConfiguredHymnalLanguage;
    }

    [RelayCommand]
    private async void ChooseLanguageAsync()
    {
        var languages = new Dictionary<string, HymnalLanguage>();

        foreach (HymnalLanguage hl in InfoConstants.HymnsLanguages)
        {
            languages.Add($"({hl.Detail}) {hl.Name}", hl);
        }

        var title = LanguageResources.VersionsAndLanguages;
        var cancelButton = LanguageResources.Generic_Cancel;

        var languageKey = await Shell.Current.DisplayActionSheet(
            title,
            cancelButton,
            null,
            languages.Select(hld => hld.Key).ToArray()
        );

        if (string.IsNullOrEmpty(languageKey) || languageKey.Equals(cancelButton))
            return;

        ConfiguredHymnalLanguage = languages[languageKey];
        preferencesService.ConfiguredHymnalLanguage = ConfiguredHymnalLanguage;
    }

    [RelayCommand]
    private async void OpenGitHubAsync()
    {
        await browser.OpenAsync(AppConstants.WebLinks.GitHubDevelopingLink, browserLaunchOptions);
    }

    [RelayCommand]
    private async void SupportProjectAsync()
    {
        await browser.OpenAsync(AppConstants.WebLinks.SupportProjectLink, browserLaunchOptions);
    }
}
