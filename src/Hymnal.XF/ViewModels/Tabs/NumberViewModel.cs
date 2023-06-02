using System.Collections.Generic;
using System.Linq;
using Hymnal.XF.Constants;
using Hymnal.XF.Extensions;
using Hymnal.XF.Models;
using Hymnal.XF.Models.Parameters;
using Hymnal.XF.Services;
using Microsoft.AppCenter.Analytics;
using Prism.Commands;
using Prism.Navigation;

namespace Hymnal.XF.ViewModels
{
    public sealed class NumberViewModel : BaseViewModel
    {
        private readonly IHymnsService hymnsService;
        private readonly IPreferencesService preferencesService;

        private string hymnNumber;
        public string HymnNumber
        {
            get => hymnNumber;
            set => SetProperty(ref hymnNumber, value);
        }

        #region Commands
        public DelegateCommand<string> OpenHymnCommand { get; internal set; }
        public DelegateCommand OpenRecordsCommand { get; internal set; }
        public DelegateCommand OpenSearchCommand { get; internal set; }
        #endregion

        public NumberViewModel(
            INavigationService navigationService,
            IHymnsService hymnsService,
            IPreferencesService preferencesService
            ) : base(navigationService)
        {
            this.hymnsService = hymnsService;
            this.preferencesService = preferencesService;
            OpenHymnCommand = new DelegateCommand<string>(OpenHymnAsync).ObservesCanExecute(() => NotBusy);
            OpenRecordsCommand = new DelegateCommand(OpenRecordsAsync).ObservesCanExecute(() => NotBusy);
            OpenSearchCommand = new DelegateCommand(OpenSearchAsync).ObservesCanExecute(() => NotBusy);
#if DEBUG
            // A long hymn
            HymnNumber = $"{1}";
#endif
        }

        public override void OnAppearing()
        {
            base.OnAppearing();

            Analytics.TrackEvent(TrackingConstants.TrackEv.Navigation, new Dictionary<string, string>
            {
                { TrackingConstants.TrackEv.NavigationReferenceScheme.PageName, nameof(NumberViewModel) },
                { TrackingConstants.TrackEv.NavigationReferenceScheme.CultureInfo, InfoConstants.CurrentCultureInfo.Name },
                { TrackingConstants.TrackEv.NavigationReferenceScheme.HymnalVersion, preferencesService.ConfiguredHymnalLanguage.Id }
            });
        }

        #region Command Actions
        private async void OpenHymnAsync(string text)
        {
            Busy = true;
            var num = text ?? HymnNumber;

            HymnalLanguage language = preferencesService.ConfiguredHymnalLanguage;
            IEnumerable<Hymn> hymns = await hymnsService.GetHymnListAsync(language);

            if (int.TryParse(num, out var number))
            {
                if (number < 1 || number > hymns.Count())
                {
                    Busy = false;
                    return;
                }

                //await NavigationService.NavigateAsync(
                // This is for android
                await RootPageNavigationService?.NavigateAsync(
                    //NavRoutes.HymnViewerAsFormSheetModal,
                    NavRoutes.HymnPage,
                    new HymnIdParameter
                    {
                        Number = number,
                        HymnalLanguage = language
                    },
                    false, true);
                //true, true);
            }
            Busy = false;
        }

        private async void OpenRecordsAsync()
        {
            //await NavigationService.NavigateAsync(NavRoutes.RecordsPageAsFormSheetModal, true, true);
            // This is for android
            await RootPageNavigationService?.NavigateAsync(NavRoutes.RecordsPage, false, true);
        }

        private async void OpenSearchAsync()
        {
            //await NavigationService.NavigateAsync(NavRoutes.SearchPageAsFormSheetModal, true, true);
            // This is for android
            await RootPageNavigationService?.NavigateAsync(NavRoutes.SearchPage, false, true);
        }
        #endregion
    }
}
