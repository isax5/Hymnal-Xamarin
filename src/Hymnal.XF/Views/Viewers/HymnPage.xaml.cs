using System.ComponentModel;
using Hymnal.XF.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public sealed partial class HymnPage : BaseContentPage<HymnViewModel>
    {
        public HymnPage()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.Tizen)
            {
                this.SetBinding(TitleProperty, $"{nameof(ViewModel.Hymn)}.{nameof(ViewModel.Hymn.Title)}");
//                hymnContentLabel.FontSize = 80;
//                BackgroundImage.Source = new FileImageSource { File = "Background.png" };
            }

            if (Device.RuntimePlatform != Device.iOS)
            {
                ToolbarItems.Remove(CloseToolbar);
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            ViewModel.PropertyChanged += ViewModelOnPropertyChanged;
        }

        private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(ViewModel.Language)) && ViewModel.Language is not null)
            {
                ViewModel.PropertyChanged -= ViewModelOnPropertyChanged;

                if (!ViewModel.Language.SupportSheets)
                    ToolbarItems.Remove(SheetToolbar);
            }
        }
    }
}
