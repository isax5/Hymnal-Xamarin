namespace Hymnal.Extensions;

public static class ExceptionExtensions
{
    public static void Report(this Exception exception, Dictionary<string, string> properties = null)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Shell.Current.DisplayAlert("#Error", exception.Message, "Ok");
        });
#if DEBUG
#elif RELEASE
            // TODO: Reportar algo
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (properties is not null)
                    Crashes.TrackError(exception, properties);
            });
#endif
    }
}
