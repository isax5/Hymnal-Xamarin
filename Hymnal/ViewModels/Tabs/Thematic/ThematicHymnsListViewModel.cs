using System.Collections;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using CommunityToolkit.Mvvm.Input;

namespace Hymnal.ViewModels;

public sealed partial class ThematicHymnsListViewModel : BaseViewModelParameter<Ambit>
{
    private readonly HymnsService hymnsService;

    #region Properties
    [ObservableProperty]
    private IEnumerable hymns;
    #endregion


    public ThematicHymnsListViewModel(HymnsService hymnsService)
    {
        this.hymnsService = hymnsService;
    }

    public override void Initialize()
    {
        base.Initialize();

        hymnsService.GetHymnListAsync(InfoConstants.HymnsLanguages.First())
            .ToObservable()
            .SubscribeOn(new NewThreadScheduler())
            .Select(result => result.GetRange(Parameter.Star, Parameter.End))
            .Subscribe(result => MainThread.BeginInvokeOnMainThread(() => Hymns = result),
            error => error.Report());
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
