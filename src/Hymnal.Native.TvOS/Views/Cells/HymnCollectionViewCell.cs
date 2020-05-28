using System;

using Foundation;
using Hymnal.Core.Models;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Tvos.Binding.Views;
using UIKit;

namespace Hymnal.Native.TvOS.Views.Cells
{
    public partial class HymnCollectionViewCell : MvxCollectionViewCell
    {
        public static readonly NSString Key = new NSString("HymnCollectionViewCell");
        public static readonly UINib Nib;

        static HymnCollectionViewCell()
        {
            Nib = UINib.FromName("HymnCollectionViewCell", NSBundle.MainBundle);
        }

        protected HymnCollectionViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.


            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<HymnCollectionViewCell, string>();
                set.Bind(textLabel).To(m => m);
                set.Apply();
            });
        }
    }
}

