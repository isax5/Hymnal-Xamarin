using System;

namespace Hymnal.Core.Services
{
    public interface IMediaService
    {
        /// <summary>
        /// Play media from url
        /// </summary>
        /// <param name="url"></param>
        void Play(string url);

        /// <summary>
        /// Stop media
        /// </summary>
        void Stop();

        /// <summary>
        /// Is playing?
        /// </summary>
        bool IsPlaying { get; }

        /// <summary>
        /// Media started
        /// </summary>
        event EventHandler<EventArgs> Playing;

        /// <summary>
        /// Media Stopped
        /// </summary>
        event EventHandler<EventArgs> Stopped;

        /// <summary>
        /// Media finished
        /// </summary>
        event EventHandler<EventArgs> EndReached;
    }
}
