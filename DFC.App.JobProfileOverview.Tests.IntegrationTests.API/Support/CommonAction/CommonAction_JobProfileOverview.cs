using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.Interface;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support
{
    internal partial class CommonAction : IJobProfileOverviewSupport
    {
        public SOCCodeContentType GenerateSOCCodeContentType()
        {
            SOCCodeContentType socCodeContentType = ResourceManager.GetResource<SOCCodeContentType>("SOCCodeContentType");
            throw new System.NotImplementedException();
        }
    }
}