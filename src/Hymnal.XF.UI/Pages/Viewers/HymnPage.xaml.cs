using Hymnal.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxModalPresentation(WrapInNavigationPage = true)]
    //[MvxContentPagePresentation(WrapInNavigationPage = true, NoHistory = false)]
    public partial class HymnPage : BaseContentPage<HymnViewModel>
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

            // Toolbar player
            //PlaySomethingToolbarItem.Text = string.Empty;
            //PlaySomethingToolbarItem.Command = new Command(() => ViewModel.PlayCommand.Execute());
            //PlaySomethingToolbarItem.SetBinding(ToolbarItem.IconImageSourceProperty, "IsPlaying", BindingMode.Default, null, "ToolbarPlaying{0}");

            //OpenPlayerToolbarItem.SetBinding(ToolbarItem.TextProperty, "Hymn.Title");
            //OpenPlayerToolbarItem.Command = new Command(() => ViewModel.OpenPlayerCommand.Execute());
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (!ViewModel.Language.SupportSheets)
                ToolbarItems.Remove(SheetToolbar);

            //PlayerVisible = ViewModel.Language.SupportMusic;
        }
    }
}
