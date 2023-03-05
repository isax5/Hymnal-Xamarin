using System.Collections;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using CommunityToolkit.Mvvm.Input;

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
            .SubscribeOn(new NewThreadScheduler())
            .Subscribe(result =>
            {
                IEnumerable orderedHymns = deviceInfo.Platform == DevicePlatform.WinUI
                    ? result.OrderByTitle()
                    : result.OrderByTitle().GroupByTitle();

                MainThread.BeginInvokeOnMainThread(() => Hymns = orderedHymns);
            }, error => error.Report());
    }

    [RelayCommand]
    private async void OpenHymnAsync(Hymn hymn)
    {
        if (hymn is null)
            return;

        await Shell.Current.GoToAsync(nameof(HymnPage),
            new HymnIdParameter()
            {
                Number = hymn.Number,
                SaveInRecords = true,
                HymnalLanguage = InfoConstants.HymnsLanguages.First(),
            }.AsParameter());
    }
}
