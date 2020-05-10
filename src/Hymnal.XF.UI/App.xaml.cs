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
            //AppTheme = AppTheme.Unspecified;
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
        #endregion
    }
}
