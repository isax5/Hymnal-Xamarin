using System;
using System.Collections.Generic;
using Hymnal.AzureFunctions.Models;

namespace Hymnal.AzureFunctions.Client
{
    public interface IAzureHymnService
    {
        void SetNextValues(IEnumerable<HymnSettingsResponse> settingsResponse);
        IObservable<HymnSettingsResponse> ObserveSettings(bool reload = false);
    }
}
