using DFC.App.JobProfileOverview.Data.Models;
using DFC.App.JobProfileOverview.ViewModels;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Xunit;

namespace DFC.App.JobProfileOverview.UnitTests.ControllerTests.SegmentControllerTests
{
    [Trait("Segment Controller", "Document Tests")]
    public class SegmentControllerDocumentTests : BaseSegmentController
    {
        [Theory]
        [MemberData(nameof(HtmlMediaTypes))]
        public async void ReturnsSuccessForHtmlMediaType(string mediaTypeName)
        {
            // Arrange
            const string article = "an-article-name";
            var expectedResult = A.Fake<JobProfileOverviewSegmentModel>();
            var fakeBodyViewModel = A.Fake<BodyViewModel>();
            var controller = BuildSegmentController(mediaTypeName);

            expectedResult.Data = A.Fake<JobProfileOverviewSegmentDataModel>();
            fakeBodyViewModel.Data = A.Fake<BodyDataViewModel>();

            A.CallTo(() => FakeJobProfileOverviewSegmentService.GetByNameAsync(A<string>.Ignored)).Returns(expectedResult);
            A.CallTo(() => FakeMapper.Map<BodyViewModel>(A<JobProfileOverviewSegmentModel>.Ignored)).Returns(fakeBodyViewModel);

            // Act
            var result = await controller.Document(article).ConfigureAwait(false);

            // Assert
            A.CallTo(() => FakeJobProfileOverviewSegmentService.GetByNameAsync(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => FakeMapper.Map<BodyViewModel>(A<JobProfileOverviewSegmentModel>.Ignored)).MustHaveHappenedOnceExactly();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<BodyViewModel>(viewResult.ViewData.Model);

            controller.Dispose();
        }

        [Theory]
        [MemberData(nameof(HtmlMediaTypes))]
        public async void ReturnsNoContentWhenNoData(string mediaTypeName)
        {
            // Arrange
            const string article = "an-article-name";
            JobProfileOverviewSegmentModel expectedResult = null;
            var controller = BuildSegmentController(mediaTypeName);

            A.CallTo(() => FakeJobProfileOverviewSegmentService.GetByNameAsync(A<string>.Ignored)).Returns(expectedResult);

            // Act
            var result = await controller.Document(article).ConfigureAwait(false);

            // Assert
            A.CallTo(() => FakeJobProfileOverviewSegmentService.GetByNameAsync(A<string>.Ignored)).MustHaveHappenedOnceExactly();

            var statusResult = Assert.IsType<NoContentResult>(result);

            A.Equals((int)HttpStatusCode.NoContent, statusResult.StatusCode);

            controller.Dispose();
        }
    }
}