using Foundation;
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
    public partial class HymnViewController : MvxViewController<HymnViewModel>
    {
        public HymnViewController (IntPtr handle) : base (handle)
        { }
         
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // CollectionView
            //listCollectionView.RegisterNibForCell(HymnCollectionViewCell.Nib, HymnCollectionViewCell.Key); // personalized Cell
            var dataSource = new MvxCollectionViewSource(listCollectionView, LyricsCollectionViewCell.Key);
            listCollectionView.Source = dataSource;

            // Bindings
            MvxFluentBindingDescriptionSet<HymnViewController, HymnViewModel> set = this.CreateBindingSet<HymnViewController, HymnViewModel>();
            set.Bind(titleLabel).To(vm => vm.Hymn.Title);
            set.Bind(numberLabel).To(vm => vm.Hymn.Number);
            set.Bind(dataSource).To(vm => vm.Hymn.ListContent);
            set.Apply();

            ////Dynamic CollectionView
            //if (listCollectionView.CollectionViewLayout is UICollectionViewFlowLayout)
            //{
            //    var layout = listCollectionView.CollectionViewLayout as UICollectionViewFlowLayout;
            //    layout.SectionInset = new UIEdgeInsets(top: 0, left: 16, bottom: 0, right: 16);
            //    layout.MinimumInteritemSpacing = 20;

            //    Calculated CollectionView Size
            //    int width = 1708;
            //    int height = 819;

            //    layout.EstimatedItemSize = new CGSize(width: width / 4, height: height);
            //    layout.EstimatedItemSize = new CGSize(width: 500, height: 1000);
            //    layout.EstimatedItemSize = UICollectionViewFlowLayout.AutomaticSize;
            //}
        }
    }
}