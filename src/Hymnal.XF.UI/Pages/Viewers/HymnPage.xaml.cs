using Hymnal.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.UI.Pages
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
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (!ViewModel.Language.SupportSheets)
                ToolbarItems.Remove(SheetToolbar);

            if (!ViewModel.Language.SupportMusic)
                ToolbarItems.Remove(MusicToolbar);
        }
    }
}
