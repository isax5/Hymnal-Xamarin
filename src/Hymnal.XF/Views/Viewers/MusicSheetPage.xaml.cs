using Hymnal.XF.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public sealed partial class MusicSheetPage : BaseContentPage<MusicSheetViewModel>
    {
        public MusicSheetPage()
        {
            InitializeComponent();
        }

        private bool showingNavigationBar = true;
        private void StackLayoutTapped(object sender, System.EventArgs e)
        {
            if (showingNavigationBar)
                NavigationPage.SetHasNavigationBar(this, false);
            else
                NavigationPage.SetHasNavigationBar(this, true);

            showingNavigationBar = !showingNavigationBar;
        }
    }
}
