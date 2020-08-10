using Hymnal.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
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
#if TIZEN
            backgroundImage.Source = new FileImageSource { File = "Background.png" };
#endif
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Focuse HymnSearchBar
            if (string.IsNullOrWhiteSpace(HymnSearchBar.Text))
            {
                HymnSearchBar.Focus();
            }
        }

        private void HymnSearchBar_SearchButtonPressed(object sender, System.EventArgs e)
        {
            HymnSearchBar.Unfocus();
        }
    }
}
