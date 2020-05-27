using Hymnal.Core.ViewModels;
using MvvmCross.Platforms.Ios.Views;
using System;
using MvvmCross.Platforms.Ios.Presenters.Attributes;

namespace Hymnal.iOS.Views
{
    [MvxFromStoryboard("Main")]
    [MvxRootPresentation]
    //[MvxTabPresentation(WrapInNavigationController = true)]
    public partial class RootViewController : MvxTabBarViewController<RootViewModel>
    {
        public RootViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

        }
    }
}