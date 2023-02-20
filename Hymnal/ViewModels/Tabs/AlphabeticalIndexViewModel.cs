using System.Collections;

namespace Hymnal.ViewModels;
public sealed partial class AlphabeticalIndexViewModel : BaseViewModel
{
    private readonly HymnsService hymnsService;
    private readonly IDeviceInfo deviceInfo;

    #region Properties
    [ObservableProperty]
    private IEnumerable hymns;
    #endregion


    public AlphabeticalIndexViewModel(
        HymnsService hymnsService,
        IDeviceInfo deviceInfo)
    {
        this.hymnsService = hymnsService;
        this.deviceInfo = deviceInfo;
    }

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        List<Hymn> result = await hymnsService.GetHymnListAsync(InfoConstants.HymnsLanguages.First());
        Hymns = deviceInfo.Platform == DevicePlatform.WinUI
            ? result.OrderByTitle()
            : result.OrderByTitle().GroupByTitle();
    }
}
