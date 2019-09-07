using Xamarin.Forms;

namespace Hymnal.UI
{
    public partial class App : Application
    {
        public static new App Current;

        public App()
        {
            Current = this;
            InitializeComponent();

            //HotReloader.Current.Run(this);
        }
    }
}
