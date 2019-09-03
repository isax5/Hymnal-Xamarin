using System;

namespace Hymnal.Core.Services
{
    public interface IMediaService
    {
        void Play(string url);
        void Stop();
        bool IsPlaying { get; }

        event EventHandler<EventArgs> Playing;
        event EventHandler<EventArgs> Stopped;
        event EventHandler<EventArgs> EndReached;
    }
}
