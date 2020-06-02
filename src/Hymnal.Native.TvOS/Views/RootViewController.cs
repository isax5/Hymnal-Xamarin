using Hymnal.Core.ViewModels;
using MvvmCross.Platforms.Tvos.Views;
using System;

namespace Hymnal.Native.TvOS.Views
{
    [MvxFromStoryboard("Main")]
    //[MvxTabPresentation(WrapInNavigationController = true)]
    public partial class RootViewController : MvxTabBarViewController<RootViewModel>
    {
        public RootViewController(IntPtr handle) : base(handle)
        { }
    }
}