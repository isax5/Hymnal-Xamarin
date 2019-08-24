using Hymnal.Core.ViewModels;
using Hymnal.UI.Pages.Base;
using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxModalPresentation(WrapInNavigationPage = true, Title = "Title 1")]
    public partial class HymnPage : CustomMvxContentPage<HymnViewModel>
    {
        public HymnPage()
        {
            InitializeComponent();

            if (Device.RuntimePlatform != Device.iOS)
                ToolbarItems.Remove(CloseToolbar);
        }
    }
}
