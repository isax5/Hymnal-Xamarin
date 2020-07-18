using Hymnal.Core.ViewModels;
using MvvmCross.Platforms.Tvos.Presenters.Attributes;
using MvvmCross.Platforms.Tvos.Views;
using System;

namespace Hymnal.Native.TvOS.Views
{
    [MvxFromStoryboard("Main")]
    [MvxTabPresentation(TabName = "Index")]
    public partial class NumericalIndexViewController : MvxTableViewController<NumericalIndexViewModel>
    {
        public NumericalIndexViewController (IntPtr handle) : base (handle)
        {
        }
    }
}