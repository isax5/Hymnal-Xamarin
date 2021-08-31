using Hymnal.XF.iOS.Renderers;
using Hymnal.XF.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(IndexPage), typeof(CustomCarouselPageRenderer))]
namespace Hymnal.XF.iOS.Renderers
{
    public class CustomCarouselPageRenderer : CarouselPageRenderer
    {
        // Override the OnElementChanged method so we can tweak this renderer post-initial setup
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            UIView view = NativeView;
            var scrollView = (UIScrollView)view.Subviews[0];
            scrollView.ContentSize = new CoreGraphics.CGSize(scrollView.ContentSize.Width, scrollView.Frame.Size.Height);
            AutomaticallyAdjustsScrollViewInsets = false;
        }
    }
}
