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
            WorkingHoursDetailsClassification workingHoursDetailsClassification = CommonAction.Genr(JobProfile);
            byte[] messageBody = CommonAction.ConvertObjectToByteArray(socCodeContentType);
            Message message = CommonAction.CreateServiceBusMessage(socCodeContentType.Id, messageBody, ContentType.JSON, ActionType.Published, CType.JobProfileSoc);
            await CommonAction.SendMessage(Topic, message);
            await Task.Delay(5000);
            string endpoint = Settings.APIConfig.EndpointBaseUrl.Replace("{id}", JobProfile.JobProfileId);
            Response<JobProfileOverviewAPIResponse> apiResponse = await CommonAction.ExecuteGetRequest<JobProfileOverviewAPIResponse>(endpoint, GetRequest.ContentType.Json);
            Assert.AreEqual(socCodeContentType.SOCCode.Substring(0, 4), apiResponse.Data.soc);
            Assert.AreEqual(socCodeContentType.ONetOccupationalCode, apiResponse.Data.oNetOccupationalCode);
        }
    }
}