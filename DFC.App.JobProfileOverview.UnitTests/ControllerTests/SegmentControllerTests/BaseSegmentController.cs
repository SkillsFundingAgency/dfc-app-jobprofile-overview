﻿using DFC.App.JobProfileOverview.Controllers;
using DFC.App.JobProfileOverview.Data.ServiceBusModels;
using DFC.App.JobProfileOverview.SegmentService;
using DFC.Logger.AppInsights.Contracts;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Collections.Generic;
using System.Net.Mime;

namespace DFC.App.JobProfileOverview.UnitTests.ControllerTests.SegmentControllerTests
{
    public abstract class BaseSegmentController
    {
        public BaseSegmentController()
        {
            FakeLogger = A.Fake<ILogService>();
            FakeJobProfileOverviewSegmentService = A.Fake<IJobProfileOverviewSegmentService>();
            FakeMapper = A.Fake<AutoMapper.IMapper>();
            FakeJobProfileSegmentRefreshService = A.Fake<IJobProfileSegmentRefreshService<RefreshJobProfileSegmentServiceBusModel>>();
        }

        public static IEnumerable<object[]> HtmlMediaTypes => new List<object[]>
        {
            new string[] { "*/*" },
            new string[] { MediaTypeNames.Text.Html },
        };

        public static IEnumerable<object[]> InvalidMediaTypes => new List<object[]>
        {
            new string[] { MediaTypeNames.Text.Plain },
        };

        public static IEnumerable<object[]> JsonMediaTypes => new List<object[]>
        {
            new string[] { MediaTypeNames.Application.Json },
        };

        protected ILogService FakeLogger { get; }

        protected IJobProfileOverviewSegmentService FakeJobProfileOverviewSegmentService { get; }

        protected IJobProfileSegmentRefreshService<RefreshJobProfileSegmentServiceBusModel> FakeJobProfileSegmentRefreshService { get; }

        protected AutoMapper.IMapper FakeMapper { get; }

        protected SegmentController BuildSegmentController(string mediaTypeName)
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers[HeaderNames.Accept] = mediaTypeName;

            var controller = new SegmentController(FakeLogger, FakeJobProfileOverviewSegmentService, FakeMapper, FakeJobProfileSegmentRefreshService)
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