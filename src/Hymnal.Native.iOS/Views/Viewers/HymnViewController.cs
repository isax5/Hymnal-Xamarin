using Foundation;
using Hymnal.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using System;
using UIKit;

namespace Hymnal.iOS.Views
{
    [MvxFromStoryboard("Main")]
    public partial class HymnViewController : MvxViewController<HymnViewModel>
    {
        public HymnViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            MvxFluentBindingDescriptionSet<HymnViewController, HymnViewModel> set = this.CreateBindingSet<HymnViewController, HymnViewModel>();
            Title = ViewModel.Hymn.Title;
            set.Bind(hymnTitleLabel).To(vm => vm.Hymn.Title);
            set.Bind(hymnContentLabel).To(vm => vm.Hymn.Content);
            set.Apply();
        }
    }
}