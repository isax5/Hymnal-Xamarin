using Hymnal.Core.ViewModels;
using MvvmCross.Platforms.Tvos.Views;
using System;

namespace Hymnal.Native.TvOS
{
    [MvxFromStoryboard("Main")]
    public partial class NumericalIndexViewController : MvxTableViewController<NumericalIndexViewModel>
    {
        public NumericalIndexViewController (IntPtr handle) : base (handle)
        {
        }
    }
}