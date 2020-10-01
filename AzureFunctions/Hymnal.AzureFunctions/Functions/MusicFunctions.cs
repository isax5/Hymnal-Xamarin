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

            return new OkObjectResult(Constants.HYMN_SETTINGS);
        }


        [FunctionName("MusicSettingsList_v2")]
        public static IActionResult MusicSettingsList_v2(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "v2/music/settings")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("[Music Settings Service] triggered");

            return new OkObjectResult(Constants.HYMN_SETTINGS);
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
