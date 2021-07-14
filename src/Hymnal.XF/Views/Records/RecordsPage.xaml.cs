using Hymnal.XF.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecordsPage : BaseContentPage<RecordsViewModel>
    {
        public RecordsPage()
        {
            InitializeComponent();

            if ((Device.RuntimePlatform == Device.iOS && DeviceInfo.Version.Major < 13)
                || Device.RuntimePlatform != Device.iOS)
            {
                SlideBar.IsVisible = false;
            }
        }

        //public MvxBasePresentationAttribute PresentationAttribute(MvxViewModelRequest request)
        //{
        //    if (Device.RuntimePlatform == Device.iOS && DeviceInfo.Version.Major >= 13)
        //    {
        //        return new MvxModalPresentationAttribute
        //        {
        //            WrapInNavigationPage = false
        //        };

        //    }
        //    else
        //    {
        //        return new MvxContentPagePresentationAttribute
        //        {
        //            WrapInNavigationPage = true,
        //            NoHistory = false
        //        };
        //    }
        //}
    }
}
