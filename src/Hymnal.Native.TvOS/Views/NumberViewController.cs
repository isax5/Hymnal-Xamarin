using Hymnal.Core.ViewModels;
using MvvmCross.Platforms.Tvos.Presenters.Attributes;
using MvvmCross.Platforms.Tvos.Views;
using System;

namespace Hymnal.Native.TvOS
{
    [MvxFromStoryboard("Main")]
    //[MvxPagePresentation(WrapInNavigationController = true)]
    public partial class NumberViewController : MvxViewController<NumberViewModel>
    {
        public NumberViewController (IntPtr handle) : base (handle)
        {
        }
    }
}