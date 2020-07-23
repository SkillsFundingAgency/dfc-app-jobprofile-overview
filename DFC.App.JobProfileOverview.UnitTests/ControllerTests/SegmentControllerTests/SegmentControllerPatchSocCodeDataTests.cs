using DFC.App.JobProfileOverview.Data.Models.PatchModels;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace DFC.App.JobProfileOverview.UnitTests.ControllerTests.SegmentControllerTests
{
    [Trait("Segment Controller", "Patch Soc Code Data Tests")]
    public class SegmentControllerPatchSocCodeDataTests : BaseSegmentController
    {
        [Theory]
        [MemberData(nameof(JsonMediaTypes))]
        public async Task SegmentControllerPatchSocCodeDataReturnsSuccess(string mediaTypeName)
        {
            // Arrange
            const HttpStatusCode expectedResponse = HttpStatusCode.OK;
            var patchModel = A.Fake<PatchSocDataModel>();
            var documentId = Guid.NewGuid();
            var controller = BuildSegmentController(mediaTypeName);

            A.CallTo(() => FakeJobProfileOverviewSegmentService.PatchSocCodeDataAsync(patchModel, documentId)).Returns(expectedResponse);

            // Act
            var result = await controller.PatchSocCodeData(patchModel, documentId).ConfigureAwait(false);

            // Assert
            A.CallTo(() => FakeJobProfileOverviewSegmentService.PatchSocCodeDataAsync(patchModel, documentId)).MustHaveHappenedOnceExactly();
            var statusResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal((int)expectedResponse, statusResult.StatusCode);

            controller.Dispose();
        }

        [Theory]
        [MemberData(nameof(JsonMediaTypes))]
        public async Task SegmentControllerPatchSocCodeDataReturnsNotFound(string mediaTypeName)
        {
            // Arrange
            const HttpStatusCode expectedResponse = HttpStatusCode.NotFound;
            var patchModel = A.Fake<PatchSocDataModel>();
            var documentId = Guid.NewGuid();
            var controller = BuildSegmentController(mediaTypeName);

            A.CallTo(() => FakeJobProfileOverviewSegmentService.PatchSocCodeDataAsync(patchModel, documentId)).Returns(expectedResponse);

            // Act
            var result = await controller.PatchSocCodeData(patchModel, documentId).ConfigureAwait(false);

            // Assert
            A.CallTo(() => FakeJobProfileOverviewSegmentService.PatchSocCodeDataAsync(patchModel, documentId)).MustHaveHappenedOnceExactly();
            var statusResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal((int)expectedResponse, statusResult.StatusCode);

            controller.Dispose();
        }

        [Theory]
        [MemberData(nameof(JsonMediaTypes))]
        public async Task SegmentControllerPatchSocCodeDataReturnsBadRequestWhenNullPatchmodel(string mediaTypeName)
        {
            // Arrange
            const HttpStatusCode expectedResponse = HttpStatusCode.BadRequest;
            PatchSocDataModel patchModel = null;
            var documentId = Guid.NewGuid();
            var controller = BuildSegmentController(mediaTypeName);

            // Act
            var result = await controller.PatchSocCodeData(patchModel, documentId).ConfigureAwait(false);

            // Assert
            var statusResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal((int)expectedResponse, statusResult.StatusCode);

            controller.Dispose();
        }

        [Theory]
        [MemberData(nameof(JsonMediaTypes))]
        public async Task SegmentControllerPatchSocCodeDataReturnsBadRequestWhenInvalidPatchmodel(string mediaTypeName)
        {
            // Arrange
            const HttpStatusCode expectedResponse = HttpStatusCode.BadRequest;
            var patchModel = A.Fake<PatchSocDataModel>();
            var documentId = Guid.NewGuid();
            var controller = BuildSegmentController(mediaTypeName);

            controller.ModelState.AddModelError(string.Empty, "Model is not valid");

            // Act
            var result = await controller.PatchSocCodeData(patchModel, documentId).ConfigureAwait(false);

            // Assert
            var statusResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal((int)expectedResponse, statusResult.StatusCode);

            controller.Dispose();
        }
    }
}