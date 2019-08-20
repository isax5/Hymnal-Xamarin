using System.Threading.Tasks;
using Hymnal.Core.Models;
using Hymnal.Core.Models.Parameter;
using Hymnal.Core.Services;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Linq;

namespace Hymnal.Core.ViewModels
{
    public class RecordsViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;
        private readonly IDataStorageService dataStorageService;

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

        public RecordsViewModel(IMvxNavigationService navigationService, IDataStorageService dataStorageService)
        {
            this.navigationService = navigationService;
            this.dataStorageService = dataStorageService;
        }

        public override Task Initialize()
        {
            Hymns.AddRange(dataStorageService.GetItems<HistoryHymn>().OrderByDescending(h => h.SavedAt));

            return base.Initialize();
        }


        private void SelectedHymnExecute(Hymn hymn)
        {
            navigationService.Navigate<HymnViewModel, HymnId>(new HymnId { Number = hymn.Number });
        }
    }
}
