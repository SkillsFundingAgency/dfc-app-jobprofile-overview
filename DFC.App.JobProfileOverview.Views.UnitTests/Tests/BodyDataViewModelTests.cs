using DFC.App.JobProfileOverview.Data.Models;
using DFC.App.JobProfileOverview.Views.UnitTests.Services;
using System;
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
            var model = new JobProfileOverviewSegmentDataModel()
            {
                Overview = "overview1",
                MinimumHours = 20,
                MaximumHours = 37.5M,
                SalaryStarter = 40,
                SalaryExperienced = 55,
                Title = "title1",
                WorkingHoursDetails = "WorkingHoursDetails1",
                WorkingPattern = "WorkingPattern1",
                WorkingPatternDetails = "WorkingPatternDetails1",
            };

            var viewBag = new Dictionary<string, object>();
            var viewRenderer = new RazorEngineRenderer(ViewRootPath);

            //Act
            var viewRenderResponse = viewRenderer.Render(@"BodyData", model, viewBag);

            //Assert
            Assert.Contains(model.Overview, viewRenderResponse, StringComparison.OrdinalIgnoreCase);
            Assert.Contains(model.MinimumHours.ToString(), viewRenderResponse, StringComparison.OrdinalIgnoreCase);
            Assert.Contains(model.MaximumHours.ToString(), viewRenderResponse, StringComparison.OrdinalIgnoreCase);
        }

        [Fact(Skip ="Not working in the pipeline. Needs further investigation.")]
        public void ViewContainsFormattedCurrency()
        {
            //Arrange
            var model = new JobProfileOverviewSegmentDataModel()
            {
                SalaryStarter = 40,
                SalaryExperienced = 55,
            };

            var viewBag = new Dictionary<string, object>();
            var viewRenderer = new RazorEngineRenderer(ViewRootPath);

            //Act
            var viewRenderResponse = viewRenderer.Render(@"BodyData", model, viewBag);

            //Assert
            Assert.Contains(string.Concat(HtmlEncode("£"), model.SalaryStarter), viewRenderResponse, StringComparison.OrdinalIgnoreCase);
            Assert.Contains(string.Concat(HtmlEncode("£"), model.SalaryExperienced), viewRenderResponse, StringComparison.OrdinalIgnoreCase);
        }
    }
}
