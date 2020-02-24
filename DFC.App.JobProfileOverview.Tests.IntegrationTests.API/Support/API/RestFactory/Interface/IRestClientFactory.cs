using RestSharp;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.API.RestFactory.Interface
{
    public interface IRestClientFactory
    {
        RestClient Create(string baseUrl);
    }
}
