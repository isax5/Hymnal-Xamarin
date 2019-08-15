using Hymnal.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace Hymnal.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxModalPresentation(WrapInNavigationPage = true, Title = "Title 1")]
    public partial class HymnPage : MvxContentPage<HymnViewModel>
    {
        public HymnPage()
        {
            InitializeComponent();
        }
    }
}
