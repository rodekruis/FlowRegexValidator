using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using Microsoft.Extensions.Primitives;
using System.Text.RegularExpressions;

namespace Company.Function
{
    public static class FlowRegexValidator
    {
        [FunctionName("FlowRegexValidator")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string value = req.Query["value"];
            string pattern = req.Query["pattern"];

            log.LogInformation(pattern);

            return (Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase)) ?
                (ActionResult)new OkObjectResult(new { Result = true, Response = Regex.Match(value, pattern, RegexOptions.IgnoreCase).Value}) :
                (ActionResult)new OkObjectResult(new { Result = false});

            /*string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);*/
        }
    }
}