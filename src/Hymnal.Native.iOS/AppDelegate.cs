using Foundation;
using MvvmCross.Platforms.Ios.Core;
using Hymnal.Core;

namespace Hymnal.iOS
{
    [Register(nameof(AppDelegate))]
    public class AppDelegate : MvxApplicationDelegate<Setup, App>
    {
    }
}
