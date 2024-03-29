﻿using DFC.App.JobProfileOverview.Data.Models.PatchModels;
using DFC.App.JobProfileOverview.MessageFunctionApp.Models;
using DFC.App.JobProfileOverview.MessageFunctionApp.Services;
using DFC.App.JobProfiles.JobProfileOverview.MFA.UnitTests.FakeHttpHandlers;
using DFC.Logger.AppInsights.Contracts;
using FakeItEasy;
using Moq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace DFC.App.JobProfiles.JobProfileOverview.MFA.UnitTests.Services
{
    [Trait("Messaging Function", "HttpClientService Patch Tests")]
    public class HttpClientServicePatchTests
    {
        private Mock<IHttpClientFactory> mockFactory;
        private readonly ILogService logService;
        private readonly ICorrelationIdProvider correlationIdProvider;
        private readonly SegmentClientOptions segmentClientOptions;

        public HttpClientServicePatchTests()
        {
            logService = A.Fake<ILogService>();
            correlationIdProvider = A.Fake<ICorrelationIdProvider>();

            segmentClientOptions = new SegmentClientOptions
            {
                BaseAddress = new Uri("https://somewhere.com", UriKind.Absolute),
            };
        }

        public Mock<IHttpClientFactory> CreateClientFactory(HttpClient httpClient)
        {
            mockFactory = new Mock<IHttpClientFactory>();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);
            return mockFactory;
        }

        [Fact]
        public async Task PatchAsyncReturnsNotFoundStatusCodeForExistingId()
        {
            // arrange
            const HttpStatusCode expectedResult = HttpStatusCode.NotFound;
            var httpResponse = new HttpResponseMessage { StatusCode = expectedResult };
            var fakeHttpRequestSender = A.Fake<IFakeHttpRequestSender>();
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(fakeHttpRequestSender);
            var httpClient = new HttpClient(fakeHttpMessageHandler) { BaseAddress = segmentClientOptions.BaseAddress };
            var httpClientService = new HttpClientService(segmentClientOptions, CreateClientFactory(httpClient).Object, logService, correlationIdProvider);

            A.CallTo(() => fakeHttpRequestSender.Send(A<HttpRequestMessage>.Ignored)).Returns(httpResponse);

            // act
            var result = await httpClientService.PatchAsync(A.Fake<PatchHiddenAlternativeTitleModel>(), "endpoint").ConfigureAwait(false);

            // assert
            A.CallTo(() => fakeHttpRequestSender.Send(A<HttpRequestMessage>.Ignored)).MustHaveHappenedOnceExactly();
            Assert.Equal(expectedResult, result);

            httpResponse.Dispose();
            httpClient.Dispose();
            fakeHttpMessageHandler.Dispose();
        }

        [Fact]
        public async Task PatchAsyncReturnsExceptionForBadStatus()
        {
            // arrange
            const HttpStatusCode expectedResult = HttpStatusCode.Forbidden;
            var httpResponse = new HttpResponseMessage { StatusCode = expectedResult, Content = new StringContent("bad Patch") };
            var fakeHttpRequestSender = A.Fake<IFakeHttpRequestSender>();
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(fakeHttpRequestSender);
            var httpClient = new HttpClient(fakeHttpMessageHandler) { BaseAddress = segmentClientOptions.BaseAddress };
            var httpClientService = new HttpClientService(segmentClientOptions, CreateClientFactory(httpClient).Object, logService, correlationIdProvider);

            A.CallTo(() => fakeHttpRequestSender.Send(A<HttpRequestMessage>.Ignored)).Returns(httpResponse);

            // act
            var exceptionResult = await Assert.ThrowsAsync<HttpRequestException>(async () => await httpClientService.PatchAsync(A.Fake<PatchHiddenAlternativeTitleModel>(), "endpoint").ConfigureAwait(false)).ConfigureAwait(false);

            // assert
            A.CallTo(() => fakeHttpRequestSender.Send(A<HttpRequestMessage>.Ignored)).MustHaveHappenedOnceExactly();
            Assert.Equal($"Response status code does not indicate success: {(int)expectedResult} ({expectedResult}).", exceptionResult.Message);

            httpResponse.Dispose();
            httpClient.Dispose();
            fakeHttpMessageHandler.Dispose();
        }
    }
}