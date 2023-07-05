using System.Globalization;
using Hymnal.Helpers;

namespace Hymnal;

public sealed class Setup
{
    private readonly PreferencesService preferencesService;
    private readonly IDeviceInfo deviceInfo;
    private readonly IDeviceDisplay deviceDisplay;

    public ThemeHelper ThemeHelper;

    public Setup(PreferencesService preferencesService, IDeviceInfo deviceInfo, IDeviceDisplay deviceDisplay)
    {
        this.preferencesService = preferencesService;
        this.deviceInfo = deviceInfo;
        this.deviceDisplay = deviceDisplay;
    }

    public void InitializeFirstChance(App app)
    {
        // Last hymnbook opened
        if (preferencesService is { ConfiguredHymnalLanguage: null })
        {
            CultureInfo currentCulture = CultureInfo.InstalledUICulture;

            List<HymnalLanguage> lngs = InfoConstants.HymnsLanguages.FindAll(l => l.TwoLetterIsoLanguageName == currentCulture.TwoLetterISOLanguageName);
            preferencesService.ConfiguredHymnalLanguage = lngs.Count == 0 ? InfoConstants.HymnsLanguages.First() : lngs.First();
        }

        // Theme
        ThemeHelper = new ThemeHelper(app);
    }

    public void InitializeLastChance()
    { }

    public void AfterStartUp()
    {
        // Keep display on
        if (deviceInfo.Platform == DevicePlatform.iOS || deviceInfo.Platform == DevicePlatform.Android)
            deviceDisplay.KeepScreenOn = preferencesService.KeepScreenOn;
    }
}
