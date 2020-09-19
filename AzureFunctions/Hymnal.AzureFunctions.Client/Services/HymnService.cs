using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Hymnal.AzureFunctions.Models;
using Refit;

namespace Hymnal.AzureFunctions.Client
{
    public class HymnService
    {
        private readonly HttpClient httpClient;
        private readonly IMusicAPI musicAPI;

        public HymnService()
        {
            httpClient = new HttpClient(new HttpClientDiagnosticsHandler(new HttpClientHandler()))
            {
                BaseAddress = new Uri(@"https://hymnal-functions.azurewebsites.net/api")
            };
            musicAPI = RestService.For<IMusicAPI>(httpClient);
        }

        public async Task<IEnumerable<MusicSettingsResponse>> MusicSettingsAsync()
        {
            return await musicAPI.GetMusicSettings().ConfigureAwait(false);
        }
    }
}
