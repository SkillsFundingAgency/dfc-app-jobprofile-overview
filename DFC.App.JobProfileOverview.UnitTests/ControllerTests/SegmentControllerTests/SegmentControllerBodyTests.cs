using DFC.App.JobProfileOverview.Data.Models;
using DFC.App.JobProfileOverview.ViewModels;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using Xunit;

namespace DFC.App.JobProfileOverview.UnitTests.ControllerTests.SegmentControllerTests
{
    public class SegmentControllerBodyTests : BaseSegmentController
    {
        private const string ArticleName = "an-article-name";
        private readonly Guid documentId = Guid.NewGuid();

        [Theory]
        [MemberData(nameof(HtmlMediaTypes))]
        public async void ReturnsSuccessForHtmlMediaType(string mediaTypeName)
        {
            // Arrange
            var expectedResult = A.Fake<JobProfileOverviewSegmentModel>();
            var fakeBodyViewModel = A.Fake<BodyViewModel>();
            var controller = BuildSegmentController(mediaTypeName);

            expectedResult.CanonicalName = ArticleName;
            expectedResult.Data = A.Fake<JobProfileOverviewSegmentDataModel>();
            fakeBodyViewModel.Data = A.Fake<BodyDataViewModel>();

            A.CallTo(() => FakeJobProfileOverviewSegmentService.GetByIdAsync(A<Guid>.Ignored)).Returns(expectedResult);
            A.CallTo(() => FakeMapper.Map<BodyViewModel>(A<JobProfileOverviewSegmentModel>.Ignored)).Returns(fakeBodyViewModel);

            // Act
            var result = await controller.Body(documentId).ConfigureAwait(false);

            // Assert
            A.CallTo(() => FakeJobProfileOverviewSegmentService.GetByIdAsync(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => FakeMapper.Map<BodyViewModel>(A<JobProfileOverviewSegmentModel>.Ignored)).MustHaveHappenedOnceExactly();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<BodyViewModel>(viewResult.ViewData.Model);

            controller.Dispose();
        }

        [Theory]
        [MemberData(nameof(JsonMediaTypes))]
        public async void ReturnsSuccessForJsonMediaType(string mediaTypeName)
        {
            // Arrange
            var expectedResult = A.Fake<JobProfileOverviewSegmentModel>();
            var fakeBodyViewModel = A.Fake<BodyViewModel>();
            var controller = BuildSegmentController(mediaTypeName);

            expectedResult.CanonicalName = ArticleName;
            expectedResult.Data = A.Fake<JobProfileOverviewSegmentDataModel>();
            fakeBodyViewModel.Data = A.Fake<BodyDataViewModel>();

            A.CallTo(() => FakeJobProfileOverviewSegmentService.GetByIdAsync(A<Guid>.Ignored)).Returns(expectedResult);
            A.CallTo(() => FakeMapper.Map<BodyViewModel>(A<JobProfileOverviewSegmentModel>.Ignored)).Returns(fakeBodyViewModel);

            // Act
            var result = await controller.Body(documentId).ConfigureAwait(false);

            // Assert
            A.CallTo(() => FakeJobProfileOverviewSegmentService.GetByIdAsync(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => FakeMapper.Map<BodyViewModel>(A<JobProfileOverviewSegmentModel>.Ignored)).MustHaveHappenedOnceExactly();

            var jsonResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<JobProfileOverviewSegmentDataModel>(jsonResult.Value);

            controller.Dispose();
        }

        [Theory]
        [MemberData(nameof(InvalidMediaTypes))]
        public async void ReturnsNotAcceptableForInvalidMediaType(string mediaTypeName)
        {
            // Arrange
            var expectedResult = A.Fake<JobProfileOverviewSegmentModel>();
            var fakeBodyViewModel = A.Fake<BodyViewModel>();
            var controller = BuildSegmentController(mediaTypeName);

            expectedResult.CanonicalName = ArticleName;
            expectedResult.Data = A.Fake<JobProfileOverviewSegmentDataModel>();
            fakeBodyViewModel.Data = A.Fake<BodyDataViewModel>();

            A.CallTo(() => FakeJobProfileOverviewSegmentService.GetByIdAsync(A<Guid>.Ignored)).Returns(expectedResult);
            A.CallTo(() => FakeMapper.Map<BodyViewModel>(A<JobProfileOverviewSegmentModel>.Ignored)).Returns(fakeBodyViewModel);

            // Act
            var result = await controller.Body(documentId).ConfigureAwait(false);

            // Assert
            A.CallTo(() => FakeJobProfileOverviewSegmentService.GetByIdAsync(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => FakeMapper.Map<BodyViewModel>(A<JobProfileOverviewSegmentModel>.Ignored)).MustHaveHappenedOnceExactly();

            var statusResult = Assert.IsType<StatusCodeResult>(result);

            Assert.Equal((int)HttpStatusCode.NotAcceptable, statusResult.StatusCode);

            controller.Dispose();
        }
    }
}