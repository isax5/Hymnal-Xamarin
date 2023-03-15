namespace Hymnal.Views;

public sealed partial class HymnPage : BaseContentPage<HymnViewModel>
{
    public HymnPage(HymnViewModel vm)
        : base(vm)
    {
        InitializeComponent();

        SizeChanged += HymnPage_SizeChanged;
    }

    ~HymnPage()
    {
        SizeChanged -= HymnPage_SizeChanged;
    }


    private void HymnPage_SizeChanged(object sender, EventArgs e)
    {
        //var index = MainCarousel.ZIndex;
        //Content = new StackLayout();
        //Content = MainCarousel;
        //MainCarousel.ZIndex = index;
        //Content = MainCarousel;
        //this.UpdateChildrenLayout();
        //this.ForceLayout();
    }
}
