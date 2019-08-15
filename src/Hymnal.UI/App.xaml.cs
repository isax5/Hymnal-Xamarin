using Xamarin.Forms;

namespace Hymnal.UI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            HotReloader.Current.Run(this);
        }
    }
}
