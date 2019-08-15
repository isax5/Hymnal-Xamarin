using MvvmCross.Forms.Views;
using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms.Xaml;
using Hymnal.Core.ViewModels;

namespace Hymnal.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxCarouselPagePresentation(CarouselPosition.Carousel, NoHistory = true)]
    public partial class ThematicIndexPage : MvxContentPage<ThematicIndexViewModel>
    {
        public ThematicIndexPage()
        {
            InitializeComponent();
        }
    }
}
