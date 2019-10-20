using DFC.App.JobProfileOverview.Data.Models;
using DFC.App.JobProfileOverview.IntegrationTests.Data;
using FluentAssertions;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Xunit;

namespace DFC.App.JobProfileOverview.IntegrationTests.ControllerTests
{
    public class SegmentControllerRoutePostTests :
        IClassFixture<CustomWebApplicationFactory<Startup>>,
        IClassFixture<DataSeeding>
    {
        private const string Url = "/segment";

        private readonly CustomWebApplicationFactory<Startup> factory;
        private readonly DataSeeding dataSeeding;

        public SegmentControllerRoutePostTests(
            CustomWebApplicationFactory<Startup> factory,
            DataSeeding dataSeeding)
        {
            this.factory = factory;
            this.dataSeeding = dataSeeding;

            if (dataSeeding == null)
            {
                throw new ArgumentNullException(nameof(dataSeeding));
            }

            dataSeeding.AddData(factory).GetAwaiter().GetResult();
        }

        [Fact]
        public async Task WhenAddingNewArticleReturnCreated()
        {
            // Arrange
            var documentId = Guid.NewGuid();
            var overviewSegmentModel = new JobProfileOverviewSegmentModel()
            {
                DocumentId = documentId,
                CanonicalName = documentId.ToString().ToLowerInvariant(),
                SocLevelTwo = "12PostSoc",
                Data = new JobProfileOverviewSegmentDataModel(),
            };
            var client = factory.CreateClient();

            client.DefaultRequestHeaders.Accept.Clear();

            // Act
            var response = await client.PostAsync(Url, overviewSegmentModel, new JsonMediaTypeFormatter()).ConfigureAwait(false);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task WhenUpdateExistingArticleReturnsAlreadyReported()
        {
            // Arrange
            var overviewSegmentModel = new JobProfileOverviewSegmentModel()
            {
                DocumentId = dataSeeding.Article2Id,
                CanonicalName = "article2_modified",
                SocLevelTwo = dataSeeding.Article2SocCode,
                Data = new JobProfileOverviewSegmentDataModel(),
            };
            var client = factory.CreateClient();

            client.DefaultRequestHeaders.Accept.Clear();

            // Act
            var response = await client.PostAsync(Url, overviewSegmentModel, new JsonMediaTypeFormatter()).ConfigureAwait(false);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.AlreadyReported);
        }
    }
}