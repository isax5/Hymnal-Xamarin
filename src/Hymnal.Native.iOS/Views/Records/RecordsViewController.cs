using System;
using Hymnal.Core.ViewModels;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace Hymnal.Native.iOS.Views
{
    [MvxFromStoryboard("Main")]
    [MvxModalPresentation(Animated = true, ModalPresentationStyle = UIModalPresentationStyle.PageSheet)]
    public partial class RecordsViewController : MvxViewController<RecordsViewModel>
    {
        public RecordsViewController(IntPtr handle) : base(handle)
        {
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
