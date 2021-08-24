using Foundation;
using Prism;
using Prism.Ioc;
using UIKit;

namespace Hymnal.XF.iOS
{
    [Preserve(AllMembers = true)]
    [Register(nameof(AppDelegate))]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
#if ENABLE_TEST_CLOUD
            // Xamarin.Calabash.Start();
#endif
            Xamarin.Forms.Forms.Init();
            Xamarin.Forms.FormsMaterial.Init();
            LoadApplication(new App(new IOSInitializer()));

            return base.FinishedLaunching(app, options);
        }

        //public override void PerformActionForShortcutItem(UIApplication application, UIApplicationShortcutItem shortcutItem, UIOperationHandler completionHandler)
        //{
        //    //base.PerformActionForShortcutItem(application, shortcutItem, completionHandler);

        //    if (shortcutItem == null || Core.App.Current == null)
        //    {
        //        Debug.WriteLine("No shortcut or app not deployed");
        //        return;
        //    }

        //    Debug.WriteLine($"Shortcut ${shortcutItem.Type}");

        //    switch (shortcutItem.Type)
        //    {
        //        case "SearchAction":
        //            Core.App.Current?.PerformPageRequest(Core.Models.PageRequest.Search);
        //            break;

        //        case "HistoryAction":
        //            Core.App.Current?.PerformPageRequest(Core.Models.PageRequest.Records);
        //            break;

        //        default:
        //            break;
        //    }
        //}
    }

    [Preserve(AllMembers = true)]
    public class IOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}
