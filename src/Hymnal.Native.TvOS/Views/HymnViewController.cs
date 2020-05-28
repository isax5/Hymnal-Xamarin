using Foundation;
using Hymnal.Core.ViewModels;
using Hymnal.Native.TvOS.Views.Cells;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Tvos.Binding.Views;
using MvvmCross.Platforms.Tvos.Views;
using UIKit;
using TVUIKit;
using System;

namespace Hymnal.Native.TvOS
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
            listCollectionView.RegisterNibForCell(HymnCollectionViewCell.Nib, HymnCollectionViewCell.Key);
            var dataSource = new MvxCollectionViewSource(listCollectionView, HymnCollectionViewCell.Key);
            listCollectionView.Source = dataSource;

            // Bindings
            MvxFluentBindingDescriptionSet<HymnViewController, HymnViewModel> set = this.CreateBindingSet<HymnViewController, HymnViewModel>();
            set.Bind(titleLabel).To(vm => vm.Hymn.Title);
            set.Bind(numberLabel).To(vm => vm.Hymn.Number);
            set.Bind(dataSource).To(vm => vm.Hymn.ListContent);
            //set.Bind(contentLabel).To(vm => vm.Hymn.Content);
            //set.Bind(textFocusableLabel).To(vm => vm.Hymn.Content);
            set.Apply();


            listCollectionView.TranslatesAutoresizingMaskIntoConstraints = false;
            listCollectionView.BottomAnchor.ConstraintEqualTo(View.BottomAnchor, constant: 0).Active = true;
            listCollectionView.LeftAnchor.ConstraintEqualTo(View.LeftAnchor, constant: 0).Active = true;
            listCollectionView.TopAnchor.ConstraintEqualTo(View.TopAnchor, constant: 0).Active = true;
            listCollectionView.RightAnchor.ConstraintEqualTo(View.RightAnchor, constant: 0).Active = true;
            listCollectionView.ReloadData();
        }
    }
}