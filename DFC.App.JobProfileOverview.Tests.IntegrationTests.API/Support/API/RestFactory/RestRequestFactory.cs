using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.API.RestFactory.Interface;
using RestSharp;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.API.RestFactory
{
    internal class RestRequestFactory : IRestRequestFactory
    {
        public RestRequest Create(string url)
        {
            return new RestRequest(url);
        }
    }
}
