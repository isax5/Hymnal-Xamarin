using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hymnal.Core.Models;
using Hymnal.Core.Models.Parameter;
using Hymnal.Core.Services;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Realms;

namespace Hymnal.Core.ViewModels
{
    public class RecordsViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;
        private readonly IHymnsService hymnsService;
        private readonly Realm realm;

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

        public RecordsViewModel(
            IMvxNavigationService navigationService,
            IHymnsService hymnsService
            )
        {
            this.navigationService = navigationService;
            this.hymnsService = hymnsService;
            realm = Realm.GetInstance();
        }

        public override async Task Initialize()
        {
            var recordHymns = realm.All<RecordHymn>().OrderByDescending(r => r.SavedAt).ToList();

            Hymn[] hymns = await Task.WhenAll(recordHymns.Select(r => hymnsService.GetHymnAsync(r)));

            Hymns.AddRange(hymns);

            await base.Initialize();
        }


        private void SelectedHymnExecute(Hymn hymn)
        {
            navigationService.Navigate<HymnViewModel, HymnIdParameter>(new HymnIdParameter
            {
                Number = hymn.Number,
                HymnalLanguage = HymnalLanguage.GetHymnalLanguageWithId(hymn.HymnalLanguageId),
                SaveInRecords = false
            });
        }
    }
}
