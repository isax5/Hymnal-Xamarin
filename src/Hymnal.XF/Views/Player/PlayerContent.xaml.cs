using Hymnal.XF.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerContent : BaseContentPage<PlayerViewModel>
    {
        public PlayerContent()
        {
            InitializeComponent();

            if (Device.RuntimePlatform != Device.iOS)
            {
                ToolbarItems.Remove(CloseToolbar);
            }
        }
    }
}
