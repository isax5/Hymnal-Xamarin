namespace Hymnal.Views;

public sealed partial class MusicSheetPage : BaseContentPage<MusicSheetViewModel>
{
    public MusicSheetPage(MusicSheetViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }

    private bool showingNavigationBar = true;
    private void ViewTapped(object sender, EventArgs e)
    {
        //NavigationPage.SetHasNavigationBar(this, !showingNavigationBar);

        //showingNavigationBar = !showingNavigationBar;
    }
}
