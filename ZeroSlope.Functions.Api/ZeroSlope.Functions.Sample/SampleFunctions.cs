using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ZeroSlope.Domain.Services;
using ZeroSlope.Packages.DotNet.Exceptions;
using ZeroSlope.Packages.DotNet.Exceptions.Handlers;
using ZeroSlope.Models.Sample.Requests;

namespace ZeroSlope.Functions.Sample
{
    public class SampleFunctions
    {
        private readonly SampleService _service;

        public SampleFunctions(SampleService service)
        {
            _service = service;
        }

        [FunctionName("Sample_List")]
        public async Task<IActionResult> RunList(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");
                return new OkObjectResult(_service.List());
            }
            catch (HandledException ex)
            {
                log.LogError(ex.Message, ex.StackTrace);
                return FunctionErrorHandler.Handle(ex);
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message, ex.StackTrace);
                return FunctionErrorHandler.Handle(ex);
            }
        }

        [FunctionName("Sample_GetById")]
        public async Task<IActionResult> RunGetById(
           [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
           ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");
                int.TryParse(req.Query["id"], out int id);
                return new OkObjectResult(_service.Read(id));
            }
            catch (HandledException ex)
            {
                log.LogError(ex.Message, ex.StackTrace);
                return FunctionErrorHandler.Handle(ex);
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message, ex.StackTrace);
                return FunctionErrorHandler.Handle(ex);
            }
        }

        [FunctionName("Sample_Create")]
        public async Task<IActionResult> RunCreate(
           [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
           ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");
                string body = await new StreamReader(req.Body).ReadToEndAsync();
                var model = JsonConvert.DeserializeObject<SampleRequest>(body);
                return new OkObjectResult(_service.Save(model));
            }
            catch (HandledException ex)
            {
                log.LogError(ex.Message, ex.StackTrace);
                return FunctionErrorHandler.Handle(ex);
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message, ex.StackTrace);
                return FunctionErrorHandler.Handle(ex);
            }
        }

        [FunctionName("Sample_Update")]
        public async Task<IActionResult> RunUpdate(
           [HttpTrigger(AuthorizationLevel.Function, "put", Route = null)] HttpRequest req,
           ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");
                string body = await new StreamReader(req.Body).ReadToEndAsync();
                var model = JsonConvert.DeserializeObject<SampleRequest>(body);
                return new OkObjectResult(_service.Save(model));
            }
            catch (HandledException ex)
            {
                log.LogError(ex.Message, ex.StackTrace);
                return FunctionErrorHandler.Handle(ex);
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message, ex.StackTrace);
                return FunctionErrorHandler.Handle(ex);
            }
        }

        [FunctionName("Sample_Delete")]
        public async Task<IActionResult> RunDelete(
           [HttpTrigger(AuthorizationLevel.Function, "delete", Route = null)] HttpRequest req,
           ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");
                int.TryParse(req.Query["id"], out int id);
                _service.Delete(id);
                return new NoContentResult();
            }
            catch (HandledException ex)
            {
                log.LogError(ex.Message, ex.StackTrace);
                return FunctionErrorHandler.Handle(ex);
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message, ex.StackTrace);
                return FunctionErrorHandler.Handle(ex);
            }
        }
    }
}
