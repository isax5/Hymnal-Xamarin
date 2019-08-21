using Xamarin.Forms;

namespace Hymnal.UI
{
    public partial class App : Application
    {
        public static new App Current;

        public FileImageSource BackImage => (FileImageSource)Resources["BackImage"];

        public FileImageSource BackLightImage => (FileImageSource)Resources["BackLightImage"];


        public App()
        {
            Current = this;
            InitializeComponent();

            HotReloader.Current.Run(this);
        }
    }
}
