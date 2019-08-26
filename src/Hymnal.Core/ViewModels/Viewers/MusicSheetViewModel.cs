using Hymnal.Core.Models.Parameter;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class MusicSheetViewModel : MvxViewModel<HymnIdParameter>
    {
        private HymnIdParameter hymn;
        public HymnIdParameter HymnId
        {
            get => hymn;
            set => SetProperty(ref hymn, value);
        }

        private readonly IMvxNavigationService navigationService;

        public MusicSheetViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public override void Prepare(HymnIdParameter parameter)
        {
            HymnId = parameter;
        }
    }
}
