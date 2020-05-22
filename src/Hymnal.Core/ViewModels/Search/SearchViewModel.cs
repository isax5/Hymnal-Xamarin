using System.Collections.Generic;
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
    public class SearchViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;
        private readonly IHymnsService hymnsService;
        private readonly IPreferencesService preferencesService;

        public MvxObservableCollection<Hymn> Hymns { get; set; } = new MvxObservableCollection<Hymn>();

        /// <summary>
        /// Hymn id for transition
        /// </summary>
        private int hymnId;
        public int HymnId
        {
            get => hymnId;
            set => SetProperty(ref hymnId, value);
        }

        public Hymn SelectedHymn
        {
            get => null;
            set
            {
                if (value == null)
                    return;

                HymnId = value.Number;
                RaisePropertyChanged(nameof(SelectedHymn));

                SelectedHymnExecute(value);
            }
        }

        private string textSearchBar;
        public string TextSearchBar
        {
            get => textSearchBar;
            set
            {
                SetProperty(ref textSearchBar, value);
                TextSearchExecuteAsync(value);
            }
        }

        private readonly HymnalLanguage language;

        public SearchViewModel(
            IMvxNavigationService navigationService,
            IHymnsService hymnsService,
            IPreferencesService preferencesService
            )
        {
            this.navigationService = navigationService;
            this.hymnsService = hymnsService;
            this.preferencesService = preferencesService;

            language = this.preferencesService.ConfiguratedHymnalLanguage;
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            TextSearchExecuteAsync(string.Empty);
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();

            Analytics.TrackEvent(Constants.TrackEvents.Navigation, new Dictionary<string, string>
            {
                { Constants.TrackEvents.NavigationReferenceScheme.PageName, nameof(SearchViewModel) },
                { Constants.TrackEvents.NavigationReferenceScheme.CultureInfo, Constants.CurrentCultureInfo.Name },
                { Constants.TrackEvents.NavigationReferenceScheme.HymnalVersion, preferencesService.ConfiguratedHymnalLanguage.Id }
            });
        }

        private async void TextSearchExecuteAsync(string text)
        {
            Hymns.Clear();
            IEnumerable<Hymn> hymns = (await hymnsService.GetHymnListAsync(language)).OrderByNumber();

            if (string.IsNullOrWhiteSpace(text))
            {
                Hymns.AddRange(hymns);
                return;
            }

            Hymns.AddRange(hymns.SearchQuery(text));
        }

        private void SelectedHymnExecute(Hymn hymn)
        {
            navigationService.Navigate<HymnViewModel, HymnIdParameter>(new HymnIdParameter
            {
                Number = hymn.Number,
                HymnalLanguage = language
            });

            if (!string.IsNullOrWhiteSpace(TextSearchBar))
            {

                Analytics.TrackEvent(Constants.TrackEvents.HymnFounded, new Dictionary<string, string>
                {
                    { Constants.TrackEvents.HymnFoundedScheme.Query, TextSearchBar },
                    { Constants.TrackEvents.HymnFoundedScheme.HymnFounded, hymn.Number.ToString() },
                    { Constants.TrackEvents.HymnFoundedScheme.HymnalVersion, language.Id }
                });
            }
        }
    }
}
