using DFC.App.JobProfileOverview.Controllers;
using DFC.App.JobProfileOverview.SegmentService;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace DFC.App.JobProfileOverview.UnitTests.ControllerTests.HealthControllerTests
{
    public abstract class BaseHealthController
    {
        public BaseHealthController()
        {
            Logger = A.Fake<ILogger<HealthController>>();
            JobProfileOverviewSegmentService = A.Fake<IJobProfileOverviewSegmentService>();
        }

        protected IJobProfileOverviewSegmentService JobProfileOverviewSegmentService { get; }

        protected ILogger<HealthController> Logger { get; }

        protected HealthController BuildHealthController(string mediaTypeName)
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers[HeaderNames.Accept] = mediaTypeName;

            var controller = new HealthController(Logger, JobProfileOverviewSegmentService)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext,
                },
            };

            return controller;
        }
    }
}