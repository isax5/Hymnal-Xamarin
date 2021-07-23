using System;
using System.Linq;
using System.Threading.Tasks;
using Hymnal.XF.Constants;
using Hymnal.XF.Extensions;
using Hymnal.XF.Models;
using Hymnal.XF.Models.Parameters;
using Hymnal.XF.Models.Realm;
using Hymnal.XF.Services;
using MvvmHelpers;
using Prism.Navigation;

namespace Hymnal.XF.ViewModels
{
    public class RecordsViewModel : BaseViewModel
    {
        private readonly IHymnsService hymnsService;
        private readonly IStorageManagerService storageManager;

        public ObservableRangeCollection<Tuple<RecordHymn, Hymn>> Hymns { get; } = new();

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
            INavigationService navigationService,
            IHymnsService hymnsService,
            IStorageManagerService storageManager
            ) : base(navigationService)
        {
            this.hymnsService = hymnsService;
            this.storageManager = storageManager;
        }

        public override async Task InitializeAsync(INavigationParameters parameters)
        {
            await base.InitializeAsync(parameters);
            Tuple<RecordHymn, Hymn>[] hymns = await Task.WhenAll(
                storageManager
                .All<RecordHymn>()
                .OrderByDescending(r => r.SavedAt)
                .ToList()
                .Select(async r => new Tuple<RecordHymn, Hymn>(r, await hymnsService.GetHymnAsync(r))));

            Hymns.AddRange(hymns);
        }

        //public override void ViewAppeared()
        //{
        //    base.ViewAppeared();

        //    Analytics.TrackEvent(Constants.TrackEv.Navigation, new Dictionary<string, string>
        //    {
        //        { Constants.TrackEv.NavigationReferenceScheme.PageName, nameof(RecordsViewModel) },
        //        { Constants.TrackEv.NavigationReferenceScheme.CultureInfo, Constants.CurrentCultureInfo.Name },
        //        { Constants.TrackEv.NavigationReferenceScheme.HymnalVersion, preferencesService.ConfiguratedHymnalLanguage.Id }
        //    });
        //}

        private async Task SelectedHymnExecuteAsync(Tuple<RecordHymn, Hymn> hymn)
        {
            await NavigationService.NavigateAsync(
                NavRoutes.HymnViewerAsModal,
                new HymnIdParameter
                {
                    Number = hymn.Item2.Number,
                    HymnalLanguage = HymnalLanguage.GetHymnalLanguageWithId(hymn.Item2.HymnalLanguageId),
                    SaveInRecords = false
                }, true, true);
        }
    }
}
