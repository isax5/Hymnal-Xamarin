using System;
using Hymnal.Core.Models;
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


        public ThematicSubGroupViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public override void Prepare(Thematic parameter)
        {
            Thematic = parameter;
        }


        private void SelectedAmbitExecute(Ambit ambit)
        {
            navigationService.Navigate<ThematicHymnsListViewModel, Ambit>(ambit);
        }
    }
}
