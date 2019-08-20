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


        public ThematicIndexViewModel(IMvxNavigationService navigationService, IHymnsService hymnsService)
        {
            this.navigationService = navigationService;
            this.hymnsService = hymnsService;
        }

        public override async Task Initialize()
        {
            Thematics.AddRange(await hymnsService.GetThematicListAsync());

            await base.Initialize();
        }


        private void SelectedThematicExecute(Thematic thematic)
        {
            navigationService.Navigate<ThematicSubGroupViewModel, Thematic>(thematic);
        }
    }
}
