using System.Collections.Generic;
using Hymnal.AzureFunctions.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Hymnal.AzureFunctions.Functions
{
    public static class MusicFunctions
    {
        [FunctionName("MusicSettingsList_v1")]
        public static IActionResult MusicSettingsList_v1(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "v1/music/settings")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("[Music Settings Service] triggered");

            return new OkObjectResult(new List<MusicSettingsResponse>()
            {
                new MusicSettingsResponse
                {
                    Id = "es-newVersion",
                    TwoLetterISOLanguageName = "es",
                    InstrumentalMusicUrl = @"https://hymnalstorage.blob.core.windows.net/hymn-music/spanish/2009%20version/instrumental/###.mp3",
                    SungMusicUrl = @"https://hymnalstorage.blob.core.windows.net/hymn-music/spanish/2009%20version/sung/###.mp3",
                },
                new MusicSettingsResponse
                {
                    Id = "es-oldVersion",
                    TwoLetterISOLanguageName = "es",
                    InstrumentalMusicUrl = @"https://hymnalstorage.blob.core.windows.net/hymn-music/spanish/1962%20version/instrumental/###.mp3"
                },
                new MusicSettingsResponse
                {
                    Id = "en-newVersion",
                    TwoLetterISOLanguageName = "en",
                    InstrumentalMusicUrl = @"https://hymnalstorage.blob.core.windows.net/hymn-music/english/1985%20version/instrumental/###.mp3",
                }
            });
        }


        [FunctionName("MusicSettingsList_v2")]
        public static IActionResult MusicSettingsList_v2(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "v2/music/settings")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("[Music Settings Service] triggered");

            return new OkObjectResult(new List<MusicSettingsResponse>()
            {
                new MusicSettingsResponse
                {
                    Id = "es-newVersion",
                    TwoLetterISOLanguageName = "es",
                    InstrumentalMusicUrl = @"https://hymnalstorage.blob.core.windows.net/hymn-music/spanish/2009%20version/instrumental/###.mp3",
                    SungMusicUrl = @"https://hymnalstorage.blob.core.windows.net/hymn-music/spanish/2009%20version/sung/###.mp3",
                }
            });
        }

        /*
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            var responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
        */
    }
}
