﻿using DFC.App.JobProfileOverview.ViewModels;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Xunit;

namespace DFC.App.JobProfileOverview.UnitTests.ControllerTests.HealthControllerTests
{
    public class HealthControllerHealthTests : BaseHealthController
    {
        [Fact]
        public async Task ReturnsSuccessWhenhealthy()
        {
            // Arrange
            const bool expectedResult = true;
            var controller = BuildHealthController(MediaTypeNames.Application.Json);
            A.CallTo(() => JobProfileOverviewSegmentService.PingAsync()).Returns(expectedResult);

            // Act
            var result = await controller.Health().ConfigureAwait(false);

            // Assert
            A.CallTo(() => JobProfileOverviewSegmentService.PingAsync()).MustHaveHappenedOnceExactly();

            var jsonResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<HealthViewModel>(jsonResult.Value);

            jsonResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            model.HealthItems.Count.Should().BeGreaterThan(0);
            model.HealthItems.First().Service.Should().NotBeNullOrWhiteSpace();
            model.HealthItems.First().Message.Should().NotBeNullOrWhiteSpace();

            controller.Dispose();
        }

        [Fact]
        public async Task ReturnsServiceUnavailableWhenUnhealthy()
        {
            // Arrange
            const bool expectedResult = false;
            var controller = BuildHealthController(MediaTypeNames.Application.Json);
            A.CallTo(() => JobProfileOverviewSegmentService.PingAsync()).Returns(expectedResult);

            // Act
            var result = await controller.Health().ConfigureAwait(false);

            // Assert
            A.CallTo(() => JobProfileOverviewSegmentService.PingAsync()).MustHaveHappenedOnceExactly();
            var statusResult = Assert.IsType<StatusCodeResult>(result);
            statusResult.StatusCode.Should().Be((int)HttpStatusCode.ServiceUnavailable);

            controller.Dispose();
        }

        [Fact]
        public async Task ReturnsServiceUnavailableWhenException()
        {
            // Arrange
            var controller = BuildHealthController(MediaTypeNames.Application.Json);
            A.CallTo(() => JobProfileOverviewSegmentService.PingAsync()).Throws<Exception>();

            // Act
            var result = await controller.Health().ConfigureAwait(false);

            // Assert
            A.CallTo(() => JobProfileOverviewSegmentService.PingAsync()).MustHaveHappenedOnceExactly();
            var statusResult = Assert.IsType<StatusCodeResult>(result);
            statusResult.StatusCode.Should().Be((int)HttpStatusCode.ServiceUnavailable);

            controller.Dispose();
        }
    }
}