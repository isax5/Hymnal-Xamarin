using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Hymnal.ViewModels;

public sealed partial class HymnViewModel : BaseViewModel
{
    private readonly PreferencesService preferencesService;

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


    public HymnViewModel(PreferencesService preferencesService)
    {
        this.preferencesService = preferencesService;
    }


    [RelayCommand]
    private void CarouselViewPositionChanged()
    { }
}
