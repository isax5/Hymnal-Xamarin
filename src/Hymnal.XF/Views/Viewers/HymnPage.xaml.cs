using Hymnal.XF.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HymnPage : BaseContentPage<HymnViewModel>
    {
        public HymnPage()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.Tizen)
            {
                this.SetBinding(TitleProperty, $"{nameof(ViewModel.Hymn)}.{nameof(ViewModel.Hymn.Title)}");
                hymnContentLabel.FontSize = 80;
                backgroundImage.Source = new FileImageSource { File = "Background.png" };
            }

            if (Device.RuntimePlatform != Device.iOS)
            {
                ToolbarItems.Remove(CloseToolbar);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!ViewModel.Language.SupportSheets)
                ToolbarItems.Remove(SheetToolbar);
        }
    }
}
