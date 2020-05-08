using Hymnal.XF.UI.Models;
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
            AppTheme = AppTheme.Unspecified;
            //HotReloader.Current.Run(this);
        }

        #region AppTheme
        private static DarkTheme darkAppTheme = new DarkTheme();
        private static WhiteTheme whiteAppTheme = new WhiteTheme();

        private static AppTheme appTheme;
        public static AppTheme AppTheme
        {
            get => appTheme;
            set
            {
                if (value == appTheme)
                    return;

                appTheme = value;

                lock (Current.Resources)
                {
                    if (Current.Resources.MergedDictionaries.Contains(darkAppTheme))
                    {
                        Current.Resources.MergedDictionaries.Remove(darkAppTheme);
                    }

                    if (Current.Resources.MergedDictionaries.Contains(whiteAppTheme))
                    {
                        Current.Resources.MergedDictionaries.Remove(whiteAppTheme);
                    }


                    switch (value)
                    {
                        case AppTheme.Dark:
                            Current.Resources.MergedDictionaries.Add(darkAppTheme);
                            break;

                        case AppTheme.Light:
                        case AppTheme.Unspecified:
                        default:
                            Current.Resources.MergedDictionaries.Add(whiteAppTheme);
                            break;
                    }
                }
            }
        }
        #endregion
    }
}
