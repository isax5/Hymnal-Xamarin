using Hymnal.ViewModels;

namespace Hymnal.Views;

public sealed partial class HymnPage : BaseContentPage<HymnViewModel>
{
    public HymnPage(HymnViewModel vm)
        : base(vm)
    {
        InitializeComponent();
    }
}
