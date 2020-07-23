using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model.API;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model.Support;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.API;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.API.RestFactory.Interface;
using FakeItEasy;
using NUnit.Framework;
using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;

namespace DFC.App.JobProfileOverview.IntegrationTestFramework.UnitTests
{
    public class APITests
    {
        private const string ApimSubscriptionKey = "ApimSubscriptionKey";
        private const string Version = "Version";

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
            this.appSettings.APIConfig.ApimSubscriptionKey = ApimSubscriptionKey;
            this.appSettings.APIConfig.Version = Version;
            this.restClientFactory = A.Fake<IRestClientFactory>();
            this.restRequestFactory = A.Fake<IRestRequestFactory>();
            this.restClient = A.Fake<IRestClient>();
            this.restRequest = A.Fake<IRestRequest>();
            A.CallTo(() => this.restClientFactory.Create(A<Uri>.Ignored)).Returns(this.restClient);
            A.CallTo(() => this.restRequestFactory.Create(A<string>.Ignored)).Returns(this.restRequest);
            this.jobProfileOverviewAPI = new JobProfileOverviewAPI(this.restClientFactory, this.restRequestFactory, this.appSettings);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public async Task EmptyOrNullIdResultsInNullBeingReturned(string id)
        {
            Assert.IsNull(await this.jobProfileOverviewAPI.GetById(id).ConfigureAwait(true));
        }

        [Test]
        public async Task SuccessfulGetRequest()
        {
            var apiResponse = new RestResponse<JobProfileOverviewResponseBody>();
            apiResponse.StatusCode = HttpStatusCode.OK;
            A.CallTo(() => this.restClient.Execute<JobProfileOverviewResponseBody>(A<IRestRequest>.Ignored)).Returns(apiResponse);
            var response = await this.jobProfileOverviewAPI.GetById("id").ConfigureAwait(false);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        [TestCase("Accept", "application/json")]
        [TestCase("Ocp-Apim-Subscription-Key", ApimSubscriptionKey)]
        [TestCase("version", Version)]
        public async Task AllRequestHeadersAreSet(string headerKey, string headerValue)
        {
            var response = await this.jobProfileOverviewAPI.GetById("id").ConfigureAwait(false);
            A.CallTo(() => this.restRequest.AddHeader(headerKey, headerValue)).MustHaveHappenedOnceExactly();
        }
    }
}