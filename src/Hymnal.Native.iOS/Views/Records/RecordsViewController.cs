using Hymnal.Core.ViewModels;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using System;
using UIKit;
using MvvmCross;
using MvvmCross.Navigation;

namespace Hymnal.iOS
{
    [MvxFromStoryboard("Main")]
    [MvxModalPresentation(Animated = true, ModalPresentationStyle = UIModalPresentationStyle.PageSheet)]
    public partial class RecordsViewController : MvxViewController<RecordsViewModel>
    {
        public RecordsViewController (IntPtr handle) : base (handle)
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