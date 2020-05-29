using CoreGraphics;
using Foundation;
using Hymnal.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Tvos.Binding.Views;
using MvvmCross.Platforms.Tvos.Presenters.Attributes;
using MvvmCross.Platforms.Tvos.Views;
using System;
using UIKit;

namespace Hymnal.Native.TvOS.Views.Cells
{
    [MvxFromStoryboard("Main")]
    public partial class LyricsCollectionViewCell : MvxCollectionViewCell
    {
        public UILabel TextLabel => textLabel;
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


        public override UICollectionViewLayoutAttributes PreferredLayoutAttributesFittingAttributes(UICollectionViewLayoutAttributes layoutAttributes)
        {
            //return base.PreferredLayoutAttributesFittingAttributes(layoutAttributes);

            SetNeedsLayout();
            LayoutIfNeeded();

            CGSize size = ContentView.SystemLayoutSizeFittingSize(layoutAttributes.Size);
            var frame = layoutAttributes.Frame;
            
            nfloat height = NMath.Ceiling(size.Height);
            layoutAttributes.Frame = new CGRect(x: frame.X, y: frame.Y, width: frame.Width, height: height);
            return layoutAttributes;
        }
    }
}