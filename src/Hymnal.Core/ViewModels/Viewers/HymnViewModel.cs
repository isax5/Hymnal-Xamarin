using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Hymnal.AzureFunctions.Client;
using Hymnal.AzureFunctions.Extensions;
using Hymnal.Core.Extensions;
using Hymnal.Core.Helpers;
using Hymnal.Core.Models;
using Hymnal.Core.Models.Parameter;
using Hymnal.Core.Services;
using Hymnal.StorageModels.Models;
using MediaManager;
using MediaManager.Library;
using MediaManager.Player;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using Plugin.StorageManager;
using Xamarin.Essentials;

namespace Hymnal.Core.ViewModels
{
    public class HymnViewModel : BaseViewModel<HymnIdParameter>
    {
        private readonly IMvxNavigationService navigationService;
        private readonly IMvxLog log;
        private readonly IHymnsService hymnsService;
        private readonly IPreferencesService preferencesService;
        private readonly IMediaManager mediaManager;
        private readonly IDialogService dialogService;
        private readonly IStorageManager storageService;
        private readonly IAzureHymnService azureHymnService;

        #region Properties
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
        #endregion


        public HymnViewModel(
            IMvxNavigationService navigationService,
            IMvxLog log,
            IHymnsService hymnsService,
            IPreferencesService preferencesService,
            IMediaManager mediaManager,
            IDialogService dialogService,
            IStorageManager storageService,
            IAzureHymnService azureHymnService
            )
        {
            this.navigationService = navigationService;
            this.log = log;
            this.hymnsService = hymnsService;
            this.preferencesService = preferencesService;
            this.mediaManager = mediaManager;
            this.dialogService = dialogService;
            this.storageService = storageService;
            this.azureHymnService = azureHymnService;
            mediaManager.StateChanged += MediaManager_StateChanged;
        }

        ~HymnViewModel()
        {
            mediaManager.StateChanged -= MediaManager_StateChanged;
        }

        public override void Prepare(HymnIdParameter parameter)
        {
            HymnParameter = parameter;
            Language = HymnParameter.HymnalLanguage;
        }

        public override async Task Initialize()
        {
            try
            {
                Hymn = await hymnsService.GetHymnAsync(HymnParameter.Number, HymnParameter.HymnalLanguage);
            }
            catch (Exception ex)
            {
                var properties = new Dictionary<string, string>()
                {
                    { "File", nameof(HymnViewModel) },
                    { "Hymn Number", HymnParameter.Number.ToString() },
                    { "Hymn Version", HymnParameter.HymnalLanguage.Id }
                };

                log.TraceException("Exception opening a hymnal", ex, properties);
                Crashes.TrackError(ex, properties);

                await navigationService.Close(this);
                return;
            }

            IsPlaying = mediaManager.IsPlaying();

            // Is Favorite
            IsFavorite = storageService.All<FavoriteHymn>().ToList().Exists(f => f.Number == Hymn.Number && f.HymnalLanguageId.Equals(Language.Id));

            // Record
            if (HymnParameter.SaveInRecords)
            {

                IQueryable<RecordHymn> records = storageService.All<RecordHymn>().Where(h => h.Number == Hymn.Number && h.HymnalLanguageId.Equals(Language.Id));
                storageService.RemoveRange(records);

                storageService.Add(Hymn.ToRecordHymn());
            }

            await base.Initialize();
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();

            // Precarga de datos para reproduccion
            azureHymnService.ObserveSettings().Subscribe(x => { }, ex => { });

            Debug.WriteLine($"Opening Hymn: {Hymn.Number} of {Language.Id}");

            Analytics.TrackEvent(Constants.TrackEv.HymnOpened, new Dictionary<string, string>
            {
                { Constants.TrackEv.HymnReferenceScheme.Number, Hymn.Number.ToString() },
                { Constants.TrackEv.HymnReferenceScheme.HymnalVersion, Language.Id },
                { Constants.TrackEv.HymnReferenceScheme.CultureInfo, Constants.CurrentCultureInfo.Name },
                { Constants.TrackEv.HymnReferenceScheme.Time, DateTime.Now.ToLocalTime().ToString() },
                { "Font Size Hymnal", preferencesService.HymnalsFontSize.ToString() }
            });
        }

        #region Events
        private void MediaManager_StateChanged(object sender, MediaManager.Playback.StateChangedEventArgs e)
        {
            switch (e.State)
            {
                case MediaPlayerState.Playing:
                case MediaPlayerState.Buffering:
                case MediaPlayerState.Loading:
                    IsPlaying = true;
                    break;

                case MediaPlayerState.Stopped:
                case MediaPlayerState.Failed:
                case MediaPlayerState.Paused:
                    IsPlaying = false;
                    break;

                default:
                    break;
            }
        }
        #endregion

        #region Commands
        public MvxAsyncCommand OpenSheetCommand => new MvxAsyncCommand(OpenSheetAsync);
        private async Task OpenSheetAsync()
        {
            await navigationService.Navigate<MusicSheetViewModel, HymnIdParameter>(HymnParameter);
        }

        public MvxCommand FavoriteCommand => new MvxCommand(FavoriteExecute);
        private void FavoriteExecute()
        {

            var favorites = storageService.All<FavoriteHymn>().Where(f => f.Number == Hymn.Number && f.HymnalLanguageId.Equals(Language.Id));

            if (IsFavorite || favorites.Count() > 0)
            {
                storageService.RemoveRange(favorites);

                Analytics.TrackEvent(Constants.TrackEv.HymnRemoveFromFavorites, new Dictionary<string, string>
                {
                    { Constants.TrackEv.HymnReferenceScheme.Number, Hymn.Number.ToString() },
                    { Constants.TrackEv.HymnReferenceScheme.HymnalVersion, Language.Id },
                    { Constants.TrackEv.HymnReferenceScheme.CultureInfo, Constants.CurrentCultureInfo.Name },
                    { Constants.TrackEv.HymnReferenceScheme.Time, DateTime.Now.ToLocalTime().ToString() }
                });
            }
            else
            {
                storageService.Add(Hymn.ToFavoriteHymn());

                Analytics.TrackEvent(Constants.TrackEv.HymnAddedToFavorites, new Dictionary<string, string>
                {
                    { Constants.TrackEv.HymnReferenceScheme.Number, Hymn.Number.ToString() },
                    { Constants.TrackEv.HymnReferenceScheme.HymnalVersion, Language.Id },
                    { Constants.TrackEv.HymnReferenceScheme.CultureInfo, Constants.CurrentCultureInfo.Name },
                    { Constants.TrackEv.HymnReferenceScheme.Time, DateTime.Now.ToLocalTime().ToString() }
                });
            }

            IsFavorite = !IsFavorite;
        }

        public MvxCommand ShareCommand => new MvxCommand(ShareExecute);
        private void ShareExecute()
        {
            Share.RequestAsync(
                title: hymn.Title,
                text: $"{hymn.Title}\n\n{hymn.Content}\n\n{Constants.WebLinks.DeveloperWebSite}");

            Analytics.TrackEvent(Constants.TrackEv.HymnShared, new Dictionary<string, string>
            {
                { Constants.TrackEv.HymnReferenceScheme.Number, Hymn.Number.ToString() },
                { Constants.TrackEv.HymnReferenceScheme.HymnalVersion, Language.Id },
                { Constants.TrackEv.HymnReferenceScheme.CultureInfo, Constants.CurrentCultureInfo.Name },
                { Constants.TrackEv.HymnReferenceScheme.Time, DateTime.Now.ToLocalTime().ToString() }
            });
        }

        public MvxAsyncCommand PlayCommand => new MvxAsyncCommand(PlayExecuteAsync);
        private async Task PlayExecuteAsync()
        {
            // Check internet connection
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                await dialogService.Alert(Languages.WeHadAProblem, Languages.NoInternetConnection, Languages.Ok);
                return;
            }

            // Stop/Start playing
            if (IsPlaying)
            {
                await mediaManager.Stop();

                // IsPlaying is setted here becouse maybe the internet is not so fast enough and the song can be loading and not put play
                IsPlaying = false;
                return;
            }

            azureHymnService.ObserveSettings()
                .Where(x => x.Id == Language.Id)
                .Subscribe(hymnSettings => InvokeOnMainThreadAsync(async () =>
                {
                    var songUrl = string.Empty;
                    var isPlayingInstrumentalMusic = false;

                    // Choose music
                    if (hymnSettings.InstrumentalMusicUrl != null && hymnSettings.SungMusicUrl != null)
                    {
                        var instrumentalTitle = Languages.Instrumental;
                        var sungTitle = Languages.Sung;

                        var result = await dialogService.DisplayActionSheet(
                            Languages.ChooseYourHymnal, Languages.Cancel,
                            null, new[] { instrumentalTitle, sungTitle });

                        if (result.Equals(instrumentalTitle))
                        {
                            songUrl = hymnSettings.GetInstrumentURL(Hymn.Number);
                            isPlayingInstrumentalMusic = true;
                        }
                        else if (result.Equals(sungTitle))
                        {
                            songUrl = hymnSettings.GetSungURL(hymn.Number);
                            isPlayingInstrumentalMusic = false;
                        }
                        // Tap on "Close"
                        else
                        {
                            return;
                        }
                    }

                    if (string.IsNullOrWhiteSpace(songUrl))
                    {
                        isPlayingInstrumentalMusic = language.SupportInstrumentalMusic;
                        songUrl = language.SupportInstrumentalMusic ? hymnSettings.GetInstrumentURL(Hymn.Number) : hymnSettings.GetSungURL(Hymn.Number);
                    }

                    // IsPlaying is setted here becouse maybe the internet is not so fast enough and the song can be loading and not to put play from the first moment
                    IsPlaying = true;
                    IMediaItem mediaItem = new MediaItem(songUrl) { IsMetadataExtracted = true };
                    mediaItem = await mediaManager.Extractor.UpdateMediaItem(mediaItem).ConfigureAwait(false);
                    mediaItem.DisplayTitle = Hymn.Title;
                    mediaItem.Album = Language.Name;
                    mediaItem.Year = Language.Year;
                    mediaItem.MediaType = MediaType.Audio;

                    await mediaManager.Play(mediaItem);

                    Analytics.TrackEvent(Constants.TrackEv.HymnMusicPlayed, new Dictionary<string, string>
                    {
                        { Constants.TrackEv.HymnReferenceScheme.Number, Hymn.Number.ToString() },
                        { Constants.TrackEv.HymnReferenceScheme.HymnalVersion, Language.Id },
                        { Constants.TrackEv.HymnReferenceScheme.TypeOfMusicPlaying, isPlayingInstrumentalMusic ?
                        Constants.TrackEv.HymnReferenceScheme.InstrumentalMusic : Constants.TrackEv.HymnReferenceScheme.SungMusic },
                        { Constants.TrackEv.HymnReferenceScheme.CultureInfo, Constants.CurrentCultureInfo.Name },
                        { Constants.TrackEv.HymnReferenceScheme.Time, DateTime.Now.ToLocalTime().ToString() }
                    });
                }));

        }

        public MvxAsyncCommand OpenPlayerCommand => new MvxAsyncCommand(OpenPlayerExecuteAsync);
        private async Task OpenPlayerExecuteAsync()
        {
            await navigationService.Close(this);
            await navigationService.Navigate<PlayerViewModel, HymnIdParameter>(HymnParameter);
        }

        public MvxAsyncCommand CloseCommand => new MvxAsyncCommand(CloseAsync);
        private async Task CloseAsync()
        {
            await navigationService.Close(this);
        }
        #endregion
    }
}
