using Hymnal.Core.ViewModels;
using Hymnal.UI.Pages.Base;
using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms.Xaml;

namespace Hymnal.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxTabbedPagePresentation(WrapInNavigationPage = false, Title = "Favoritos", Icon = "TabFavorites")]
    public partial class FavoritesPage : CustomMvxContentPage<FavoritesViewModel>
    {
        public FavoritesPage()
        {
            InitializeComponent();
        }
    }
}
