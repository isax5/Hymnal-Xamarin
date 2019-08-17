using System.Collections.Generic;
using Himnario.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage), typeof(CustomContentPageRenderer))]
namespace Himnario.iOS.Renderers
{
    /// <summary>
    /// Custom renderer for ContentPage that allows for action buttons to be on the left or the right hand side (ex: a modal with cancel and done buttons)
    /// ToolbarItems need to have Priority set to 0 to show on the left, 1 to show on the right (ref: https://gist.github.com/alexlau811/f1fff9e726333e6b4a2f)
    /// </summary>
    public class CustomContentPageRenderer : PageRenderer
    {
        public new ContentPage Element => (ContentPage)base.Element;

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            var LeftNavList = new List<UIBarButtonItem>();
            var RightNavList = new List<UIBarButtonItem>();
            var ToolbarList = new List<ToolbarItem>();

            UINavigationItem navigationItem = NavigationController.TopViewController.NavigationItem;

            // Add to new list for sorting
            foreach (ToolbarItem itm in Element.ToolbarItems)
            {
                ToolbarList.Add(itm);
            }

            // Sort the list
            ToolbarList.Sort((ToolbarItem i1, ToolbarItem i2) =>
            {
                return i1.Priority > i2.Priority ? -1 : 1;
            });

            foreach (ToolbarItem itm in ToolbarList)
            {
                if (itm.Priority < 0)
                {
                    LeftNavList.Add(itm.ToUIBarButtonItem());
                }
                else
                {
                    RightNavList.Add(itm.ToUIBarButtonItem());
                }
            }
            navigationItem.SetLeftBarButtonItems(LeftNavList.ToArray(), false);
            navigationItem.SetRightBarButtonItems(RightNavList.ToArray(), false);
        }
    }
}
