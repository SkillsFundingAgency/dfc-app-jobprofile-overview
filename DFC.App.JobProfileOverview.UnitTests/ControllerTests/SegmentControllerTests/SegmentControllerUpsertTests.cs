using DFC.App.JobProfileOverview.Data.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using Xunit;

namespace DFC.App.JobProfileOverview.UnitTests.ControllerTests.SegmentControllerTests
{
    public class SegmentControllerUpsertTests : BaseSegmentController
    {
        [Theory]
        [MemberData(nameof(JsonMediaTypes))]
        public async void SegmentControllerUpsertReturnsSuccessForCreate(string mediaTypeName)
        {
            // Arrange
            var overviewSegmentModel = A.Fake<JobProfileOverviewSegmentModel>();
            var controller = BuildSegmentController(mediaTypeName);
            var expectedUpsertResponse = HttpStatusCode.Created;

            A.CallTo(() => FakeJobProfileOverviewSegmentService.GetByIdAsync(A<Guid>.Ignored)).Returns((JobProfileOverviewSegmentModel)null);
            A.CallTo(() => FakeJobProfileOverviewSegmentService.UpsertAsync(A<JobProfileOverviewSegmentModel>.Ignored)).Returns(expectedUpsertResponse);

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
        public async void SegmentControllerUpsertReturnsSuccessForUpdate(string mediaTypeName)
        {
            // Arrange
            var existingModel = A.Fake<JobProfileOverviewSegmentModel>();
            existingModel.SequenceNumber = 123;

            var modelToUpsert = A.Fake<JobProfileOverviewSegmentModel>();
            modelToUpsert.SequenceNumber = 124;

            var controller = BuildSegmentController(mediaTypeName);

            var expectedUpsertResponse = HttpStatusCode.OK;

            A.CallTo(() => FakeJobProfileOverviewSegmentService.GetByIdAsync(A<Guid>.Ignored)).Returns(existingModel);
            A.CallTo(() => FakeJobProfileOverviewSegmentService.UpsertAsync(A<JobProfileOverviewSegmentModel>.Ignored)).Returns(expectedUpsertResponse);

            // Act
            var result = await controller.Put(modelToUpsert).ConfigureAwait(false);

            // Assert
            A.CallTo(() => FakeJobProfileOverviewSegmentService.UpsertAsync(A<JobProfileOverviewSegmentModel>.Ignored)).MustHaveHappenedOnceExactly();
            var okResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);

            controller.Dispose();
        }

        [Theory]
        [MemberData(nameof(JsonMediaTypes))]
        public async void SegmentControllerUpsertReturnsBadResultWhenModelIsNull(string mediaTypeName)
        {
            // Arrange
            var controller = BuildSegmentController(mediaTypeName);

            // Act
            var result = await controller.Put(null).ConfigureAwait(false);

            // Assert
            var statusResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, statusResult.StatusCode);

            controller.Dispose();
        }

        [Theory]
        [MemberData(nameof(JsonMediaTypes))]
        public async void SegmentControllerUpsertReturnsBadResultWhenModelIsInvalid(string mediaTypeName)
        {
            // Arrange
            var relatedCareersSegmentModel = new JobProfileOverviewSegmentModel();
            var controller = BuildSegmentController(mediaTypeName);

            controller.ModelState.AddModelError(string.Empty, "Model is not valid");

            // Act
            var result = await controller.Put(relatedCareersSegmentModel).ConfigureAwait(false);

            // Assert
            var statusResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, statusResult.StatusCode);

            controller.Dispose();
        }
    }
}