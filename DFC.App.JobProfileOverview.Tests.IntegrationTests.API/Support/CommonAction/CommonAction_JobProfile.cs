using DFC.Api.JobProfiles.Common.AzureServiceBusSupport;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.Interface;
using System;
using System.Threading.Tasks;
using static DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.EnumLibrary;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support
{
    internal partial class CommonAction : IJobProfileSupport
    {
        public async Task CreateJobProfile(Topic topic, Guid messageId, string canonicalName)
        {
            JobProfileCreateMessageBody messageBody = ResourceManager.GetResource<JobProfileCreateMessageBody>("JobProfileCreateMessageBody");
            messageBody.JobProfileId = messageId.ToString();
            messageBody.UrlName = canonicalName;
            messageBody.CanonicalName = canonicalName;
            Message message = CreateServiceBusMessage(messageId, ConvertObjectToByteArray(messageBody), EnumLibrary.ContentType.JSON, ActionType.Published, CType.JobProfile);
            await topic.SendAsync(message);
        }
    }
}