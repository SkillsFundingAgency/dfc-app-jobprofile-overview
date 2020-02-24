using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model.API;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model.Support;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.API.RestFactory.Interface;
using RestSharp;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.API
{
    internal class JobProfileOverviewAPI : IJobProfileOverviewAPI
    {
        private IRestClientFactory restClientFactory;
        private IRestRequestFactory restRequestFactory;
        private AppSettings appSettings;

        public JobProfileOverviewAPI(IRestClientFactory restClientFactory, IRestRequestFactory restRequestFactory, AppSettings appSettings)
        {
            this.restClientFactory = restClientFactory;
            this.restRequestFactory = restRequestFactory;
            this.appSettings = appSettings;
        }

        public IRestResponse<JobProfileOverviewResponseBody> GetById(string id)
        {
            var restClient = this.restClientFactory.Create(this.appSettings.APIConfig.EndpointBaseUrl);
            var restRequest = this.restRequestFactory.Create(string.Format("{0}/contents", id), Method.GET);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Ocp-Apim-Subscription-Key", this.appSettings.APIConfig.ApimSubscriptionKey);
            restRequest.AddHeader("version", this.appSettings.APIConfig.Version);
            return restClient.Execute<JobProfileOverviewResponseBody>(restRequest);
        }
    }
}
