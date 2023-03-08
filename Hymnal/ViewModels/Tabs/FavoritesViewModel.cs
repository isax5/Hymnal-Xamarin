using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Hymnal.Models.DataBase;

namespace Hymnal.ViewModels;

public sealed partial class FavoritesViewModel : BaseViewModel
{
    private readonly HymnsService hymnsService;
    private readonly DatabaseService databaseService;

    public ObservableRangeCollection<Tuple<Hymn, FavoriteHymn>> Hymns { get; } = new();

    public FavoritesViewModel(
        HymnsService hymnsService,
        DatabaseService databaseService)
    {
        this.hymnsService = hymnsService;
        this.databaseService = databaseService;
    }

    public override void OnAppearing()
    {
        base.OnAppearing();

        databaseService.GetTable<FavoriteHymn>()
            .ToListAsync()
            .ToObservable()
            .SubscribeOn(new NewThreadScheduler())
            .Subscribe(async hymnReferences =>
            {
                try
                {
                    var values = GetHymnsAsync(hymnReferences.OrderBy(r => r.Order));

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
        //.Select(items => Observable.Zip(items.Select(item => hymnsService.GetHymnAsync(item).ToObservable())))
        //.Subscribe(values => values.Subscribe(result => MainThread.BeginInvokeOnMainThread(delegate
        //{
        //    Hymns = result.OrderBy(h => h.order).ToList();
        //}), error => error.Report()), error => error.Report());
        //.Subscribe(result => MainThread.BeginInvokeOnMainThread(delegate
        //{
        //    Hymns = result;
        //}), error => error.Report());
    }
}
