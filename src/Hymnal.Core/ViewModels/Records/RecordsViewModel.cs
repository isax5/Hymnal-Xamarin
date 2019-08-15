using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class RecordsViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;

        public RecordsViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
    }
}
