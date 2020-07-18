using System;
using Hymnal.Core.ViewModels;
using MvvmCross.Platforms.Ios.Views;

namespace Hymnal.Native.iOS.Views
{
    [MvxFromStoryboard("Main")]
    //[MvxModalPresentation(WrapInNavigationController = true)]
    public partial class MusicSheetViewController : MvxViewController<MusicSheetViewModel>
    {
        public MusicSheetViewController(IntPtr handle) : base(handle)
        {
        }
    }
}
