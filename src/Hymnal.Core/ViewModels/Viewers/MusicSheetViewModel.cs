using System.Threading.Tasks;
using Hymnal.Core.Extensions;
using Hymnal.Core.Models.Parameter;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class MusicSheetViewModel : MvxViewModel<HymnIdParameter>
    {
        private readonly IMvxNavigationService navigationService;

        private HymnIdParameter hymn;
        public HymnIdParameter HymnId
        {
            get => hymn;
            set => SetProperty(ref hymn, value);
        }

        private string imageSource;
        public string ImageSource
        {
            get => imageSource;
            set => SetProperty(ref imageSource, value);
        }

        public MusicSheetViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public override void Prepare(HymnIdParameter parameter)
        {
            HymnId = parameter;
        }

        public override Task Initialize()
        {
            ImageSource = HymnId.HymnalLanguage.GetMusicSheetSource(HymnId.Number);
            return base.Initialize();
        }
    }
}
