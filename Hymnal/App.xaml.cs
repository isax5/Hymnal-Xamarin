using System.Globalization;

namespace Hymnal;

public partial class App : Application
{
    private readonly PreferencesService preferencesService;

    public App(PreferencesService preferencesService)
    {
        this.preferencesService = preferencesService;

        InitializeComponent();

        // Initialize
        Init();

        MainPage = new AppShell();
    }

    private void Init()
    {
        // Last hymnbook opened
        if (preferencesService is { ConfiguredHymnalLanguage: null })
        {
            CultureInfo currentCulture = CultureInfo.InstalledUICulture;

            List<HymnalLanguage> lngs = InfoConstants.HymnsLanguages.FindAll(l => l.TwoLetterIsoLanguageName == currentCulture.TwoLetterISOLanguageName);
            preferencesService.ConfiguredHymnalLanguage = lngs.Count == 0 ? InfoConstants.HymnsLanguages.First() : lngs.First();
        }
    }
}
