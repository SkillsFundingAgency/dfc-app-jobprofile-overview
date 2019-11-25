using System.Net.Http;

namespace DFC.App.JobProfiles.JobProfileOverview.MFA.UnitTests.FakeHttpHandlers
{
    public interface IFakeHttpRequestSender
    {
        HttpResponseMessage Send(HttpRequestMessage request);
    }
}