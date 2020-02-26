using RestSharp;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.API.RestFactory.Interface
{
    public interface IRestRequestFactory
    {
        RestRequest Create(string url);
    }
}
