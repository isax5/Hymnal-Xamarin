namespace Hymnal.Views;

public sealed partial class FavoritesPage : BaseContentPage<FavoritesViewModel>
{
    public FavoritesPage(FavoritesViewModel vm)
        : base(vm)
    {
        InitializeComponent();
    }
}
