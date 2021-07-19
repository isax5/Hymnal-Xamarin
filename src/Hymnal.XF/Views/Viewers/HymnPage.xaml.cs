using Hymnal.XF.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    //[MvxContentPagePresentation(WrapInNavigationPage = true, NoHistory = false)]
    public partial class HymnPage : BaseContentPage<HymnViewModel>
    {
        public HymnPage()
        {
            InitializeComponent();

            //if (Device.RuntimePlatform == Device.Tizen)
            //{
            //    this.SetBinding(TitleProperty, $"{nameof(ViewModel.Hymn)}.{nameof(ViewModel.Hymn.Title)}");
            //    hymnContentLabel.FontSize = 80;
            //    backgroundImage.Source = new FileImageSource { File = "Background.png" };
            //}

            //if (Device.RuntimePlatform != Device.iOS)
            //{
            //    ToolbarItems.Remove(CloseToolbar);
            //}

            // Toolbar player
            //PlaySomethingToolbarItem.Text = string.Empty;
            //PlaySomethingToolbarItem.Command = new Command(() => ViewModel.PlayCommand.Execute());
            //PlaySomethingToolbarItem.SetBinding(ToolbarItem.IconImageSourceProperty, "IsPlaying", BindingMode.Default, null, "ToolbarPlaying{0}");

            //OpenPlayerToolbarItem.SetBinding(ToolbarItem.TextProperty, "Hymn.Title");
            //OpenPlayerToolbarItem.Command = new Command(() => ViewModel.OpenPlayerCommand.Execute());
        }

        //protected override void OnBindingContextChanged()
        //{
        //    base.OnBindingContextChanged();

        //    if (!ViewModel.Language.SupportSheets)
        //        ToolbarItems.Remove(SheetToolbar);

        //    //PlayerVisible = ViewModel.Language.SupportMusic;
        //}
    }
}
