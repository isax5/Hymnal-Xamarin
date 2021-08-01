using System;
using Foundation;
using Hymnal.XF.iOS.Renderers;
using Hymnal.XF.Resources.Languages;
using Hymnal.XF.Views;
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

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            // At this point Element is already null
            if (disposing)
            {
                ViewWillDisposeSearchImplementation();
            }
        }
    }

    // SearchPage
    public partial class ContentPageRenderer : IUISearchResultsUpdating, IUISearchBarDelegate
    {
        private UISearchController searchController;
        private ISearchPage searchPage => Element as ISearchPage;
        private IDisposable themeSubscription;

        private void ViewWillAppearSearchImplementation()
        {
            if (searchController is null)
            {
                searchController = new()
                {
                    SearchResultsUpdater = this,
                    DimsBackgroundDuringPresentation = false,

                    // En caso de true: Oculta titue, toolbars, back, etc
                    // En caso de false: Oculta solo title
                    // Produce problemas si se configura false en una pagina que debe retornar luego de realizar una busqueda en iOS
                    HidesNavigationBarDuringPresentation = searchPage.Settings.HideNavBarWhenSearch,
                };

                //searchController.SearchBar.Placeholder = searchPage.Settings.PlaceHolder;


                searchController.SearchBar.SearchTextField.AttributedPlaceholder = new NSAttributedString(searchPage.PlaceholderText,
                    attributes: new UIStringAttributes()
                    {
                        ForegroundColor = searchPage.PlaceHolderColor.ToUIColor(),
                    });
            }

            if (ParentViewController is not null && ParentViewController.NavigationItem.SearchController is null)
            {
                ParentViewController.NavigationItem.SearchController = searchController;
                DefinesPresentationContext = true;
            }
        }

        private void ViewDidAppearSearchImplementation()
        {
            //Configure Theme
            if (themeSubscription is null)
            {
                themeSubscription = searchPage.ObservableThemeChange
                    .Subscribe(ev => InvokeOnMainThread(() =>
                    {
                        {
                            searchController.SearchBar.SearchTextField.TextColor = searchPage.TextColor.ToUIColor();
                            searchController.SearchBar.SearchTextField.AttributedPlaceholder = new NSAttributedString(searchPage.PlaceholderText,
                                attributes: new UIStringAttributes()
                                {
                                    ForegroundColor = searchPage.PlaceHolderColor.ToUIColor(),
                                });
                        }
                    }));
            }

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

        private void ViewWillDisposeSearchImplementation()
        {
            themeSubscription?.Dispose();
        }
    }
}
