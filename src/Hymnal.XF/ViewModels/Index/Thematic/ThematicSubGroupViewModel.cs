using System.Threading.Tasks;
using Hymnal.XF.Extensions;
using Hymnal.XF.Models;
using Hymnal.XF.Models.Parameters;
using Hymnal.XF.Services;
using Hymnal.XF.Views;
using Prism.Navigation;

namespace Hymnal.XF.ViewModels
{
    /// <summary>
    /// Navigable from <see cref="ThematicIndexViewModel"/>
    /// </summary>
    public class ThematicSubGroupViewModel : BaseViewModelParameter<GenericNavigationParameter<Thematic>>
    {
        private readonly IPreferencesService preferencesService;

        private Thematic thematic;
        public Thematic Thematic
        {
            get => thematic;
            set => SetProperty(ref thematic, value);
        }

        public Ambit SelectedAmbit
        {
            get => null;
            set
            {
                if (value == null)
                    return;

                SelectedAmbitExecuteAsync(value).ConfigureAwait(true);
                RaisePropertyChanged(nameof(SelectedAmbit));
            }
        }


        public ThematicSubGroupViewModel(
            INavigationService navigationService,
            IPreferencesService preferencesService
            ) : base(navigationService)
        {
            this.preferencesService = preferencesService;
        }

        public override void OnNavigatedTo(INavigationParameters parameters, GenericNavigationParameter<Thematic> parameter)
        {
            base.OnNavigatedTo(parameters, parameter);
            Thematic = parameter.Value;
        }

        //public override void ViewAppeared()
        //{
        //    base.ViewAppeared();

        //    Analytics.TrackEvent(Constants.TrackEv.Navigation, new Dictionary<string, string>
        //    {
        //        { Constants.TrackEv.NavigationReferenceScheme.PageName, nameof(ThematicSubGroupViewModel) },
        //        { "Thematic Name", Thematic.ThematicName },
        //        { Constants.TrackEv.NavigationReferenceScheme.CultureInfo, Constants.CurrentCultureInfo.Name },
        //        { Constants.TrackEv.NavigationReferenceScheme.HymnalVersion, preferencesService.ConfiguratedHymnalLanguage.Id }
        //    });
        //}

        private async Task SelectedAmbitExecuteAsync(Ambit ambit)
        {
            await NavigationService.NavigateAsync(nameof(ThematicHymnsListPage), new GenericNavigationParameter<Ambit>(ambit));
        }
    }
}
