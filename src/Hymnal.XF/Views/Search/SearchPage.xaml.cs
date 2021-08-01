using Hymnal.XF.ViewModels;
using Prism.Navigation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : BaseContentPage<SearchViewModel>, ISearchPage, IModalPage
    {
        private readonly IDeviceInfo deviceinfo;
        private readonly INavigationService navigationService;

        public ISearchPageSettings Settings { get; }

        public SearchPage(
            IDeviceInfo deviceInfo,
            INavigationService navigationService)
        {
            InitializeComponent();

            Settings = new SearchPageSettings
            {
                PlaceHolder = HymnSearchBar.Placeholder,
            };
            deviceinfo = deviceInfo;
            this.navigationService = navigationService;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Focuse HymnSearchBar
            if (deviceinfo.Platform != Xamarin.Essentials.DevicePlatform.iOS && string.IsNullOrWhiteSpace(HymnSearchBar.Text))
            {
                HymnSearchBar.Focus();
            }
        }

        public void OnSearchBarTextChanged(in string text) => ViewModel.TextSearchBar = text;

        private void HymnSearchBar_SearchButtonPressed(object sender, System.EventArgs e)
        {
            HymnSearchBar.Unfocus();
        }

        public void PopModal() => navigationService.GoBackAsync(null, true, true);
    }
}
