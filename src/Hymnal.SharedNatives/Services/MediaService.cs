using System;
using Hymnal.Core.Services;
using MediaManager;
using MediaManager.Playback;
using MediaManager.Player;

namespace Hymnal.SharedNatives.Services
{
    public class MediaService : IMediaService
    {
        public bool IsPlaying => CrossMediaManager.Current.IsPlaying();

        public MediaService()
        {
            CrossMediaManager.Current.RepeatMode = RepeatMode.Off;

            CrossMediaManager.Current.MediaItemFinished += Current_MediaItemFinished;
            CrossMediaManager.Current.MediaItemFailed += Current_MediaItemFailed;
            CrossMediaManager.Current.MediaItemChanged += Current_MediaItemChanged;
            CrossMediaManager.Current.PositionChanged += Current_PositionChanged;
            CrossMediaManager.Current.PropertyChanged += Current_PropertyChanged;
            CrossMediaManager.Current.StateChanged += Current_StateChanged;
            CrossMediaManager.Current.BufferedChanged += Current_BufferedChanged;
        }

        ~MediaService()
        {
            CrossMediaManager.Current.MediaItemFinished -= Current_MediaItemFinished;
            CrossMediaManager.Current.MediaItemFailed -= Current_MediaItemFailed;
            CrossMediaManager.Current.MediaItemChanged -= Current_MediaItemChanged;
            CrossMediaManager.Current.PositionChanged -= Current_PositionChanged;
            CrossMediaManager.Current.PropertyChanged -= Current_PropertyChanged;
            CrossMediaManager.Current.StateChanged -= Current_StateChanged;
            CrossMediaManager.Current.BufferedChanged -= Current_BufferedChanged;
        }

        public event EventHandler<EventArgs> Playing;
        public event EventHandler<EventArgs> Stopped;
        public event EventHandler<EventArgs> EndReached;

        public void Play(string url)
        {
            CrossMediaManager.Current.Play(url);
        }

        public void Stop()
        {
            CrossMediaManager.Current.Stop();
        }

        #region CrossMediaManager Events
        private void Current_BufferedChanged(object sender, BufferedChangedEventArgs e)
        {
        }

        private void Current_StateChanged(object sender, StateChangedEventArgs e)
        {
            switch (e.State)
            {
                case MediaPlayerState.Buffering:
                    if (Playing != null)
                        Playing.Invoke(sender, e);
                    break;

                case MediaPlayerState.Failed:
                    if (Stopped!= null)
                        Stopped.Invoke(sender, e);
                    break;

                case MediaPlayerState.Loading:
                    if (Playing != null)
                        Playing.Invoke(sender, e);
                    break;

                case MediaPlayerState.Paused:
                    if (Stopped != null)
                        Stopped.Invoke(sender, e);
                    break;

                case MediaPlayerState.Playing:
                    if (Playing != null)
                        Playing.Invoke(sender, e);
                    break;

                case MediaPlayerState.Stopped:
                    if (Stopped != null)
                        Stopped.Invoke(sender, e);
                    break;

                default:
                    break;
            }
        }

        private void Current_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
        }

        private void Current_PositionChanged(object sender, PositionChangedEventArgs e)
        {
        }

        private void Current_MediaItemChanged(object sender, MediaManager.Media.MediaItemEventArgs e)
        {
        }

        private void Current_MediaItemFailed(object sender, MediaManager.Media.MediaItemFailedEventArgs e)
        {
            if (Stopped != null)
                Stopped.Invoke(sender, e);
        }

        private void Current_MediaItemFinished(object sender, MediaManager.Media.MediaItemEventArgs e)
        {
            if (EndReached != null)
                EndReached.Invoke(sender, e);
        }
        #endregion
    }
}
