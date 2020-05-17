using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using UIKit;

namespace Hymnal.XF.iOS
{
    public static class Application
    {
        private static void Main(string[] args)
        {
            UIApplication.Main(args, null, nameof(AppDelegate));
        }
    }
}
