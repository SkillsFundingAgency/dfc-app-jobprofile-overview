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
        IClassFixture<DataSeeding>,
        IDisposable
    {
        private readonly CustomWebApplicationFactory<Startup> factory;
        private readonly DataSeeding dataSeeding;

        public SegmentControllerRoutePostTests(
            CustomWebApplicationFactory<Startup> factory,
            DataSeeding dataSeeding)
        {
            this.factory = factory;
            this.dataSeeding = dataSeeding;
            dataSeeding.AddData(factory).Wait();
        }

        [Fact]
        public async Task WhenAddingNewArticleReturnCreated()
        {
            // Arrange
            var url = "/segment";
            var documentId = Guid.NewGuid();
            var overviewSegmentModel = new JobProfileOverviewSegmentModel()
            {
                DocumentId = documentId,
                CanonicalName = documentId.ToString().ToLowerInvariant(),
                Data = new JobProfileOverviewSegmentDataModel(),
            };
            var client = factory.CreateClient();

            client.DefaultRequestHeaders.Accept.Clear();

            // Act
            var response = await client.PostAsync(url, overviewSegmentModel, new JsonMediaTypeFormatter()).ConfigureAwait(false);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            // Cleanup
            await client.DeleteAsync(string.Concat(url, "/", documentId)).ConfigureAwait(false);
        }

        [Fact(Skip ="Updating the partition key seems to be a no go.")]
        public async Task WhenUpdateExistingArticleReturnsOK()
        {
            // Arrange
            const string url = "/segment";
            var careerPathSegmentModel = new JobProfileOverviewSegmentModel()
            {
                DocumentId = dataSeeding.Article2Id,
                CanonicalName = "article2_modified",
                Data = new JobProfileOverviewSegmentDataModel(),
            };
            var client = factory.CreateClient();

            client.DefaultRequestHeaders.Accept.Clear();

            // Act
            var response = await client.PostAsync(url, careerPathSegmentModel, new JsonMediaTypeFormatter()).ConfigureAwait(false);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        public void Dispose()
        {
            dataSeeding.RemoveData(factory).Wait();
        }
    }
}