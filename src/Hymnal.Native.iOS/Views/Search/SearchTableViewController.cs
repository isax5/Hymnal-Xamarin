using System;
using CoreFoundation;
using Hymnal.Core.ViewModels;
using Hymnal.Native.iOS.Sources;
using Hymnal.Native.iOS.Views.Cells;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace Hymnal.Native.iOS.Views
{
    [MvxFromStoryboard("Main")]
    public partial class SearchTableViewController : MvxTableViewController<SearchViewModel>
    {
        private MvxTableViewSource _dataSource;

        private UISearchController _searchController = new UISearchController();

        public SearchTableViewController(IntPtr handle) : base(handle)
        {
            this.DelayBind(() =>
            {
                // TableView
                _dataSource = new SBTableViewSource(TableView, SearchTableViewCell.Key);

                // Bindings
                var set = this.CreateBindingSet<SearchTableViewController, SearchViewModel>();
                set.Bind(_dataSource).To(vm => vm.Hymns);
                set.Bind(_searchController.SearchBar.SearchTextField).To(vm => vm.TextSearchBar);
                set.Apply();

                TableView.Source = _dataSource;
            });
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Search controller
            _searchController.ObscuresBackgroundDuringPresentation = false;
            _searchController.SearchBar.Placeholder = "Search";
            NavigationItem.SearchController = _searchController;
            DefinesPresentationContext = true;
            // Show Search always visible | if its not using, use 'searchController.isActive = true' to make it visible at the beginning
            // The effect is betther when it is after appearing
            NavigationItem.HidesSearchBarWhenScrolling = false;
        }

        private bool _loadedBefore = false;
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (!_loadedBefore)
            {
                _loadedBefore = true;

                DispatchQueue.MainQueue.DispatchAsync(() =>
                {
                    _searchController.SearchBar.BecomeFirstResponder();
                });
            }
        }
    }
}
