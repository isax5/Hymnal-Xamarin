using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hymnal.Core.Models;
using Hymnal.Core.Models.Parameter;
using Hymnal.Core.Services;
using Hymnal.StorageModels.Models;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.StorageManager;

namespace Hymnal.Core.ViewModels
{
    public class FavoritesViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;
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

                SelectedHymnExecute(value);
                RaisePropertyChanged(nameof(SelectedHymn));
            }
        }


        public FavoritesViewModel(
            IMvxNavigationService navigationService,
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
            var favorites = await Task.WhenAll(
                storageManager.All<FavoriteHymn>().OrderByDescending(f => f.SavedAt).ToList()
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

            Analytics.TrackEvent(Constants.TrackEvents.Navigation, new Dictionary<string, string>
            {
                { Constants.TrackEvents.NavigationReferenceScheme.PageName, nameof(FavoritesViewModel) },
                { Constants.TrackEvents.NavigationReferenceScheme.CultureInfo, Constants.CurrentCultureInfo.Name },
                { Constants.TrackEvents.NavigationReferenceScheme.HymnalVersion, preferencesService.ConfiguratedHymnalLanguage.Id }
            });
        }

        private void SelectedHymnExecute(Tuple<FavoriteHymn, Hymn> hymn)
        {
            navigationService.Navigate<HymnViewModel, HymnIdParameter>(new HymnIdParameter
            {
                Number = hymn.Item2.Number,
                HymnalLanguage = HymnalLanguage.GetHymnalLanguageWithId(hymn.Item2.HymnalLanguageId)
            });
        }

        public MvxCommand<Tuple<FavoriteHymn, Hymn>> DeleteHymnCommand => new MvxCommand<Tuple<FavoriteHymn, Hymn>>(DeleteHymnExecute);
        private void DeleteHymnExecute(Tuple<FavoriteHymn, Hymn> favoriteHymn)
        {
            Analytics.TrackEvent(Constants.TrackEvents.HymnRemoveFromFavorites, new Dictionary<string, string>
            {
                { Constants.TrackEvents.HymnReferenceScheme.Number, favoriteHymn.Item1.Number.ToString() },
                { Constants.TrackEvents.HymnReferenceScheme.HymnalVersion, favoriteHymn.Item1.HymnalLanguageId },
                { Constants.TrackEvents.HymnReferenceScheme.CultureInfo, Constants.CurrentCultureInfo.Name },
                { Constants.TrackEvents.HymnReferenceScheme.Time, DateTime.Now.ToLocalTime().ToString() }
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
