using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hymnal.Resources.Languages;

namespace Hymnal.ViewModels;

public sealed partial class HymnViewModel : BaseViewModelParameter<HymnIdParameter>
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

    public override async Task InitializeAsync(NavigatedToEventArgs args)
    {
        base.InitializeAsync(args);

        await Shell.Current.DisplayAlert(null, $"Numero: {Parameter?.Number}", LanguageResources.Generic_Ok);
    }


    [RelayCommand]
    private void CarouselViewPositionChanged()
    { }
}
