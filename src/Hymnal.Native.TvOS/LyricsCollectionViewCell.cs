using Foundation;
using Hymnal.Core.ViewModels;
using Hymnal.Native.TvOS.Views.Cells;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Tvos.Binding.Views;
using MvvmCross.Platforms.Tvos.Presenters.Attributes;
using MvvmCross.Platforms.Tvos.Views;
using System;

namespace Hymnal.Native.TvOS
{
    [MvxFromStoryboard("Main")]
    public partial class LyricsCollectionViewCell : MvxCollectionViewCell
    {
        public static readonly NSString Key = new NSString("LyricsCollectionViewCell");

        public LyricsCollectionViewCell (IntPtr handle) : base (handle)
        {

            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<LyricsCollectionViewCell, string>();
                set.Bind(textLabel).To(m => m);
                set.Apply();
            });
        }
    }
}