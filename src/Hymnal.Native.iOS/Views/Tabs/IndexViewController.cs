using Foundation;
using Hymnal.Core.ViewModels;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using System;
using UIKit;

namespace Hymnal.Native.iOS.Views
{
    [MvxFromStoryboard("Main")]
    [MvxTabPresentation(TabName = "Index", TabIconName = "list tab")]
    public partial class IndexViewController : BaseViewController<IndexViewModel>
    {
        public IndexViewController (IntPtr handle) : base (handle)
        {
        }
    }
}