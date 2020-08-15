using System;
using Hymnal.XF.UI.Resources;
using Xamarin.Forms;

namespace Hymnal.XF.UI
{
    public partial class App : Application
    {
        public static new App Current;

        public App()
        {
            Current = this;

            InitializeComponent();
        }

        #region System events
        protected override void OnStart()
        {
            base.OnStart();
            ThemeHelper.CheckTheme();
        }

        protected override void OnResume()
        {
            base.OnResume();
            ThemeHelper.CheckTheme();
        }

        protected override void OnAppLinkRequestReceived(Uri uri)
        {
            base.OnAppLinkRequestReceived(uri);
        }
        #endregion
    }
}
