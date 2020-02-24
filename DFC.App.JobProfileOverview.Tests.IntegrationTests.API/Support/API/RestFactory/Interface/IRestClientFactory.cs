using RestSharp;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.API.RestFactory.Interface
{
    internal interface IRestClientFactory
    {
        RestClient Create(string baseUrl);
    }
}
