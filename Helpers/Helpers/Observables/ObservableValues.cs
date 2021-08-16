using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;

namespace Helpers
{
    public class ObservableValues<T> : ObservableBase<T>
    {
        public IEnumerable<T> Current { get; internal set; }

        protected readonly List<IObserver<T>> Observers = new();
        private readonly bool autoDispose;

        public ObservableValues(bool autoDispose = true)
        {
            this.autoDispose = autoDispose;
        }

        public ObservableValues(IEnumerable<T> initialValue, bool autoDispose = true)
            : this(autoDispose)
        {
            Current = initialValue;
        }

        protected override IDisposable SubscribeCore(IObserver<T> observer)
        {
            if (Current != null)
            {
                foreach (T value in Current.ToArray())
                    observer.OnNext(value);

                if (autoDispose)
                {
                    observer.OnCompleted();
                    return Disposable.Empty;
                }
            }

            lock (Observers)
            {
                Observers.Add(observer);
            }

            return Disposable.Create(() =>
            {
                lock (Observers)
                {
                    Observers.Remove(observer);
                }
            });
        }

        public void NextValues(IEnumerable<T> values)
        {
            lock (Observers)
            {
                Current = values;

                foreach (T value in values.ToArray())
                    foreach (IObserver<T> obs in Observers.ToArray())
                        obs.OnNext(value);

                if (autoDispose)
                {
                    foreach (IObserver<T> obs in Observers.ToArray())
                        obs.OnCompleted();

                    // This is not realy necessary, but just in case
                    Observers.Clear();
                }
            }
        }

        public void NextValues(IObservable<IEnumerable<T>> value)
        {
            value.Subscribe(x => NextValues(x), ex => OnError(ex));
        }

        public void OnError(Exception ex)
        {
            foreach (IObserver<T> obs in Observers.ToArray())
                obs.OnError(ex);
        }

        public void DisposeAll()
        {
            foreach (IObserver<T> observer in Observers.ToArray())
                observer.OnCompleted();

            Observers.Clear();
        }
    }
}
