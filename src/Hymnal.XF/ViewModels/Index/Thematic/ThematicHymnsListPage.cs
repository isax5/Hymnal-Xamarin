using System.Collections.Generic;
using System.Threading.Tasks;
using Hymnal.XF.Extensions;
using Hymnal.XF.Models;
using Hymnal.XF.Models.Parameters;
using Hymnal.XF.Services;
using Microsoft.AppCenter.Analytics;
using MvvmHelpers;
using Prism.Navigation;

namespace Hymnal.XF.ViewModels
{
    public class ThematicHymnsListViewModel : BaseViewModel<GenericNavigationParameter<Ambit>>
    {
        private readonly IHymnsService hymnsService;
        private readonly IPreferencesService preferencesService;
        private Ambit ambit;
        public Ambit Ambit
        {
            get => ambit;
            set => SetProperty(ref ambit, value);
        }

        public ObservableRangeCollection<Hymn> Hymns { get; set; } = new ObservableRangeCollection<Hymn>();

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

        private readonly HymnalLanguage language;

        public ThematicHymnsListViewModel(
            INavigationService navigationService,
            IHymnsService hymnsService,
            IPreferencesService preferencesService
            ) : base(navigationService)
        {
            this.hymnsService = hymnsService;
            this.preferencesService = preferencesService;

            language = this.preferencesService.ConfiguratedHymnalLanguage;
        }

        public override void OnNavigatedTo(INavigationParameters parameters, GenericNavigationParameter<Ambit> parameter)
        {
            base.OnNavigatedTo(parameters, parameter);
            Ambit = parameter.Value;
        }

        public override async void OnAppearing()
        {
            base.OnAppearing();

            Hymns.AddRange((await hymnsService.GetHymnListAsync(language)).GetRange(Ambit.Star, Ambit.End));
        }

        //public override void ViewAppeared()
        //{
        //    base.ViewAppeared();

        //    Analytics.TrackEvent(Constants.TrackEv.Navigation, new Dictionary<string, string>
        //    {
        //        { Constants.TrackEv.NavigationReferenceScheme.PageName, nameof(ThematicHymnsListViewModel) },
        //        { "Ambit", Ambit.AmbitName },
        //        { Constants.TrackEv.NavigationReferenceScheme.CultureInfo, Constants.CurrentCultureInfo.Name },
        //        { Constants.TrackEv.NavigationReferenceScheme.HymnalVersion, preferencesService.ConfiguratedHymnalLanguage.Id }
        //    });
        //}

        private async Task SelectedHymnExecuteAsync(Hymn hymn)
        {
            await NavigationService.NavigateAsync<HymnViewModel, HymnIdParameter>(new HymnIdParameter
            {
                Number = hymn.Number,
                HymnalLanguage = language
            });
        }
    }
}
