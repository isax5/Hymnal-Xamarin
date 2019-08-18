using System.Linq;
using System.Threading.Tasks;
using Hymnal.Core.Models;
using Hymnal.Core.Models.Parameter;
using Hymnal.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class HymnViewModel : MvxViewModel<HymnId>
    {
        private readonly IMvxNavigationService navigationService;
        private readonly IHymnsService hymnsService;


        private Hymn hymn;
        public Hymn Hymn
        {
            get => hymn;
            set => SetProperty(ref hymn, value);
        }

        public MvxObservableCollection<string> Lyric { get; set; } = new MvxObservableCollection<string>();

        private HymnId hymnId;
        public HymnId HymnId
        {
            get => hymnId;
            set => SetProperty(ref hymnId, value);
        }


        public HymnViewModel(IMvxNavigationService navigationService, IHymnsService hymnsService)
        {
            this.navigationService = navigationService;
            this.hymnsService = hymnsService;
        }

        public override void Prepare(HymnId parameter)
        {
            HymnId = parameter;
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            // Do something
            Hymn = await hymnsService.GetHymnAsync(hymnId.Number);
            Lyric.AddRange(Hymn.Content.Split('¶'));
        }


        #region Commands
        public MvxCommand OpenSheetCommand => new MvxCommand(OpenSheet);
        private void OpenSheet()
        {
            navigationService.Navigate<MusicSheetViewModel, HymnId>(HymnId);
        }

        public MvxCommand CloseCommand => new MvxCommand(Close);
        private void Close()
        {
            navigationService.Close(this);
        }
        #endregion
    }
}
