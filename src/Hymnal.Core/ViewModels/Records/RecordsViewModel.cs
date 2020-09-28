using System;
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

        public MvxObservableCollection<Tuple<RecordHymn, Hymn>> Hymns { get; set; } = new MvxObservableCollection<Tuple<RecordHymn, Hymn>>();

        public Tuple<RecordHymn, Hymn> SelectedHymn
        {
            get => null;
            set
            {
                if (value == null)
                    return;

                SelectedHymnExecuteAsync(value).ConfigureAwait(true);
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

            Tuple<RecordHymn, Hymn>[] hymns = await Task.WhenAll(
                storageManager
                .All<RecordHymn>()
                .OrderByDescending(r => r.SavedAt)
                .ToList()
                .Select(async r => new Tuple<RecordHymn, Hymn>(r, await hymnsService.GetHymnAsync(r))));

            Hymns.AddRange(hymns);

            await base.Initialize();
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();

            Analytics.TrackEvent(Constants.TrackEv.Navigation, new Dictionary<string, string>
            {
                { Constants.TrackEv.NavigationReferenceScheme.PageName, nameof(RecordsViewModel) },
                { Constants.TrackEv.NavigationReferenceScheme.CultureInfo, Constants.CurrentCultureInfo.Name },
                { Constants.TrackEv.NavigationReferenceScheme.HymnalVersion, preferencesService.ConfiguratedHymnalLanguage.Id }
            });
        }

        private async Task SelectedHymnExecuteAsync(Tuple<RecordHymn, Hymn> hymn)
        {
            await navigationService.Navigate<HymnViewModel, HymnIdParameter>(new HymnIdParameter
            {
                Number = hymn.Item2.Number,
                HymnalLanguage = HymnalLanguage.GetHymnalLanguageWithId(hymn.Item2.HymnalLanguageId),
                SaveInRecords = false
            });
        }
    }
}
