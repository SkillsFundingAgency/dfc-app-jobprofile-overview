using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DFC.App.JobProfileOverview.IntegrationTests.ControllerTests
{
    public class SegmentControllerRouteTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private const string Segment = "segment";
        private const string DefaultArticleName = "segment-article";

        private readonly CustomWebApplicationFactory<Startup> factory;

        public SegmentControllerRouteTests(CustomWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        public static IEnumerable<object[]> SegmentContentRouteData => new List<object[]>
        {
            new object[] { $"/{Segment}" },
            new object[] { $"/{Segment}/{DefaultArticleName}" },
        };

        public static IEnumerable<object[]> MissingSegmentContentRouteData => new List<object[]>
        {
            new object[] { $"/Segment/invalid-segment-name" },
        };

        [Theory]
        [InlineData("/" + Segment + "/" + DefaultArticleName, MediaTypeNames.Text.Html)]
        [InlineData("/" + Segment + "/" + DefaultArticleName, MediaTypeNames.Application.Json)]
        public async Task GetSegmentContentEndpointsReturnSuccessAndCorrectContentType(string url, string acceptHeaderValue)
        {
            // Arrange
            var uri = new Uri(url, UriKind.Relative);
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add(HeaderNames.Accept, acceptHeaderValue);

            // Act
            var response = await client.GetAsync(uri).ConfigureAwait(false);

            // Assert
            response.EnsureSuccessStatusCode();
            AssertContentType(acceptHeaderValue, response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [MemberData(nameof(MissingSegmentContentRouteData))]
        public async Task GetSegmentHtmlContentEndpointsReturnNoContent(string url)
        {
            // Arrange
            var uri = new Uri(url, UriKind.Relative);
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();

            // Act
            var response = await client.GetAsync(uri).ConfigureAwait(false);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        private void AssertContentType(string expectedContentType, string actualContentType)
        {
            Assert.Equal($"{expectedContentType}; charset={Encoding.UTF8.WebName}", actualContentType);
        }
    }
}