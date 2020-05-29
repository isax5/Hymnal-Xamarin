using Hymnal.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Tvos.Presenters.Attributes;
using MvvmCross.Platforms.Tvos.Views;
using System;

namespace Hymnal.Native.TvOS.Views
{
    [MvxFromStoryboard("Main")]
    //[MvxPagePresentation(WrapInNavigationController = true)]
    public partial class NumberViewController : MvxViewController<NumberViewModel>
    {
        public NumberViewController (IntPtr handle) : base (handle)
        { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Bindings
            MvxFluentBindingDescriptionSet<NumberViewController, NumberViewModel> set = this.CreateBindingSet<NumberViewController, NumberViewModel>();
            set.Bind(hymnNumberTextField).To(vm => vm.HymnNumber);
            set.Bind(openHymnButton).To(vm => vm.OpenHymnCommand);
            set.Apply();
        }
    }
}