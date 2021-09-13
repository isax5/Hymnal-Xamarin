using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Hymnal.AzureFunctions.Client;
using Hymnal.AzureFunctions.Extensions;
using Hymnal.XF.Constants;
using Hymnal.XF.Extensions;
using Hymnal.XF.Models;
using Hymnal.XF.Models.Parameters;
using Hymnal.XF.Models.Realm;
using Hymnal.XF.Resources.Languages;
using Hymnal.XF.Services;
using Hymnal.XF.Views;
using MediaManager;
using MediaManager.Library;
using MediaManager.Player;
using Microsoft.AppCenter.Analytics;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Essentials;
using Xamarin.Essentials.Interfaces;

namespace Hymnal.XF.ViewModels
{
    public sealed class HymnViewModel : BaseViewModelParameter<HymnIdParameter>
    {
        private readonly IHymnsService hymnsService;
        private readonly IPreferencesService preferencesService;
        private readonly IMediaManager mediaManager;
        private readonly IPageDialogService dialogService;
        private readonly IStorageManagerService storageService;
        private readonly IAzureHymnService azureHymnService;
        private readonly IMainThread mainThread;
        private readonly IConnectivity connectivity;
        private readonly IShare share;

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

        private HymnIdParameter hymnParameter;

        public HymnIdParameter HymnParameter
        {
            get => hymnParameter;
            set => SetProperty(ref hymnParameter, value);
        }

        public bool ShowBackgroundImageInHymnal => preferencesService.ShowBackgroundImageInHymnal;

        #endregion

        #region Commands

        public DelegateCommand FavoriteCommand { get; private set; }
        public DelegateCommand ShareCommand { get; private set; }
        public DelegateCommand PlayCommand { get; private set; }
        public DelegateCommand CloseCommand { get; private set; }
        public DelegateCommand OpenSheetCommand { get; private set; }

        #endregion

        public HymnViewModel(
            INavigationService navigationService,
            IHymnsService hymnsService,
            IPreferencesService preferencesService,
            IMediaManager mediaManager,
            IPageDialogService dialogService,
            IStorageManagerService storageService,
            IAzureHymnService azureHymnService,
            IMainThread mainThread,
            IConnectivity connectivity,
            IShare share
        ) : base(navigationService)
        {
            this.hymnsService = hymnsService;
            this.preferencesService = preferencesService;
            this.mediaManager = mediaManager;
            this.dialogService = dialogService;
            this.storageService = storageService;
            this.azureHymnService = azureHymnService;
            this.mainThread = mainThread;
            this.connectivity = connectivity;
            this.share = share;

            mediaManager.StateChanged += MediaManager_StateChanged;

            OpenSheetCommand = new DelegateCommand(OpenSheetAsync).ObservesCanExecute(() => NotBusy);
            FavoriteCommand = new DelegateCommand(FavoriteExecute).ObservesCanExecute(() => NotBusy);
            ShareCommand = new DelegateCommand(ShareExecute).ObservesCanExecute(() => NotBusy);
            PlayCommand = new DelegateCommand(PlayExecuteAsync).ObservesCanExecute(() => NotBusy);
            CloseCommand = new DelegateCommand(CloseAsync).ObservesCanExecute(() => NotBusy);
        }

        ~HymnViewModel()
        {
            mediaManager.StateChanged -= MediaManager_StateChanged;
        }

        public override async Task InitializeAsync(INavigationParameters parameters, HymnIdParameter parameter)
        {
            await base.InitializeAsync(parameters, parameter);

            HymnParameter = parameter;
            Language = HymnParameter.HymnalLanguage;

            try
            {
                Hymn = await hymnsService.GetHymnAsync(HymnParameter.Number, HymnParameter.HymnalLanguage);
            }
            catch (Exception ex)
            {
                ex.Report();
            }

            IsPlaying = mediaManager.IsPlaying();

            // Is Favorite
            IsFavorite = storageService.All<FavoriteHymn>().ToList()
                .Exists(f => f.Number == Hymn.Number && f.HymnalLanguageId.Equals(Language.Id));

            // Record
            if (HymnParameter.SaveInRecords)
            {
                IQueryable<RecordHymn> records = storageService.All<RecordHymn>()
                    .Where(h => h.Number == Hymn.Number && h.HymnalLanguageId.Equals(Language.Id));
                storageService.RemoveRange(records);

                storageService.Add(Hymn.ToRecordHymn());
            }
        }

        public override void OnAppearing()
        {
            base.OnAppearing();

            // Precarga de datos para reproduccion
            azureHymnService.ObserveSettings().Subscribe(x => { }, ex => { });

            Analytics.TrackEvent(TrackingConstants.TrackEv.HymnOpened,
                new Dictionary<string, string>
                {
                    { TrackingConstants.TrackEv.HymnReferenceScheme.Number, Hymn.Number.ToString() },
                    { TrackingConstants.TrackEv.HymnReferenceScheme.HymnalVersion, Language.Id },
                    {
                        TrackingConstants.TrackEv.HymnReferenceScheme.CultureInfo,
                        InfoConstants.CurrentCultureInfo.Name
                    },
                    {
                        TrackingConstants.TrackEv.HymnReferenceScheme.Time,
                        DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture)
                    },
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

        #region Command Actions

        private async void OpenSheetAsync()
        {
            await NavigationService.NavigateAsync(nameof(MusicSheetPage), HymnParameter);
        }

        private void FavoriteExecute()
        {
            IQueryable<FavoriteHymn> favorites = storageService.All<FavoriteHymn>()
                .Where(f => f.Number == Hymn.Number && f.HymnalLanguageId.Equals(Language.Id));

            if (IsFavorite || favorites.Count() > 0)
            {
                storageService.RemoveRange(favorites);

                Analytics.TrackEvent(TrackingConstants.TrackEv.HymnRemoveFromFavorites,
                    new Dictionary<string, string>
                    {
                        { TrackingConstants.TrackEv.HymnReferenceScheme.Number, Hymn.Number.ToString() },
                        { TrackingConstants.TrackEv.HymnReferenceScheme.HymnalVersion, Language.Id },
                        {
                            TrackingConstants.TrackEv.HymnReferenceScheme.CultureInfo,
                            InfoConstants.CurrentCultureInfo.Name
                        },
                        {
                            TrackingConstants.TrackEv.HymnReferenceScheme.Time,
                            DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture)
                        }
                    });
            }
            else
            {
                storageService.Add(Hymn.ToFavoriteHymn());

                Analytics.TrackEvent(TrackingConstants.TrackEv.HymnAddedToFavorites,
                    new Dictionary<string, string>
                    {
                        { TrackingConstants.TrackEv.HymnReferenceScheme.Number, Hymn.Number.ToString() },
                        { TrackingConstants.TrackEv.HymnReferenceScheme.HymnalVersion, Language.Id },
                        {
                            TrackingConstants.TrackEv.HymnReferenceScheme.CultureInfo,
                            InfoConstants.CurrentCultureInfo.Name
                        },
                        {
                            TrackingConstants.TrackEv.HymnReferenceScheme.Time,
                            DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture)
                        }
                    });
            }

            IsFavorite = !IsFavorite;
        }

        private void ShareExecute()
        {
            share.RequestAsync(
                title: hymn.Title,
                text: $"{hymn.Title}\n\n{hymn.Content}\n\n{AppConstants.WebLinks.DeveloperWebSite}");

            Analytics.TrackEvent(TrackingConstants.TrackEv.HymnShared,
                new Dictionary<string, string>
                {
                    { TrackingConstants.TrackEv.HymnReferenceScheme.Number, Hymn.Number.ToString() },
                    { TrackingConstants.TrackEv.HymnReferenceScheme.HymnalVersion, Language.Id },
                    {
                        TrackingConstants.TrackEv.HymnReferenceScheme.CultureInfo,
                        InfoConstants.CurrentCultureInfo.Name
                    },
                    {
                        TrackingConstants.TrackEv.HymnReferenceScheme.Time,
                        DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture)
                    }
                });
        }

        private async void PlayExecuteAsync()
        {
            // Check internet connection
            if (connectivity.NetworkAccess == NetworkAccess.None)
            {
                await dialogService.DisplayAlertAsync(Languages.Error_WeHadAProblem, Languages.NoInternetConnection,
                    Languages.Generic_Ok);
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
                .Subscribe(hymnSettings => mainThread.InvokeOnMainThreadAsync(async () =>
                    {
                        var songUrl = string.Empty;
                        var isPlayingInstrumentalMusic = false;

                        // Choose music
                        if (hymnSettings.InstrumentalMusicUrl != null && hymnSettings.SungMusicUrl != null)
                        {
                            var instrumentalTitle = Languages.Instrumental;
                            var sungTitle = Languages.Sung;

                            var result = await dialogService.DisplayActionSheetAsync(
                                Languages.ChooseYourHymnal, Languages.Generic_Cancel,
                                null, new[] { instrumentalTitle, sungTitle });

                            if (result.Equals(instrumentalTitle))
                            {
                                songUrl = hymnSettings.GetInstrumentUrl(Hymn.Number);
                                isPlayingInstrumentalMusic = true;
                            }
                            else if (result.Equals(sungTitle))
                            {
                                songUrl = hymnSettings.GetSungUrl(hymn.Number);
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
                            isPlayingInstrumentalMusic = hymnSettings.SupportsInstrumentalMusic();
                            songUrl = hymnSettings.SupportsInstrumentalMusic()
                                ? hymnSettings.GetInstrumentUrl(Hymn.Number)
                                : hymnSettings.GetSungUrl(Hymn.Number);
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

                        Analytics.TrackEvent(TrackingConstants.TrackEv.HymnMusicPlayed,
                            new Dictionary<string, string>
                            {
                                { TrackingConstants.TrackEv.HymnReferenceScheme.Number, Hymn.Number.ToString() },
                                { TrackingConstants.TrackEv.HymnReferenceScheme.HymnalVersion, Language.Id },
                                {
                                    TrackingConstants.TrackEv.HymnReferenceScheme.TypeOfMusicPlaying,
                                    isPlayingInstrumentalMusic
                                        ? TrackingConstants.TrackEv.HymnReferenceScheme.InstrumentalMusic
                                        : TrackingConstants.TrackEv.HymnReferenceScheme.SungMusic
                                },
                                {
                                    TrackingConstants.TrackEv.HymnReferenceScheme.CultureInfo,
                                    InfoConstants.CurrentCultureInfo.Name
                                },
                                {
                                    TrackingConstants.TrackEv.HymnReferenceScheme.Time,
                                    DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture)
                                }
                            });
                    }),
                    ex => ex.Report());
        }

        private async void CloseAsync()
        {
            await NavigationService.GoBackAsync();
        }

        #endregion
    }
}
