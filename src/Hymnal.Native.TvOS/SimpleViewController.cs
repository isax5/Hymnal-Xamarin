using Foundation;
using System;
using MvvmCross.Platforms.Tvos.Views;
using Hymnal.Core.ViewModels;

namespace Hymnal.Native.TvOS
{
    [MvxFromStoryboard("Main")]
    public partial class SimpleViewController : MvxViewController<SimpleViewModel>
    {
        public SimpleViewController (IntPtr handle) : base (handle)
        {
        }
    }
}