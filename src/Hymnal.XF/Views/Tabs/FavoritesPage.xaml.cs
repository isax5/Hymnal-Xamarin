using Hymnal.XF.Resources.Languages;
using Hymnal.XF.ViewModels;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoritesPage : BaseContentPage<FavoritesViewModel>, ITabbedPage
    {
        public string TabbedPageName => Languages.Favorites;

        public FavoritesPage()
        {
            InitializeComponent();
        }
    }
}
