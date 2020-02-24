using DFC.Api.JobProfiles.Common.AzureServiceBusSupport;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model.ContentType.JobProfile;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.Interface;
using System;
using System.Threading.Tasks;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support
{
    internal partial class CommonAction : IJobProfileSupport
    {
        public JobProfileContentType GenerateJobProfileContentType()
        {
            string canonicalName = this.RandomString(10).ToLower();
            JobProfileContentType jobProfileContentType = ResourceManager.GetResource<JobProfileContentType>("JobProfileContentType");
            jobProfileContentType.JobProfileId = Guid.NewGuid().ToString();
            jobProfileContentType.UrlName = canonicalName;
            jobProfileContentType.CanonicalName = canonicalName;
            return jobProfileContentType;
        }
    }
}