using Hymnal.XF.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecordsPage : BaseContentPage<RecordsViewModel>, IModalPage
    {
        public RecordsPage()
        {
            InitializeComponent();
        }

        public void PopModal() => ViewModel.NavigationService.GoBackAsync(null, true, true);

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
