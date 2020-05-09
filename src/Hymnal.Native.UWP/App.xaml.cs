using MvvmCross.Platforms.Uap.Core;
using MvvmCross.Platforms.Uap.Views;

namespace Hymnal.UWP
{
    sealed partial class App
    {
        public App()
        {
            InitializeComponent();
        }
    }

    public abstract class HymnalApp : MvxApplication<Setup, Core.App>
    {
    }
}
