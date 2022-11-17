using Hymnal.ViewModels;

namespace Hymnal.Views;

public partial class NumberPage : BaseContentPage<NumberViewModel>
{
    public NumberPage(NumberViewModel vm)
        : base(vm)
    {
        InitializeComponent();
    }

    private void HymnNumberEntry_Focused(object sender, FocusEventArgs e)
    {

    }

    private void OpenButton_Clicked(object sender, EventArgs e)
    {

    }
}
