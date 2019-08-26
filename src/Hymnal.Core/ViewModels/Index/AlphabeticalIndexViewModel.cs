using System.Threading.Tasks;
using Hymnal.Core.Extensions;
using Hymnal.Core.Models;
using Hymnal.Core.Models.Parameter;
using Hymnal.Core.Services;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class AlphabeticalIndexViewModel : MvxViewModel
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

                SelectedHymnExecute(value);
                RaisePropertyChanged(nameof(SelectedHymn));
            }
        }

        private HymnalLanguage loadedLanguage;

        public AlphabeticalIndexViewModel(
            IMvxNavigationService navigationService,
            IHymnsService hymnsService,
            IPreferencesService preferencesService
            )
        {
            this.navigationService = navigationService;
            this.hymnsService = hymnsService;
            this.preferencesService = preferencesService;
        }

        public override async Task Initialize()
        {
            await CheckAsync();
            await base.Initialize();
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();
            CheckAsync();
        }

        private async Task CheckAsync()
        {
            HymnalLanguage newLanguage = preferencesService.ConfiguratedHymnalLanguage;

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
                Hymns.AddRange((await hymnsService.GetHymnListAsync(loadedLanguage)).OrderByTitle().GroupByTitle());
            }
        }


        private void SelectedHymnExecute(Hymn hymn)
        {
            navigationService.Navigate<HymnViewModel, HymnIdParameter>(new HymnIdParameter
            {
                Number = hymn.Number,
                HymnalLanguage = loadedLanguage
            });
        }
    }
}
