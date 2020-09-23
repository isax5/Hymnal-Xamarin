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
            var service = AzureHymnService.Current;

            var observable = service.ObserveSettings();

            var subs1 = observable
                .Subscribe(
                x => Console.WriteLine($"New Value: {x.Id}"),
                ex => Console.WriteLine($"Problem: {ex.Message}"),
                () => Console.WriteLine("Completed"));

            Thread.Sleep(3500);

            while (true)
            {
                observable
                    .Subscribe(x => Console.WriteLine($"New value 2: {x.Id}"));
            }

            await new TaskCompletionSource<object>().Task;
        }

    }
}
