using MvvmCross.Forms.Views;
using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms.Xaml;
using Hymnal.Core.ViewModels;

namespace Hymnal.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxTabbedPagePresentation(WrapInNavigationPage = false, Title = "Settins")]
    public partial class SettingsPage : MvxContentPage<SettingsViewModel>
    {
        public SettingsPage()
        {
            InitializeComponent();
        }
    }
}
