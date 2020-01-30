using DFC.App.JobProfileOverview.Data.Models;
using DFC.App.JobProfileOverview.ViewModels;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace DFC.App.JobProfileOverview.UnitTests.ControllerTests.SegmentControllerTests
{
    [Trait("Segment Controller", "Get By Name Tests")]
    public class SegmentControllerGetByNameTests : BaseSegmentController
    {
        private const string Article = "an-article-name";

        [Theory]
        [MemberData(nameof(HtmlMediaTypes))]
        public async Task ReturnsSuccessForHtmlMediaType(string mediaTypeName)
        {
            // Arrange
            var expectedResult = A.Fake<JobProfileOverviewSegmentModel>();
            var fakeBodyViewModel = A.Fake<BodyViewModel>();
            var controller = BuildSegmentController(mediaTypeName);

            expectedResult.Data = A.Fake<JobProfileOverviewSegmentDataModel>();
            fakeBodyViewModel.Data = A.Fake<BodyDataViewModel>();

            A.CallTo(() => FakeJobProfileOverviewSegmentService.GetByNameAsync(A<string>.Ignored)).Returns(expectedResult);
            A.CallTo(() => FakeMapper.Map<BodyViewModel>(A<JobProfileOverviewSegmentModel>.Ignored)).Returns(fakeBodyViewModel);

            // Act
            var result = await controller.GetByName(Article).ConfigureAwait(false);

            // Assert
            A.CallTo(() => FakeJobProfileOverviewSegmentService.GetByNameAsync(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => FakeMapper.Map<BodyViewModel>(A<JobProfileOverviewSegmentModel>.Ignored)).MustHaveHappenedOnceExactly();

            var viewResult = Assert.IsType<ViewResult>(result);
            var resultViewModel = Assert.IsAssignableFrom<BodyViewModel>(viewResult.ViewData.Model);
            Assert.Null(resultViewModel.Data.Breadcrumb);

            controller.Dispose();
        }

        [Theory]
        [MemberData(nameof(HtmlMediaTypes))]
        public async Task ReturnsNoContentWhenNoData(string mediaTypeName)
        {
            // Arrange
            JobProfileOverviewSegmentModel expectedResult = null;
            var controller = BuildSegmentController(mediaTypeName);

            A.CallTo(() => FakeJobProfileOverviewSegmentService.GetByNameAsync(A<string>.Ignored)).Returns(expectedResult);

            // Act
            var result = await controller.GetByName(Article).ConfigureAwait(false);

            // Assert
            A.CallTo(() => FakeJobProfileOverviewSegmentService.GetByNameAsync(A<string>.Ignored)).MustHaveHappenedOnceExactly();

            var statusResult = Assert.IsType<NotFoundResult>(result);

            Assert.Equal((int)HttpStatusCode.NotFound, statusResult.StatusCode);

            controller.Dispose();
        }
    }
}