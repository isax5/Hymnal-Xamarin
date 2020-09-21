using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Disposables;

namespace Hymnal.AzureFunctions.Models
{
    public class ObservableValue<T> : ObservableBase<T>
    {
        public T Current { get; internal set; }

        protected readonly List<IObserver<T>> Observers = new List<IObserver<T>>();
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
                Observers.ForEach(obs => obs.OnNext(Current));

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
            Observers.ForEach(obs => obs.OnError(ex));
        }
    }
}
