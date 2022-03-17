using System;
using System.Collections.Generic;
using Hymnal.AzureFunctions.Models;
using Refit;

namespace Hymnal.AzureFunctions.Client
{
    public interface IMusicApi
    {
        [Get("/settings.json")]
        IObservable<IEnumerable<HymnSettingsResponse>> ObserveSettings();
    }
}
