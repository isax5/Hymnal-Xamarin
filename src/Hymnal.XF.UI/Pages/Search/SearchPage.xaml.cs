using Hymnal.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
#if TIZEN
    [MvxTabbedPagePresentation(WrapInNavigationPage = false)]
#else
    [MvxContentPagePresentation(WrapInNavigationPage = true, NoHistory = false)]
#endif
    public partial class SearchPage : MvxContentPage<SearchViewModel>
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
