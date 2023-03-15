using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace Hymnal.ViewModels;

public sealed partial class ThematicIndexViewModel : BaseViewModel
{
    private readonly HymnsService hymnsService;
    private readonly PreferencesService preferencesService;

    #region Properties
    [ObservableProperty]
    private List<Thematic> thematics;
    #endregion


    public ThematicIndexViewModel(
        HymnsService hymnsService,
        PreferencesService preferencesService)
    {
        this.hymnsService = hymnsService;
        this.preferencesService = preferencesService;

        preferencesService.HymnalLanguageConfiguredChanged += PreferencesService_HymnalLanguageConfiguredChanged;
    }

    public override void Initialize()
    {
        base.Initialize();

        LoadData();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        preferencesService.HymnalLanguageConfiguredChanged -= PreferencesService_HymnalLanguageConfiguredChanged;
    }


    private void LoadData()
    {
        hymnsService.GetThematicListAsync(preferencesService.ConfiguredHymnalLanguage)
            .ToObservable()
            .SubscribeOn(new NewThreadScheduler())
            .Subscribe(result => MainThread.BeginInvokeOnMainThread(() => Thematics = result),
            error => error.Report());
    }

    private void PreferencesService_HymnalLanguageConfiguredChanged(object sender, HymnalLanguage e) => LoadData();
}
