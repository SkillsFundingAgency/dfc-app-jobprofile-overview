using DFC.App.JobProfileOverview.Data.Models;
using DFC.App.JobProfileOverview.ViewModels;
using DFC.App.JobProfileOverview.Views.UnitTests.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using Xunit;

namespace DFC.App.JobProfileOverview.Views.UnitTests.Tests
{
    public class BodyDataViewModelTests : TestBase
    {
        [Fact]
        public void ViewContainsRenderedContent()
        {
            //Arrange
            var model = new BodyDataViewModel
            {
                Overview = "overview1",
                MinimumHours = 20,
                MaximumHours = 37.5M,
                SalaryStarter = 40,
                SalaryExperienced = 55,
                Title = "title1",
                WorkingHoursDetails = new List<WorkingHoursDetail> { new WorkingHoursDetail { Id = Guid.NewGuid(), Title = "Title 1", Description = "WorkingHoursDetails1", Url = "http://something.com" } },
                WorkingPattern = new List<WorkingPattern> { new WorkingPattern { Id = Guid.NewGuid(), Title = "Title 1", Description = "WorkingPattern1", Url = "http://something.com" } },
                WorkingPatternDetails = new List<WorkingPatternDetail> { new WorkingPatternDetail { Id = Guid.NewGuid(), Title = "Title 1", Description = "WorkingPattern1", Url = "http://something.com" } },
            };

            var viewBag = new Dictionary<string, object>();
            var viewRenderer = new RazorEngineRenderer(ViewRootPath);

            //Act
            var viewRenderResponse = viewRenderer.Render(@"BodyData", model, viewBag);

            //Assert
            Assert.Contains(model.Overview, viewRenderResponse, StringComparison.OrdinalIgnoreCase);
            Assert.Contains(model.MinimumHours.ToString(CultureInfo.InvariantCulture), viewRenderResponse, StringComparison.OrdinalIgnoreCase);
            Assert.Contains(model.MaximumHours.ToString(CultureInfo.InvariantCulture), viewRenderResponse, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void ViewContainsCurrencySymbol()
        {
            //Arrange
            var model = new BodyDataViewModel
            {
                SalaryStarter = 40,
                SalaryExperienced = 55,
            };

            var viewBag = new Dictionary<string, object>();
            var viewRenderer = new RazorEngineRenderer(ViewRootPath);

            //Act
            var viewRenderResponse = viewRenderer.Render(@"BodyData", model, viewBag);

            //Assert
            Assert.Contains(string.Concat(HtmlEncode(CurrencySymbol), model.SalaryStarter), viewRenderResponse, StringComparison.OrdinalIgnoreCase);
            Assert.Contains(string.Concat(HtmlEncode(CurrencySymbol), model.SalaryExperienced), viewRenderResponse, StringComparison.OrdinalIgnoreCase);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SalaryValidationViewTests(bool isVariableSalary)
        {
            //Arrange
            var model = new BodyDataViewModel
            {
                SalaryStarter = 40,
                SalaryExperienced = 55,
            };

            if (isVariableSalary)
            {
                model.SalaryStarter = 0;
                model.SalaryExperienced = 0;
            }

            var viewBag = new Dictionary<string, object>();
            var viewRenderer = new RazorEngineRenderer(ViewRootPath);

            //Act
            var viewRenderResponse = viewRenderer.Render(@"BodyData", model, viewBag);

            //Assert
            if (isVariableSalary)
            {
                Assert.Contains("Variable", viewRenderResponse, StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                Assert.Contains(string.Concat(HtmlEncode(CurrencySymbol), model.SalaryStarter), viewRenderResponse, StringComparison.OrdinalIgnoreCase);
                Assert.Contains(string.Concat(HtmlEncode(CurrencySymbol), model.SalaryExperienced), viewRenderResponse, StringComparison.OrdinalIgnoreCase);
            }
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void WorkingHrsValidationViewTests(bool isVariableWorkingHrs)
        {
            //Arrange
            var model = new BodyDataViewModel
            {
                MinimumHours = 40,
                MaximumHours = 55,
            };

            if (isVariableWorkingHrs)
            {
                model.MinimumHours = 0;
                model.MaximumHours = 0;
            }

            var viewBag = new Dictionary<string, object>();
            var viewRenderer = new RazorEngineRenderer(ViewRootPath);

            //Act
            var viewRenderResponse = viewRenderer.Render(@"BodyData", model, viewBag);

            //Assert
            if (isVariableWorkingHrs)
            {
                Assert.Contains("Variable", viewRenderResponse, StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                Assert.Contains(model.MinimumHours.ToString(new NumberFormatInfo()), viewRenderResponse, StringComparison.OrdinalIgnoreCase);
                Assert.Contains(model.MaximumHours.ToString(new NumberFormatInfo()), viewRenderResponse, StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}