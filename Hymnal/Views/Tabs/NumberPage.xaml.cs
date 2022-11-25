namespace Hymnal.Views;

public sealed partial class NumberPage : BaseContentPage<NumberViewModel>
{
    public NumberPage(NumberViewModel vm)
        : base(vm)
    {
        InitializeComponent();
    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);
        HymnNumberEntry.Unfocus(); // Maui problem dismissiong keyboard
    }

    private void HymnNumberEntry_Focused(object sender, FocusEventArgs e)
    {
        // Maui problem selecting content of the entry
        HymnNumberEntry.CursorPosition = 0;
        HymnNumberEntry.SelectionLength = HymnNumberEntry.Text != null ? HymnNumberEntry.Text.Length : 0;
    }

    private void OpenButton_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(HymnNumberEntry.Text))
            HymnNumberEntry.Focus(); // Maui problem showing keyboard
    }
}
