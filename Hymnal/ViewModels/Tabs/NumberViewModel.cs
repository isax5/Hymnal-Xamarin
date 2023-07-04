using System.Reactive.Concurrency;
using System.Reactive.Linq;
using CommunityToolkit.Mvvm.Input;

namespace Hymnal.ViewModels;

public sealed partial class NumberViewModel : BaseViewModel
{
    private readonly HymnsService hymnsService;
    private readonly PreferencesService preferencesService;


    [ObservableProperty]
    private IEnumerable<Hymn> hymns;

    [ObservableProperty]
    private string hymnNumber;


    public NumberViewModel(
        HymnsService hymnsService,
        PreferencesService preferencesService)
    {
        this.hymnsService = hymnsService;
        this.preferencesService = preferencesService;

        preferencesService.HymnalLanguageConfiguredChanged += PreferencesService_HymnalLanguageConfiguredChanged;

#if DEBUG
        hymnNumber = 255.ToString();
#endif
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
            .Subscribe(result => MainThread.BeginInvokeOnMainThread(() => Hymns = result.OrderByNumber()), error => error.Report());
    }

    [RelayCommand]
    private async void OpenHymnAsync(string number)
    {
        if (string.IsNullOrEmpty(number))
            return;

        await Shell.Current.GoToAsync(nameof(HymnPage),
            new HymnIdParameter()
            {
                Number = int.Parse(number),
                SaveInRecords = true,
                HymnalLanguage = preferencesService.ConfiguredHymnalLanguage,
            }.AsParameter());
    }

    private void PreferencesService_HymnalLanguageConfiguredChanged(object sender, HymnalLanguage e) => LoadData();
}
