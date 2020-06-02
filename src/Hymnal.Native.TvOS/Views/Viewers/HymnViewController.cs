using Foundation;
using System.Linq;
using Hymnal.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Tvos.Binding.Views;
using MvvmCross.Platforms.Tvos.Views;
using Hymnal.Native.TvOS.Views.Cells;
using Hymnal.Native.TvOS.Views.CollectionViewLayout;
using UIKit;
using TVUIKit;
using System;
using CoreGraphics;
using MvvmCross.Platforms.Tvos.Presenters.Attributes;
using MvvmCross.Navigation;
using MvvmCross;

namespace Hymnal.Native.TvOS.Views
{
    [MvxFromStoryboard("Main")]
    //[MvxPagePresentation(WrapInNavigationController = true)]
    [MvxModalPresentation(WrapInNavigationController = true)]
    //[MvxModalPresentation(Animated = true, ModalPresentationStyle = UIModalPresentationStyle.PageSheet)]
    public partial class HymnViewController : MvxViewController<HymnViewModel>, IUICollectionViewDelegateFlowLayout
    {
        private MvxCollectionViewSource _dataSource;

        public HymnViewController (IntPtr handle) : base (handle)
        {

            this.DelayBind(() =>
            {
                // CollectionView
                //listCollectionView.RegisterNibForCell(HymnCollectionViewCell.Nib, HymnCollectionViewCell.Key); // personalized Cell
                _dataSource = new MvxCollectionViewSource(listCollectionView, LyricsCollectionViewCell.Key);

                // Bindings
                MvxFluentBindingDescriptionSet<HymnViewController, HymnViewModel> set = this.CreateBindingSet<HymnViewController, HymnViewModel>();
                set.Bind(navigationItem).For(item => item.Title).To(vm => vm.Hymn.Title);
                set.Bind(_dataSource).To(vm => vm.Hymn.ListContent);
                set.Bind(playButton).To(vm => vm.PlayCommand);
                set.Apply();

                //Dynamic CollectionView
                //listCollectionView.CollectionViewLayout = new TopAlignedCollectionViewFlowLayout();
                if (listCollectionView.CollectionViewLayout is UICollectionViewFlowLayout)
                {
                    var layout = listCollectionView.CollectionViewLayout as UICollectionViewFlowLayout;
                    //layout.EstimatedItemSize = UICollectionViewFlowLayout.AutomaticSize;
                    layout.ItemSize = UICollectionViewFlowLayout.AutomaticSize;
                    //layout.EstimatedItemSize = new CGSize(600, 400); // I doesn't affect if I'm using <GetSizeForItem>

                }

                listCollectionView.Source = _dataSource;
                listCollectionView.Delegate = this;
            });
        }
         
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(false);

            // TODO: Correction MVX Modal Page
            IMvxNavigationService navigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
            navigationService.Close(ViewModel);
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

            nfloat width = (frameWidth / numberOfColumns) - (xInsets + cellSpacing);
            nfloat height = 200; // The cell grows by it's self if it's necessary

            return new CGSize(width: width, height: height);
            //return new CGSize(width: cellWidth, height: cellHeight);
        }
        /**/

        #endregion

    }
}