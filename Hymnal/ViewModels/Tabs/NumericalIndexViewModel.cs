using CommunityToolkit.Mvvm.Collections;

namespace Hymnal.ViewModels;
public sealed partial class NumericalIndexViewModel : BaseViewModel
{
    private readonly HymnsService hymnsService;

    #region Properties
    [ObservableProperty]
    private ObservableGroupedCollection<string, Hymn> hymns;
    #endregion


    public NumericalIndexViewModel(HymnsService hymnsService)
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
                Hymns = result.OrderByNumber().GroupByNumber();
            }), error => error.Report());
    }
}
