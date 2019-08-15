using System.Linq;
using System.Reflection;
using Himnario.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage), typeof(ContentPageRenderer))]
namespace Himnario.iOS.Renderers
{
    /// <summary>
    /// Custom renderer for ContentPage that allows for action buttons to be on the left or the right hand side (ex: a modal with cancel and done buttons)
    /// ToolbarItems need to have Priority set to 0 to show on the left, 1 to show on the right (ref: https://gist.github.com/alexlau811/f1fff9e726333e6b4a2f)
    /// </summary>
    public class ContentPageRenderer : PageRenderer, IUIGestureRecognizerDelegate
    {
        //public override void WillMoveToParentViewController(UIViewController parent)
        //{
        // Buscador iOS 11
        //    if (Element is Abstractions.ISearchPage)
        //    {
        //        var page = Element as Abstractions.ISearchPage;

        //        page.SearchBar.IsVisible = false;

        //        UISearchController searchController = new UISearchController(searchResultsController: null)
        //        {
        //            DimsBackgroundDuringPresentation = false
        //        };

        //        searchController.SearchBar.SearchBarStyle = UISearchBarStyle.Minimal;
        //        searchController.SearchBar.BarStyle = UIBarStyle.BlackTranslucent;
        //        searchController.SearchBar.Placeholder = page.SearchBar.Placeholder;
        //        searchController.SearchBar.TextChanged += (s, e) =>
        //        {
        //            page.OnSearchBarTextChanged(e.SearchText);
        //        };
        //        // Funciona tambien en Cancel
        //        searchController.SearchBar.OnEditingStopped += (s, e) =>
        //        {
        //            page.OnSearchBarTextChanged(searchController.SearchBar.Text);
        //        };


        //        parent.NavigationItem.SearchController = searchController;
        //        parent.NavigationItem.HidesSearchBarWhenScrolling = false;
        //    }

        //    base.WillMoveToParentViewController(parent);
        //}

        private bool isLoaded = false;
        public override void ViewWillAppear(bool animated)
        {
            // Navegacion iOS 11
            //NavigationController.NavigationBar.PrefersLargeTitles = true;

            if (!isLoaded)
            {
                #region             Easy Back in pages Navigaion Page (Hidden)
                //if (Element != null)
                //{
                //    try
                //    {
                //        if (this.Element.Navigation.NavigationStack.Count > 1)
                //            if (NavigationController != null)
                //                if (NavigationController.InteractivePopGestureRecognizer != null)
                //                    NavigationController.InteractivePopGestureRecognizer.Delegate = null;
                //    }
                //    catch (Exception)
                //    { }
                //}
                #endregion

                #region Left Button Toolbar
                if (Element == null || NavigationController == null)
                    return;

                var contentPage = Element as ContentPage;

                var itemsInfo = contentPage.ToolbarItems;

                var navigationItem = NavigationController.TopViewController.NavigationItem;
                var leftNativeButtons = (navigationItem.LeftBarButtonItems ?? new UIBarButtonItem[] { }).ToList();
                var rightNativeButtons = (navigationItem.RightBarButtonItems ?? new UIBarButtonItem[] { }).ToList();

                var newLeftButtons = new UIBarButtonItem[] { }.ToList();
                var newRightButtons = new UIBarButtonItem[] { }.ToList();

                rightNativeButtons.ForEach(nativeItem =>
                {
                    // [Hack] Get Xamarin private field "item"
                    var field = nativeItem.GetType().GetField("_item", BindingFlags.NonPublic | BindingFlags.Instance);
                    if (field == null)
                        return;

                    var info = field.GetValue(nativeItem) as ToolbarItem;
                    if (info == null)
                        return;

                    if (info.Priority == 0)
                        newRightButtons.Add(nativeItem);
                    else
                        newLeftButtons.Add(nativeItem);
                });

                leftNativeButtons.ForEach(nativeItem =>
                {
                    newLeftButtons.Add(nativeItem);
                });

                navigationItem.RightBarButtonItems = newRightButtons.ToArray();
                navigationItem.LeftBarButtonItems = newLeftButtons.ToArray();
                #endregion
            }

            isLoaded = true;
            base.ViewWillAppear(animated);
        }
    }
}
