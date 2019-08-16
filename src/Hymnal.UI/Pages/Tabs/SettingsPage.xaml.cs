using MvvmCross.Forms.Views;
using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms.Xaml;
using Hymnal.Core.ViewModels;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Hymnal.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxTabbedPagePresentation(WrapInNavigationPage = true, Title = "Settings")]
    public partial class SettingsPage : MvxContentPage<SettingsViewModel>
    {
        public SettingsPage()
        {
            InitializeComponent();
        }
    }
}
