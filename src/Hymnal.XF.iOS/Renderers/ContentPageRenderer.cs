using Hymnal.XF.Views;
using Hymnal.XF.iOS.Renderers;
using Hymnal.XF.Resources.Languages;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage), typeof(ContentPageRenderer))]
namespace Hymnal.XF.iOS.Renderers
{
    public partial class ContentPageRenderer : PageRenderer
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (Element is IModalPage modalPage)
            {
                var leftItem = new UIBarButtonItem(Languages.Generic_Close, UIBarButtonItemStyle.Plain, (sender, e) =>
                {
                    modalPage.PopModal();
                });

                NavigationController.TopViewController.NavigationItem.SetLeftBarButtonItem(leftItem, false);
            }

            if (Element is ISearchPage)
                ViewWillAppearSearchImplementation();
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (Element is ISearchPage)
                ViewDidAppearSearchImplementation();
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            if (Element is ISearchPage)
                ViewWillDisappearSearchImplementation();
        }
    }

    // SearchPage
    public partial class ContentPageRenderer : IUISearchResultsUpdating, IUISearchBarDelegate
    {
        private UISearchController searchController;
        private ISearchPage searchPage => Element as ISearchPage;

        private void ViewWillAppearSearchImplementation()
        {
            if (searchController is null)
            {
                searchController = new UISearchController()
                {
                    SearchResultsUpdater = this,
                    DimsBackgroundDuringPresentation = false,

                    // En caso de true: Oculta titue, toolbars, back, etc
                    // En caso de false: Oculta solo title
                    // Produce problemas si se configura false en una pagina que debe retornar luego de realizar una busqueda en iOS
                    HidesNavigationBarDuringPresentation = searchPage.Settings.HideNavBarWhenSearch,
                };

                searchController.SearchBar.Placeholder = searchPage.Settings.PlaceHolder;
            }

            if (ParentViewController is not null && ParentViewController.NavigationItem.SearchController is null)
            {
                ParentViewController.NavigationItem.SearchController = searchController;
                DefinesPresentationContext = true;
            }
        }

        private void ViewDidAppearSearchImplementation()
        {
            if (searchPage.Settings.InitialDisplay)
                ParentViewController.NavigationController.NavigationBar.SizeToFit();

            if (searchPage.Settings.InitiallyFocus)
            {
                searchController.Active = searchPage.Settings.InitiallyFocus;

                searchController.SearchBar.SearchTextField.BecomeFirstResponder();
            }
        }

        private void ViewWillDisappearSearchImplementation()
        {
            if (searchPage.Settings.HideWhenPageDisappear)
                ParentViewController.NavigationItem.SearchController = null;
        }

        public void UpdateSearchResultsForSearchController(UISearchController searchController)
        {
            searchPage.OnSearchBarTextChanged(searchController.SearchBar.Text);
        }
    }
}
