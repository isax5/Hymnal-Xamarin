using System.Collections.Generic;
using System.Threading.Tasks;
using Hymnal.XF.Constants;
using Hymnal.XF.Extensions;
using Hymnal.XF.Models;
using Hymnal.XF.Models.Parameters;
using Hymnal.XF.Services;
using Microsoft.AppCenter.Analytics;
using MvvmHelpers;
using Prism.Navigation;

namespace Hymnal.XF.ViewModels
{
    public sealed class NumericalIndexViewModel : BaseViewModel
    {
        private readonly IHymnsService hymnsService;
        private readonly IPreferencesService preferencesService;

        #region Properties
        public ObservableRangeCollection<ObservableGroupCollection<string, Hymn>> Hymns { get; } = new();

        public Hymn SelectedHymn
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

        private HymnalLanguage loadedLanguage;
        #endregion

        public NumericalIndexViewModel(
            INavigationService navigationService,
            IHymnsService hymnsService,
            IPreferencesService preferencesService
            ) : base(navigationService)
        {
            this.hymnsService = hymnsService;
            this.preferencesService = preferencesService;
        }

        ~NumericalIndexViewModel()
        {
            preferencesService.HymnalLanguageConfiguredChanged -= PreferencesServiceHymnalLanguageConfiguredChangedAsync;
        }

        public override async Task InitializeAsync(INavigationParameters parameters)
        {
            await base.InitializeAsync(parameters);
            preferencesService.HymnalLanguageConfiguredChanged += PreferencesServiceHymnalLanguageConfiguredChangedAsync;

            HymnalLanguage language = preferencesService.ConfiguredHymnalLanguage;
            await CheckAsync(language);
        }

        public override void OnAppearing()
        {
            base.OnAppearing();

            Analytics.TrackEvent(TrackingConstants.TrackEv.Navigation, new Dictionary<string, string>
            {
                { TrackingConstants.TrackEv.NavigationReferenceScheme.PageName, nameof(NumericalIndexViewModel) },
                { TrackingConstants.TrackEv.NavigationReferenceScheme.CultureInfo, InfoConstants.CurrentCultureInfo.Name },
                { TrackingConstants.TrackEv.NavigationReferenceScheme.HymnalVersion, preferencesService.ConfiguredHymnalLanguage.Id }
            });
        }

        private async void PreferencesServiceHymnalLanguageConfiguredChangedAsync(object sender, HymnalLanguage e)
        {
            await CheckAsync(e);
        }

        private async Task CheckAsync(HymnalLanguage newLanguage)
        {
            if (loadedLanguage == null)
            {
                loadedLanguage = newLanguage;
            }
            else
            {
                // If the Language changed
                if (!newLanguage.Equals(loadedLanguage))
                {
                    loadedLanguage = newLanguage;
                    Hymns.Clear();
                }
            }

            if (Hymns.Count == 0)
            {
                Hymns.AddRange((await hymnsService.GetHymnListAsync(loadedLanguage)).OrderByNumber().GroupByNumber());
            }
        }

        private async Task SelectedHymnExecuteAsync(Hymn hymn)
        {
            await NavigationService.NavigateAsync(
                NavRoutes.HymnViewerAsModal,
                new HymnIdParameter
                {
                    Number = hymn.Number,
                    HymnalLanguage = loadedLanguage
                }, true, true);
        }
    }
}
