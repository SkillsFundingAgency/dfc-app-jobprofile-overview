using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.AzureServiceBus.ServiceBusFactory;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Test
{
    public class JobProfileOverviewTest : SetUpAndTearDown
    {
        [Test]
        [Description("Tests that the CType 'JobProfileSoc' successfully tiggers an update to an existing job profile")]
        public async Task JobProfileOverviewJobProfileSOC()
        {
            var socCodeContentType = this.CommonAction.GenerateSOCCodeContentTypeForJobProfile(this.JobProfile);
            var messageBody = this.CommonAction.ConvertObjectToByteArray(socCodeContentType);
            var message = new MessageFactory().Create(this.JobProfile.JobProfileId, messageBody, "Published", "JobProfileSoc");
            await this.ServiceBus.SendMessage(message).ConfigureAwait(true);
            await Task.Delay(5000);
            var apiResponse = this.API.GetById(this.JobProfile.JobProfileId);
            Assert.AreEqual(socCodeContentType.SOCCode.Substring(0, 4), apiResponse.Data.SOC);
            Assert.AreEqual(socCodeContentType.ONetOccupationalCode, apiResponse.Data.ONetOccupationalCode);
        }

        [Test]
        [Description("Tests that the CType 'WorkingHoursDetails' successfully tiggers an update to an existing job profile")]
        public async Task JobProfileOverviewWorkingHoursDetails()
        {
            var workingHoursDetailsClassification = this.CommonAction.GenerateWorkingHoursDetailsClassificationForJobProfile(this.JobProfile);
            var messageBody = this.CommonAction.ConvertObjectToByteArray(workingHoursDetailsClassification);
            var message = new MessageFactory().Create(this.JobProfile.JobProfileId, messageBody, "Published", "WorkingHoursDetails");
            await this.ServiceBus.SendMessage(message).ConfigureAwait(true);
            await Task.Delay(5000);
            var apiResponse = this.API.GetById(this.JobProfile.JobProfileId);
            Assert.AreEqual(workingHoursDetailsClassification.Title, apiResponse.Data.WorkingHoursDetails);
        }

        [Test]
        [Description("Tests that the CType 'WorkingPattern' successfully tiggers an update to an existing job profile")]
        public async Task JobProfileOverviewWorkingPattern()
        {
            var workingPatternClassification = this.CommonAction.GenerateWorkingPatternClassificationForJobProfile(this.JobProfile);
            var messageBody = this.CommonAction.ConvertObjectToByteArray(workingPatternClassification);
            var message = new MessageFactory().Create(this.JobProfile.JobProfileId, messageBody, "Published", "WorkingPattern");
            await this.ServiceBus.SendMessage(message).ConfigureAwait(true);
            await Task.Delay(5000);
            var apiResponse = this.API.GetById(this.JobProfile.JobProfileId);
            Assert.AreEqual(workingPatternClassification.Title, apiResponse.Data.WorkingPattern);
        }

        [Test]
        [Description("Tests that the CType 'WorkingPatternDetails' successfully tiggers an update to an existing job profile")]
        public async Task JobProfileOverviewWorkingPatternDetails()
        {
            var workingPatternDetailClassification = this.CommonAction.GenerateWorkingPatternDetailsClassificationForJobProfile(this.JobProfile);
            var messageBody = this.CommonAction.ConvertObjectToByteArray(workingPatternDetailClassification);
            var message = new MessageFactory().Create(this.JobProfile.JobProfileId, messageBody, "Published", "WorkingPatternDetails");
            await this.ServiceBus.SendMessage(message).ConfigureAwait(true);
            await Task.Delay(5000);
            var apiResponse = this.API.GetById(this.JobProfile.JobProfileId);
            Assert.AreEqual(workingPatternDetailClassification.Title, apiResponse.Data.WorkingPatternDetails);
        }
    }
}