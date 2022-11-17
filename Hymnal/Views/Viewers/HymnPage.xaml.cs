using Hymnal.ViewModels;

namespace Hymnal.Views;

public partial class HymnPage : BaseContentPage<HymnViewModel>
{
    public HymnPage(HymnViewModel vm)
        : base(vm)
    {
        InitializeComponent();
    }
}
