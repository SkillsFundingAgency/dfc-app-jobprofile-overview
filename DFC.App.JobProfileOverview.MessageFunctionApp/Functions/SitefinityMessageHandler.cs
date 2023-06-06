using DFC.App.JobProfileOverview.Data.Enums;
using DFC.App.JobProfileOverview.MessageFunctionApp.Services;
using DFC.Logger.AppInsights.Contracts;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DFC.App.JobProfileOverview.MessageFunctionApp.Functions
{
    public class SitefinityMessageHandler
    {
        private readonly string classFullName = typeof(SitefinityMessageHandler).FullName;
        private readonly IMessageProcessor messageProcessor;
        private readonly IMessagePropertiesService messagePropertiesService;
        private readonly ICorrelationIdProvider correlationIdProvider;
        private readonly ILogService logService;

        public SitefinityMessageHandler(
            IMessageProcessor messageProcessor,
            IMessagePropertiesService messagePropertiesService,
            ICorrelationIdProvider correlationIdProvider,
            ILogService logService)
        {
            this.messageProcessor = messageProcessor;
            this.messagePropertiesService = messagePropertiesService;
            this.correlationIdProvider = correlationIdProvider;
            this.logService = logService;
        }

        [FunctionName("SitefinityMessageHandler")]
        public async Task Run(
            [ServiceBusTrigger("%cms-messages-topic%", "%cms-messages-subscription%", Connection = "service-bus-connection-string")] Message sitefinityMessage)
        {
            logService.LogInformation($"SitefinityMessageHandler Run {nameof(sitefinityMessage)} ");
            if (sitefinityMessage == null)
            {
                logService.LogInformation($"SitefinityMessageHandler Run {nameof(sitefinityMessage)} ArgumentNullException");
                throw new ArgumentNullException(nameof(sitefinityMessage));
            }

            correlationIdProvider.CorrelationId = sitefinityMessage.CorrelationId;

            sitefinityMessage.UserProperties.TryGetValue("ActionType", out var actionType);
            sitefinityMessage.UserProperties.TryGetValue("CType", out var contentType);
            sitefinityMessage.UserProperties.TryGetValue("Id", out var messageContentId);

            // logger should allow setting up correlation id and should be picked up from message
            logService.LogInformation($"{nameof(SitefinityMessageHandler)}: Received message action '{actionType}' for type '{contentType}' with Id: '{messageContentId}'");

            var message = Encoding.UTF8.GetString(sitefinityMessage?.Body);

            if (string.IsNullOrWhiteSpace(message))
            {
                logService.LogInformation($"SitefinityMessageHandler Run {nameof(sitefinityMessage)} Message cannot be null or empty ");
                throw new ArgumentException("Message cannot be null or empty.", nameof(sitefinityMessage));
            }

            if (!Enum.IsDefined(typeof(MessageAction), actionType?.ToString()))
            {
                logService.LogInformation($"SitefinityMessageHandler Run Invalid message action {actionType} ");
                throw new ArgumentOutOfRangeException(nameof(actionType), $"Invalid message action '{actionType}' received, should be one of '{string.Join(",", Enum.GetNames(typeof(MessageAction)))}'");
            }

            if (!Enum.IsDefined(typeof(MessageContentType), contentType?.ToString()))
            {
                logService.LogInformation($"SitefinityMessageHandler Run Invalid message content type {contentType} ");

                throw new ArgumentOutOfRangeException(nameof(contentType), $"Invalid message content type '{contentType}' received, should be one of '{string.Join(",", Enum.GetNames(typeof(MessageContentType)))}'");
            }

            var messageAction = Enum.Parse<MessageAction>(actionType?.ToString());
            var messageContentType = Enum.Parse<MessageContentType>(contentType?.ToString());
            var sequenceNumber = messagePropertiesService.GetSequenceNumber(sitefinityMessage);

            var result = await messageProcessor.ProcessAsync(message, sequenceNumber, messageContentType, messageAction).ConfigureAwait(false);

            switch (result)
            {
                case HttpStatusCode.OK:
                    logService.LogInformation($"{classFullName}: JobProfile Id: {messageContentId}: Updated segment");
                    break;

                case HttpStatusCode.Created:
                    logService.LogInformation($"{classFullName}: JobProfile Id: {messageContentId}: Created segment");
                    break;

                case HttpStatusCode.AlreadyReported:
                    logService.LogInformation($"{classFullName}: JobProfile Id: {messageContentId}: Segment previously updated");
                    break;

                default:
                    logService.LogWarning($"{classFullName}: JobProfile Id: {messageContentId}: Segment not Posted: Status: {result}");
                    break;
            }
        }
    }
}
