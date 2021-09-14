using System.Collections.Generic;
using System.Threading.Tasks;
using Hymnal.XF.Constants;
using Hymnal.XF.Extensions;
using Hymnal.XF.Models;
using Hymnal.XF.Models.Parameters;
using Hymnal.XF.Services;
using Hymnal.XF.Views;
using Microsoft.AppCenter.Analytics;
using MvvmHelpers;
using Prism.Navigation;

namespace Hymnal.XF.ViewModels
{
    public sealed class ThematicIndexViewModel : BaseViewModel
    {
        private readonly IHymnsService hymnsService;
        private readonly IPreferencesService preferencesService;

        #region Properties
        public ObservableRangeCollection<Thematic> Thematics { get; } = new();

        public Thematic SelectedThematic
        {
            get => null;
            set
            {
                if (value == null)
                    return;

                SelectedThematicExecuteAsync(value).ConfigureAwait(true);
                RaisePropertyChanged(nameof(SelectedThematic));
            }
        }

        private HymnalLanguage loadedLanguage;
        #endregion

        public ThematicIndexViewModel(
            INavigationService navigationService,
            IHymnsService hymnsService,
            IPreferencesService preferencesService
            ) : base(navigationService)
        {
            this.hymnsService = hymnsService;
            this.preferencesService = preferencesService;
        }

        ~ThematicIndexViewModel()
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
                { TrackingConstants.TrackEv.NavigationReferenceScheme.PageName, nameof(ThematicIndexViewModel) },
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
                    Thematics.Clear();
                }
            }

            if (loadedLanguage.SupportThematicList && Thematics.Count == 0)
            {
                Thematics.AddRange(await hymnsService.GetThematicListAsync(loadedLanguage));
            }
        }

        private async Task SelectedThematicExecuteAsync(Thematic thematic)
        {
            await NavigationService.NavigateAsync(nameof(ThematicSubGroupPage), new GenericNavigationParameter<Thematic>(thematic));
        }
    }
}
