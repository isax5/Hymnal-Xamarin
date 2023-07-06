using System.Reactive.Concurrency;
using System.Reactive.Linq;
using CommunityToolkit.Mvvm.Input;
using Hymnal.Models.DataBase;

namespace Hymnal.ViewModels;

public sealed partial class FavoritesViewModel : BaseViewModel
{
    private readonly HymnsService hymnsService;
    private readonly PreferencesService preferencesService;
    private readonly DatabaseService databaseService;

    public ObservableRangeCollection<Tuple<Hymn, FavoriteHymn>> Hymns { get; } = new();

    public FavoritesViewModel(
        HymnsService hymnsService,
        PreferencesService preferencesService,
        DatabaseService databaseService)
    {
        this.hymnsService = hymnsService;
        this.preferencesService = preferencesService;
        this.databaseService = databaseService;
    }

    public override void OnAppearing()
    {
        base.OnAppearing();

        LoadData();
    }


    // TODO: Problemas con Windows al recargar luego de abrir esta pag por segunda vez (la primera vez funciona bien)
    private void LoadData()
    {
        databaseService.GetTable<FavoriteHymn>()
            .ToListAsync()
            .ToObservable()
            .SubscribeOn(new NewThreadScheduler())
            .Subscribe(async hymnReferences =>
            {
                try
                {
                    var values = GetHymnsAsync(hymnReferences.OrderBy(r => r.Order));

                    MainThread.BeginInvokeOnMainThread(() => Hymns.Clear());
                    await foreach (var value in values)
                        MainThread.BeginInvokeOnMainThread(() => Hymns.Add(value));

                    async IAsyncEnumerable<Tuple<Hymn, FavoriteHymn>> GetHymnsAsync(IEnumerable<FavoriteHymn> hymnReferences)
                    {
                        foreach (var hymnReference in hymnReferences)
                            yield return new Tuple<Hymn, FavoriteHymn>(await hymnsService.GetHymnAsync(hymnReference), hymnReference);
                    }
                }
                catch (Exception ex)
                {
                    ex.Report();
                }
            }, error => error.Report());
    }

    [RelayCommand]
    private async void OpenHymnAsync(Tuple<Hymn, FavoriteHymn> hymn)
    {
        if (hymn is null)
            return;

        await Shell.Current.GoToAsync(nameof(HymnPage),
            new HymnIdParameter()
            {
                Number = hymn.Item1.Number,
                SaveInRecords = true,
                HymnalLanguage = HymnalLanguage.GetHymnalLanguageWithId(hymn.Item1.HymnalLanguageId),
            }.AsParameter());
    }

    [RelayCommand]
    private void RemoveHymn(Tuple<Hymn, FavoriteHymn> hymn)
    {
        databaseService.RemoveAsync(hymn.Item2)
            .ToObservable()
            .Subscribe(result => MainThread.BeginInvokeOnMainThread(() => Hymns.Remove(hymn)),
                       error => error.Report());
    }
}
