using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Helpers;
using Hymnal.XF.Constants;
using Hymnal.XF.Extensions;
using Hymnal.XF.Models;
using Hymnal.XF.Models.Parameters;
using Hymnal.XF.Services;
using Microsoft.AppCenter.Analytics;
using MvvmHelpers;
using Prism.Navigation;
using Xamarin.Essentials.Interfaces;

namespace Hymnal.XF.ViewModels
{
    public sealed class SearchViewModel : BaseViewModel
    {
        private readonly IHymnsService hymnsService;
        private readonly IPreferencesService preferencesService;
        private readonly IMainThread mainThread;

        #region Properties
        public ObservableRangeCollection<Hymn> Hymns { get; set; } = new ObservableRangeCollection<Hymn>();

        public Hymn SelectedHymn
        {
            get => null;
            set
            {
                if (value == null)
                    return;

                RaisePropertyChanged(nameof(SelectedHymn));

                SelectedHymnExecuteAsync(value).ConfigureAwait(true);
            }
        }

        private string textSearchBar = string.Empty;
        public string TextSearchBar
        {
            get => textSearchBar;
            set
            {
                if (SetProperty(ref textSearchBar, value))
                    observableTextSearchBar.NextValue(value);
            }
        }
        private readonly ObservableValue<string> observableTextSearchBar = new(false);

        private HymnalLanguage _language;
        #endregion

        public SearchViewModel(
            INavigationService navigationService,
            IHymnsService hymnsService,
            IPreferencesService preferencesService,
            IMainThread mainThread
            ) : base(navigationService)
        {
            this.hymnsService = hymnsService;
            this.preferencesService = preferencesService;
            this.mainThread = mainThread;
            _language = this.preferencesService.ConfiguratedHymnalLanguage;

            preferencesService.HymnalLanguageConfiguratedChanged += PreferencesService_HymnalLanguageConfiguratedChangedAsync;

            observableTextSearchBar
                .DistinctUntilChanged()
                .Throttle(TimeSpan.FromSeconds(0.3))
                .Subscribe(text => this.mainThread.InvokeOnMainThreadAsync(async () => await TextSearchExecuteAsync(text)));
        }

        ~SearchViewModel()
        {
            preferencesService.HymnalLanguageConfiguratedChanged -= PreferencesService_HymnalLanguageConfiguratedChangedAsync;
            observableTextSearchBar.DisposeAll();
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            Task.Run(() => mainThread.InvokeOnMainThreadAsync(async () => await TextSearchExecuteAsync(string.Empty)));
        }

        private void PreferencesService_HymnalLanguageConfiguratedChangedAsync(object sender, HymnalLanguage e)
        {
            _language = e;
            observableTextSearchBar.NextValue(string.Empty);
        }

        public override void OnAppearing()
        {
            base.OnAppearing();

            Analytics.TrackEvent(TrackingConstants.TrackEv.Navigation, new Dictionary<string, string>
            {
                { TrackingConstants.TrackEv.NavigationReferenceScheme.PageName, nameof(SearchViewModel) },
                { TrackingConstants.TrackEv.NavigationReferenceScheme.CultureInfo, InfoConstants.CurrentCultureInfo.Name },
                { TrackingConstants.TrackEv.NavigationReferenceScheme.HymnalVersion, preferencesService.ConfiguratedHymnalLanguage.Id }
            });
        }

        private async Task TextSearchExecuteAsync(string text)
        {
            IEnumerable<Hymn> hymns = (await hymnsService.GetHymnListAsync(_language)).OrderByNumber();

            if (string.IsNullOrWhiteSpace(text))
            {
                Hymns.ReplaceRange(hymns);
                return;
            }

            Hymns.ReplaceRange(hymns.SearchQuery(text));
        }

        private async Task SelectedHymnExecuteAsync(Hymn hymn)
        {
            // Some iOS devices that have had problems in this place
            if (hymn == null)
                return;

            try
            {
                await NavigationService.NavigateAsync(
                    NavRoutes.HymnViewerAsModal,
                    new HymnIdParameter
                    {
                        Number = hymn.Number,
                        HymnalLanguage = _language
                    }, true, true);

                if (!string.IsNullOrWhiteSpace(TextSearchBar))
                {

                    Analytics.TrackEvent(TrackingConstants.TrackEv.HymnFounded, new Dictionary<string, string>
                    {
                        { TrackingConstants.TrackEv.HymnFoundedScheme.Query, TextSearchBar },
                        { TrackingConstants.TrackEv.HymnFoundedScheme.HymnFounded, hymn.Number.ToString() },
                        { TrackingConstants.TrackEv.HymnFoundedScheme.HymnalVersion, _language.Id }
                    });
                }
            }
            catch (Exception ex)
            {
                ex.Report(new Dictionary<string, string>()
                    {
                        { "File", nameof(SearchViewModel) },
                        { "Opening Hymn", hymn.Number.ToString() }
                    });
            }
        }
    }
}
