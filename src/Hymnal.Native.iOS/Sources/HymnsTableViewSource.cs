using System.Windows.Input;
using MvvmCross.Binding;
using MvvmCross.Platforms.Ios.Binding.Views;
using Hymnal.Core.Models;
//using Hymnal.Native.iOS.Views.Cells;
using UIKit;
using Foundation;
using MvvmCross.Binding.Extensions;

namespace Hymnal.Native.iOS.Sources
{
    //public class HymnsTableViewSource : MvxSimpleTableViewSource
    //{
    //    public ICommand FetchCommand { get; set; }

    //    public HymnsTableViewSource(UITableView tableView): base(tableView, typeof(HymnSearchTableViewCell))
    //    {
    //        DeselectAutomatically = true;
    //    }

    //    protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
    //    {
    //        var cell = base.GetOrCreateCellFor(tableView, indexPath, item);

    //        if (indexPath.Item == ItemsSource.Count() - 5)
    //        {
    //            FetchCommand?.Execute(null);
    //        }

    //        return cell;
    //    }
    //}
}