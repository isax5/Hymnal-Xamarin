using Hymnal.XF.ViewModels;
using Prism.Navigation;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecordsPage : BaseContentPage<RecordsViewModel>, IModalPage
    {
        private readonly INavigationService navigationService;

        public RecordsPage(INavigationService navigationService)
        {
            InitializeComponent();
            this.navigationService = navigationService;
        }

        public void PopModal() => navigationService.GoBackAsync(null, true, true);

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
