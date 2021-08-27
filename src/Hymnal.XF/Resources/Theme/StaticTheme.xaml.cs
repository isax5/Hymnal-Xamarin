using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.Resources.Theme
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public sealed partial class StaticTheme : ResourceDictionary
    {
        public StaticTheme()
        {
            InitializeComponent();
        }
    }
}
