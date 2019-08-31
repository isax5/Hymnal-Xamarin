namespace Hymnal.Core.Services
{
    public interface IMediaService
    {
        void Play(string url);
        void Stop();
        bool IsPlaying { get; }
    }
}
