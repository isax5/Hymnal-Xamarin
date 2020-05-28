using Hymnal.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Tvos.Views;
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

            // Bindings
            MvxFluentBindingDescriptionSet<HymnViewController, HymnViewModel> set = this.CreateBindingSet<HymnViewController, HymnViewModel>();
            set.Bind(titleLabel).To(vm => vm.Hymn.Title);
            set.Bind(numberLabel).To(vm => vm.Hymn.Number);
            set.Bind(contentLabel).To(vm => vm.Hymn.Content);
            set.Bind(textFocusableLabel).To(vm => vm.Hymn.Content);
            set.Apply();
            
        }
    }
}