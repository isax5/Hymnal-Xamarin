using System;
using Hymnal.AzureFunctions.Models;

namespace Hymnal.AzureFunctions.Client
{
    public interface IAzureHymnService
    {
        IObservable<HymnSettingsResponse> ObserveSettings(bool reload = false);
    }
}
