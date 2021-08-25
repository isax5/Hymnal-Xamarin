#define NEWTONSOFT
//#define SYSTEMJSON

using System;
using System.Net.Http;
using Helpers;
using Hymnal.AzureFunctions.Models;
using Refit;
#if NEWTONSOFT
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
#elif SYSTEMJSON
using System.Text.Json;
#endif

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
                BaseAddress = new Uri(@"https://hymnal-functions.azurewebsites.net/api")
            };
            var settings = new RefitSettings
            {
#if NEWTONSOFT
                ContentSerializer = new NewtonsoftJsonContentSerializer(new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }),
#elif SYSTEMJSON
                ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true,
                }),
#endif
            };

            musicApi = RestService.For<IMusicApi>(httpClient, settings);
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
