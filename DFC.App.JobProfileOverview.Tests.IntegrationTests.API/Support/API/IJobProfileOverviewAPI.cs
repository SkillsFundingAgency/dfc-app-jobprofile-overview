using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model.API;
using RestSharp;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.API
{
    internal interface IJobProfileOverviewAPI
    {
        IRestResponse<JobProfileOverviewResponseBody> GetById(string id);
    }
}
