using System.Diagnostics;
using Foundation;
using MvvmCross.Forms.Platforms.Ios.Core;
using UIKit;

namespace Hymnal.iOS
{
    [Register(nameof(AppDelegate))]
    public partial class AppDelegate : MvxFormsApplicationDelegate<Setup, Core.App, UI.App>
    {
        public override void PerformActionForShortcutItem(UIApplication application, UIApplicationShortcutItem shortcutItem, UIOperationHandler completionHandler)
        {
            //base.PerformActionForShortcutItem(application, shortcutItem, completionHandler);

            if (shortcutItem == null || Hymnal.Core.App.Current == null)
            {
                Debug.WriteLine("No shortcut or app not deployed");
                return;
            }

            Debug.WriteLine($"Shortcut ${shortcutItem.Type}");

            switch (shortcutItem.Type)
            {
                case "SearchAction":
                    Hymnal.Core.App.Current?.LaunchPage<Hymnal.Core.ViewModels.SearchViewModel>();
                    break;

                case "HistoryAction":
                    Hymnal.Core.App.Current?.LaunchPage<Hymnal.Core.ViewModels.RecordsViewModel>();
                    break;

                default:
                    break;
            }
        }
    }
}
