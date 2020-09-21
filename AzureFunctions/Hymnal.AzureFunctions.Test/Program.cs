using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hymnal.AzureFunctions.Client;

namespace Hymnal.AzureFunctions.Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var service = new HymnService();

            var observable = service.ObserveSettings();

            var subs1 = observable
                .Subscribe(
                x => Console.WriteLine($"New Value: {x.Count()} items"),
                ex => Console.WriteLine($"Problem: {ex.Message}"),
                () => Console.WriteLine("Completed"));

            var subs2 = observable
                .Subscribe(x => Console.WriteLine($"New value 2: {x.Count()}"));

            await new TaskCompletionSource<object>().Task;
        }

    }
}
