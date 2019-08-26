using System.Linq;
using System.Threading.Tasks;
using Hymnal.Core.Models;
using Hymnal.Core.Models.Parameter;
using Hymnal.Core.Services;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class RecordsViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;
        private readonly IDataStorageService dataStorageService;

        public MvxObservableCollection<HistoryHymn> Hymns { get; set; } = new MvxObservableCollection<HistoryHymn>();

        public HistoryHymn SelectedHymn
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


        private void SelectedHymnExecute(HistoryHymn hymn)
        {
            navigationService.Navigate<HymnViewModel, HymnIdParameter>(new HymnIdParameter
            {
                Number = hymn.Number,
                HymnalLanguage = hymn.HymnalLanguage
            });
        }
    }
}
