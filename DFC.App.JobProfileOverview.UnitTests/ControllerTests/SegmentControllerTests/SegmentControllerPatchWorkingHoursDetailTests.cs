using DFC.App.JobProfileOverview.Data.Models.PatchModels;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace DFC.App.JobProfileOverview.UnitTests.ControllerTests.SegmentControllerTests
{
    [Trait("Segment Controller", "Patch Working Hours Detail Tests")]
    public class SegmentControllerPatchWorkingHoursDetailTests : BaseSegmentController
    {
        [Theory]
        [MemberData(nameof(JsonMediaTypes))]
        public async Task SegmentControllerPatchWorkingHoursDetailReturnsSuccess(string mediaTypeName)
        {
            // Arrange
            const HttpStatusCode expectedResponse = HttpStatusCode.OK;
            var patchModel = A.Fake<PatchWorkingHoursDetailModel>();
            var documentId = Guid.NewGuid();
            var controller = BuildSegmentController(mediaTypeName);

            A.CallTo(() => FakeJobProfileOverviewSegmentService.PatchWorkingHoursDetailAsync(patchModel, documentId)).Returns(expectedResponse);

            // Act
            var result = await controller.PatchWorkingHoursDetail(patchModel, documentId).ConfigureAwait(false);

            // Assert
            A.CallTo(() => FakeJobProfileOverviewSegmentService.PatchWorkingHoursDetailAsync(patchModel, documentId)).MustHaveHappenedOnceExactly();
            var statusResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal((int)expectedResponse, statusResult.StatusCode);

            controller.Dispose();
        }

        [Theory]
        [MemberData(nameof(JsonMediaTypes))]
        public async Task SegmentControllerPatchWorkingHoursDetailReturnsNotFound(string mediaTypeName)
        {
            // Arrange
            const HttpStatusCode expectedResponse = HttpStatusCode.NotFound;
            var patchModel = A.Fake<PatchWorkingHoursDetailModel>();
            var documentId = Guid.NewGuid();
            var controller = BuildSegmentController(mediaTypeName);

            A.CallTo(() => FakeJobProfileOverviewSegmentService.PatchWorkingHoursDetailAsync(patchModel, documentId)).Returns(expectedResponse);

            // Act
            var result = await controller.PatchWorkingHoursDetail(patchModel, documentId).ConfigureAwait(false);

            // Assert
            A.CallTo(() => FakeJobProfileOverviewSegmentService.PatchWorkingHoursDetailAsync(patchModel, documentId)).MustHaveHappenedOnceExactly();
            var statusResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal((int)expectedResponse, statusResult.StatusCode);

            controller.Dispose();
        }

        [Theory]
        [MemberData(nameof(JsonMediaTypes))]
        public async Task SegmentControllerPatchWorkingHoursDetailReturnsBadRequestWhenNullPatchmodel(string mediaTypeName)
        {
            // Arrange
            const HttpStatusCode expectedResponse = HttpStatusCode.BadRequest;
            PatchWorkingHoursDetailModel patchModel = null;
            var documentId = Guid.NewGuid();
            var controller = BuildSegmentController(mediaTypeName);

            // Act
            var result = await controller.PatchWorkingHoursDetail(patchModel, documentId).ConfigureAwait(false);

            // Assert
            var statusResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal((int)expectedResponse, statusResult.StatusCode);

            controller.Dispose();
        }

        [Theory]
        [MemberData(nameof(JsonMediaTypes))]
        public async Task SegmentControllerPatchWorkingHoursDetailReturnsBadRequestWhenInvalidPatchmodel(string mediaTypeName)
        {
            // Arrange
            const HttpStatusCode expectedResponse = HttpStatusCode.BadRequest;
            var patchModel = A.Fake<PatchWorkingHoursDetailModel>();
            var documentId = Guid.NewGuid();
            var controller = BuildSegmentController(mediaTypeName);

            controller.ModelState.AddModelError(string.Empty, "Model is not valid");

            // Act
            var result = await controller.PatchWorkingHoursDetail(patchModel, documentId).ConfigureAwait(false);

            // Assert
            var statusResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal((int)expectedResponse, statusResult.StatusCode);

            controller.Dispose();
        }
    }
}