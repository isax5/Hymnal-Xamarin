using Foundation;
using Hymnal.Core.ViewModels;
using MvvmCross.Platforms.Ios.Views;
using System;
using UIKit;

namespace Hymnal.iOS
{
    [MvxFromStoryboard("Main")]
    public partial class RecordsViewController : MvxViewController<RecordsViewModel>
    {
        public RecordsViewController (IntPtr handle) : base (handle)
        {
        }
    }
}