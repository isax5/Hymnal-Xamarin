using Hymnal.Core.Services;
using LibVLCSharp.Shared;

namespace Hymnal.SharedNatives.Services
{
    public class MediaService : IMediaService
    {
        private static bool Initialize = false;
        private static readonly LibVLC LibVLC = new LibVLC();
        private static readonly MediaPlayer MediaPlayer = new MediaPlayer(LibVLC);

        public MediaService()
        {
            if (!Initialize)
            {
                Initialize = true;
                LibVLCSharp.Shared.Core.Initialize();
            }
        }

        public void Play(string url)
        {
            var media = new Media(LibVLC, url, FromType.FromLocation);

            MediaPlayer.Media = media;

            // Stop previous media
            if (MediaPlayer.IsPlaying)
                MediaPlayer.Stop();

            MediaPlayer.Play();
        }

        public void Stop()
        {
            MediaPlayer.Stop();
        }
    }
}
