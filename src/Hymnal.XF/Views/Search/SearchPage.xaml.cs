using System;
using Hymnal.XF.Models.Events;
using Hymnal.XF.ViewModels;
using Prism.Navigation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : BaseContentPage<SearchViewModel>, ISearchPage, IModalPage
    {
        private readonly IDeviceInfo deviceInfo;
        private readonly INavigationService navigationService;

        public string PlaceholderText => HymnSearchBar.Placeholder;
        public Color PlaceHolderColor => (Color)App.Current.ThemeHelper.CurrentResourceDictionaryTheme["PrimaryLightColor"];
        public Color TextColor => (Color)App.Current.ThemeHelper.CurrentResourceDictionaryTheme["NavBarTextColor"];
        public IObservable<OSAppTheme> ObservableThemeChange => App.Current.ThemeHelper.ObservableThemeChange;
        public ISearchPageSettings Settings { get; }

        public SearchPage(
            IDeviceInfo deviceInfo,
            INavigationService navigationService)
        {
            InitializeComponent();

            this.deviceInfo = deviceInfo;
            this.navigationService = navigationService;

            Settings = new SearchPageSettings();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Focuse HymnSearchBar
            if (deviceInfo.Platform != Xamarin.Essentials.DevicePlatform.iOS && string.IsNullOrWhiteSpace(HymnSearchBar.Text))
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
