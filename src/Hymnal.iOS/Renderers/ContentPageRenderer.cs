using System;
using Hymnal.iOS.Renderers;
using Hymnal.UI;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage), typeof(ContentPageRenderer))]
namespace Hymnal.iOS.Renderers
{
    public class ContentPageRenderer : PageRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }

            try
            {
                SetAppTheme();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"\t\t\tERROR: {ex.Message}");
            }
        }

        public override void TraitCollectionDidChange(UITraitCollection previousTraitCollection)
        {
            base.TraitCollectionDidChange(previousTraitCollection);
            Console.WriteLine($"TraitCollectionDidChange: {TraitCollection.UserInterfaceStyle} != {previousTraitCollection.UserInterfaceStyle}");

            if (TraitCollection.UserInterfaceStyle != previousTraitCollection.UserInterfaceStyle)
            {
                SetAppTheme();
            }
        }

        private void SetAppTheme()
        {
            switch (TraitCollection.UserInterfaceStyle)
            {
                case UIUserInterfaceStyle.Dark:
                    if (App.AppTheme != UI.Models.AppTheme.Dark)
                        App.AppTheme = UI.Models.AppTheme.Dark;
                    break;

                case UIUserInterfaceStyle.Light:
                    if (App.AppTheme != UI.Models.AppTheme.Light)
                        App.AppTheme = UI.Models.AppTheme.Light;
                    break;

                case UIUserInterfaceStyle.Unspecified:
                default:
                    if (App.AppTheme != UI.Models.AppTheme.Unspecified)
                        App.AppTheme = UI.Models.AppTheme.Unspecified;
                    break;
            }
        }
    }
}
