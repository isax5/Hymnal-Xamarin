using System;
using System.Net.Http;
using Helpers;
using Hymnal.AzureFunctions.Models;
using Refit;

namespace Hymnal.AzureFunctions.Client
{
    public class AzureHymnService : IAzureHymnService
    {
        private static IAzureHymnService current;
        public static IAzureHymnService Current
        {
            get
            {
                if (current == null)
                    current = new AzureHymnService();

                return current;
            }
        }

        private readonly HttpClient httpClient;
        private readonly IMusicApi musicApi;

        private readonly ObservableValues<HymnSettingsResponse> musicSettingsObservable = new ObservableValues<HymnSettingsResponse>();

        private AzureHymnService()
        {
            httpClient = new HttpClient(new HttpClientDiagnosticsHandler(new HttpClientHandler()))
            {
                BaseAddress = new Uri(@"https://hymnal-functions.azurewebsites.net/api")
            };
            musicApi = RestService.For<IMusicApi>(httpClient);
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
