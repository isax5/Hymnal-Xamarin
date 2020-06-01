using System;
using MvvmCross.Platforms.Tvos.Views;
using Hymnal.Core.ViewModels;
using MvvmCross.Presenters.Attributes;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Hints;
using MvvmCross.Platforms.Tvos.Presenters.Attributes;
using MvvmCross.Binding.BindingContext;

namespace Hymnal.Native.TvOS.Views
{
    [MvxFromStoryboard("Main")]
    //[MvxModalPresentation(ModalTransitionStyle = UIKit.UIModalTransitionStyle.CoverVertical)]
    //[MvxTabPresentation]
    //[MvxPagePresentation(WrapInNavigationController = true)]
    public partial class SimpleViewController : MvxViewController<SimpleViewModel>
    {
        public SimpleViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Bindings
            MvxFluentBindingDescriptionSet<SimpleViewController, SimpleViewModel> set = this.CreateBindingSet<SimpleViewController, SimpleViewModel>();
            set.Bind(ClickmeButton).To(vm => vm.ClickmeCommand);
            set.Apply();
        }
    }
}