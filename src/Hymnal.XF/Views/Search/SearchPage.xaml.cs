using Hymnal.XF.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : BaseContentPage<SearchViewModel>, ISearchPage
    {
        public ISearchPageSettings Settings { get; }

        public SearchPage()
        {
            InitializeComponent();

            Settings = new SearchPageSettings
            {
                PlaceHolder = HymnSearchBar.Placeholder,
            };
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

        public void OnSearchBarTextChanged(in string text) => ViewModel.TextSearchBar = text;

        private void HymnSearchBar_SearchButtonPressed(object sender, System.EventArgs e)
        {
            HymnSearchBar.Unfocus();
        }
    }
}
