using Hymnal.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxTabbedPagePresentation(TabbedPosition.Root, NoHistory = true, WrapInNavigationPage = true)]
    public partial class RootPage : MvxTabbedPage<RootViewModel>
    {
        public RootPage()
        {
            InitializeComponent();
        }
    }
}
