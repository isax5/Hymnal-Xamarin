using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hymnal.XF.Constants;
using Hymnal.XF.Extensions;
using Hymnal.XF.Models;
using Hymnal.XF.Models.Parameters;
using Hymnal.XF.Models.Realm;
using Hymnal.XF.Services;
using Microsoft.AppCenter.Analytics;
using MvvmHelpers;
using Prism.Navigation;

namespace Hymnal.XF.ViewModels
{
    public sealed class RecordsViewModel : BaseViewModel
    {
        private readonly IHymnsService hymnsService;
        private readonly IStorageManagerService storageManager;
        private readonly IPreferencesService preferencesService;

        public ObservableRangeCollection<Tuple<RecordHymn, Hymn>> Hymns { get; } = new();

        public Tuple<RecordHymn, Hymn> SelectedHymn
        {
            get => null;
            set
            {
                if (value == null)
                    return;

                SelectedHymnExecuteAsync(value).ConfigureAwait(true);
                OnPropertyChanged(nameof(SelectedHymn));
            }
        }

        public RecordsViewModel(
            INavigationService navigationService,
            IHymnsService hymnsService,
            IStorageManagerService storageManager,
            IPreferencesService preferencesService
            ) : base(navigationService)
        {
            this.hymnsService = hymnsService;
            this.storageManager = storageManager;
            this.preferencesService = preferencesService;
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

        public override void OnAppearing()
        {
            base.OnAppearing();

            Analytics.TrackEvent(TrackingConstants.TrackEv.Navigation, new Dictionary<string, string>
            {
                { TrackingConstants.TrackEv.NavigationReferenceScheme.PageName, nameof(RecordsViewModel) },
                { TrackingConstants.TrackEv.NavigationReferenceScheme.CultureInfo, InfoConstants.CurrentCultureInfo.Name },
                { TrackingConstants.TrackEv.NavigationReferenceScheme.HymnalVersion, preferencesService.ConfiguredHymnalLanguage.Id }
            });
        }

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
