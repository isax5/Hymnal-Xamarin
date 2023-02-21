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

    public override void Initialize()
    {
        base.Initialize();

        hymnsService.GetHymnListAsync(InfoConstants.HymnsLanguages.First())
            .ToObservable()
            .Subscribe(result => MainThread.BeginInvokeOnMainThread(delegate
            {
                Hymns = deviceInfo.Platform == DevicePlatform.WinUI
                    ? result.OrderByTitle()
                    : result.OrderByTitle().GroupByTitle();
            }), error => error.Report());
    }
}
