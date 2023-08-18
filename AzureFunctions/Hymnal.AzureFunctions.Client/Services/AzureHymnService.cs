using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reactive.Subjects;
using Hymnal.AzureFunctions.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;

namespace Hymnal.AzureFunctions.Client;

public sealed class AzureHymnService : IAzureHymnService
{
    private static IAzureHymnService current;
    public static IAzureHymnService Current => current ??= new AzureHymnService();

    private readonly HttpClient httpClient;
    private readonly IMusicApi musicApi;

    private readonly BehaviorSubject<IEnumerable<HymnSettingsResponse>> musicSettingsObservable = new(null);

    private AzureHymnService()
    {
        httpClient = new HttpClient(new HttpClientDiagnosticsHandler(new HttpClientHandler()))
        {
            BaseAddress = new Uri(@"https://isax5.github.io/hymnal/backend-data/v1")
        };
        var settings = new RefitSettings
        {
            ContentSerializer = new NewtonsoftJsonContentSerializer(new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            }),
        };

        musicApi = RestService.For<IMusicApi>(httpClient, settings);
    }

    private bool loadingSettings = false;
    public IObservable<IEnumerable<HymnSettingsResponse>> ObserveSettings(bool reload)
    {
        if ((!loadingSettings && musicSettingsObservable.Value == null) || reload)
        {
            loadingSettings = true;
            musicApi.ObserveSettings().Subscribe(x =>
            {
                musicSettingsObservable.OnNext(x);
                loadingSettings = false;
            }, ex =>
            {
                musicSettingsObservable.OnError(ex);
                loadingSettings = false;
            });
        }

        return musicSettingsObservable;
    }
}
