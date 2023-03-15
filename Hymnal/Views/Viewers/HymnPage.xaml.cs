namespace Hymnal.Views;

public sealed partial class HymnPage : BaseContentPage<HymnViewModel>
{
    public HymnPage(HymnViewModel vm)
        : base(vm)
    {
        InitializeComponent();
    }

    protected override void OnParentSet()
    {
        base.OnParentSet();

        if (!ViewModel.Parameter.HymnalLanguage.SupportSheets)
            ToolbarItems.Remove(openSheets);
    }
}
