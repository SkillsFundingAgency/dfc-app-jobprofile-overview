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
            SOCCodeContentType socCodeContentType = CommonAction.GenerateSOCCodeContentTypeForJobProfile(JobProfile);
            byte[] messageBody = CommonAction.ConvertObjectToByteArray(socCodeContentType);
            Message message = CommonAction.CreateServiceBusMessage(socCodeContentType.Id, messageBody, ContentType.JSON, ActionType.Published, CType.JobProfileSoc);
            await CommonAction.SendMessage(Topic, message);
            await Task.Delay(5000);
            string endpoint = Settings.APIConfig.EndpointBaseUrl.Replace("{id}", JobProfile.JobProfileId);
            Response<JobProfileOverviewAPIResponse> apiResponse = await CommonAction.ExecuteGetRequest<JobProfileOverviewAPIResponse>(endpoint, GetRequest.ContentType.Json);
            Assert.AreEqual(socCodeContentType.SOCCode.Substring(0, 4), apiResponse.Data.soc);
            Assert.AreEqual(socCodeContentType.ONetOccupationalCode, apiResponse.Data.oNetOccupationalCode);
        }

        [Test]
        [Description("Tests that the CType 'WorkingHoursDetails' successfully tiggers an update to an existing job profile")]
        public async Task JobProfileOverview_WorkingHoursDetails()
        {
            WorkingHoursDetailsClassification workingHoursDetailsClassification = CommonAction.GenerateWorkingHoursDetailsClassificationForJobProfile(JobProfile);
            byte[] messageBody = CommonAction.ConvertObjectToByteArray(workingHoursDetailsClassification);
            Message message = CommonAction.CreateServiceBusMessage(workingHoursDetailsClassification.Id, messageBody, ContentType.JSON, ActionType.Published, CType.WorkingHoursDetails);
            await CommonAction.SendMessage(Topic, message);
            await Task.Delay(5000);
            string endpoint = Settings.APIConfig.EndpointBaseUrl.Replace("{id}", JobProfile.JobProfileId);
            Response<JobProfileOverviewAPIResponse> apiResponse = await CommonAction.ExecuteGetRequest<JobProfileOverviewAPIResponse>(endpoint, GetRequest.ContentType.Json);
            Assert.AreEqual(workingHoursDetailsClassification.Title, apiResponse.Data.workingHoursDetails);
        }

        [Test]
        [Description("Tests that the CType 'WorkingPattern' successfully tiggers an update to an existing job profile")]
        public async Task JobProfileOverview_WorkingPattern()
        {
            WorkingPatternClassification workingPatternClassification = CommonAction.GenerateWorkingPatternClassificationForJobProfile(JobProfile);
            byte[] messageBody = CommonAction.ConvertObjectToByteArray(workingPatternClassification);
            Message message = CommonAction.CreateServiceBusMessage(workingPatternClassification.Id, messageBody, ContentType.JSON, ActionType.Published, CType.WorkingPattern);
            await CommonAction.SendMessage(Topic, message);
            await Task.Delay(5000);
            string endpoint = Settings.APIConfig.EndpointBaseUrl.Replace("{id}", JobProfile.JobProfileId);
            Response<JobProfileOverviewAPIResponse> apiResponse = await CommonAction.ExecuteGetRequest<JobProfileOverviewAPIResponse>(endpoint, GetRequest.ContentType.Json);
            Assert.AreEqual(workingPatternClassification.Title, apiResponse.Data.workingPattern);
        }

        [Test]
        [Description("Tests that the CType 'WorkingPatternDetails' successfully tiggers an update to an existing job profile")]
        public async Task JobProfileOverview_WorkingPatternDetails()
        {
            WorkingPatternDetailClassification workingPatternDetailClassification = CommonAction.GenerateWorkingPatternDetailsClassificationForJobProfile(JobProfile);
            byte[] messageBody = CommonAction.ConvertObjectToByteArray(workingPatternDetailClassification);
            Message message = CommonAction.CreateServiceBusMessage(workingPatternDetailClassification.Id, messageBody, ContentType.JSON, ActionType.Published, CType.WorkingPatternDetails);
            await CommonAction.SendMessage(Topic, message);
            await Task.Delay(5000);
            string endpoint = Settings.APIConfig.EndpointBaseUrl.Replace("{id}", JobProfile.JobProfileId);
            Response<JobProfileOverviewAPIResponse> apiResponse = await CommonAction.ExecuteGetRequest<JobProfileOverviewAPIResponse>(endpoint, GetRequest.ContentType.Json);
            Assert.AreEqual(workingPatternDetailClassification.Title, apiResponse.Data.workingPatternDetails);
        }
    }
}