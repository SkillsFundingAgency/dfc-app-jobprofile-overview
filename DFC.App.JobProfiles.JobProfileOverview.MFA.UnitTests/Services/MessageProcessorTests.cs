using AutoMapper;
using DFC.App.JobProfileOverview.Data.Enums;
using DFC.App.JobProfileOverview.Data.Models;
using DFC.App.JobProfileOverview.Data.Models.PatchModels;
using DFC.App.JobProfileOverview.Data.ServiceBusModels.PatchModels;
using DFC.App.JobProfileOverview.MessageFunctionApp.Services;
using DFC.Logger.AppInsights.Contracts;
using FakeItEasy;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace DFC.App.JobProfiles.JobProfileOverview.MFA.UnitTests.Services
{
    [Trait("Messaging Function", "Message Processor Tests")]
    public class MessageProcessorTests
    {
        private readonly IMapper mapper;
        private readonly IHttpClientService httpClientService;
        private readonly IMappingService mappingService;
        private readonly IMessageProcessor messageProcessor;

        public MessageProcessorTests()
        {
            mapper = A.Fake<IMapper>();
            httpClientService = A.Fake<IHttpClientService>();
            mappingService = A.Fake<IMappingService>();

            messageProcessor = new MessageProcessor(mapper, httpClientService, mappingService, A.Fake<ILogService>());
        }

        [Fact]
        public async Task ProcessAsyncWorkingPatternTestReturnsOk()
        {
            // arrange
            const HttpStatusCode expectedResult = HttpStatusCode.OK;
            const string message = "{}";
            const long sequenceNumber = 1;

            A.CallTo(() => mapper.Map<PatchWorkingPatternModel>(A<PatchWorkingPatternServiceBusModel>.Ignored)).Returns(A.Fake<PatchWorkingPatternModel>());
            A.CallTo(() => httpClientService.PatchAsync(A<PatchWorkingPatternModel>.Ignored, A<string>.Ignored)).Returns(expectedResult);

            // act
            var result = await messageProcessor.ProcessAsync(message, sequenceNumber, MessageContentType.WorkingPattern, MessageAction.Published).ConfigureAwait(false);

            // assert
            A.CallTo(() => mapper.Map<PatchWorkingPatternModel>(A<PatchWorkingPatternServiceBusModel>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => httpClientService.PatchAsync(A<PatchWorkingPatternModel>.Ignored, A<string>.Ignored)).MustHaveHappenedOnceExactly();
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task ProcessAsyncHiddenAlternativeTitleTestReturnsOk()
        {
            // arrange
            const HttpStatusCode expectedResult = HttpStatusCode.OK;
            const string message = "{}";
            const long sequenceNumber = 1;

            A.CallTo(() => mapper.Map<PatchHiddenAlternativeTitleModel>(A<PatchHiddenAlternativeTitleServiceBusModel>.Ignored)).Returns(A.Fake<PatchHiddenAlternativeTitleModel>());
            A.CallTo(() => httpClientService.PatchAsync(A<PatchHiddenAlternativeTitleModel>.Ignored, A<string>.Ignored)).Returns(expectedResult);

            // act
            var result = await messageProcessor.ProcessAsync(message, sequenceNumber, MessageContentType.HiddenAlternativeTitle, MessageAction.Published).ConfigureAwait(false);

            // assert
            A.CallTo(() => mapper.Map<PatchHiddenAlternativeTitleModel>(A<PatchHiddenAlternativeTitleServiceBusModel>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => httpClientService.PatchAsync(A<PatchHiddenAlternativeTitleModel>.Ignored, A<string>.Ignored)).MustHaveHappenedOnceExactly();
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task ProcessAsyncJobProfileSpecialismTestReturnsOk()
        {
            // arrange
            const HttpStatusCode expectedResult = HttpStatusCode.OK;
            const string message = "{}";
            const long sequenceNumber = 1;

            A.CallTo(() => mapper.Map<PatchJobProfileSpecialismModel>(A<PatchJobProfileSpecialismServiceBusModel>.Ignored)).Returns(A.Fake<PatchJobProfileSpecialismModel>());
            A.CallTo(() => httpClientService.PatchAsync(A<PatchJobProfileSpecialismModel>.Ignored, A<string>.Ignored)).Returns(expectedResult);

            // act
            var result = await messageProcessor.ProcessAsync(message, sequenceNumber, MessageContentType.JobProfileSpecialism, MessageAction.Published).ConfigureAwait(false);

            // assert
            A.CallTo(() => mapper.Map<PatchJobProfileSpecialismModel>(A<PatchJobProfileSpecialismServiceBusModel>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => httpClientService.PatchAsync(A<PatchJobProfileSpecialismModel>.Ignored, A<string>.Ignored)).MustHaveHappenedOnceExactly();
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task ProcessAsyncWorkingHoursDetailsTestReturnsOk()
        {
            // arrange
            const HttpStatusCode expectedResult = HttpStatusCode.OK;
            const string message = "{}";
            const long sequenceNumber = 1;

            A.CallTo(() => mapper.Map<PatchWorkingHoursDetailModel>(A<PatchWorkingHoursDetailServiceBusModel>.Ignored)).Returns(A.Fake<PatchWorkingHoursDetailModel>());
            A.CallTo(() => httpClientService.PatchAsync(A<PatchWorkingHoursDetailModel>.Ignored, A<string>.Ignored)).Returns(expectedResult);

            // act
            var result = await messageProcessor.ProcessAsync(message, sequenceNumber, MessageContentType.WorkingHoursDetails, MessageAction.Published).ConfigureAwait(false);

            // assert
            A.CallTo(() => mapper.Map<PatchWorkingHoursDetailModel>(A<PatchWorkingHoursDetailServiceBusModel>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => httpClientService.PatchAsync(A<PatchWorkingHoursDetailModel>.Ignored, A<string>.Ignored)).MustHaveHappenedOnceExactly();
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task ProcessAsyncWorkingPatternDetailsTestReturnsOk()
        {
            // arrange
            const HttpStatusCode expectedResult = HttpStatusCode.OK;
            const string message = "{}";
            const long sequenceNumber = 1;

            A.CallTo(() => mapper.Map<PatchWorkingPatternDetailModel>(A<PatchWorkingPatternDetailServiceBusModel>.Ignored)).Returns(A.Fake<PatchWorkingPatternDetailModel>());
            A.CallTo(() => httpClientService.PatchAsync(A<PatchWorkingPatternDetailModel>.Ignored, A<string>.Ignored)).Returns(expectedResult);

            // act
            var result = await messageProcessor.ProcessAsync(message, sequenceNumber, MessageContentType.WorkingPatternDetails, MessageAction.Published).ConfigureAwait(false);

            // assert
            A.CallTo(() => mapper.Map<PatchWorkingPatternDetailModel>(A<PatchWorkingPatternDetailServiceBusModel>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => httpClientService.PatchAsync(A<PatchWorkingPatternDetailModel>.Ignored, A<string>.Ignored)).MustHaveHappenedOnceExactly();
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task ProcessAsyncJobProfileSocTestReturnsOk()
        {
            // arrange
            const HttpStatusCode expectedResult = HttpStatusCode.OK;
            const string message = "{}";
            const long sequenceNumber = 1;

            A.CallTo(() => mapper.Map<PatchSocDataModel>(A<PatchSocDataServiceBusModel>.Ignored)).Returns(A.Fake<PatchSocDataModel>());
            A.CallTo(() => httpClientService.PatchAsync(A<PatchSocDataModel>.Ignored, A<string>.Ignored)).Returns(expectedResult);

            // act
            var result = await messageProcessor.ProcessAsync(message, sequenceNumber, MessageContentType.JobProfileSoc, MessageAction.Published).ConfigureAwait(false);

            // assert
            A.CallTo(() => mapper.Map<PatchSocDataModel>(A<PatchSocDataServiceBusModel>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => httpClientService.PatchAsync(A<PatchSocDataModel>.Ignored, A<string>.Ignored)).MustHaveHappenedOnceExactly();
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task ProcessAsyncJobProfileCreatePublishedTestReturnsOk()
        {
            // arrange
            const HttpStatusCode expectedResult = HttpStatusCode.Created;
            const string message = "{}";
            const long sequenceNumber = 1;

            A.CallTo(() => mappingService.MapToSegmentModel(message, sequenceNumber)).Returns(A.Fake<JobProfileOverviewSegmentModel>());
            A.CallTo(() => httpClientService.PutAsync(A<JobProfileOverviewSegmentModel>.Ignored)).Returns(expectedResult);

            // act
            var result = await messageProcessor.ProcessAsync(message, sequenceNumber, MessageContentType.JobProfile, MessageAction.Published).ConfigureAwait(false);

            // assert
            A.CallTo(() => mappingService.MapToSegmentModel(message, sequenceNumber)).MustHaveHappenedOnceExactly();
            A.CallTo(() => httpClientService.PutAsync(A<JobProfileOverviewSegmentModel>.Ignored)).MustHaveHappenedOnceExactly();
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task ProcessAsyncJobProfileUpdatePublishedTestReturnsOk()
        {
            // arrange
            const HttpStatusCode expectedResult = HttpStatusCode.OK;
            const string message = "{}";
            const long sequenceNumber = 1;

            A.CallTo(() => mappingService.MapToSegmentModel(message, sequenceNumber)).Returns(A.Fake<JobProfileOverviewSegmentModel>());
            A.CallTo(() => httpClientService.PutAsync(A<JobProfileOverviewSegmentModel>.Ignored)).Returns(HttpStatusCode.NotFound);
            A.CallTo(() => httpClientService.PostAsync(A<JobProfileOverviewSegmentModel>.Ignored)).Returns(expectedResult);

            // act
            var result = await messageProcessor.ProcessAsync(message, sequenceNumber, MessageContentType.JobProfile, MessageAction.Published).ConfigureAwait(false);

            // assert
            A.CallTo(() => mappingService.MapToSegmentModel(message, sequenceNumber)).MustHaveHappenedOnceExactly();
            A.CallTo(() => httpClientService.PutAsync(A<JobProfileOverviewSegmentModel>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => httpClientService.PostAsync(A<JobProfileOverviewSegmentModel>.Ignored)).MustHaveHappenedOnceExactly();
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task ProcessAsyncJobProfileDeletedTestReturnsOk()
        {
            // arrange
            const HttpStatusCode expectedResult = HttpStatusCode.OK;
            const string message = "{}";
            const long sequenceNumber = 1;

            A.CallTo(() => mappingService.MapToSegmentModel(message, sequenceNumber)).Returns(A.Fake<JobProfileOverviewSegmentModel>());
            A.CallTo(() => httpClientService.DeleteAsync(A<Guid>.Ignored)).Returns(expectedResult);

            // act
            var result = await messageProcessor.ProcessAsync(message, sequenceNumber, MessageContentType.JobProfile, MessageAction.Deleted).ConfigureAwait(false);

            // assert
            A.CallTo(() => mappingService.MapToSegmentModel(message, sequenceNumber)).MustHaveHappenedOnceExactly();
            A.CallTo(() => httpClientService.DeleteAsync(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task ProcessAsyncWithBadMessageMessageActionReturnsException()
        {
            // arrange
            const string message = "{}";
            const long sequenceNumber = 1;

            A.CallTo(() => mappingService.MapToSegmentModel(message, sequenceNumber)).Returns(A.Fake<JobProfileOverviewSegmentModel>());

            // act
            var exceptionResult = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await messageProcessor.ProcessAsync(message, sequenceNumber, MessageContentType.JobProfile, (MessageAction)(-1)).ConfigureAwait(false)).ConfigureAwait(false);

            // assert
            A.CallTo(() => mappingService.MapToSegmentModel(message, sequenceNumber)).MustHaveHappenedOnceExactly();
            Assert.Equal("Invalid message action '-1' received, should be one of 'Published,Deleted,Draft' (Parameter 'messageAction')", exceptionResult.Message);
        }

        [Fact]
        public async Task ProcessAsyncWithBadMessageContentTypeReturnsException()
        {
            // act
            var exceptionResult = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await messageProcessor.ProcessAsync(string.Empty, 1, (MessageContentType)(-1), MessageAction.Published).ConfigureAwait(false)).ConfigureAwait(false);

            // assert
            Assert.Equal("Unexpected sitefinity content type '-1' (Parameter 'messageContentType')", exceptionResult.Message);
        }
    }
}
