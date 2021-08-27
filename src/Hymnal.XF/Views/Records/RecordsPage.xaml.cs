using Hymnal.XF.Resources.Languages;
using Hymnal.XF.ViewModels;
using Xamarin.Forms.Xaml;

namespace Hymnal.XF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public sealed partial class RecordsPage : BaseContentPage<RecordsViewModel>, IModalPage
    {

        public RecordsPage()
        {
            InitializeComponent();
        }

        #region IModalPage
        public string CloseButtonText => Languages.Generic_Close;

        public void PopModal() => ViewModel.NavigationService.GoBackAsync(null, true, true);
        #endregion
    }
}
