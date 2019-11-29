using DFC.App.JobProfileOverview.ViewModels;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace DFC.App.JobProfileOverview.UnitTests.ControllerTests.HomeControllerTests
{
    public class HomeControllerErrorTests : BaseHomeController
    {
        [Theory]
        [MemberData(nameof(HtmlMediaTypes))]
        public void HomeControllerErrorHtmlReturnsSuccess(string mediaTypeName)
        {
            // Arrange
            var controller = BuildHomeController(mediaTypeName);

            // Act
            var result = controller.Error();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            _ = Assert.IsAssignableFrom<ErrorViewModel>(viewResult.ViewData.Model);

            controller.Dispose();
        }
    }
}
