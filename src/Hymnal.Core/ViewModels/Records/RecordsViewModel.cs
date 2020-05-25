using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hymnal.Core.Models;
using Hymnal.Core.Models.Parameter;
using Hymnal.Core.Services;
using Microsoft.AppCenter.Analytics;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
#if __IOS__ || __ANDROID__
using Realms;
#endif

namespace Hymnal.Core.ViewModels
{
    public class RecordsViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;
        private readonly IHymnsService hymnsService;
        private readonly IPreferencesService preferencesService;
#if __IOS__ || __ANDROID__
        private readonly Realm realm;
#endif

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
            IPreferencesService preferencesService
            )
        {
            this.navigationService = navigationService;
            this.hymnsService = hymnsService;
            this.preferencesService = preferencesService;
#if __IOS__ || __ANDROID__
            realm = Realm.GetInstance();
#endif
        }

        public override async Task Initialize()
        {
#if __IOS__ || __ANDROID__
            var recordHymns = realm.All<RecordHymn>().OrderByDescending(r => r.SavedAt).ToList();

            Hymn[] hymns = await Task.WhenAll(recordHymns.Select(r => hymnsService.GetHymnAsync(r)));

            Hymns.AddRange(hymns);
#endif

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
