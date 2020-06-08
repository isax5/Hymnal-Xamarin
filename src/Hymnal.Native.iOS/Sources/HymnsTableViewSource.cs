using System.Windows.Input;
using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;

namespace Hymnal.Native.iOS.Sources
{
    public class SBTableViewSource : MvxTableViewSource
    {
        private readonly NSString cellIdentifier;

        public ICommand FetchCommand { get; set; }

        public SBTableViewSource(UITableView tableView, NSString cellIdentifier) : base(tableView)
        {
            DeselectAutomatically = true;
            this.cellIdentifier = cellIdentifier;
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            return tableView.DequeueReusableCell(cellIdentifier);
        }
    }
}
