using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hymnal.Core.Models;
using Hymnal.Core.Models.Parameter;
using Hymnal.Core.Services;
using Hymnal.StorageModels.Models;
using Microsoft.AppCenter.Analytics;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.StorageManager;

namespace Hymnal.Core.ViewModels
{
    public class RecordsViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;
        private readonly IHymnsService hymnsService;
        private readonly IPreferencesService preferencesService;
        private readonly IStorageManager storageManager;

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
            IHymnsService hymnsService,
            IPreferencesService preferencesService,
            IStorageManager storageManager
            )
        {
            this.navigationService = navigationService;
            this.hymnsService = hymnsService;
            this.preferencesService = preferencesService;
            this.storageManager = storageManager;
        }

        public override async Task Initialize()
        {

            var recordHymns = storageManager.All<RecordHymn>().OrderByDescending(r => r.SavedAt).ToList();

            Hymn[] hymns = await Task.WhenAll(recordHymns.Select(r => hymnsService.GetHymnAsync(r)));

            Hymns.AddRange(hymns);

            await base.Initialize();
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();

            Analytics.TrackEvent(Constants.TrackEvents.Navigation, new Dictionary<string, string>
            {
                { Constants.TrackEvents.NavigationReferenceScheme.PageName, nameof(RecordsViewModel) },
                { Constants.TrackEvents.NavigationReferenceScheme.CultureInfo, Constants.CurrentCultureInfo.Name },
                { Constants.TrackEvents.NavigationReferenceScheme.HymnalVersion, preferencesService.ConfiguratedHymnalLanguage.Id }
            });
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
