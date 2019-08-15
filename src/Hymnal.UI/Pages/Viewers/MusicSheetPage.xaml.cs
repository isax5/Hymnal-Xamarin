using MvvmCross.Forms.Views;
using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms.Xaml;
using Hymnal.Core.ViewModels;

namespace Hymnal.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxModalPresentation(WrapInNavigationPage = true, Title = "Sheets")]
    public partial class MusicSheetPage : MvxContentPage<MusicSheetViewModel>
    {
        public MusicSheetPage()
        {
            InitializeComponent();
        }
    }
}
