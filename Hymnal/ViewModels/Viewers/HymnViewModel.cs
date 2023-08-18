using System.Reactive.Concurrency;
using System.Reactive.Linq;
using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using Hymnal.AzureFunctions.Client;
using Hymnal.AzureFunctions.Extensions;
using Hymnal.Models.DataBase;
using Hymnal.Resources.Languages;

namespace Hymnal.ViewModels;

public sealed partial class HymnViewModel : BaseViewModelParameter<HymnIdParameter>
{
    private readonly IConnectivity connectivity;
    private readonly PreferencesService preferencesService;
    private readonly HymnsService hymnsService;
    private readonly DatabaseService databaseService;
    private readonly MediaElement mediaElement;
    private readonly IAzureHymnService azureHymnService;

    #region Properties
    public int HymnTitleFontSize => preferencesService.HymnalsFontSize + 10;
    public int HymnFontSize => preferencesService.HymnalsFontSize;

    //[ObservableProperty]
    //private List<Hymn> carouselHymns;

    [ObservableProperty]
    private Hymn currentHymn;

    [ObservableProperty]
    private HymnalLanguage language;

    [ObservableProperty]
    private bool isFavorite;

    [ObservableProperty]
    private bool isPlaying;

    [ObservableProperty]
    private HymnIdParameter hymnParameter;

    public bool BackgroundImageAppearance => preferencesService.BackgroundImageAppearance;
    #endregion


    public HymnViewModel(
        IConnectivity connectivity,
        PreferencesService preferencesService,
        HymnsService hymnsService,
        DatabaseService databaseService,
        MediaElement mediaElement,
        IAzureHymnService azureHymnService)
    {
        this.connectivity = connectivity;
        this.preferencesService = preferencesService;
        this.hymnsService = hymnsService;
        this.databaseService = databaseService;
        this.mediaElement = mediaElement;
        this.azureHymnService = azureHymnService;

        mediaElement.StateChanged += MediaElementStateChanged;
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        mediaElement.StateChanged -= MediaElementStateChanged;
    }

    public override void Initialize()
    {
        base.Initialize();

        HymnParameter = Parameter;
        Language = HymnParameter.HymnalLanguage;

        hymnsService.GetHymnAsync(HymnParameter.Number, Language)
            .ToObservable()
            .SubscribeOn(new NewThreadScheduler())
            .Subscribe(result => MainThread.BeginInvokeOnMainThread(() => CurrentHymn = result),
            error => error.Report());

        IsPlaying = mediaElement.CurrentState == MediaElementState.Playing;

        // Is Favorite
        UpdateHymnIsFavorite();

        // Record
        if (HymnParameter.SaveInRecords)
        {
            databaseService.FindAsync<RecordHymn>(r => r.Number == HymnParameter.Number && r.HymnalLanguageId == HymnParameter.HymnalLanguage.Id)
                .ToObservable()
                .Subscribe(async reference =>
                {
                    try
                    {
                        if (reference is not null)
                            await databaseService.RemoveAsync(reference);

                        await databaseService.InserAsync(HymnParameter.ToRecordHymn());
                        var count = await databaseService.GetTable<RecordHymn>().CountAsync();

                        if (count > AppConstants.MAXIMUM_RECORDS)
                        {
                            do
                            {
                                RecordHymn toDelete = await databaseService.GetTable<RecordHymn>().OrderBy(r => r.Id).FirstAsync();
                                await databaseService.RemoveAsync(toDelete);
                                count--;
                            } while (count > AppConstants.MAXIMUM_RECORDS);
                        }
                    }
                    catch (Exception ex)
                    {
                        ex.Report();
                    }
                },
                error => error.Report());
        }
    }

    public override void OnAppearing()
    {
        base.OnAppearing();

        // Pre-loading data for playing
        azureHymnService.ObserveSettings().Subscribe(x => { }, ex => { });
    }


    private void MediaElementStateChanged(object sender, MediaStateChangedEventArgs e)
        => IsPlaying = e.NewState is
            MediaElementState.Playing
            or MediaElementState.Buffering
            or MediaElementState.Opening
            or MediaElementState.Paused;

    private void UpdateHymnIsFavorite()
    {
        var hymnNumber = CurrentHymn?.Number ?? HymnParameter.Number;
        databaseService
            .FindAsync<FavoriteHymn>(h => h.HymnalLanguageId == HymnParameter.HymnalLanguage.Id & h.Number == hymnNumber)
            .ToObservable()
            .Subscribe(result => MainThread.BeginInvokeOnMainThread(() => IsFavorite = result is not null),
            error => error.Report());
    }

    //[RelayCommand]
    //private void CarouselViewPositionChanged()
    //{
    //    HymnParameter.Number = CurrentHymn.Number;
    //}

    [RelayCommand]
    private void PreviousHymn()
    {
        hymnsService.GetHymnListAsync(HymnParameter.HymnalLanguage)
            .ToObservable()
            .Finally(UpdateHymnIsFavorite)
            .Subscribe(result => MainThread.BeginInvokeOnMainThread(delegate
            {
                var newHymnIndex = CurrentHymn.Number - 1;

                if (newHymnIndex > 0)
                    CurrentHymn = result[newHymnIndex - 1];

                HymnParameter = new()
                {
                    HymnalLanguage = Parameter.HymnalLanguage,
                    Number = newHymnIndex,
                    SaveInRecords = Parameter.SaveInRecords,
                };
            }), error => error.Report());
    }

    [RelayCommand]
    private void NextHymn()
    {
        hymnsService.GetHymnListAsync(HymnParameter.HymnalLanguage)
            .ToObservable()
            .Finally(UpdateHymnIsFavorite)
            .Subscribe(result => MainThread.BeginInvokeOnMainThread(delegate
            {
                var newHymnIndex = CurrentHymn.Number + 1;

                if (newHymnIndex <= result.Count)
                    CurrentHymn = result[newHymnIndex - 1];

                HymnParameter = new()
                {
                    HymnalLanguage = Parameter.HymnalLanguage,
                    Number = newHymnIndex,
                    SaveInRecords = Parameter.SaveInRecords,
                };
            }), error => error.Report());
    }

    [RelayCommand]
    private async void Play()
    {
        // Check internet connection
        if (connectivity.NetworkAccess == NetworkAccess.None)
        {
            await Shell.Current.DisplayAlert(LanguageResources.Error_WeHadAProblem, LanguageResources.NoInternetConnection, LanguageResources.Generic_Ok);
            return;
        }

        // Stop/Start playing
        if (IsPlaying)
        {
            try
            {
                mediaElement.Stop();
            }
            catch (Exception ex)
            {
                ex.Report();
            }

            // IsPlaying is setted here becouse maybe the internet is not so fast enough and the song can be loading and not put play
            IsPlaying = false;
            return;
        }

        azureHymnService.ObserveSettings()
            .SelectMany(x => x)
            .Where(x => x.Id == Language.Id)
            .Subscribe(hymnSettings => MainThread.InvokeOnMainThreadAsync(async delegate
            {
                var songUrl = string.Empty;
                var isPlayingInstrumentalMusic = false;

                // Choose music
                if (hymnSettings.InstrumentalMusicUrl != null && hymnSettings.SungMusicUrl != null)
                {
                    var instrumentalTitle = LanguageResources.Instrumental;
                    var sungTitle = LanguageResources.Sung;

                    var result = await Shell.Current.DisplayActionSheet(
                        LanguageResources.VersionsAndLanguages, LanguageResources.Generic_Cancel,
                        null, new[] { instrumentalTitle, sungTitle });

                    if (result.Equals(instrumentalTitle))
                    {
                        songUrl = hymnSettings.GetInstrumentUrl(CurrentHymn.Number);
                        isPlayingInstrumentalMusic = true;
                    }
                    else if (result.Equals(sungTitle))
                    {
                        songUrl = hymnSettings.GetSungUrl(CurrentHymn.Number);
                        isPlayingInstrumentalMusic = false;
                    }
                    // Tap on "Close"
                    else
                        return;
                }

                if (string.IsNullOrWhiteSpace(songUrl))
                {
                    isPlayingInstrumentalMusic = hymnSettings.SupportsInstrumentalMusic();
                    songUrl = hymnSettings.SupportsInstrumentalMusic()
                        ? hymnSettings.GetInstrumentUrl(CurrentHymn.Number)
                        : hymnSettings.GetSungUrl(CurrentHymn.Number);
                }

                // IsPlaying is setted here becouse maybe the internet is not so fast enough and the song can be loading and not to put play from the first moment
                try
                {
                    IsPlaying = true;
                    mediaElement.Source = MediaSource.FromUri(songUrl);

                    // Buffering
                    mediaElement.Play();
                    mediaElement.Pause();

                    new Thread(() =>
                    {
                        Thread.Sleep(1800);

                        // You can stop playing while buffering
                        if (!IsPlaying)
                            return;

                        MainThread.BeginInvokeOnMainThread(() => mediaElement.Play());
                    }).Start();
                }
                catch (Exception ex)
                {
                    ex.Report();
                    IsPlaying = false;
                }

                //Analytics.TrackEvent(TrackingConstants.TrackEv.HymnMusicPlayed,
                //    new Dictionary<string, string>
                //    {
                //            { TrackingConstants.TrackEv.HymnReferenceScheme.Number, CurrentHymn.Number.ToString() },
                //            { TrackingConstants.TrackEv.HymnReferenceScheme.HymnalVersion, Language.Id },
                //            {
                //                TrackingConstants.TrackEv.HymnReferenceScheme.TypeOfMusicPlaying,
                //                isPlayingInstrumentalMusic
                //                    ? TrackingConstants.TrackEv.HymnReferenceScheme.InstrumentalMusic
                //                    : TrackingConstants.TrackEv.HymnReferenceScheme.SungMusic
                //            },
                //            {
                //                TrackingConstants.TrackEv.HymnReferenceScheme.CultureInfo,
                //                InfoConstants.CurrentCultureInfo.Name
                //            },
                //            {
                //                TrackingConstants.TrackEv.HymnReferenceScheme.Time,
                //                DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture)
                //            }
                //    });
            }),
            ex => ex.Report());
    }

    [RelayCommand]
    private void AddToFavorites()
    {
        if (IsFavorite)
            databaseService
                .FindAsync<FavoriteHymn>(h => h.HymnalLanguageId == HymnParameter.HymnalLanguage.Id & h.Number == HymnParameter.Number)
                .ToObservable()
                .Subscribe(result =>
                {
                    if (result is not null)
                    {
                        databaseService.RemoveAsync(result)
                            .ToObservable()
                            .Subscribe(result2 => MainThread.BeginInvokeOnMainThread(() => IsFavorite = false),
                            error => error.Report());
                    }
                },
                error => error.Report());
        else
            databaseService.InserAsync(CurrentHymn.ToFavoriteHymn())
                .ToObservable()
                .Subscribe(result => MainThread.BeginInvokeOnMainThread(() => IsFavorite = true),
                error => error.Report());
    }
}
