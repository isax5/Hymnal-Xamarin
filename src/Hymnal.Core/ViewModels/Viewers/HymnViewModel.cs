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
    public class HymnViewModel : MvxViewModel<HymnId>
    {
        private readonly IMvxNavigationService navigationService;
        private readonly IHymnsService hymnsService;
        private readonly IDataStorageService dataStorageService;
        private Hymn hymn;
        public Hymn Hymn
        {
            get => hymn;
            set => SetProperty(ref hymn, value);
        }

        private bool isFavorite;
        public bool IsFavorite
        {
            get => isFavorite;
            set => SetProperty(ref isFavorite, value);
        }

        public MvxObservableCollection<string> Lyric { get; set; } = new MvxObservableCollection<string>();

        private HymnId hymnId;
        public HymnId HymnId
        {
            get => hymnId;
            set => SetProperty(ref hymnId, value);
        }


        public HymnViewModel(
            IMvxNavigationService navigationService,
            IHymnsService hymnsService,
            IDataStorageService dataStorageService)
        {
            this.navigationService = navigationService;
            this.hymnsService = hymnsService;
            this.dataStorageService = dataStorageService;
        }

        public override void Prepare(HymnId parameter)
        {
            HymnId = parameter;
        }

        public override async Task Initialize()
        {
            Hymn = await hymnsService.GetHymnAsync(hymnId.Number);
            Lyric.AddRange(Hymn.Content.Split('¶').Select(ss => ss.Trim()));

            // Is Favorite
            IsFavorite = dataStorageService.GetItems<FavoriteHymn>().Exists(h => h.Number == Hymn.Number);

            // History
            if (HymnId.SaveInHistory)
            {
                List<HistoryHymn> history = dataStorageService.GetItems<HistoryHymn>();

                // was this hymn in the history?
                history.RemoveAll(h => h.Number == Hymn.Number);

                // add new in History
                history.Add(Hymn.ToHistoryHymn());

                history = history.OrderByDescending(h => h.SavedAt).ToList();

                // remove over history
                if (history.Count > Constants.MAXIMUM_RECORDS)
                    history = history.GetRange(0, Constants.MAXIMUM_RECORDS);

                // save history
                dataStorageService.ReplaceItems(history);
            }

            await base.Initialize();
        }


        #region Commands
        public MvxCommand OpenSheetCommand => new MvxCommand(OpenSheet);
        private void OpenSheet()
        {
            navigationService.Navigate<MusicSheetViewModel, HymnId>(HymnId);
        }

        public MvxCommand FavoriteCommand => new MvxCommand(FavoriteExecute);
        private void FavoriteExecute()
        {
            List<FavoriteHymn> favorites = dataStorageService.GetItems<FavoriteHymn>();

            if (IsFavorite)
                favorites.RemoveAll(h => h.Number == HymnId.Number);
            else
                favorites.Add(Hymn.ToFavoriteHymn());

            dataStorageService.ReplaceItems(favorites);

            IsFavorite = !IsFavorite;
        }

        public MvxCommand CloseCommand => new MvxCommand(Close);
        private void Close()
        {
            navigationService.Close(this);
        }
        #endregion
    }
}
