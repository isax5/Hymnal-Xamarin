using System.Reactive.Concurrency;
using System.Reactive.Linq;
using CommunityToolkit.Mvvm.Input;
using Hymnal.Models.DataBase;

namespace Hymnal.ViewModels;

public sealed partial class HymnViewModel : BaseViewModelParameter<HymnIdParameter>
{
    private readonly PreferencesService preferencesService;
    private readonly HymnsService hymnsService;
    private readonly DatabaseService databaseService;

    #region Properties
    public int HymnTitleFontSize => preferencesService.HymnalsFontSize + 10;
    public int HymnFontSize => preferencesService.HymnalsFontSize;

    //[ObservableProperty]
    //private List<Hymn> carouselHymns;

    [ObservableProperty]
    private Hymn currentHymn;

    [ObservableProperty]
    private HymnalLanguage language;

    [ObservableProperty]
    private bool isFavorite;

    [ObservableProperty]
    private bool isPlaying;

    [ObservableProperty]
    private HymnIdParameter hymnParameter;

    public bool BackgroundImageAppearance => preferencesService.BackgroundImageAppearance;
    #endregion


    public HymnViewModel(
        PreferencesService preferencesService,
        HymnsService hymnsService,
        DatabaseService databaseService)
    {
        this.preferencesService = preferencesService;
        this.hymnsService = hymnsService;
        this.databaseService = databaseService;
    }

    public override void Initialize()
    {
        base.Initialize();

        if (Parameter is null)
            return;

        HymnParameter = Parameter;
        Language = HymnParameter.HymnalLanguage;

        hymnsService.GetHymnAsync(HymnParameter.Number, Language)
            .ToObservable()
            .SubscribeOn(new NewThreadScheduler())
            .Subscribe(result => MainThread.BeginInvokeOnMainThread(() => CurrentHymn = result),
            error => error.Report());

        //Observable.Zip(
        //    hymnsService.GetHymnListAsync(HymnParameter.HymnalLanguage).ToObservable(),
        //    hymnsService.GetHymnAsync(HymnParameter.Number, HymnParameter.HymnalLanguage).ToObservable(),
        //    (list, hymn) => new Tuple<List<Hymn>, Hymn>(list, hymn))
        //    .Subscribe(result => MainThread.BeginInvokeOnMainThread(delegate
        //    {
        //        CarouselHymns = result.Item1;
        //        CurrentHymn = result.Item2;
        //    }), error => error.Report());

        //IsPlaying = mediaManager.IsPlaying();

        // Is Favorite
        UpdateHymnIsFavorite();

        // Record
        if (HymnParameter.SaveInRecords)
        {
            databaseService.FindAsync<RecordHymn>(r => r.Number == HymnParameter.Number && r.HymnalLanguageId == HymnParameter.HymnalLanguage.Id)
                .ToObservable()
                .Subscribe(async reference =>
                {
                    try
                    {
                        if (reference is not null)
                            await databaseService.RemoveAsync(reference);

                        await databaseService.InserAsync(Parameter.ToRecordHymn());
                        var count = await databaseService.GetTable<RecordHymn>().CountAsync();

                        if (count > AppConstants.MAXIMUM_RECORDS)
                        {
                            do
                            {
                                RecordHymn toDelete = await databaseService.GetTable<RecordHymn>().OrderBy(r => r.Id).FirstAsync();
                                await databaseService.RemoveAsync(toDelete);
                                count--;
                            } while (count > AppConstants.MAXIMUM_RECORDS);
                        }
                    }
                    catch (Exception ex)
                    {
                        ex.Report();
                    }
                },
                error => error.Report());
        }
    }


    private void UpdateHymnIsFavorite()
    {
        var hymnNumber = CurrentHymn?.Number ?? HymnParameter.Number;
        databaseService
            .FindAsync<FavoriteHymn>(h => h.HymnalLanguageId == HymnParameter.HymnalLanguage.Id & h.Number == hymnNumber)
            .ToObservable()
            .Subscribe(result => MainThread.BeginInvokeOnMainThread(() => IsFavorite = result is not null),
            error => error.Report());
    }

    //[RelayCommand]
    //private void CarouselViewPositionChanged()
    //{
    //    HymnParameter.Number = CurrentHymn.Number;
    //}

    [RelayCommand]
    private void PreviousHymn()
    {
        hymnsService.GetHymnListAsync(HymnParameter.HymnalLanguage)
            .ToObservable()
            .Finally(UpdateHymnIsFavorite)
            .Subscribe(result => MainThread.BeginInvokeOnMainThread(delegate
            {
                var newHymnIndex = CurrentHymn.Number - 1;

                if (newHymnIndex > 0)
                    CurrentHymn = result[newHymnIndex - 1];
            }), error => error.Report());
    }

    [RelayCommand]
    private void NextHymn()
    {
        hymnsService.GetHymnListAsync(HymnParameter.HymnalLanguage)
            .ToObservable()
            .Finally(UpdateHymnIsFavorite)
            .Subscribe(result => MainThread.BeginInvokeOnMainThread(delegate
            {
                var newHymnIndex = CurrentHymn.Number + 1;

                if (newHymnIndex <= result.Count)
                    CurrentHymn = result[newHymnIndex - 1];
            }), error => error.Report());
    }

    [RelayCommand]
    private async void AddToFavoritesAsync()
    {
        if (IsFavorite)
            databaseService
                .FindAsync<FavoriteHymn>(h => h.HymnalLanguageId == HymnParameter.HymnalLanguage.Id & h.Number == HymnParameter.Number)
                .ToObservable()
                .Subscribe(result =>
                {
                    if (result is not null)
                    {
                        databaseService.RemoveAsync(result)
                            .ToObservable()
                            .Subscribe(result2 => MainThread.BeginInvokeOnMainThread(() => IsFavorite = false),
                            error => error.Report());
                    }
                },
                error => error.Report());
        else
            databaseService.InserAsync(CurrentHymn.ToFavoriteHymn())
                .ToObservable()
                .Subscribe(result => MainThread.BeginInvokeOnMainThread(() => IsFavorite = true),
                error => error.Report());
    }
}
