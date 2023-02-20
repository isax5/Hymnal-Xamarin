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

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        List<Thematic> result = await hymnsService.GetThematicListAsync(InfoConstants.HymnsLanguages.First());
        Thematics = result;
    }
}
