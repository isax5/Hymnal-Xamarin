using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hymnal.XF.Models;
using Hymnal.XF.Models.Parameter;
using Hymnal.XF.Services;
using Hymnal.StorageModels.Models;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.StorageManager;

namespace Hymnal.XF.ViewModels
{
    public class FavoritesViewModel : MvxViewModel
    {
        private readonly INavigationService navigationService;
        private readonly IDialogService dialogService;
        private readonly IHymnsService hymnsService;
        private readonly IPreferencesService preferencesService;
        private readonly IStorageManager storageManager;

        public MvxObservableCollection<Tuple<FavoriteHymn, Hymn>> Hymns { get; set; } = new MvxObservableCollection<Tuple<FavoriteHymn, Hymn>>();

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


        public FavoritesViewModel(
            INavigationService navigationService,
            IDialogService dialogService,
            IHymnsService hymnsService,
            IPreferencesService preferencesService,
            IStorageManager storageManager
            )
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.hymnsService = hymnsService;
            this.preferencesService = preferencesService;
            this.storageManager = storageManager;
        }

        public override async void ViewAppearing()
        {
            base.ViewAppearing();

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
            foreach (Tuple<FavoriteHymn, Hymn> hymn in favorites.Where(t1 => Hymns.Where(t2 => t2.Item2.Number == t1.Item2.Number && t2.Item2.HymnalLanguageId.Equals(t1.Item2.HymnalLanguageId)).Count() == 0))
            {
                // if hymn doesn't exist in Hymns

                var position = Hymns.Where(t => t.Item1.SavedAt > hymn.Item1.SavedAt).Count();
                Hymns.Insert(position, hymn);
            }

            // Remove no favorites hymns
            var toRemoveList = new List<Tuple<FavoriteHymn, Hymn>>();
            foreach (var hymn in Hymns.Where(t1 => favorites.Where(t2 => t2.Item2.Number == t1.Item2.Number && t2.Item2.HymnalLanguageId.Equals(t1.Item2.HymnalLanguageId)).Count() == 0))
            {
                toRemoveList.Add(hymn);
            }

            foreach (var item in toRemoveList)
                Hymns.Remove(item);
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();

            Analytics.TrackEvent(Constants.TrackEv.Navigation, new Dictionary<string, string>
            {
                { Constants.TrackEv.NavigationReferenceScheme.PageName, nameof(FavoritesViewModel) },
                { Constants.TrackEv.NavigationReferenceScheme.CultureInfo, Constants.CurrentCultureInfo.Name },
                { Constants.TrackEv.NavigationReferenceScheme.HymnalVersion, preferencesService.ConfiguratedHymnalLanguage.Id }
            });
        }

        private async Task SelectedHymnExecuteAsync(Tuple<FavoriteHymn, Hymn> hymn)
        {
            await navigationService.Navigate<HymnViewModel, HymnIdParameter>(new HymnIdParameter
            {
                Number = hymn.Item2.Number,
                HymnalLanguage = HymnalLanguage.GetHymnalLanguageWithId(hymn.Item2.HymnalLanguageId)
            });
        }

        public MvxCommand<Tuple<FavoriteHymn, Hymn>> DeleteHymnCommand => new MvxCommand<Tuple<FavoriteHymn, Hymn>>(DeleteHymnExecute);
        private void DeleteHymnExecute(Tuple<FavoriteHymn, Hymn> favoriteHymn)
        {
            Analytics.TrackEvent(Constants.TrackEv.HymnRemoveFromFavorites, new Dictionary<string, string>
            {
                { Constants.TrackEv.HymnReferenceScheme.Number, favoriteHymn.Item1.Number.ToString() },
                { Constants.TrackEv.HymnReferenceScheme.HymnalVersion, favoriteHymn.Item1.HymnalLanguageId },
                { Constants.TrackEv.HymnReferenceScheme.CultureInfo, Constants.CurrentCultureInfo.Name },
                { Constants.TrackEv.HymnReferenceScheme.Time, DateTime.Now.ToLocalTime().ToString() }
            });

            try
            {
                Hymns.Remove(favoriteHymn);
                storageManager.Remove(favoriteHymn.Item1);
            }
            catch (Exception ex)
            {
                var properties = new Dictionary<string, string>()
                    {
                        { "File", nameof(FavoritesViewModel) },
                        { "Deleting Favorite", favoriteHymn.Item1.Number.ToString() }
                    };

                Crashes.TrackError(ex, properties);
            }
        }
    }
}
