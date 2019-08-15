using Hymnal.Core.Models.Parameter;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class MusicSheetViewModel : MvxViewModel<HymnId>
    {
        private HymnId hymn;
        public HymnId HymnId
        {
            get => hymn;
            set => SetProperty(ref hymn, value);
        }

        private readonly IMvxNavigationService navigationService;

        public MusicSheetViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public override void Prepare(HymnId parameter)
        {
            HymnId = parameter;
        }
    }
}
