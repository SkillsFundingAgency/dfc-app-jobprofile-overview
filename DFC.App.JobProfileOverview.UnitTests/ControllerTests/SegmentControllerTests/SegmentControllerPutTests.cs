using DFC.App.JobProfileOverview.Data.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using Xunit;

namespace DFC.App.JobProfileOverview.UnitTests.ControllerTests.SegmentControllerTests
{
    [Trait("Segment Controller", "Put Tests")]
    public class SegmentControllerPutTests : BaseSegmentController
    {
        [Theory]
        [MemberData(nameof(JsonMediaTypes))]
        public async void SegmentControllerPutReturnsSuccessForUpdate(string mediaTypeName)
        {
            // Arrange
            const HttpStatusCode expectedResponse = HttpStatusCode.OK;
            var existingModel = A.Fake<JobProfileOverviewSegmentModel>();
            existingModel.SequenceNumber = 123;

            var modelToPut = A.Fake<JobProfileOverviewSegmentModel>();
            modelToPut.SequenceNumber = 124;

            var controller = BuildSegmentController(mediaTypeName);

            A.CallTo(() => FakeJobProfileOverviewSegmentService.GetByIdAsync(A<Guid>.Ignored)).Returns(existingModel);
            A.CallTo(() => FakeJobProfileOverviewSegmentService.UpsertAsync(A<JobProfileOverviewSegmentModel>.Ignored)).Returns(expectedResponse);

            // Act
            var result = await controller.Put(modelToPut).ConfigureAwait(false);

            // Assert
            A.CallTo(() => FakeJobProfileOverviewSegmentService.UpsertAsync(A<JobProfileOverviewSegmentModel>.Ignored)).MustHaveHappenedOnceExactly();
            var okResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal((int)expectedResponse, okResult.StatusCode);

            controller.Dispose();
        }

        [Theory]
        [MemberData(nameof(JsonMediaTypes))]
        public async void SegmentControllerPutReturnsAlreadyReportedForUpdate(string mediaTypeName)
        {
            // Arrange
            const HttpStatusCode expectedResponse = HttpStatusCode.AlreadyReported;
            var existingModel = A.Fake<JobProfileOverviewSegmentModel>();
            existingModel.SequenceNumber = 123;

            var modelToUpsert = A.Fake<JobProfileOverviewSegmentModel>();
            modelToUpsert.SequenceNumber = existingModel.SequenceNumber - 1;

            var controller = BuildSegmentController(mediaTypeName);

            A.CallTo(() => FakeJobProfileOverviewSegmentService.GetByIdAsync(A<Guid>.Ignored)).Returns(existingModel);

            // Act
            var result = await controller.Put(modelToUpsert).ConfigureAwait(false);

            // Assert
            A.CallTo(() => FakeJobProfileOverviewSegmentService.GetByIdAsync(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal((int)expectedResponse, statusCodeResult.StatusCode);

            controller.Dispose();
        }

        [Theory]
        [MemberData(nameof(JsonMediaTypes))]
        public async void SegmentControllerPutReturnsNotFoundForUpdate(string mediaTypeName)
        {
            // Arrange
            const HttpStatusCode expectedResponse = HttpStatusCode.NotFound;
            var modelToUpsert = A.Fake<JobProfileOverviewSegmentModel>();

            var controller = BuildSegmentController(mediaTypeName);

            A.CallTo(() => FakeJobProfileOverviewSegmentService.GetByIdAsync(A<Guid>.Ignored)).Returns((JobProfileOverviewSegmentModel)null);

            // Act
            var result = await controller.Put(modelToUpsert).ConfigureAwait(false);

            // Assert
            A.CallTo(() => FakeJobProfileOverviewSegmentService.GetByIdAsync(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal((int)expectedResponse, statusCodeResult.StatusCode);

            controller.Dispose();
        }

        [Theory]
        [MemberData(nameof(JsonMediaTypes))]
        public async void SegmentControllerPutReturnsBadResultWhenModelIsNull(string mediaTypeName)
        {
            // Arrange
            const HttpStatusCode expectedResponse = HttpStatusCode.BadRequest;
            var controller = BuildSegmentController(mediaTypeName);

            // Act
            var result = await controller.Put(null).ConfigureAwait(false);

            // Assert
            var statusResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal((int)expectedResponse, statusResult.StatusCode);

            controller.Dispose();
        }

        [Theory]
        [MemberData(nameof(JsonMediaTypes))]
        public async void SegmentControllerPutReturnsBadResultWhenModelIsInvalid(string mediaTypeName)
        {
            // Arrange
            const HttpStatusCode expectedResponse = HttpStatusCode.BadRequest;
            var relatedCareersSegmentModel = new JobProfileOverviewSegmentModel();
            var controller = BuildSegmentController(mediaTypeName);

            controller.ModelState.AddModelError(string.Empty, "Model is not valid");

            // Act
            var result = await controller.Put(relatedCareersSegmentModel).ConfigureAwait(false);

            // Assert
            var statusResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal((int)expectedResponse, statusResult.StatusCode);

            controller.Dispose();
        }
    }
}