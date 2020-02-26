using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model.API;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model.Support;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.API;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.API.RestFactory.Interface;
using FakeItEasy;
using NUnit.Framework;
using RestSharp;
using System.Threading;
using System.Threading.Tasks;

namespace DFC.App.JobProfileOverview.IntegrationTestFramework.UnitTests
{
    public class APITests
    {
        private AppSettings appSettings;
        private IRestClientFactory restClientFactory;
        private IRestRequestFactory restRequestFactory;
        private IJobProfileOverviewAPI jobProfileOverviewAPI;
        private IRestClient restClient;
        private IRestRequest restRequest;

        [SetUp]
        public void Setup()
        {
            this.appSettings = new AppSettings();
            this.appSettings.APIConfig.EndpointBaseUrl = "myUrl";
            this.restClientFactory = A.Fake<IRestClientFactory>();
            this.restRequestFactory = A.Fake<IRestRequestFactory>();
            this.restClient = A.Fake<IRestClient>();
            this.restRequest = A.Fake<IRestRequest>();
            this.jobProfileOverviewAPI = new JobProfileOverviewAPI(this.restClientFactory, this.restRequestFactory, this.appSettings);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public async Task EmptyIdResultsInNullBeingReturned(string id)
        {
            Assert.IsNull(await this.jobProfileOverviewAPI.GetById(id).ConfigureAwait(true));
        }

        [Test]
        public async Task SuccessfulResponseFromApi()
        {
            var apiResponse = new RestResponse<JobProfileOverviewResponseBody>();
            A.CallTo(() => this.restClient.ExecuteGetAsync<JobProfileOverviewResponseBody>(A<IRestRequest>.Ignored, A<CancellationToken>.Ignored)).Returns(apiResponse);
            var response = await this.jobProfileOverviewAPI.GetById("identifier").ConfigureAwait(false);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}