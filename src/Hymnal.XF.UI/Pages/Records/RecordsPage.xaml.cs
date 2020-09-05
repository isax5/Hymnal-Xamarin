using Hymnal.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecordsPage : MvxContentPage<RecordsViewModel>, IMvxOverridePresentationAttribute
    {
        public RecordsPage()
        {
            InitializeComponent();
        }

        public MvxBasePresentationAttribute PresentationAttribute(MvxViewModelRequest request)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    return new MvxModalPresentationAttribute
                    {
                        WrapInNavigationPage = false
                    };
                default:
                    return new MvxContentPagePresentationAttribute
                    {
                        WrapInNavigationPage = true,
                        NoHistory = false
                    };
            }
        }
    }
}
