using Hymnal.Core.ViewModels;
using Hymnal.UI.Pages.Base;
using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms.Xaml;

namespace Hymnal.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxContentPagePresentation(WrapInNavigationPage = true, NoHistory = false, Title = "Historial")]
    public partial class RecordsPage : CustomMvxContentPage<RecordsViewModel>
    {
        public RecordsPage()
        {
            InitializeComponent();
        }
    }
}
