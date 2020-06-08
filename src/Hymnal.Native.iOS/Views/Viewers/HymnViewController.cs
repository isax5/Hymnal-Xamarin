using System;
using Hymnal.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;

namespace Hymnal.Native.iOS.Views
{
    [MvxFromStoryboard("Main")]
    //[MvxModalPresentation(Animated = true, ModalPresentationStyle = UIModalPresentationStyle.PageSheet)]
    [MvxModalPresentation(Animated = true, WrapInNavigationController = true)]
    public partial class HymnViewController : BaseViewController<HymnViewModel>
    {
        public HymnViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            MvxFluentBindingDescriptionSet<HymnViewController, HymnViewModel> set = this.CreateBindingSet<HymnViewController, HymnViewModel>();
            Title = ViewModel.Hymn.Title;
            set.Bind(hymnTitleLabel).To(vm => vm.Hymn.Title);
            set.Bind(hymnContentLabel).To(vm => vm.Hymn.Content);
            set.Bind(closeBarButton).To(vm => vm.CloseCommand);
            set.Bind(openSheetBarButton).To(vm => vm.OpenSheetCommand);
            set.Apply();
        }

        //public override void ViewDidDisappear(bool animated)
        //{
        //    base.ViewDidDisappear(false);

        //    // TODO: Correction MVX Modal Page
        //    IMvxNavigationService navigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
        //    navigationService.Close(ViewModel);
        //}
    }
}
