using System;
using System.ComponentModel;
using Hymnal.XF.Resources.Languages;
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

        public SearchPage(
            IDeviceInfo deviceInfo,
            INavigationService navigationService)
        {
            InitializeComponent();

            this.deviceInfo = deviceInfo;
            this.navigationService = navigationService;
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

        #region IModalPage
        string IModalPage.CloseButtonText => Languages.Generic_Close;

        void IModalPage.PopModal() => navigationService.GoBackAsync(null, true, true);
        #endregion

        #region ISearchPage
        private ISearchDelegate _delegate;
        ISearchDelegate ISearchPage.Delegate
        {
            get => _delegate;
            set
            {
                _delegate = value;

                ViewModel.PropertyChanged += ViewModel_PropertyChanged;
            }
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(ViewModel.TextSearchBar)) && _delegate is not null && !ViewModel.TextSearchBar.Equals(_delegate.SearchText))
                _delegate.SearchText = ViewModel.TextSearchBar;
        }

        ISearchPageSettings ISearchPage.Settings { get; } = new SearchPageSettings { };

        string ISearchPage.PlaceholderText => HymnSearchBar.Placeholder;

        Color ISearchPage.PlaceHolderColor => (Color)App.Current.ThemeHelper.CurrentResourceDictionaryTheme["PrimaryLightColor"];

        Color ISearchPage.TextColor => (Color)App.Current.ThemeHelper.CurrentResourceDictionaryTheme["NavBarTextColor"];

        void ISearchPage.OnSearchBarTextChanged(in string text) => ViewModel.TextSearchBar = text;
        void ISearchPage.SearchTapped(in string text) { }
        void ISearchPage.Focused() { }
        void ISearchPage.Unfocused() { }
        void ISearchPage.Canceled() { }
        #endregion
    }
}
