using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using Hymnal.AzureFunctions.Client;

namespace Hymnal.AzureFunctions.Test
{
    class Program
    {
        private static Subject<int> _sInt = new Subject<int>();
        private static IObservable<int> observableInt => _sInt.AsObservable();

        static async Task Main(string[] args)
        {
            /*
            observableInt.Subscribe(x => Console.WriteLine($"New Value 1: {x}"));
            observableInt.Subscribe(x => Console.WriteLine($"New Value 2: {x}"));
            observableInt.Subscribe(x => Console.WriteLine($"New Value 3: {x}"));
            observableInt.Subscribe(x => Console.WriteLine($"New Value 4: {x}"));
            observableInt.Subscribe(x => Console.WriteLine($"New Value 5: {x}"));
            observableInt
                .Throttle(TimeSpan.FromSeconds(1))
                .Subscribe(x => Console.WriteLine($"New Value 6: {x}"));

            _sInt.OnNext(12);
            _sInt.OnNext(10);
            _sInt.OnNext(12);
            _sInt.OnNext(13);
            _sInt.OnNext(1002);
            _sInt.OnNext(1874934);

            await Task.Delay(2000);

            _sInt.OnNext(133);

            _sInt.OnCompleted();

            _sInt.OnNext(007);


            var subs = observableInt.Subscribe(x => Console.WriteLine($"New value 7: {x}"));
            subs.Dispose();

            observableInt.Subscribe(x => Console.WriteLine($"New value 8: {x}")).Dispose();


            Console.WriteLine("Finished");
            */

            var service = AzureHymnService.Current;

            var observable = service.ObserveSettings();

            var subs1 = observable
                .Subscribe(
                x => Console.WriteLine($"New Value: {x.Id}"),
                ex => Console.WriteLine($"Problem: {ex.Message}"),
                () => Console.WriteLine("Completed"));

            service.ObserveSettings().Subscribe(x => Console.WriteLine($"Nuevamente valor: {x:id}"), ex => { });
            service.ObserveSettings().Subscribe(x => Console.WriteLine($"Nuevamente valor: {x:id}"), ex => { });
            service.ObserveSettings().Subscribe(x => Console.WriteLine($"Nuevamente valor: {x:id}"), ex => { });
            service.ObserveSettings().Subscribe(x => Console.WriteLine($"Nuevamente valor: {x:id}"), ex => { });
            service.ObserveSettings().Subscribe(x => Console.WriteLine($"Nuevamente valor: {x:id}"), ex => { });

            Thread.Sleep(3500);

            while (true)
            {
                service.ObserveSettings().Subscribe(x => Console.WriteLine($"New value 2: {x.Id}"), ex => Console.WriteLine($"Problema en subscripción ciclica: {ex}"));
                await Task.Delay(1000);
            }

            await new TaskCompletionSource<object>().Task;
        }

    }
}
