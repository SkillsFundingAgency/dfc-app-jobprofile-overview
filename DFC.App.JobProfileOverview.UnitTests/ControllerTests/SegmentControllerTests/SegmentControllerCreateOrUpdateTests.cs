using DFC.App.JobProfileOverview.Data.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using Xunit;

namespace DFC.App.JobProfileOverview.UnitTests.ControllerTests.SegmentControllerTests
{
    public class SegmentControllerCreateOrUpdateTests : BaseSegmentController
    {
        [Theory]
        [MemberData(nameof(JsonMediaTypes))]
        public async void CreateOrUpdateReturnsSuccessForCreate(string mediaTypeName)
        {
            // Arrange
            var overviewSegmentModel = A.Fake<JobProfileOverviewSegmentModel>();
            JobProfileOverviewSegmentModel existingJobProfileOverviewSegmentModel = null;
            var createdJobProfileOverviewSegmentModel = A.Fake<JobProfileOverviewSegmentModel>();
            var controller = BuildSegmentController(mediaTypeName);

            A.CallTo(() => FakeJobProfileOverviewSegmentService.GetByIdAsync(A<Guid>.Ignored)).Returns(existingJobProfileOverviewSegmentModel);
            A.CallTo(() => FakeJobProfileOverviewSegmentService.CreateAsync(A<JobProfileOverviewSegmentModel>.Ignored)).Returns(createdJobProfileOverviewSegmentModel);

            // Act
            var result = await controller.CreateOrUpdate(overviewSegmentModel).ConfigureAwait(false);

            // Assert
            A.CallTo(() => FakeJobProfileOverviewSegmentService.GetByIdAsync(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => FakeJobProfileOverviewSegmentService.CreateAsync(A<JobProfileOverviewSegmentModel>.Ignored)).MustHaveHappenedOnceExactly();

            var okResult = Assert.IsType<CreatedAtActionResult>(result);

            A.Equals((int)HttpStatusCode.Created, okResult.StatusCode);

            controller.Dispose();
        }

        [Theory]
        [MemberData(nameof(JsonMediaTypes))]
        public async void CreateOrUpdateReturnsSuccessForUpdate(string mediaTypeName)
        {
            // Arrange
            var jobProfileOverviewSegmentModel = A.Fake<JobProfileOverviewSegmentModel>();
            var existingJobProfileOverviewSegmentModel = A.Fake<JobProfileOverviewSegmentModel>();
            JobProfileOverviewSegmentModel updatedOverviewSegmentModel = null;
            var controller = BuildSegmentController(mediaTypeName);

            A.CallTo(() => FakeJobProfileOverviewSegmentService.GetByIdAsync(A<Guid>.Ignored)).Returns(existingJobProfileOverviewSegmentModel);
            A.CallTo(() => FakeJobProfileOverviewSegmentService.ReplaceAsync(A<JobProfileOverviewSegmentModel>.Ignored)).Returns(updatedOverviewSegmentModel);

            // Act
            var result = await controller.CreateOrUpdate(jobProfileOverviewSegmentModel).ConfigureAwait(false);

            // Assert
            A.CallTo(() => FakeJobProfileOverviewSegmentService.GetByIdAsync(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => FakeJobProfileOverviewSegmentService.ReplaceAsync(A<JobProfileOverviewSegmentModel>.Ignored)).MustHaveHappenedOnceExactly();

            var okResult = Assert.IsType<OkObjectResult>(result);

            A.Equals((int)HttpStatusCode.OK, okResult.StatusCode);

            controller.Dispose();
        }

        [Theory]
        [MemberData(nameof(JsonMediaTypes))]
        public async void CreateOrUpdateReturnsBadResultWhenModelIsNull(string mediaTypeName)
        {
            // Arrange
            JobProfileOverviewSegmentModel jobProfileOverviewSegmentModel = null;
            var controller = BuildSegmentController(mediaTypeName);

            // Act
            var result = await controller.CreateOrUpdate(jobProfileOverviewSegmentModel).ConfigureAwait(false);

            // Assert
            var statusResult = Assert.IsType<BadRequestResult>(result);

            A.Equals((int)HttpStatusCode.BadRequest, statusResult.StatusCode);

            controller.Dispose();
        }

        [Theory]
        [MemberData(nameof(JsonMediaTypes))]
        public async void CreateOrUpdateReturnsBadResultWhenModelIsInvalid(string mediaTypeName)
        {
            // Arrange
            var jobProfileOverviewSegmentModel = new JobProfileOverviewSegmentModel();
            var controller = BuildSegmentController(mediaTypeName);

            controller.ModelState.AddModelError(string.Empty, "Model is not valid");

            // Act
            var result = await controller.CreateOrUpdate(jobProfileOverviewSegmentModel).ConfigureAwait(false);

            // Assert
            var statusResult = Assert.IsType<BadRequestObjectResult>(result);

            A.Equals((int)HttpStatusCode.BadRequest, statusResult.StatusCode);

            controller.Dispose();
        }
    }
}
