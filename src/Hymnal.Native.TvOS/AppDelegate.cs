using Foundation;
using Hymnal.Core;
using MvvmCross.Platforms.Tvos.Core;

namespace Hymnal.Native.TvOS
{
    [Register(nameof(AppDelegate))]
    public class AppDelegate : MvxApplicationDelegate<Setup, App>
    { }
}
