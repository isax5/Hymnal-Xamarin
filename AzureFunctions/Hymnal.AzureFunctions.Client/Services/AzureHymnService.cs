using System;
using System.Collections.Generic;
using System.Net.Http;
using Helpers;
using Hymnal.AzureFunctions.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;

namespace Hymnal.AzureFunctions.Client
{
    public class AzureHymnService : IAzureHymnService
    {
        private static IAzureHymnService current;
        public static IAzureHymnService Current => current ??= new AzureHymnService();

        private readonly HttpClient httpClient;
        private readonly IMusicApi musicApi;

        private readonly ObservableValues<HymnSettingsResponse> musicSettingsObservable = new();

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

        public void SetNextValues(IEnumerable<HymnSettingsResponse> settingsResponse)
        {
            musicSettingsObservable.NextValues(settingsResponse);
        }

        private bool loadingSettings = false;
        public IObservable<HymnSettingsResponse> ObserveSettings(bool reload)
        {
            if ((!loadingSettings && musicSettingsObservable.Current == null) || reload)
            {
                loadingSettings = true;
                musicApi.ObserveSettings().Subscribe(x =>
                {
                    musicSettingsObservable.NextValues(x);
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
}
