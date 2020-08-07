using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Hymnal.XF.UI.Test
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android
                    .InstalledApp("net.ddns.HimnarioAdventistaSPA")
                    .StartApp();
            }

            return ConfigureApp.iOS.StartApp();
        }
    }
}