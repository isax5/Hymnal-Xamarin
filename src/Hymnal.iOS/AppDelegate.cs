using Foundation;
using MvvmCross.Forms.Platforms.Ios.Core;
using UIKit;
using Xamarin.Forms;

namespace Hymnal.iOS
{
    [Register(nameof(AppDelegate))]
    public partial class AppDelegate : MvxFormsApplicationDelegate<Setup, Core.App, UI.App>
    {

    }
}
