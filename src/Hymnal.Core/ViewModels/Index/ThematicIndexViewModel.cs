using System.Threading.Tasks;
using Hymnal.Core.Models;
using Hymnal.Core.Services;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class ThematicIndexViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;
        private readonly IHymnsService hymnsService;
        private readonly IPreferencesService preferencesService;

        public MvxObservableCollection<Thematic> Thematics { get; set; } = new MvxObservableCollection<Thematic>();

        public Thematic SelectedThematic
        {
            get => null;
            set
            {
                if (value == null)
                    return;

                SelectedThematicExecute(value);
                RaisePropertyChanged(nameof(SelectedThematic));
            }
        }

        private HymnalLanguage loadedLanguage;

        public ThematicIndexViewModel(
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
                    Thematics.Clear();
                }
            }

            if (loadedLanguage.SupportThematicList && Thematics.Count == 0)
            {
                Thematics.AddRange(await hymnsService.GetThematicListAsync(loadedLanguage));
            }
        }

        private void SelectedThematicExecute(Thematic thematic)
        {
            navigationService.Navigate<ThematicSubGroupViewModel, Thematic>(thematic);
        }
    }
}
