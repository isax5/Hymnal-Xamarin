using System;
using Hymnal.Core.ViewModels;
using Hymnal.Native.iOS.Sources;
using Hymnal.Native.iOS.Views.Cells;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.Platforms.Ios.Views;

namespace Hymnal.Native.iOS.Views
{
	[MvxFromStoryboard("Main")]
	public partial class SearchTableViewController : MvxTableViewController<SearchViewModel>
    {
        private MvxTableViewSource _dataSource;

        public SearchTableViewController (IntPtr handle) : base (handle)
        {
            this.DelayBind(() =>
            {
                // TableView
                _dataSource = new SBTableViewSource(TableView, SearchTableViewCell.Key);

                // Bindings
                var set = this.CreateBindingSet<SearchTableViewController, SearchViewModel>();
                set.Bind(_dataSource).To(vm => vm.Hymns);
                set.Apply();

                TableView.Source = _dataSource;
                TableView.ReloadData();
            });
        }
    }
}
