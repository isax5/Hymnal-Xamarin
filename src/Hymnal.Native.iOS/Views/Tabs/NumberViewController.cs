using Foundation;
using Hymnal.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using System;
using UIKit;

namespace Hymnal.iOS.Views
{
    [MvxFromStoryboard("Main")]
    [MvxTabPresentation(TabName = "Numbers", TabIconName = "number tab")]
    public partial class NumberViewController : BaseViewController<NumberViewModel>
    {
        public NumberViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Bindings
            MvxFluentBindingDescriptionSet<NumberViewController, NumberViewModel> set = this.CreateBindingSet<NumberViewController, NumberViewModel>();
            set.Bind(recordsBarButtton).To(vm => vm.OpenRecordsCommand);
            set.Bind(searchBarButtton).To(vm => vm.OpenSearchCommand);
            set.Bind(hymnNumberTextField).To(vm => vm.HymnNumber);
            set.Bind(openHymnButton).To(vm => vm.OpenHymnCommand);
            set.Apply();

            // UI Configuration
            hymnNumberTextField.AttributedPlaceholder = new NSAttributedString("....", null, UIColor.DarkGray);
        }


        partial void OpenHymnButton_TouchUpInside(UIButton sender)
        {
            if (string.IsNullOrWhiteSpace(hymnNumberTextField.Text))
            {
                hymnNumberTextField.BecomeFirstResponder();
            }
        }
    }
}