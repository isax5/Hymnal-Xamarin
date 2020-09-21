using System;

namespace Hymnal.AzureFunctions.Models
{
    public interface IObservableValue<T>
    {
        T Current { get; }

        void NextValue(IObservable<T> value);
        void NextValue(T value);
        void OnError(Exception ex);
    }
}