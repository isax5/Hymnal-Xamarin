//using System;
//using Hymnal.XF.iOS.Renderers;
//using Hymnal.XF.UI.Resources;
//using UIKit;
//using Xamarin.Forms;
//using Xamarin.Forms.Platform.iOS;

//[assembly: ExportRenderer(typeof(ContentPage), typeof(ContentPageRenderer))]
//namespace Hymnal.XF.iOS.Renderers
//{
//    // Making theme transition for iOS more fluid
//    public class ContentPageRenderer : PageRenderer
//    {
//        public override void TraitCollectionDidChange(UITraitCollection previousTraitCollection)
//        {
//            base.TraitCollectionDidChange(previousTraitCollection);

//            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
//            {
//                Console.WriteLine($"TraitCollectionDidChange: {TraitCollection.UserInterfaceStyle} != {previousTraitCollection.UserInterfaceStyle}");

//                if (TraitCollection.UserInterfaceStyle != previousTraitCollection.UserInterfaceStyle)
//                {
//                    ThemeHelper.CheckTheme();
//                }
//            }
//        }
//    }
//}
