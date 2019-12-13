using DFC.App.JobProfileOverview.Controllers;
using DFC.App.JobProfileOverview.SegmentService;
using DFC.Logger.AppInsights.Contracts;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace DFC.App.JobProfileOverview.UnitTests.ControllerTests.HealthControllerTests
{
    public abstract class BaseHealthController
    {
        public BaseHealthController()
        {
            Logger = A.Fake<ILogService>();
            JobProfileOverviewSegmentService = A.Fake<IJobProfileOverviewSegmentService>();
        }

        protected IJobProfileOverviewSegmentService JobProfileOverviewSegmentService { get; }

        protected ILogService Logger { get; }

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