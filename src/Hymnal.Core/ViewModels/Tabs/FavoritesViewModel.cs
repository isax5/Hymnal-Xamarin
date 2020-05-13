using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hymnal.Core.Models;
using Hymnal.Core.Models.Parameter;
using Hymnal.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Realms;

namespace Hymnal.Core.ViewModels
{
    public class FavoritesViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;
        private readonly IDataStorageService dataStorageService;
        private readonly IDialogService dialogService;
        private readonly IHymnsService hymnsService;
        private readonly Realm realm;

        // TODO: Add inteaction for cells: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/listview/interactivity#context-actions
        public MvxObservableCollection<Hymn> Hymns { get; set; } = new MvxObservableCollection<Hymn>();
        private List<FavoriteHymn> favoriteHymns = new List<FavoriteHymn>();

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
            IDialogService dialogService,
            IHymnsService hymnsService
            )
        {
            this.navigationService = navigationService;
            this.dataStorageService = dataStorageService;
            this.dialogService = dialogService;
            this.hymnsService = hymnsService;
            realm = Realm.GetInstance();
        }

        public override async Task Initialize()
        {
            favoriteHymns.AddRange(realm.All<FavoriteHymn>().OrderByDescending(f => f.SavedAt).ToList());

            Hymn[] hymns = await Task.WhenAll(favoriteHymns.Select(f => hymnsService.GetHymnAsync(f)));

            Hymns.AddRange(hymns);

            await base.Initialize();
        }

        public override async void ViewAppearing()
        {
            base.ViewAppearing();
            /*
            // Update list
            //IOrderedEnumerable<Hymn> favorites = dataStorageService.GetItems<FavoriteHymn>().OrderByDescending(h => h.SavedAt);
            Hymn[] favorites = await Task.WhenAll(favoriteHymns.Select(f => hymnsService.GetHymnAsync(f)));

            // If there weren't hymns in the list before
            if (Hymns.Count() == 0)
            {
                Hymns.AddRange(favorites);
                return;
            }

            // Add new Hymns
            foreach (Hymn item in favorites.Where(
                h1 => Hymns.Where(h2 => h2.Number == h1.Number && h2.HymnalLanguageId.Equals(h1.HymnalLanguageId)).Count() == 0))
            {
                // if item doesn't exist in Hymns

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
            */
        }

        private void SelectedHymnExecute(FavoriteHymn hymn)
        {
            //navigationService.Navigate<HymnViewModel, HymnIdParameter>(new HymnIdParameter
            //{
            //    Number = hymn.Number,
            //    HymnalLanguage = hymn.HymnalLanguage
            //});
        }

        public MvxCommand<FavoriteHymn> DeleteHymnCommand => new MvxCommand<FavoriteHymn>(DeleteHymnExecute);
        private void DeleteHymnExecute(FavoriteHymn favoriteHymn)
        {
            //Hymns.Remove(favoriteHymn);

            //List<FavoriteHymn> favorites = dataStorageService.GetItems<FavoriteHymn>();
            //favorites.RemoveAll(h => h.Number == favoriteHymn.Number && h.HymnalLanguage.Equals(favoriteHymn.HymnalLanguage));
            //dataStorageService.ReplaceItems(favorites);
        }
    }
}
