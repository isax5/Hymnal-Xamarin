using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hymnal.Core.Models;
using Hymnal.Core.Models.Parameter;
using Hymnal.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class FavoritesViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;
        private readonly IDataStorageService dataStorageService;
        private readonly IDialogService dialogService;

        // TODO: Add inteaction for cells: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/listview/interactivity#context-actions
        public MvxObservableCollection<FavoriteHymn> Hymns { get; set; } = new MvxObservableCollection<FavoriteHymn>();

        public FavoriteHymn SelectedHymn
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
            IDataStorageService dataStorageService,
            IDialogService dialogService
            )
        {
            this.navigationService = navigationService;
            this.dataStorageService = dataStorageService;
            this.dialogService = dialogService;
        }

        public override Task Initialize()
        {
            Hymns.AddRange(dataStorageService.GetItems<FavoriteHymn>().OrderByDescending(h => h.SavedAt));
            return base.Initialize();
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();

            // Update list
            IOrderedEnumerable<FavoriteHymn> favorites = dataStorageService.GetItems<FavoriteHymn>().OrderByDescending(h => h.SavedAt);

            // If there weren't hymns in the list before
            if (Hymns.Count() == 0)
            {
                Hymns.AddRange(favorites);
                return;
            }

            // Add new Hymns
            foreach (FavoriteHymn item in favorites.Where(
                h1 => Hymns.Where(h2 => h2.Number == h1.Number && h2.HymnalLanguage.Equals(h1.HymnalLanguage)).Count() == 0))
            {
                var position = Hymns.Where(h => h.SavedAt > item.SavedAt).Count();
                Hymns.Insert(position, item);
            }

            // Remove no favorites hymns
            var removeList = new List<FavoriteHymn>();
            foreach (FavoriteHymn item in Hymns.Where(
                h1 => favorites.Where(h2 => h2.Number == h1.Number && h2.HymnalLanguage.Equals(h1.HymnalLanguage)).Count() == 0))
            {
                removeList.Add(item);
            }

            foreach (FavoriteHymn item in removeList)
                Hymns.Remove(item);
        }

        private void SelectedHymnExecute(FavoriteHymn hymn)
        {
            navigationService.Navigate<HymnViewModel, HymnIdParameter>(new HymnIdParameter
            {
                Number = hymn.Number,
                HymnalLanguage = hymn.HymnalLanguage
            });
        }

        public MvxCommand<FavoriteHymn> DeleteHymnCommand => new MvxCommand<FavoriteHymn>(DeleteHymnExecute);
        private void DeleteHymnExecute(FavoriteHymn favoriteHymn)
        {
            //dialogService.Alert("Cat", $"bla bla\n{favoriteHymn.Title}", "Ok");
            Hymns.Remove(favoriteHymn);

            List<FavoriteHymn> favorites = dataStorageService.GetItems<FavoriteHymn>();
            favorites.RemoveAll(h => h.Number == favoriteHymn.Number && h.HymnalLanguage.Equals(favoriteHymn.HymnalLanguage));
            dataStorageService.ReplaceItems(favorites);
        }
    }
}
