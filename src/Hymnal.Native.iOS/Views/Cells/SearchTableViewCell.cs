using System;
using Foundation;
using Hymnal.Core.Models;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;

namespace Hymnal.Native.iOS.Views.Cells
{
    public partial class SearchTableViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("SearchTableViewCell");

        public SearchTableViewCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(() =>
            {
                // Bindings
                var set = this.CreateBindingSet<SearchTableViewCell, Hymn>();
                set.Bind(titleLabel).To(m => m.Title);
                set.Apply();
            });
        }

    }
}
