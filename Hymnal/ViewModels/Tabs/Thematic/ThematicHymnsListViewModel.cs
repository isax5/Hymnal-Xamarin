using System.Collections;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using CommunityToolkit.Mvvm.Input;

namespace Hymnal.ViewModels;

public sealed partial class ThematicHymnsListViewModel : BaseViewModelParameter<Ambit>
{
    private readonly HymnsService hymnsService;
    private readonly PreferencesService preferencesService;

    #region Properties
    [ObservableProperty]
    private IEnumerable hymns;
    #endregion


    public ThematicHymnsListViewModel(
        HymnsService hymnsService,
        PreferencesService preferencesService)
    {
        this.hymnsService = hymnsService;
        this.preferencesService = preferencesService;
    }

    public override void Initialize()
    {
        base.Initialize();

        hymnsService.GetHymnListAsync(preferencesService.ConfiguredHymnalLanguage)
            .ToObservable()
            .SubscribeOn(new NewThreadScheduler())
            .Select(result => result.Where(h => h.Number >= Parameter.Star && h.Number <= Parameter.End))//.GetRange(Parameter.Star - 1, Parameter.End))
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
                HymnalLanguage = preferencesService.ConfiguredHymnalLanguage,
            }.AsParameter());
    }
}
