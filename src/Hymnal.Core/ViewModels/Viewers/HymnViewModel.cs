using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hymnal.Core.Extensions;
using Hymnal.Core.Models;
using Hymnal.Core.Models.Parameter;
using Hymnal.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hymnal.Core.ViewModels
{
    public class HymnViewModel : MvxViewModel<HymnIdParameter>
    {
        private readonly IMvxNavigationService navigationService;
        private readonly IHymnsService hymnsService;
        private readonly IDataStorageService dataStorageService;
        private readonly IPreferencesService preferencesService;
        private readonly IMediaService mediaService;

        public int HymnTitleFontSize => preferencesService.HymnalsFontSize + 10;
        public int HymnFontSize => preferencesService.HymnalsFontSize;

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

        private HymnIdParameter hymnId;
        public HymnIdParameter HymnParameter
        {
            get => hymnId;
            set => SetProperty(ref hymnId, value);
        }


        public HymnViewModel(
            IMvxNavigationService navigationService,
            IHymnsService hymnsService,
            IDataStorageService dataStorageService,
            IPreferencesService preferencesService,
            IMediaService mediaService
            )
        {
            this.navigationService = navigationService;
            this.hymnsService = hymnsService;
            this.dataStorageService = dataStorageService;
            this.preferencesService = preferencesService;
            this.mediaService = mediaService;
        }

        public override void Prepare(HymnIdParameter parameter)
        {
            HymnParameter = parameter;
        }

        public override async Task Initialize()
        {
            Hymn = await hymnsService.GetHymnAsync(HymnParameter.Number, HymnParameter.HymnalLanguage);

            // Is Favorite
            IsFavorite = dataStorageService.GetItems<FavoriteHymn>().Exists(h => h.Number == Hymn.Number);

            // History
            if (HymnParameter.SaveInHistory)
            {
                List<HistoryHymn> history = dataStorageService.GetItems<HistoryHymn>();

                // was this hymn in the history?
                history.RemoveAll(h => h.Number == Hymn.Number);

                // add new in History
                history.Add(Hymn.ToHistoryHymn(HymnParameter.HymnalLanguage));

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
            navigationService.Navigate<MusicSheetViewModel, HymnIdParameter>(HymnParameter);
        }

        public MvxCommand FavoriteCommand => new MvxCommand(FavoriteExecute);
        private void FavoriteExecute()
        {
            List<FavoriteHymn> favorites = dataStorageService.GetItems<FavoriteHymn>();

            if (IsFavorite)
                favorites.RemoveAll(h => h.Number == HymnParameter.Number);
            else
                favorites.Add(Hymn.ToFavoriteHymn(HymnParameter.HymnalLanguage));

            dataStorageService.ReplaceItems(favorites);

            IsFavorite = !IsFavorite;
        }

        public MvxCommand PlayCommand => new MvxCommand(PlayExecute);
        private void PlayExecute()
        {
            mediaService.Play(@"https://00e9e64bacbacb95d83abca6c8ba7ed87b318a84a2270330ae-apidata.googleusercontent.com/download/storage/v1/b/hymnals-music/o/es%2Fsung%2F001.mp3?qk=AD5uMEs8h7CKehS--pwwozyqH1XsrhVEMEYjoUGgFpjF6aP83nZecx6sCQwm1aCOBBpfkpBlEDMA1mfrMGbfFyXSdkK2wcjhAD2twL9kxP4QuPiK_NBbJd3mozOmZAPK2_g_2HvQJnh1e6U2JkGb3IOrZEhfnkiA4mFDAGhcnT9kHaX5mS1zI10Fn8MS_ohCFkfLIxFha7w8C1jUxzPHBUNqujFpknGUCuPKC9vBozvnevvUnxDGXH1CAygYoqlkV6nET9YJulAyWnuG8k3f356OOTr7fXlqRvgbUiBVW0_YfSDjC37UwjMhea2CTtRJIRlgUneS_SkRbG3m13-nNrDDbcq25WPzFEqVlunaa_tUsGRE3AXmzU6FkK4CYqVw0NIOC6O-jC9Mw1qliGVfRT9-CTlefbgGEOiDAjOfqNdtYtKtYWDM9jtiQQo7kIcki8VBXkqDQZdkC3HbwU-Xide4RU08EZMsJDJFtgLKkpKuEGiPXvOLA7HeWoz0Jkrq_8QhHklDPdkr4U_udSuehwIIrPw1EbtiKGAlILFGSVTfSJt5vB1ykyFlytnXms7YV2AmE8PghJM3gZgEhKPyQGRFKrL-bq16mQ-4Vk6EKiXxDdaDYFKsqjkRVx0KGAV6VLYXKEbMt02snoK5tccivWn8QL5fPwJUjc84dWA1qDBeigkl765LiCQvgyx0bwiwebj8DW6ywAZrnKsr2GCgtZQ_MgKYrKeMmLWqsgsggFlhXWS2Z6TCQiGVGGIKitZxb6IBk6EiaTNYr1UVq2GcPJ9UCR0Vaihv0M9VckX9hvBOCoKBlm_1oe0");
        }

        public MvxCommand CloseCommand => new MvxCommand(Close);
        private void Close()
        {
            navigationService.Close(this);
        }
        #endregion
    }
}
