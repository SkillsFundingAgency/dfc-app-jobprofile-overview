using DFC.Api.JobProfiles.Common.AzureServiceBusSupport;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model.ContentType.JobProfile;
using System.Threading.Tasks;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.Interface
{
    internal interface IJobProfileSupport
    {
        JobProfileContentType GenerateJobProfileContentType();
    }
}
