using Hymnal.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxModalPresentation(WrapInNavigationPage = true)]
    //[MvxContentPagePresentation(WrapInNavigationPage = true, NoHistory = false)]
    public partial class HymnPage : MvxContentPage<HymnViewModel>
    {
        public HymnPage()
        {
            InitializeComponent();
#if TIZEN
            this.SetBinding(TitleProperty, $"{nameof(ViewModel.Hymn)}.{nameof(ViewModel.Hymn.Title)}");
            hymnContentLabel.FontSize = 80;
            backgroundImage.Source = new FileImageSource { File = "Background.png" };
#endif

#if !__IOS__
            ToolbarItems.Remove(CloseToolbar);
#endif
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
