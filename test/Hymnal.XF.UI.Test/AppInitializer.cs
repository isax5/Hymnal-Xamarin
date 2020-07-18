using System;
using System.IO;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Hymnal.UI.Test
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android
                    //.ApkFile(@"C:\GitProjects\Hymnal\src\Hymnal.Droid\bin\Release\net.ddns.HimnarioAdventistaSPA.apk")
                    .InstalledApp("net.ddns.HimnarioAdventistaSPA")
                    .StartApp();
            }

            return ConfigureApp
                .iOS
                // Simulator
                //.DeviceIdentifier("0556224C-A776-4646-AF5D-31CD6825AC23")
                //.AppBundle("net.ddns.HimnarioAdventistaSPA")

                // Real device
                .InstalledApp("net.ddns.HimnarioAdventistaSPA")

                .PreferIdeSettings()
                .EnableLocalScreenshots()
                .StartApp();
        }
    }
}
    
