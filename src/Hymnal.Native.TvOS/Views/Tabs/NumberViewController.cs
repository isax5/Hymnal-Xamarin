using Hymnal.Core.Models;
using Hymnal.Core.Models.Parameter;
using Hymnal.Core.Services;
using Hymnal.Core.ViewModels;
using MvvmCross;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Tvos.Presenters.Attributes;
using MvvmCross.Platforms.Tvos.Views;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;

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

        partial void openHymnActionButton(UIButton sender)
        {
            if (string.IsNullOrWhiteSpace(hymnNumberTextField.Text))
            {
                hymnNumberTextField.BecomeFirstResponder();
            }
        }
    }
}