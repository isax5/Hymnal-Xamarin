using Hymnal.Core;
using Hymnal.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxModalPresentation(WrapInNavigationPage = true)]
    public partial class HymnPage : MvxContentPage<HymnViewModel>
    {
        public HymnPage()
        {
            InitializeComponent();

            if (Device.RuntimePlatform != Device.iOS)
                ToolbarItems.Remove(CloseToolbar);

            if (!Constants.USING_SHEETS)
                ToolbarItems.Remove(SheetToolbar);
        }
    }
}
