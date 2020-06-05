using Foundation;
using MvvmCross.Platforms.Ios.Core;
using Hymnal.Core;

namespace Hymnal.Native.iOS
{
    [Register(nameof(AppDelegate))]
    public class AppDelegate : MvxApplicationDelegate<Setup, App>
    {
    }
}
