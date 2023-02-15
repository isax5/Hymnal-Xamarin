namespace Hymnal.ViewModels;

public sealed partial class ThematicIndexViewModel : BaseViewModel
{
    private readonly HymnsService hymnsService;

    #region Properties
    public ObservableRangeCollection<Thematic> Thematics { get; } = new();
    #endregion


    public ThematicIndexViewModel(HymnsService hymnsService)
    {
        this.hymnsService = hymnsService;
    }

    public override void Initialize()
    {
        base.Initialize();

        hymnsService.GetThematicListAsync(InfoConstants.HymnsLanguages.First())
            .ToObservable()
            .Subscribe(result => MainThread.BeginInvokeOnMainThread(delegate
            {
                Thematics.ReplaceRange(result);
            }), error => error.Report());
    }
}
