using Hymnal.Core.ViewModels;
using Hymnal.UI.Pages.Base;
using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms.Xaml;

namespace Hymnal.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxContentPagePresentation(WrapInNavigationPage = true, NoHistory = false, Title = "Buscar")]
    public partial class SearchPage : CustomMvxContentPage<SearchViewModel>
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            HymnSearchBar.Focus();

            HymnSearchBar.SearchButtonPressed += (s, e) =>
            {
                HymnSearchBar.Unfocus();
            };
        }
    }
}
