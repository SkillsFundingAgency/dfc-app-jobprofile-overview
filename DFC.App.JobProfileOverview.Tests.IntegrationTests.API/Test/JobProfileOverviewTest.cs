using DFC.Api.JobProfiles.Common.APISupport;
using DFC.Api.JobProfiles.Common.AzureServiceBusSupport;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support;
using NUnit.Framework;
using System.Threading.Tasks;
using static DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.EnumLibrary;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Test
{
    public class JobProfileOverviewTest : SetUpAndTearDown
    {
        [Test]
        [Description("Tests that the CType 'JobProfileSoc' successfully tiggers an update to an existing job profile")]
        public async Task JobProfileOverview_JobProfileSOC()
        {
            SOCCodeContentType socCodeContentType = this.CommonAction.GenerateSOCCodeContentTypeForJobProfile(this.JobProfile);
            byte[] messageBody = this.CommonAction.ConvertObjectToByteArray(socCodeContentType);
            Message message = this.CommonAction.CreateServiceBusMessage(socCodeContentType.Id, messageBody, ContentType.JSON, ActionType.Published, CType.JobProfileSoc);
            await this.CommonAction.SendMessage(this.Topic, message);
            await Task.Delay(5000);
            string endpoint = Settings.APIConfig.EndpointBaseUrl.Replace("{id}", this.JobProfile.JobProfileId);
            Response<JobProfileOverviewAPIResponse> apiResponse = await this.CommonAction.ExecuteGetRequest<JobProfileOverviewAPIResponse>(endpoint, GetRequest.ContentType.Json);
            Assert.AreEqual(socCodeContentType.SOCCode.Substring(0, 4), apiResponse.Data.SOC);
            Assert.AreEqual(socCodeContentType.ONetOccupationalCode, apiResponse.Data.ONetOccupationalCode);
        }

        [Test]
        [Description("Tests that the CType 'WorkingHoursDetails' successfully tiggers an update to an existing job profile")]
        public async Task JobProfileOverview_WorkingHoursDetails()
        {
            WorkingHoursDetailsClassification workingHoursDetailsClassification = this.CommonAction.GenerateWorkingHoursDetailsClassificationForJobProfile(JobProfile);
            byte[] messageBody = this.CommonAction.ConvertObjectToByteArray(workingHoursDetailsClassification);
            Message message = this.CommonAction.CreateServiceBusMessage(workingHoursDetailsClassification.Id, messageBody, ContentType.JSON, ActionType.Published, CType.WorkingHoursDetails);
            await this.CommonAction.SendMessage(this.Topic, message);
            await Task.Delay(5000);
            string endpoint = Settings.APIConfig.EndpointBaseUrl.Replace("{id}", this.JobProfile.JobProfileId);
            Response<JobProfileOverviewAPIResponse> apiResponse = await this.CommonAction.ExecuteGetRequest<JobProfileOverviewAPIResponse>(endpoint, GetRequest.ContentType.Json);
            Assert.AreEqual(workingHoursDetailsClassification.Title, apiResponse.Data.WorkingHoursDetails);
        }

        [Test]
        [Description("Tests that the CType 'WorkingPattern' successfully tiggers an update to an existing job profile")]
        public async Task JobProfileOverview_WorkingPattern()
        {
            WorkingPatternClassification workingPatternClassification = this.CommonAction.GenerateWorkingPatternClassificationForJobProfile(JobProfile);
            byte[] messageBody = this.CommonAction.ConvertObjectToByteArray(workingPatternClassification);
            Message message = this.CommonAction.CreateServiceBusMessage(workingPatternClassification.Id, messageBody, ContentType.JSON, ActionType.Published, CType.WorkingPattern);
            await this.CommonAction.SendMessage(this.Topic, message);
            await Task.Delay(5000);
            string endpoint = Settings.APIConfig.EndpointBaseUrl.Replace("{id}", this.JobProfile.JobProfileId);
            Response<JobProfileOverviewAPIResponse> apiResponse = await this.CommonAction.ExecuteGetRequest<JobProfileOverviewAPIResponse>(endpoint, GetRequest.ContentType.Json);
            Assert.AreEqual(workingPatternClassification.Title, apiResponse.Data.WorkingPattern);
        }

        [Test]
        [Description("Tests that the CType 'WorkingPatternDetails' successfully tiggers an update to an existing job profile")]
        public async Task JobProfileOverview_WorkingPatternDetails()
        {
            WorkingPatternDetailClassification workingPatternDetailClassification = this.CommonAction.GenerateWorkingPatternDetailsClassificationForJobProfile(JobProfile);
            byte[] messageBody = this.CommonAction.ConvertObjectToByteArray(workingPatternDetailClassification);
            Message message = this.CommonAction.CreateServiceBusMessage(workingPatternDetailClassification.Id, messageBody, ContentType.JSON, ActionType.Published, CType.WorkingPatternDetails);
            await this.CommonAction.SendMessage(this.Topic, message);
            await Task.Delay(5000);
            string endpoint = Settings.APIConfig.EndpointBaseUrl.Replace("{id}", this.JobProfile.JobProfileId);
            Response<JobProfileOverviewAPIResponse> apiResponse = await this.CommonAction.ExecuteGetRequest<JobProfileOverviewAPIResponse>(endpoint, GetRequest.ContentType.Json);
            Assert.AreEqual(workingPatternDetailClassification.Title, apiResponse.Data.WorkingPatternDetails);
        }
    }
}