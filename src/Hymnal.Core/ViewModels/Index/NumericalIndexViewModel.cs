using System;
using System.Threading.Tasks;
using Hymnal.Core.Models;
using Hymnal.Core.Models.Parameter;
using Hymnal.Core.Services;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class NumericalIndexViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;
        private readonly IHymnsService hymnsService;

        public MvxObservableCollection<ObservableGroupCollection<string, Hymn>> Hymns { get; set; } = new MvxObservableCollection<ObservableGroupCollection<string, Hymn>>();

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

        public NumericalIndexViewModel(IMvxNavigationService navigationService, IHymnsService hymnsService)
        {
            this.navigationService = navigationService;
            this.hymnsService = hymnsService;
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            Hymns.AddRange((await hymnsService.GetHymnListAsync()).OrderByNumber().GroupByNumber());
        }


        private void SelectedHymnExecute(Hymn hymn)
        {
            navigationService.Navigate<HymnViewModel, HymnId>(new HymnId { Number = hymn.ID });
        }
    }
}
