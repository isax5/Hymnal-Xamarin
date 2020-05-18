using Hymnal.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxModalPresentation(WrapInNavigationPage = true)]
    public partial class MusicSheetPage : MvxContentPage<MusicSheetViewModel>
    {
        public MusicSheetPage()
        {
            InitializeComponent();
        }

        private bool showingNavigationBar = true;
        private void ScrollViewTapped(object sender, System.EventArgs e)
        {
            if (showingNavigationBar)
                NavigationPage.SetHasNavigationBar(this, false);
            else
                NavigationPage.SetHasNavigationBar(this, true);

            showingNavigationBar = !showingNavigationBar;
        }
    }
}
