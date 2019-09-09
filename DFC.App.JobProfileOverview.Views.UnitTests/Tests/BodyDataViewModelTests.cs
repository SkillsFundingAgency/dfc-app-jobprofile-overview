using DFC.App.JobProfileOverview.Data.Models;
using DFC.App.JobProfileOverview.Views.UnitTests.Services;
using System.Collections.Generic;
using Xunit;

namespace DFC.App.JobProfileOverview.Views.UnitTests.Tests
{
    public class BodyDataViewModelTests : TestBase
    {
        [Fact]
        public void ViewContainsRenderedContent()
        {
            //Arrange
            var model = new JobProfileOverviewSegmentDataModel();
            model.Overview = "overview1";
            model.MinimumHours = 20;
            model.MaximumHours = 37.5M;
            model.SalaryStarter = 40;
            model.SalaryExperienced = 55;
            model.Title = "title1";
            model.WorkingHoursDetails = "WorkingHoursDetails1";
            model.WorkingPattern = "WorkingPattern1";
            model.WorkingPatternDetails = "WorkingPatternDetails1";

            var viewBag = new Dictionary<string, object>();
            var viewRenderer = new RazorEngineRenderer(ViewRootPath);

            //Act
            var viewRenderResponse = viewRenderer.Render(@"BodyData", model, viewBag);

            //Assert
            Assert.Contains(model.Overview, viewRenderResponse);
            Assert.Contains(model.MinimumHours.ToString(), viewRenderResponse);
            Assert.Contains(model.MaximumHours.ToString(), viewRenderResponse);
        }

        [Fact]
        public void ViewContainsFormattedCurrency()
        {
            //Arrange
            var model = new JobProfileOverviewSegmentDataModel();
            model.SalaryStarter = 40;
            model.SalaryExperienced = 55;

            var viewBag = new Dictionary<string, object>();
            var viewRenderer = new RazorEngineRenderer(ViewRootPath);

            //Act
            var viewRenderResponse = viewRenderer.Render(@"BodyData", model, viewBag);

            //Assert
            Assert.Contains(string.Concat(HtmlEncode("£"), model.SalaryStarter), viewRenderResponse);
            Assert.Contains(string.Concat(HtmlEncode("£"), model.SalaryExperienced), viewRenderResponse);
        }
  }
}
