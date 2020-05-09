using Foundation;
using Hymnal.Core.ViewModels;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using System;
using UIKit;

namespace Hymnal.iOS
{
    [MvxFromStoryboard("Main")]
    [MvxModalPresentation(WrapInNavigationController = true)]
    public partial class MusicSheetViewController : MvxViewController<MusicSheetViewModel>
    {
        public MusicSheetViewController (IntPtr handle) : base (handle)
        {
        }
    }
}