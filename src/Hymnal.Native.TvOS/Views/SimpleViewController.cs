using Foundation;
using System;
using MvvmCross.Platforms.Tvos.Views;
using Hymnal.Core.ViewModels;
using MvvmCross.Platforms.Tvos.Presenters.Attributes;

namespace Hymnal.Native.TvOS
{
    [MvxFromStoryboard("Main")]
    [MvxTabPresentation(TabName = "Simple")]
    public partial class SimpleViewController : MvxViewController<SimpleViewModel>
    {
        public SimpleViewController (IntPtr handle) : base (handle)
        {
        }
    }
}