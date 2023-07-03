using System.Reactive.Concurrency;
using System.Reactive.Linq;
using CommunityToolkit.Mvvm.Input;

namespace Hymnal.ViewModels;

public sealed partial class SearchViewModel : BaseViewModel
{
    private readonly HymnsService hymnsService;
    private readonly PreferencesService preferencesService;

    #region Properties
    [ObservableProperty]
    private IEnumerable<Hymn> hymns;
    #endregion


    public SearchViewModel(HymnsService hymnsService, PreferencesService preferencesService)
    {
        this.hymnsService = hymnsService;
        this.preferencesService = preferencesService;
    }

    override public void Initialize()
    {
        base.Initialize();

        LoadData();
    }


    private void LoadData()
    {
        hymnsService.GetHymnListAsync(preferencesService.ConfiguredHymnalLanguage)
            .ToObservable()
            .SubscribeOn(new NewThreadScheduler())
            .Subscribe(result => MainThread.BeginInvokeOnMainThread(() => Hymns = result.OrderByNumber()), error => error.Report());
    }

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
