using Hymnal.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace Hymnal.UI.Pages.Tabs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxTabbedPagePresentation(WrapInNavigationPage = false, Title = "Index")]
    [MvxCarouselPagePresentation(CarouselPosition.Root, NoHistory = true)]
    public partial class IndexPage : MvxCarouselPage<IndexViewModel>
    {
        public IndexPage()
        {
            InitializeComponent();
        }
    }
}
