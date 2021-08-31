using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.Views.Custom
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public sealed partial class CustomNavigationPage : NavigationPage
    {
        public CustomNavigationPage() : base()
        {
            InitializeComponent();
        }

        public CustomNavigationPage(Page root) : base(root)
        {
            InitializeComponent();
        }
    }
}
