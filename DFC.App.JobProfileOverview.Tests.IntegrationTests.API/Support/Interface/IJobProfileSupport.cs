using DFC.Api.JobProfiles.Common.AzureServiceBusSupport;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model;
using System;
using System.Threading.Tasks;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.Interface
{
    internal interface IJobProfileSupport
    {
        JobProfileContentType GenerateJobProfileContentType();

        Task DeleteJobProfile(Topic topic, JobProfileContentType jobProfileId);
    }
}
