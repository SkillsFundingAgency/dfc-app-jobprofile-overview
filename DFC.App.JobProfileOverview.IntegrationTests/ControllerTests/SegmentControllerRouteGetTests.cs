﻿using DFC.App.JobProfileOverview.IntegrationTests.Data;
using System;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DFC.App.JobProfileOverview.IntegrationTests.ControllerTests
{
    public class SegmentControllerRouteGetTests :
        IClassFixture<CustomWebApplicationFactory<Startup>>,
        IClassFixture<DataSeeding>
    {
        private readonly CustomWebApplicationFactory<Startup> factory;

        public SegmentControllerRouteGetTests(CustomWebApplicationFactory<Startup> factory, DataSeeding dataSeeding)
        {
            this.factory = factory;

            if (dataSeeding == null)
            {
                throw new ArgumentNullException(nameof(dataSeeding));
            }

            dataSeeding.AddData(factory).Wait();
        }

        [Theory]
        [InlineData("segment")]
        [InlineData("segment/article1")]
        [InlineData("segment/article1/contents")]
        public async Task GetSegmentHtmlContentEndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var uri = new Uri(url, UriKind.Relative);
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));

            // Act
            var response = await client.GetAsync(uri).ConfigureAwait(false);

            // Assert
            response.EnsureSuccessStatusCode();
            AssertContentType(MediaTypeNames.Text.Html, response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("segment/abc")]
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