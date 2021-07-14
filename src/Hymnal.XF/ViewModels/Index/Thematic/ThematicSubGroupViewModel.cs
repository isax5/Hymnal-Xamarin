using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hymnal.XF.Models;
using Hymnal.XF.Services;
using Microsoft.AppCenter.Analytics;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.XF.ViewModels
{
    /// <summary>
    /// Navigable from <see cref="ThematicIndexViewModel"/>
    /// </summary>
    public class ThematicSubGroupViewModel : MvxViewModel<Thematic>
    {
        private readonly INavigationService navigationService;
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
            )
        {
            this.navigationService = navigationService;
            this.preferencesService = preferencesService;
        }

        public override void Prepare(Thematic parameter)
        {
            Thematic = parameter;
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();

            Analytics.TrackEvent(Constants.TrackEv.Navigation, new Dictionary<string, string>
            {
                { Constants.TrackEv.NavigationReferenceScheme.PageName, nameof(ThematicSubGroupViewModel) },
                { "Thematic Name", Thematic.ThematicName },
                { Constants.TrackEv.NavigationReferenceScheme.CultureInfo, Constants.CurrentCultureInfo.Name },
                { Constants.TrackEv.NavigationReferenceScheme.HymnalVersion, preferencesService.ConfiguratedHymnalLanguage.Id }
            });
        }

        private async Task SelectedAmbitExecuteAsync(Ambit ambit)
        {
            await navigationService.Navigate<ThematicHymnsListViewModel, Ambit>(ambit);
        }
    }
}
