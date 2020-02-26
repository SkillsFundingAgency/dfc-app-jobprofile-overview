using RestSharp;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.API.RestFactory.Interface
{
    public interface IRestRequestFactory
    {
        IRestRequest Create(string url);
    }
}
