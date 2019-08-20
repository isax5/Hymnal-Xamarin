using System.Threading.Tasks;
using Hymnal.Core.Models;
using Hymnal.Core.Models.Parameter;
using Hymnal.Core.Services;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class ThematicHymnsListViewModel : MvxViewModel<Ambit>
    {
        private readonly IMvxNavigationService navigationService;
        private readonly IHymnsService hymnsService;

        private Ambit ambit;
        public Ambit Ambit
        {
            get => ambit;
            set => SetProperty(ref ambit, value);
        }

        public MvxObservableCollection<Hymn> Hymns { get; set; } = new MvxObservableCollection<Hymn>();

        public Hymn SelectedHymn
        {
            get => null;
            set
            {
                if (value == null)
                    return;

                SelectedHymnExecute(value);
                RaisePropertyChanged(nameof(SelectedHymn));
            }
        }


        public ThematicHymnsListViewModel(IMvxNavigationService navigationService, IHymnsService hymnsService)
        {
            this.navigationService = navigationService;
            this.hymnsService = hymnsService;
        }

        public override void Prepare(Ambit parameter)
        {
            Ambit = parameter;
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            Hymns.AddRange((await hymnsService.GetHymnListAsync()).GetRange(Ambit.Star, Ambit.End));
        }


        private void SelectedHymnExecute(Hymn hymn)
        {
            navigationService.Navigate<HymnViewModel, HymnId>(new HymnId { Number = hymn.Number });
        }
    }
}
