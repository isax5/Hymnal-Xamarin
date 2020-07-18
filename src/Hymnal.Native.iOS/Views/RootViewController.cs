using System;
using Hymnal.Core.ViewModels;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;

namespace Hymnal.Native.iOS.Views
{
    [MvxFromStoryboard("Main")]
    [MvxRootPresentation]
    public partial class RootViewController : MvxTabBarViewController<RootViewModel>
    {
        public RootViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

        }
    }
}
