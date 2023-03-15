using System.Collections;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using CommunityToolkit.Mvvm.Input;

namespace Hymnal.ViewModels;

public sealed partial class NumericalIndexViewModel : BaseViewModel
{
    private readonly HymnsService hymnsService;
    private readonly PreferencesService preferencesService;
    private readonly IDeviceInfo deviceInfo;

    #region Properties
    [ObservableProperty]
    private IEnumerable hymns;
    #endregion


    public NumericalIndexViewModel(
        HymnsService hymnsService,
        PreferencesService preferencesService,
        IDeviceInfo deviceInfo)
    {
        this.hymnsService = hymnsService;
        this.preferencesService = preferencesService;
        this.deviceInfo = deviceInfo;

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
        hymnsService.GetHymnListAsync(preferencesService.ConfiguredHymnalLanguage)
            .ToObservable()
            .SubscribeOn(new NewThreadScheduler())
            .Subscribe(result =>
            {
                IEnumerable orderedHymns = deviceInfo.Platform == DevicePlatform.WinUI
                    ? result.OrderByNumber()
                    : result.OrderByNumber().GroupByNumber();

                MainThread.BeginInvokeOnMainThread(() => Hymns = orderedHymns);
            }, error => error.Report());
    }

    private void PreferencesService_HymnalLanguageConfiguredChanged(object sender, HymnalLanguage e) => LoadData();

    [RelayCommand]
    private async void OpenHymnAsync(Hymn hymn)
    {
        if (hymn is null)
            return;

        await Shell.Current.GoToAsync(nameof(HymnPage),
            new HymnIdParameter()
            {
                Number = hymn.Number,
                SaveInRecords = true,
                HymnalLanguage = preferencesService.ConfiguredHymnalLanguage,
            }.AsParameter());
    }
}
