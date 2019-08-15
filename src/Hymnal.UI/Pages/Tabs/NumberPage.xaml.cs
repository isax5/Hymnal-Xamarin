using Hymnal.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace Hymnal.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxTabbedPagePresentation(WrapInNavigationPage = false, Title = "Number")]
    public partial class NumberPage : MvxContentPage<NumberViewModel>
    {
        public NumberPage()
        {
            InitializeComponent();
        }
    }
}
