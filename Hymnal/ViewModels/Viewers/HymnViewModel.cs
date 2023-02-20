using System.Reactive.Linq;
using CommunityToolkit.Mvvm.Input;

namespace Hymnal.ViewModels;

public sealed partial class HymnViewModel : BaseViewModelParameter<HymnIdParameter>
{
    private readonly PreferencesService preferencesService;
    private readonly HymnsService hymnsService;
    private readonly DatabaseService databaseService;

    #region Properties
    public int HymnTitleFontSize => preferencesService.HymnalsFontSize + 10;
    public int HymnFontSize => preferencesService.HymnalsFontSize;

    [ObservableProperty]
    private List<Hymn> carouselHymns;

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

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        HymnParameter = Parameter;
        Language = HymnParameter.HymnalLanguage;

        var result = await hymnsService.GetHymnAsync(HymnParameter.Number, Language);
        CurrentHymn = result;

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

        //// Is Favorite
        //IsFavorite = storageService.All<FavoriteHymn>().ToList()
        //    .Exists(f => f.Number == CurrentHymn.Number && f.HymnalLanguageId.Equals(Language.Id));

        //// Record
        //if (HymnParameter.SaveInRecords)
        //{
        //    IQueryable<RecordHymn> records = storageService.All<RecordHymn>()
        //        .Where(h => h.Number == CurrentHymn.Number && h.HymnalLanguageId.Equals(Language.Id));
        //    storageService.RemoveRange(records);

        //    storageService.Add(CurrentHymn.ToRecordHymn());
        //}
    }


    //[RelayCommand]
    //private void CarouselViewPositionChanged()
    //{
    //    HymnParameter.Number = CurrentHymn.Number;
    //}

    [RelayCommand]
    private async void PreviousHymn()
    {
        var result = await hymnsService.GetHymnListAsync(HymnParameter.HymnalLanguage);
        var newHymnIndex = CurrentHymn.Number - 1;

        if (newHymnIndex > 0)
            CurrentHymn = result[newHymnIndex - 1];
    }

    [RelayCommand]
    private async void NextHymn()
    {
        var result = await hymnsService.GetHymnListAsync(HymnParameter.HymnalLanguage);
        var newHymnIndex = CurrentHymn.Number + 1;

        if (newHymnIndex <= result.Count)
            CurrentHymn = result[newHymnIndex - 1];
    }

    [RelayCommand]
    private async void AddToFavoritesAsync()
    {
        try
        {
            await databaseService.InserAsync(CurrentHymn.ToFavoriteHymn());
        }
        catch (Exception ex)
        {
            ex.Report();
        }
    }
}
