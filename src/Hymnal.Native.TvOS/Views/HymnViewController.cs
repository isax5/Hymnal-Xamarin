using Foundation;
using System.Linq;
using Hymnal.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Tvos.Binding.Views;
using MvvmCross.Platforms.Tvos.Views;
using Hymnal.Native.TvOS.Views.Cells;
using UIKit;
using TVUIKit;
using System;
using CoreGraphics;

namespace Hymnal.Native.TvOS.Views
{
    [MvxFromStoryboard("Main")]
    public partial class HymnViewController : MvxViewController<HymnViewModel>, IUICollectionViewDelegateFlowLayout
    {
        private MvxCollectionViewSource _dataSource;

        public HymnViewController (IntPtr handle) : base (handle)
        { }
         
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // CollectionView
            //listCollectionView.RegisterNibForCell(HymnCollectionViewCell.Nib, HymnCollectionViewCell.Key); // personalized Cell
            _dataSource = new MvxCollectionViewSource(listCollectionView, LyricsCollectionViewCell.Key);
            
            // Bindings
            MvxFluentBindingDescriptionSet<HymnViewController, HymnViewModel> set = this.CreateBindingSet<HymnViewController, HymnViewModel>();
            set.Bind(titleLabel).To(vm => vm.Hymn.Title);
            set.Bind(numberLabel).To(vm => vm.Hymn.Number);
            set.Bind(_dataSource).To(vm => vm.Hymn.ListContent);
            set.Apply();

            //Dynamic CollectionView
            if (listCollectionView.CollectionViewLayout is UICollectionViewFlowLayout)
            {
                var layout = listCollectionView.CollectionViewLayout as UICollectionViewFlowLayout;
                //layout.EstimatedItemSize = UICollectionViewFlowLayout.AutomaticSize;
                layout.ItemSize = UICollectionViewFlowLayout.AutomaticSize;
                //layout.EstimatedItemSize = new CGSize(600, 400); // I doesn't affect if I'm using <GetSizeForItem>

            }

            listCollectionView.Source = _dataSource;
            listCollectionView.Delegate = this;
        }

        #region Flow layout delegate
        /**/
        [Export("collectionView:layout:sizeForItemAtIndexPath:")]
        public CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
        {
            // Modify number of columns here
            nfloat numberOfColumns = 3;

            nfloat frameWidth = collectionView.Frame.Size.Width;
            nfloat frameHeight = collectionView.Frame.Size.Height;

            // Insets configurated in collectionView as FlowLayout (StoryBoard)
            nfloat xInsets = 10;

            // Spacing configurated in collectionView (StoryBoard)
            nfloat cellSpacing = 20;

            //let image = images[indexPath.item]
            //let height = image.size.height
            //var cell = collectionView.CellForItem(indexPath) as LyricsCollectionViewCell;
            //nfloat textWidth = cell.TextLabel.Frame.Width;
            //nfloat textHeight = cell.TextLabel.Frame.Height;


            // TODO: How to know the height of the label, if the label doesn't exist yet
            // For now, I'm going to use the height of the line that is 46, multiplicated for the number of lines to use

            //var cell = _dataSource.GetCell(collectionView, indexPath) as LyricsCollectionViewCell;
            //nfloat cellWidth = cell.TextLabel.Frame.Width;
            //nfloat cellHeight = cell.TextLabel.Frame.Height;
            

            nfloat width = (frameWidth / numberOfColumns) - (xInsets + cellSpacing);
            //nfloat height = frameHeight - (xInsets * 2) - 20;
            nfloat height = 200; // The cell grows by it's self if it's necessary

            return new CGSize(width: width, height: height);
            //return new CGSize(width: cellWidth, height: cellHeight);
        }
        /**/


        #endregion

    }
}