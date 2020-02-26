using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.API.RestFactory.Interface;
using RestSharp;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.API.RestFactory
{
    internal class RestClientFactory : IRestClientFactory
    {
        public IRestClient Create(string baseUrl)
        {
            return new RestClient(baseUrl);
        }
    }
}
