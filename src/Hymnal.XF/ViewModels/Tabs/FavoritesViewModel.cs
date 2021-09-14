using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hymnal.XF.Constants;
using Hymnal.XF.Extensions;
using Hymnal.XF.Models;
using Hymnal.XF.Models.Parameters;
using Hymnal.XF.Models.Realm;
using Hymnal.XF.Services;
using Microsoft.AppCenter.Analytics;
using MvvmHelpers;
using Prism.Commands;
using Prism.Navigation;

namespace Hymnal.XF.ViewModels
{
    public sealed class FavoritesViewModel : BaseViewModel
    {
        private readonly IHymnsService hymnsService;
        private readonly IStorageManagerService storageManager;
        private readonly IPreferencesService preferencesService;

        public ObservableRangeCollection<Tuple<FavoriteHymn, Hymn>> Hymns { get; } = new();

        public Tuple<FavoriteHymn, Hymn> SelectedHymn
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

        #region Commands
        public DelegateCommand<Tuple<FavoriteHymn, Hymn>> DeleteHymnCommand { get; internal set; }
        #endregion

        public FavoritesViewModel(
            INavigationService navigationService,
            IHymnsService hymnsService,
            IStorageManagerService storageManager,
            IPreferencesService preferencesService
            ) : base(navigationService)
        {
            this.hymnsService = hymnsService;
            this.storageManager = storageManager;
            this.preferencesService = preferencesService;

            DeleteHymnCommand = new DelegateCommand<Tuple<FavoriteHymn, Hymn>>(DeleteHymnExecute).ObservesCanExecute(() => NotBusy);
        }

        public override async void OnAppearing()
        {
            base.OnAppearing();

            // Update list
            Tuple<FavoriteHymn, Hymn>[] favorites = await Task.WhenAll(
                storageManager
                .All<FavoriteHymn>()
                .OrderByDescending(f => f.SavedAt)
                .ToList()
                .Select(async f => new Tuple<FavoriteHymn, Hymn>(f, await hymnsService.GetHymnAsync(f))));

            // If there weren't hymns in the list before
            if (Hymns.Count() == 0)
            {
                Hymns.AddRange(favorites);
                return;
            }

            // Add new Hymns
            foreach (Tuple<FavoriteHymn, Hymn> hymn in
                favorites.Where(t1 => Hymns
                    .Where(t2 => t2.Item2.Number == t1.Item2.Number && t2.Item2.HymnalLanguageId.Equals(t1.Item2.HymnalLanguageId)).Count() == 0))
            {
                // if hymn doesn't exist in Hymns

                var position = Hymns.Where(t => t.Item1.SavedAt > hymn.Item1.SavedAt).Count();
                Hymns.Insert(position, hymn);
            }

            // Remove no favorites hymns
            var toRemoveList = new List<Tuple<FavoriteHymn, Hymn>>();
            foreach (Tuple<FavoriteHymn, Hymn> hymn in
                Hymns.Where(t1 => favorites
                    .Where(t2 => t2.Item2.Number == t1.Item2.Number && t2.Item2.HymnalLanguageId.Equals(t1.Item2.HymnalLanguageId)).Count() == 0))
            {
                toRemoveList.Add(hymn);
            }

            foreach (Tuple<FavoriteHymn, Hymn> item in toRemoveList)
                Hymns.Remove(item);

            Analytics.TrackEvent(TrackingConstants.TrackEv.Navigation, new Dictionary<string, string>
            {
                { TrackingConstants.TrackEv.NavigationReferenceScheme.PageName, nameof(FavoritesViewModel) },
                { TrackingConstants.TrackEv.NavigationReferenceScheme.CultureInfo, InfoConstants.CurrentCultureInfo.Name },
                { TrackingConstants.TrackEv.NavigationReferenceScheme.HymnalVersion, preferencesService.ConfiguredHymnalLanguage.Id }
            });
        }

        #region Command Actions
        private async Task SelectedHymnExecuteAsync(Tuple<FavoriteHymn, Hymn> hymn)
        {
            await NavigationService.NavigateAsync(
                NavRoutes.HymnViewerAsModal,
                new HymnIdParameter
                {
                    Number = hymn.Item2.Number,
                    HymnalLanguage = HymnalLanguage.GetHymnalLanguageWithId(hymn.Item2.HymnalLanguageId)
                }, true, true);
        }

        private void DeleteHymnExecute(Tuple<FavoriteHymn, Hymn> favoriteHymn)
        {
            Analytics.TrackEvent(TrackingConstants.TrackEv.HymnRemoveFromFavorites, new Dictionary<string, string>
            {
                { TrackingConstants.TrackEv.HymnReferenceScheme.Number, favoriteHymn.Item1.Number.ToString() },
                { TrackingConstants.TrackEv.HymnReferenceScheme.HymnalVersion, favoriteHymn.Item1.HymnalLanguageId },
                { TrackingConstants.TrackEv.HymnReferenceScheme.CultureInfo, InfoConstants.CurrentCultureInfo.Name },
                { TrackingConstants.TrackEv.HymnReferenceScheme.Time, DateTime.Now.ToLocalTime().ToString() }
            });

            Busy = true;
            try
            {
                Hymns.Remove(favoriteHymn);
                storageManager.Remove(favoriteHymn.Item1);
            }
            catch (Exception ex)
            {
                ex.Report(new Dictionary<string, string>()
                    {
                        { "File", nameof(FavoritesViewModel) },
                        { "Deleting Favorite", favoriteHymn.Item1.Number.ToString() }
                    });
            }
            finally
            {
                Busy = false;
            }
        }
        #endregion
    }
}
