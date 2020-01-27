using DFC.Api.JobProfiles.Common.AzureServiceBusSupport;
using System;
using System.Threading.Tasks;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.Interface
{
    interface IJobProfileSupport
    {
        Task CreateJobProfile(Topic topic, Guid messageId, string canonicalName);
    }
}
