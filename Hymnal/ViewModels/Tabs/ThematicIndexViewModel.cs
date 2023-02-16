namespace Hymnal.ViewModels;

public sealed partial class ThematicIndexViewModel : BaseViewModel
{
    private readonly HymnsService hymnsService;

    #region Properties
    [ObservableProperty]
    private List<Thematic> thematics;
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
                Thematics = result;
            }), error => error.Report());
    }
}
