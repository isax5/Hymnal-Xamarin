using Foundation;
using Hymnal.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using System;
using UIKit;
using MvvmCross.Navigation;
using MvvmCross;

namespace Hymnal.Native.iOS.Views
{
    [MvxFromStoryboard("Main")]
    [MvxModalPresentation(Animated = true, ModalPresentationStyle = UIModalPresentationStyle.PageSheet)]
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

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(false);

            // TODO: Correction MVX Modal Page
            IMvxNavigationService navigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
            navigationService.Close(ViewModel);
        }
    }
}