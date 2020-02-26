using RestSharp;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.API.RestFactory.Interface
{
    public interface IRestClientFactory
    {
        IRestClient Create(string baseUrl);
    }
}
