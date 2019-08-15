using Hymnal.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace Hymnal.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxTabbedPagePresentation(WrapInNavigationPage = false, Title = "Favoritos" )]
    public partial class FavoritesPage : MvxContentPage<FavoritesViewModel>
    {
        public FavoritesPage()
        {
            InitializeComponent();
        }
    }
}
