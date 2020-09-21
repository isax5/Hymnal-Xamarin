using System;
using System.Collections.Generic;
using System.Net.Http;
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
        private readonly IMusicAPI musicAPI;

        private readonly ObservableValue<IEnumerable<MusicSettingsResponse>> musicSettingsObservable = new ObservableValue<IEnumerable<MusicSettingsResponse>>();

        private AzureHymnService()
        {
            httpClient = new HttpClient(new HttpClientDiagnosticsHandler(new HttpClientHandler()))
            {
                BaseAddress = new Uri(@"https://hymnal-functions.azurewebsites.net/api")
            };
            musicAPI = RestService.For<IMusicAPI>(httpClient);
        }


        public IObservable<IEnumerable<MusicSettingsResponse>> ObserveSettings()
        {
            musicSettingsObservable.NextValue(musicAPI.ObserveSettings());
            return musicSettingsObservable;
        }
    }
}
