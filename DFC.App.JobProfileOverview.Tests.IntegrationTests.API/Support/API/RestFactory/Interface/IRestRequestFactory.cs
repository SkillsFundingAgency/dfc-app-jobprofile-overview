using RestSharp;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.API.RestFactory.Interface
{
    internal interface IRestRequestFactory
    {
        RestRequest Create(string url, Method method);
    }
}
