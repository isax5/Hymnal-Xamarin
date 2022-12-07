using System.Reactive.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Hymnal.Models.DataBase;

namespace Hymnal.ViewModels;

public sealed partial class FavoritesViewModel : BaseViewModel
{
    private readonly DatabaseService databaseService;

    [ObservableProperty]
    private List<FavoriteHymn> hymns;

    public FavoritesViewModel(DatabaseService databaseService)
    {
        this.databaseService = databaseService;
    }

    public override void OnAppearing()
    {
        base.OnAppearing();

        databaseService.GetTable<FavoriteHymn>()
            .ToListAsync()
            .ToObservable()
            .Subscribe(result => MainThread.BeginInvokeOnMainThread(delegate
            {
                Hymns = result;
            }), error => error.Report());
    }
}
