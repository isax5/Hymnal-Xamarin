namespace Hymnal.Views;

public sealed partial class HymnPage : BaseContentPage<HymnViewModel>
{
    public HymnPage(HymnViewModel vm, Setup setup)
        : base(vm)
    {
        InitializeComponent();

        // TODO: Executing last part of the setup here becouse I didn't find a better place
        setup.AfterStartUp();
    }

    protected override void OnParentSet()
    {
        base.OnParentSet();

        //if (!ViewModel.Parameter.HymnalLanguage.SupportSheets)
        //    ToolbarItems.Remove(openSheets);
    }
}
