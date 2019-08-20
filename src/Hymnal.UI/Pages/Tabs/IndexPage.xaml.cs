using Hymnal.Core.ViewModels;
using Hymnal.UI.Pages.Base;
using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms.Xaml;

namespace Hymnal.UI.Pages.Tabs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxTabbedPagePresentation(WrapInNavigationPage = false, Title = "Index", Icon = "TabIndex")]
    [MvxCarouselPagePresentation(CarouselPosition.Root, NoHistory = true)]
    public partial class IndexPage : CustomMvxCarouselPage<IndexViewModel>
    {
        public IndexPage()
        {
            InitializeComponent();
        }
    }
}
