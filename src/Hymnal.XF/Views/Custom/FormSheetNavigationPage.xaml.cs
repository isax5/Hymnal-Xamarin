using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.Views.Custom
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public sealed partial class FormSheetNavigationPage : NavigationPage
    {
        public FormSheetNavigationPage()
        {
            InitializeComponent();
        }

        public FormSheetNavigationPage(Page root) : base(root)
        {
            InitializeComponent();
        }
    }
}
