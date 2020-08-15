using Hymnal.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : MvxContentPage<SearchViewModel>, IMvxOverridePresentationAttribute
    {
        public SearchPage()
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.Tizen)
            {
                backgroundImage.Source = new FileImageSource { File = "Background.png" };
            }
        }

        public MvxBasePresentationAttribute PresentationAttribute(MvxViewModelRequest request)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Tizen:
                    return new MvxTabbedPagePresentationAttribute
                    {
                        WrapInNavigationPage = false
                    };
                default:
                    return new MvxContentPagePresentationAttribute
                    {
                        WrapInNavigationPage = true,
                        NoHistory = false
                    };
            }
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
