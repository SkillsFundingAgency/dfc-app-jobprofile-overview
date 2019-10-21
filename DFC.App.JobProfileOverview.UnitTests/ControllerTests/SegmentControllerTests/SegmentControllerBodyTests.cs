using DFC.App.JobProfileOverview.Data.Models;
using DFC.App.JobProfileOverview.ViewModels;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Xunit;

namespace DFC.App.JobProfileOverview.UnitTests.ControllerTests.SegmentControllerTests
{
    public class SegmentControllerBodyTests : BaseSegmentController
    {
        private const string ArticleName = "an-article-name";

        [Theory]
        [MemberData(nameof(HtmlMediaTypes))]
        public async void ReturnsSuccessForHtmlMediaType(string mediaTypeName)
        {
            // Arrange
            var expectedResult = A.Fake<JobProfileOverviewSegmentModel>();
            var controller = BuildSegmentController(mediaTypeName);

            expectedResult.CanonicalName = ArticleName;

            A.CallTo(() => FakeJobProfileOverviewSegmentService.GetByNameAsync(A<string>.Ignored, A<bool>.Ignored)).Returns(expectedResult);
            A.CallTo(() => FakeMapper.Map<BodyViewModel>(A<JobProfileOverviewSegmentModel>.Ignored)).Returns(A.Fake<BodyViewModel>());

            // Act
            var result = await controller.Body(ArticleName).ConfigureAwait(false);

            // Assert
            A.CallTo(() => FakeJobProfileOverviewSegmentService.GetByNameAsync(A<string>.Ignored, A<bool>.Ignored)).MustHaveHappenedOnceExactly();
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
            const string article = "an-article-name";
            var expectedResult = A.Fake<JobProfileOverviewSegmentModel>();
            var controller = BuildSegmentController(mediaTypeName);

            expectedResult.CanonicalName = article;

            A.CallTo(() => FakeJobProfileOverviewSegmentService.GetByNameAsync(A<string>.Ignored, A<bool>.Ignored)).Returns(expectedResult);
            A.CallTo(() => FakeMapper.Map<BodyViewModel>(A<JobProfileOverviewSegmentModel>.Ignored)).Returns(A.Fake<BodyViewModel>());

            // Act
            var result = await controller.Body(article).ConfigureAwait(false);

            // Assert
            A.CallTo(() => FakeJobProfileOverviewSegmentService.GetByNameAsync(A<string>.Ignored, A<bool>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => FakeMapper.Map<BodyViewModel>(A<JobProfileOverviewSegmentModel>.Ignored)).MustHaveHappenedOnceExactly();

            var jsonResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<BodyViewModel>(jsonResult.Value);

            controller.Dispose();
        }

        [Theory]
        [MemberData(nameof(InvalidMediaTypes))]
        public async void ReturnsNotAcceptableForInvalidMediaType(string mediaTypeName)
        {
            // Arrange
            const string article = "an-article-name";
            var expectedResult = A.Fake<JobProfileOverviewSegmentModel>();
            var controller = BuildSegmentController(mediaTypeName);

            expectedResult.CanonicalName = article;

            A.CallTo(() => FakeJobProfileOverviewSegmentService.GetByNameAsync(A<string>.Ignored, A<bool>.Ignored)).Returns(expectedResult);
            A.CallTo(() => FakeMapper.Map<BodyViewModel>(A<JobProfileOverviewSegmentModel>.Ignored)).Returns(A.Fake<BodyViewModel>());

            // Act
            var result = await controller.Body(article).ConfigureAwait(false);

            // Assert
            A.CallTo(() => FakeJobProfileOverviewSegmentService.GetByNameAsync(A<string>.Ignored, A<bool>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => FakeMapper.Map<BodyViewModel>(A<JobProfileOverviewSegmentModel>.Ignored)).MustHaveHappenedOnceExactly();

            var statusResult = Assert.IsType<StatusCodeResult>(result);

            Assert.Equal((int)HttpStatusCode.NotAcceptable, statusResult.StatusCode);

            controller.Dispose();
        }
    }
}