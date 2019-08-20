using Hymnal.Core.ViewModels;
using Hymnal.UI.Pages.Base;
using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms.Xaml;

namespace Hymnal.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxContentPagePresentation(WrapInNavigationPage = true, NoHistory = false)]
    public partial class ThematicHymnsListPage : CustomMvxContentPage<ThematicHymnsListViewModel>
    {
        public ThematicHymnsListPage()
        {
            InitializeComponent();
        }
    }
}
