using CommunityToolkit.Mvvm.Collections;

namespace Hymnal.ViewModels;
public sealed partial class AlphabeticalIndexViewModel : BaseViewModel
{
    private readonly HymnsService hymnsService;

    #region Properties
    [ObservableProperty]
    private ObservableGroupedCollection<string, Hymn> hymns;
    #endregion


    public AlphabeticalIndexViewModel(HymnsService hymnsService)
    {
        this.hymnsService = hymnsService;
    }

    public override void Initialize()
    {
        base.Initialize();

        hymnsService.GetHymnListAsync(InfoConstants.HymnsLanguages.First())
            .ToObservable()
            .Subscribe(result => MainThread.BeginInvokeOnMainThread(delegate
            {
                Hymns = result.OrderByTitle().GroupByTitle();
            }), error => error.Report());
    }
}
