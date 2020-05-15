using System;
using System.Linq;
using System.Threading.Tasks;
using Hymnal.Core.Extensions;
using Hymnal.Core.Helpers;
using Hymnal.Core.Models;
using Hymnal.Core.Models.Parameter;
using Hymnal.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Realms;

namespace Hymnal.Core.ViewModels
{
    public class HymnViewModel : MvxViewModel<HymnIdParameter>
    {
        private readonly IMvxNavigationService navigationService;
        private readonly IHymnsService hymnsService;
        private readonly IPreferencesService preferencesService;
        private readonly IMediaService mediaService;
        private readonly IConnectivityService connectivityService;
        private readonly IDialogService dialogService;
        private readonly IShareService shareService;
        private readonly Realm realm;

        public int HymnTitleFontSize => preferencesService.HymnalsFontSize + 10;
        public int HymnFontSize => preferencesService.HymnalsFontSize;

        private Hymn hymn;
        public Hymn Hymn
        {
            get => hymn;
            set => SetProperty(ref hymn, value);
        }

        private HymnalLanguage language;
        public HymnalLanguage Language
        {
            get => language;
            set => SetProperty(ref language, value);
        }

        private bool isFavorite;
        public bool IsFavorite
        {
            get => isFavorite;
            set => SetProperty(ref isFavorite, value);
        }

        private bool isPlaying;
        public bool IsPlaying
        {
            get => isPlaying;
            set => SetProperty(ref isPlaying, value);
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
            IPreferencesService preferencesService,
            IMediaService mediaService,
            IConnectivityService connectivityService,
            IDialogService dialogService,
            IShareService shareService
            )
        {
            this.navigationService = navigationService;
            this.hymnsService = hymnsService;
            this.preferencesService = preferencesService;
            this.mediaService = mediaService;
            this.connectivityService = connectivityService;
            this.dialogService = dialogService;
            this.shareService = shareService;
            realm = Realm.GetInstance();
            this.mediaService.Playing += MediaService_Playing;
            this.mediaService.Stopped += MediaService_Stopped;
            this.mediaService.EndReached += MediaService_EndReached;
        }

        ~HymnViewModel()
        {
            mediaService.Playing -= MediaService_Playing;
            mediaService.Stopped -= MediaService_Stopped;
            mediaService.EndReached -= MediaService_EndReached;
        }

        public override void Prepare(HymnIdParameter parameter)
        {
            HymnParameter = parameter;
            Language = HymnParameter.HymnalLanguage;
        }

        public override async Task Initialize()
        {
            // TODO: Check for any crash
            Hymn = await hymnsService.GetHymnAsync(HymnParameter.Number, HymnParameter.HymnalLanguage);

            IsPlaying = mediaService.IsPlaying;

            // Is Favorite
            IsFavorite = realm.All<FavoriteHymn>().ToList().Exists(f => f.Number == Hymn.Number && f.HymnalLanguageId.Equals(Language.Id));

            // Record
            if (HymnParameter.SaveInRecords)
            {
                IQueryable<RecordHymn> records = realm.All<RecordHymn>().Where(h => h.Number == Hymn.Number && h.HymnalLanguageId.Equals(Language.Id));

                using (Transaction trans = realm.BeginWrite())
                {
                    realm.RemoveRange(records);
                    trans.Commit();
                }

                realm.Write(() => realm.Add(Hymn.ToRecordHymn()));
            }

            await base.Initialize();
        }

        #region Events
        private void MediaService_Playing(object sender, EventArgs e)
        {
            IsPlaying = true;
        }

        private void MediaService_Stopped(object sender, EventArgs e)
        {
            IsPlaying = false;
        }

        private void MediaService_EndReached(object sender, EventArgs e)
        {
            IsPlaying = false;
        }
        #endregion

        #region Commands
        public MvxCommand OpenSheetCommand => new MvxCommand(OpenSheet);
        private void OpenSheet()
        {
            navigationService.Navigate<MusicSheetViewModel, HymnIdParameter>(HymnParameter);
        }

        public MvxCommand FavoriteCommand => new MvxCommand(FavoriteExecute);
        private void FavoriteExecute()
        {
            var favorites = realm.All<FavoriteHymn>().Where(f => f.Number == Hymn.Number && f.HymnalLanguageId.Equals(Language.Id));

            if (IsFavorite || favorites.Count() > 0)
            {
                using (Transaction trans = realm.BeginWrite())
                {
                    realm.RemoveRange(favorites);
                    trans.Commit();
                }
            }
            else
            {
                realm.Write(() => realm.Add(Hymn.ToFavoriteHymn()));
            }


            IsFavorite = !IsFavorite;
        }

        public MvxCommand ShareCommand => new MvxCommand(ShareExecute);
        private void ShareExecute()
        {
            shareService.Share(
                title: hymn.Title,
                text: $"{hymn.Title}\n\n{hymn.Content}\n\n{Constants.WebLinks.DeveloperWebSite}");
        }

        public MvxCommand PlayCommand => new MvxCommand(PlayExecute);
        private void PlayExecute()
        {
            // Check internet connection
            if (!connectivityService.InternetAccess)
            {
                dialogService.Alert(Languages.WeHadAProblem, Languages.NoInternetConnection, Languages.Ok);
                return;
            }

            // Stop/Start playing
            if (IsPlaying)
            {
                mediaService.Stop();
                // IsPlaying is setted here becouse maybe the internet is not so fast enough and the song can be loading and not put play
                IsPlaying = false;
            }
            else
            {
                if (Language.SupportInstrumentalMusic)
                    mediaService.Play(Language.GetInstrumentURL(Hymn.Number));
                else
                    mediaService.Play(language.GetSungURL(Hymn.Number));

                // IsPlaying is setted here becouse maybe the internet is not so fast enough and the song can be loading and not to put play from the first moment
                IsPlaying = true;
            }
        }

        public MvxCommand CloseCommand => new MvxCommand(Close);
        private void Close()
        {
            navigationService.Close(this);
        }
        #endregion
    }
}
