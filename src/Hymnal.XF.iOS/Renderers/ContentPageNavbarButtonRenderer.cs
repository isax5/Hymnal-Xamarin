using System.Collections.Generic;
using Foundation;
using Hymnal.XF.iOS.Renderers;
using Hymnal.XF.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NumberPage), typeof(ContentPageNavbarButtonRenderer))]
[assembly: ExportRenderer(typeof(HymnPage), typeof(ContentPageNavbarButtonRenderer))]
namespace Hymnal.XF.iOS.Renderers
{
    [Preserve(AllMembers = true)]
    /// <summary>
    /// Custom renderer for ContentPage that allows for action buttons to be on the left
    /// or the right hand side (ex: a modal with cancel and done buttons)
    /// </summary>
    public class ContentPageNavbarButtonRenderer : ContentPageRenderer
    {
        public new ContentPage Element => (ContentPage)base.Element;
        private UINavigationItem navigationControllerItems => NavigationController.TopViewController.NavigationItem;

        private readonly List<UIBarButtonItem> leftNavList = new List<UIBarButtonItem>();

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var toolbarItems = new List<ToolbarItem>();

            foreach (ToolbarItem itm in Element.ToolbarItems)
                toolbarItems.Add(itm);

            Element.ToolbarItems.Clear();

            // Sort the list
            toolbarItems.Sort((ToolbarItem i1, ToolbarItem i2) =>
            {
                return i1.Priority > i2.Priority ? -1 : 1;
            });

            foreach (ToolbarItem itm in toolbarItems)
            {
                if (itm.Priority < 0)
                    leftNavList.Add(itm.ToUIBarButtonItem());
                else
                    Element.ToolbarItems.Add(itm);
            }
        }

        public override void ViewWillAppear(bool animated)
        {
            navigationControllerItems.SetLeftBarButtonItems(leftNavList.ToArray(), false);

            base.ViewWillAppear(animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            navigationControllerItems.SetLeftBarButtonItems(new UIBarButtonItem[0], false);

            base.ViewWillDisappear(animated);
        }
    }
}
