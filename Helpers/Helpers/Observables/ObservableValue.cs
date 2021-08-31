using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Disposables;

namespace Helpers
{
    public class ObservableValue<T> : ObservableBase<T>
    {
        public T Current { get; internal set; }

        protected readonly List<IObserver<T>> Observers = new();
        private readonly bool autoDispose;

        public ObservableValue(bool autoDispose = true)
        {
            this.autoDispose = autoDispose;
        }

        public ObservableValue(T initialValue, bool autoDispose = true)
            : this(autoDispose)
        {
            Current = initialValue;
        }

        protected override IDisposable SubscribeCore(IObserver<T> observer)
        {
            if (Current != null)
            {
                observer.OnNext(Current);

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

        public void NextValue(T value)
        {
            lock (Observers)
            {
                Current = value;

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

        public void NextValue(IObservable<T> value)
        {
            value.Subscribe(x => NextValue(x), ex => OnError(ex));
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
