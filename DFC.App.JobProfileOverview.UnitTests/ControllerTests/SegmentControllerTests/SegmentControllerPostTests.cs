using DFC.App.JobProfileOverview.Data.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace DFC.App.JobProfileOverview.UnitTests.ControllerTests.SegmentControllerTests
{
    [Trait("Segment Controller", "Post Tests")]
    public class SegmentControllerPostTests : BaseSegmentController
    {
        [Theory]
        [MemberData(nameof(JsonMediaTypes))]
        public async Task SegmentControllerPostReturnsSuccessForCreate(string mediaTypeName)
        {
            // Arrange
            const HttpStatusCode expectedResponse = HttpStatusCode.Created;
            var overviewSegmentModel = A.Fake<JobProfileOverviewSegmentModel>();
            var controller = BuildSegmentController(mediaTypeName);

            A.CallTo(() => FakeJobProfileOverviewSegmentService.GetByIdAsync(A<Guid>.Ignored)).Returns((JobProfileOverviewSegmentModel)null);
            A.CallTo(() => FakeJobProfileOverviewSegmentService.UpsertAsync(A<JobProfileOverviewSegmentModel>.Ignored)).Returns(expectedResponse);

            // Act
            var result = await controller.Post(overviewSegmentModel).ConfigureAwait(false);

            // Assert
            A.CallTo(() => FakeJobProfileOverviewSegmentService.UpsertAsync(A<JobProfileOverviewSegmentModel>.Ignored)).MustHaveHappenedOnceExactly();
            var okResult = Assert.IsType<StatusCodeResult>(result);

            Assert.Equal((int)HttpStatusCode.Created, okResult.StatusCode);

            controller.Dispose();
        }

        [Theory]
        [MemberData(nameof(JsonMediaTypes))]
        public async Task SegmentControllerPostReturnsAlreadyReportedForCreate(string mediaTypeName)
        {
            // Arrange
            const HttpStatusCode expectedResponse = HttpStatusCode.AlreadyReported;
            var overviewSegmentModel = A.Fake<JobProfileOverviewSegmentModel>();
            var controller = BuildSegmentController(mediaTypeName);

            A.CallTo(() => FakeJobProfileOverviewSegmentService.GetByIdAsync(A<Guid>.Ignored)).Returns((JobProfileOverviewSegmentModel)null);
            A.CallTo(() => FakeJobProfileOverviewSegmentService.UpsertAsync(A<JobProfileOverviewSegmentModel>.Ignored)).Returns(expectedResponse);

            // Act
            var result = await controller.Post(overviewSegmentModel).ConfigureAwait(false);

            // Assert
            A.CallTo(() => FakeJobProfileOverviewSegmentService.UpsertAsync(A<JobProfileOverviewSegmentModel>.Ignored)).MustHaveHappenedOnceExactly();
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal((int)expectedResponse, statusCodeResult.StatusCode);

            controller.Dispose();
        }

        [Theory]
        [MemberData(nameof(JsonMediaTypes))]
        public async Task SegmentControllerPostReturnsBadResultWhenModelIsNull(string mediaTypeName)
        {
            // Arrange
            var controller = BuildSegmentController(mediaTypeName);

            // Act
            var result = await controller.Post(null).ConfigureAwait(false);

            // Assert
            var statusResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, statusResult.StatusCode);

            controller.Dispose();
        }

        [Theory]
        [MemberData(nameof(JsonMediaTypes))]
        public async Task SegmentControllerPostReturnsBadResultWhenModelIsInvalid(string mediaTypeName)
        {
            // Arrange
            var overviewSegmentModel = new JobProfileOverviewSegmentModel();
            var controller = BuildSegmentController(mediaTypeName);

            controller.ModelState.AddModelError(string.Empty, "Model is not valid");

            // Act
            var result = await controller.Post(overviewSegmentModel).ConfigureAwait(false);

            // Assert
            var statusResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, statusResult.StatusCode);

            controller.Dispose();
        }
    }
}