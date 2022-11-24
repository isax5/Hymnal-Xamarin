using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Hymnal.ViewModels;

public sealed partial class HymnViewModel : BaseViewModelParameter<HymnIdParameter>
{
    private readonly PreferencesService preferencesService;
    private readonly HymnsService hymnsService;

    #region Properties
    public int HymnTitleFontSize => preferencesService.HymnalsFontSize + 10;
    public int HymnFontSize => preferencesService.HymnalsFontSize;

    [ObservableProperty]
    private IEnumerable<Hymn> carouselHymns;

    [ObservableProperty]
    private Hymn carouselItem;

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


    public HymnViewModel(PreferencesService preferencesService, HymnsService hymnsService)
    {
        this.preferencesService = preferencesService;
        this.hymnsService = hymnsService;
    }

    public override async Task InitializeAsync(NavigatedToEventArgs args)
    {
        await base.InitializeAsync(args);

        var HymnParameter = Parameter;
        Language = HymnParameter.HymnalLanguage;

        try
        {
            CarouselHymns = await hymnsService.GetHymnListAsync(HymnParameter.HymnalLanguage);
            CurrentHymn = await hymnsService.GetHymnAsync(HymnParameter.Number, HymnParameter.HymnalLanguage);
        }
        catch (Exception ex) { ex.Report(); }

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


    [RelayCommand]
    private void CarouselViewPositionChanged()
    {
        //CurrentHymn = CurrentHymn;
        HymnParameter.Number = CurrentHymn.Number;
    }
}
