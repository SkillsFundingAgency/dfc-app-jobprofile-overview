﻿using DFC.App.JobProfileOverview.Data.Enums;
using DFC.App.JobProfileOverview.Data.Models.PatchModels;
using DFC.App.JobProfileOverview.MessageFunctionApp.Functions;
using DFC.App.JobProfileOverview.MessageFunctionApp.Services;
using DFC.Logger.AppInsights.Contracts;
using FakeItEasy;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DFC.App.JobProfiles.JobProfileOverview.MFA.UnitTests.Functions
{
    [Trait("Messaging Function", "Function Tests")]
    public class SitefinityMessageHandlerTests
    {
        private readonly ILogService logger;
        private readonly IMessageProcessor messageProcessor;
        private readonly IMessagePropertiesService messagePropertiesService;
        private readonly ICorrelationIdProvider correlationIdProvider;
        private readonly ILogService logService;
        private readonly SitefinityMessageHandler sitefinityMessageHandler;

        public SitefinityMessageHandlerTests()
        {
            logger = A.Fake<ILogService>();
            messageProcessor = A.Fake<IMessageProcessor>();
            messagePropertiesService = A.Fake<IMessagePropertiesService>();
            correlationIdProvider = A.Fake<ICorrelationIdProvider>();
            logService = A.Fake<ILogService>();
            sitefinityMessageHandler = new SitefinityMessageHandler(messageProcessor, messagePropertiesService, correlationIdProvider, logService);
        }

        public static IEnumerable<object[]> SuccessResultHttpStatusCodes => new List<object[]>
        {
            new object[] { HttpStatusCode.OK },
            new object[] { HttpStatusCode.Created },
            new object[] { HttpStatusCode.AlreadyReported },
            new object[] { HttpStatusCode.Accepted },
        };

        [Theory]
        [MemberData(nameof(SuccessResultHttpStatusCodes))]
        public async Task SitefinityMessageHandlerReturnsSuccessForSegmentUpdated(HttpStatusCode expectedResult)
        {
            // arrange
            const MessageAction messageAction = MessageAction.Published;
            const MessageContentType messageContentType = MessageContentType.WorkingHoursDetails;
            const long sequenceNumber = 123;
            var model = A.Fake<PatchWorkingHoursDetailModel>();
            var message = JsonConvert.SerializeObject(model);
            var serviceBusMessage = new Message(Encoding.ASCII.GetBytes(message));

            serviceBusMessage.UserProperties.Add("ActionType", messageAction);
            serviceBusMessage.UserProperties.Add("CType", messageContentType);
            serviceBusMessage.UserProperties.Add("Id", Guid.NewGuid());

            A.CallTo(() => messagePropertiesService.GetSequenceNumber(serviceBusMessage)).Returns(sequenceNumber);
            A.CallTo(() => messageProcessor.ProcessAsync(message, sequenceNumber, messageContentType, messageAction)).Returns(expectedResult);

            // act
            await sitefinityMessageHandler.Run(serviceBusMessage).ConfigureAwait(false);

            // assert
            A.CallTo(() => messagePropertiesService.GetSequenceNumber(serviceBusMessage)).MustHaveHappenedOnceExactly();
            A.CallTo(() => messageProcessor.ProcessAsync(message, sequenceNumber, messageContentType, messageAction)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task SitefinityMessageHandlerReturnsExceptionWhenNullServiceBusMessageSupplied()
        {
            // act
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await sitefinityMessageHandler.Run(null).ConfigureAwait(false)).ConfigureAwait(false);
        }

        [Fact]
        public async Task SitefinityMessageHandlerReturnsExceptionWhenEmptyServiceBusMessageSupplied()
        {
            // arrange
            const MessageAction messageAction = MessageAction.Published;
            const MessageContentType messageContentType = MessageContentType.WorkingHoursDetails;
            var serviceBusMessage = new Message(Encoding.ASCII.GetBytes(string.Empty));

            serviceBusMessage.UserProperties.Add("ActionType", messageAction);
            serviceBusMessage.UserProperties.Add("CType", messageContentType);
            serviceBusMessage.UserProperties.Add("Id", Guid.NewGuid());

            // act
            await Assert.ThrowsAsync<ArgumentException>(async () => await sitefinityMessageHandler.Run(serviceBusMessage).ConfigureAwait(false)).ConfigureAwait(false);
        }

        [Fact]
        public async Task SitefinityMessageHandlerReturnsExceptionWhenMessageActionIsInvalid()
        {
            // arrange
            const int messageAction = -1;
            const MessageContentType messageContentType = MessageContentType.WorkingHoursDetails;
            var model = A.Fake<PatchWorkingHoursDetailModel>();
            var message = JsonConvert.SerializeObject(model);
            var serviceBusMessage = new Message(Encoding.ASCII.GetBytes(message));

            serviceBusMessage.UserProperties.Add("ActionType", messageAction);
            serviceBusMessage.UserProperties.Add("CType", messageContentType);
            serviceBusMessage.UserProperties.Add("Id", Guid.NewGuid());

            // act
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await sitefinityMessageHandler.Run(serviceBusMessage).ConfigureAwait(false)).ConfigureAwait(false);
        }

        [Fact]
        public async Task SitefinityMessageHandlerReturnsExceptionWhenMessageContantTypeIsInvalid()
        {
            // arrange
            const MessageAction messageAction = MessageAction.Published;
            const int messageContentType = -1;
            var model = A.Fake<PatchWorkingHoursDetailModel>();
            var message = JsonConvert.SerializeObject(model);
            var serviceBusMessage = new Message(Encoding.ASCII.GetBytes(message));

            serviceBusMessage.UserProperties.Add("ActionType", messageAction);
            serviceBusMessage.UserProperties.Add("CType", messageContentType);
            serviceBusMessage.UserProperties.Add("Id", Guid.NewGuid());

            // act
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await sitefinityMessageHandler.Run(serviceBusMessage).ConfigureAwait(false)).ConfigureAwait(false);
        }
    }
}
