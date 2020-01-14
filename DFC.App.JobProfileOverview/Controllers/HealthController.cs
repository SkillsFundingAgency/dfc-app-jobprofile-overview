using DFC.App.JobProfileOverview.Extensions;
using DFC.App.JobProfileOverview.SegmentService;
using DFC.App.JobProfileOverview.ViewModels;
using DFC.Logger.AppInsights.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace DFC.App.JobProfileOverview.Controllers
{
    public class HealthController : Controller
    {
        private readonly ILogService logService;
        private readonly IJobProfileOverviewSegmentService jobProfileOverviewSegmentService;

        public HealthController(ILogService logService, IJobProfileOverviewSegmentService jobProfileOverviewSegmentService)
        {
            this.logService = logService;
            this.jobProfileOverviewSegmentService = jobProfileOverviewSegmentService;
        }

        [HttpGet]
        [Route("{controller}")]
        public async Task<IActionResult> Health()
        {
            var resourceName = typeof(Program).Namespace;
            string message;

            logService.LogInformation($"{nameof(Health)} has been called");

            try
            {
                var isHealthy = await jobProfileOverviewSegmentService.PingAsync().ConfigureAwait(false);

                if (isHealthy)
                {
                    message = "Document store is available";
                    logService.LogInformation($"{nameof(Health)} responded with: {resourceName} - {message}");

                    var viewModel = CreateHealthViewModel(resourceName, message);

                    return this.NegotiateContentResult(viewModel);
                }

                message = $"Ping to {resourceName} has failed";
                logService.LogError($"{nameof(Health)}: {message}");
            }
            catch (Exception ex)
            {
                message = $"{resourceName} exception: {ex.Message}";
                logService.LogError(message);
            }

            return StatusCode((int)HttpStatusCode.ServiceUnavailable);
        }

        [HttpGet]
        public IActionResult Ping()
        {
            logService.LogInformation($"{nameof(Ping)} has been called");

            return Ok();
        }

        private static HealthViewModel CreateHealthViewModel(string resourceName, string message)
        {
            return new HealthViewModel
            {
                HealthItems = new List<HealthItemViewModel>
                {
                    new HealthItemViewModel
                    {
                        Service = resourceName,
                        Message = message,
                    },
                },
            };
        }
    }
}