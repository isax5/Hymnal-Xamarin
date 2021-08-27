using Foundation;
using Hymnal.XF.Constants;
using Prism;
using Prism.Ioc;
using UIKit;

namespace Hymnal.XF.iOS
{
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
            LoadApplication(new App(new IosInitializer()));

            return base.FinishedLaunching(app, options);
        }

    // TODO: Startup actions (shortcuts) are not working yet
    public override void PerformActionForShortcutItem(UIApplication application, UIApplicationShortcutItem shortcutItem, UIOperationHandler completionHandler)
    {
        //base.PerformActionForShortcutItem(application, shortcutItem, completionHandler);

        if (shortcutItem == null || App.Current == null) return;

        switch (shortcutItem.Type)
        {
            case "SearchAction":
                App.Current?.PerformPageRequest(NavRoutes.SearchPageAsFormSheetModal, true);
                break;

            case "HistoryAction":
                App.Current?.PerformPageRequest(NavRoutes.RecordsPageAsFormSheetModal, true);
                break;

            default:
                break;
        }
    }
    }

    public class IosInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}
