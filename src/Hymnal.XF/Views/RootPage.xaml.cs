using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public sealed partial class RootPage : TabbedPage
    {
        public RootPage()
        {
            InitializeComponent();
        }

        protected override void OnChildAdded(Element child)
        {
            base.OnChildAdded(child);

            if (child is NavigationPage navigationPage)
                if (navigationPage.RootPage is ITabbedPage tabbedPage)
                    navigationPage.Title = tabbedPage.TabbedPageName;
        }
    }
}
