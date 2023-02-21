namespace Hymnal.Extensions
{
    public static class TaskObservableExtensions
    {
        public static IObservable<TResult> ToObservable<TResult>(this ValueTask<TResult> task) => task.ToTaskAsync().ToObservable();

        private static async Task<TResult> ToTaskAsync<TResult>(this ValueTask<TResult> task) => await task;
    }
}
