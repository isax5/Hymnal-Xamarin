using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Hymnal.Core.Extensions;
using Hymnal.Core.Models;
using Hymnal.Core.Models.Parameter;
using Hymnal.Core.Services;
using Microsoft.AppCenter.Analytics;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class NumericalIndexViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;
        private readonly IHymnsService hymnsService;
        private readonly IPreferencesService preferencesService;

        public MvxObservableCollection<ObservableGroupCollection<string, Hymn>> Hymns { get; set; } = new MvxObservableCollection<ObservableGroupCollection<string, Hymn>>();

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

        public NumericalIndexViewModel(
            IMvxNavigationService navigationService,
            IHymnsService hymnsService,
            IPreferencesService preferencesService
            )
        {
            this.navigationService = navigationService;
            this.hymnsService = hymnsService;
            this.preferencesService = preferencesService;
        }

        ~NumericalIndexViewModel()
        {
            preferencesService.HymnalLanguageConfiguratedChanged -= PreferencesService_HymnalLanguageConfiguratedChangedAsync;
        }

        public override async Task Initialize()
        {
            preferencesService.HymnalLanguageConfiguratedChanged += PreferencesService_HymnalLanguageConfiguratedChangedAsync;

            HymnalLanguage language = preferencesService.ConfiguratedHymnalLanguage;
            await CheckAsync(language);

            await base.Initialize();
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();

            Analytics.TrackEvent(Constants.TrackEv.Navigation, new Dictionary<string, string>
            {
                { Constants.TrackEv.NavigationReferenceScheme.PageName, nameof(NumericalIndexViewModel) },
                { Constants.TrackEv.NavigationReferenceScheme.CultureInfo, Constants.CurrentCultureInfo.Name },
                { Constants.TrackEv.NavigationReferenceScheme.HymnalVersion, preferencesService.ConfiguratedHymnalLanguage.Id }
            });
        }


        private async void PreferencesService_HymnalLanguageConfiguratedChangedAsync(object sender, HymnalLanguage e)
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
            await navigationService.Navigate<HymnViewModel, HymnIdParameter>(new HymnIdParameter
            {
                Number = hymn.Number,
                HymnalLanguage = loadedLanguage
            });
        }
    }
}
