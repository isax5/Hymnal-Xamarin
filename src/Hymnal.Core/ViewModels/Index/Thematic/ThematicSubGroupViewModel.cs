using System;
using System.Collections.Generic;
using Hymnal.Core.Models;
using Hymnal.Core.Services;
using Microsoft.AppCenter.Analytics;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    /// <summary>
    /// Navigable from <see cref="ThematicIndexViewModel"/>
    /// </summary>
    public class ThematicSubGroupViewModel : MvxViewModel<Thematic>
    {
        private readonly IMvxNavigationService navigationService;
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

                SelectedAmbitExecute(value);
                RaisePropertyChanged(nameof(SelectedAmbit));
            }
        }


        public ThematicSubGroupViewModel(
            IMvxNavigationService navigationService,
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

            Analytics.TrackEvent(Constants.TrackEvents.Navigation, new Dictionary<string, string>
            {
                { Constants.TrackEvents.NavigationReferenceScheme.PageName, nameof(ThematicSubGroupViewModel) },
                { "Thematic Name", Thematic.ThematicName },
                { Constants.TrackEvents.NavigationReferenceScheme.CultureInfo, Constants.CurrentCultureInfo.Name },
                { Constants.TrackEvents.NavigationReferenceScheme.HymnalVersion, preferencesService.ConfiguratedHymnalLanguage.Id }
            });
        }

        private void SelectedAmbitExecute(Ambit ambit)
        {
            navigationService.Navigate<ThematicHymnsListViewModel, Ambit>(ambit);
        }
    }
}
