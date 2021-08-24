using System.Runtime.CompilerServices;
using CoreFoundation;
using Foundation;
using Hymnal.XF.iOS.Renderers;
using Hymnal.XF.Views;
using ObjCRuntime;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
// ReSharper disable ParameterHidesMember

[assembly: ExportRenderer(typeof(ContentPage), typeof(ContentPageRenderer))]
namespace Hymnal.XF.iOS.Renderers
{
    [Preserve(AllMembers = true)]
    public partial class ContentPageRenderer : PageRenderer
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (Element is IModalPage modalPage)
            {
                var leftItem = new UIBarButtonItem(modalPage.CloseButtonText, UIBarButtonItemStyle.Plain, (sender, e) =>
                {
                    modalPage.PopModal();
                });

                NavigationController.TopViewController.NavigationItem.SetLeftBarButtonItem(leftItem, false);
            }

            if (Element is ISearchPage)
                ViewWillAppear_SearchImplementation();
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (Element is ISearchPage)
                ViewDidAppear_SearchImplementation();
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            if (Element is ISearchPage)
                ViewWillDisappear_SearchImplementation();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            // At this point Element is already null
            if (disposing)
            {
                ViewWillDispose_SearchImplementation();
            }
        }
        public override void TraitCollectionDidChange(UITraitCollection previousTraitCollection)
        {
            base.TraitCollectionDidChange(previousTraitCollection);

            if (Element is ISearchPage)
                TraitCollectionDidChange_SearchImplementation(TraitCollection.UserInterfaceStyle, previousTraitCollection.UserInterfaceStyle);
        }
    }

    // SearchPage
    public partial class ContentPageRenderer : ISearchDelegate, IUISearchResultsUpdating, IUISearchBarDelegate, IUITextFieldDelegate, IUISearchControllerDelegate
    {
        private ISearchPage searchPage => Element as ISearchPage;
        private UISearchController searchController;

        private void ViewWillAppear_SearchImplementation()
        {
            if (searchController is null)
            {
                searchController = new()
                {
                    Delegate = this,
                    SearchResultsUpdater = this,
                    DimsBackgroundDuringPresentation = false,

                    // En caso de true: Oculta titue, toolbars, back, etc
                    // En caso de false: Oculta solo title
                    // Produce problemas si se configura false en una pagina que debe retornar luego de realizar una busqueda en iOS
                    HidesNavigationBarDuringPresentation = searchPage.Settings.HideNavBarWhenSearch,
                };

                searchController.SearchBar.Delegate = this;
                searchController.SearchBar.SearchTextField.Delegate = this;
                searchPage.Delegate = this;

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

        private bool firstTimeAppearing = true;
        private void ViewDidAppear_SearchImplementation()
        {
            if (!firstTimeAppearing) return;
            firstTimeAppearing = false;

            if (searchPage.Settings.InitialDisplay)
                ParentViewController.NavigationController.NavigationBar.SizeToFit();

            if (searchPage.Settings.InitiallyFocus)
                searchController.Active = true;
        }

        private void ViewWillDisappear_SearchImplementation()
        {
            if (searchPage.Settings.HideWhenPageDisappear)
                ParentViewController.NavigationItem.SearchController = null;
        }

        #region ISearchDelegate
        string ISearchDelegate.SearchText
        {
            get => searchController.SearchBar.Text;
            set => searchController.SearchBar.Text = value;
        }

        void ISearchDelegate.BecomeFirstResponder()
        {
            if (!searchController.SearchBar.SearchTextField.IsFirstResponder)
            {
                searchController.Active = true;
                searchController.SearchBar.BecomeFirstResponder();
            }
        }

        void ISearchDelegate.DismissKeyboard(bool keepSearchControllerActive)
        {
            if (searchController.SearchBar.SearchTextField.IsFirstResponder)
            {
                searchController.Active = keepSearchControllerActive;
                searchController.SearchBar.SearchTextField.EndEditing(true);
            }
        }
        #endregion

        #region IUISearchResultsUpdating
        /// <summary>
        /// Text changes
        /// </summary>
        /// <param name="searchController"></param>
        public void UpdateSearchResultsForSearchController(UISearchController searchController)
        {
            searchPage.OnSearchBarTextChanged(searchController.SearchBar.Text);
        }
        #endregion

        #region IUISearchBarDelegate
        /// <summary>
        /// Cancel button tapped
        /// <para></para>
        /// Check: <see cref="UISearchBarDelegate.CancelButtonClicked(UISearchBar)"/>
        /// </summary>
        /// <param name="searchBar"></param>
        [BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
        [Export("searchBarCancelButtonClicked:")]
        [Unavailable(PlatformName.TvOS, PlatformArchitecture.All, null)]
        public virtual void CancelButtonClicked(UISearchBar searchBar)
        {
            // Search will become empty but for logic in VM, is better to have it empty already
            searchPage.OnSearchBarTextChanged(string.Empty);
            searchPage.Canceled();
        }

        /// <summary>
        /// Search button tapped
        /// <para></para>
        /// Check: <see cref="UISearchBarDelegate.SearchButtonClicked(UISearchBar)"/>
        /// </summary>
        /// <param name="searchBar"></param>
        [BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
        [Export("searchBarSearchButtonClicked:")]
        public virtual void SearchButtonClicked(UISearchBar searchBar)
        {
            searchPage.SearchTapped(searchBar.Text);
        }
        #endregion

        #region UITextFieldDelegate
        /// <summary>
        /// Start Editing
        /// <para></para>
        /// Check: <see cref="UITextFieldDelegate.EditingStarted(UITextField)"/>
        /// </summary>
        /// <param name="textField"></param>
        [BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
        [Export("textFieldDidBeginEditing:")]
        public void EditingStarted(UITextField textField)
        {
            searchPage.Focused();
        }

        /// <summary>
        /// End Editing
        /// <para></para>
        /// Check: <see cref="UITextFieldDelegate.EditingEnded(UITextField)"/>
        /// </summary>
        /// <param name="textField"></param>
        [BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
        [Export("textFieldDidEndEditing:")]
        public void EditingEnded(UITextField textField)
        {
            searchPage.Unfocused();
        }
        #endregion

        #region IUISearchControllerDelegate
        /// <summary>
        /// Start Editing
        /// <para></para>
        /// Check: <see cref="UISearchControllerDelegate.DidPresentSearchController(UISearchController)"/>
        /// </summary>
        /// <param name="searchController"></param>
        [Export("didPresentSearchController:")]
        [BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void DidPresentSearchController(UISearchController searchController)
        {
            if (searchPage.Settings.InitiallyFocus)
                DispatchQueue.MainQueue.DispatchAsync(() =>
                {
                    searchController.SearchBar.BecomeFirstResponder();
                });
        }
        #endregion

        private void ViewWillDispose_SearchImplementation()
        { }

        private void TraitCollectionDidChange_SearchImplementation(UIUserInterfaceStyle currentUserInterfaceStyle, UIUserInterfaceStyle previousUserInterfaceStyle)
        {
            searchController.SearchBar.SearchTextField.TextColor = searchPage.TextColor.ToUIColor();
            searchController.SearchBar.SearchTextField.AttributedPlaceholder = new NSAttributedString(searchPage.PlaceholderText,
                attributes: new UIStringAttributes()
                {
                    ForegroundColor = searchPage.PlaceHolderColor.ToUIColor(),
                });
        }
    }
}
