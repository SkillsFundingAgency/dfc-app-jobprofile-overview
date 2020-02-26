using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.AzureServiceBus.ServiceBusFactory.Interface;
using Microsoft.Azure.ServiceBus;
using System;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.AzureServiceBus.ServiceBusFactory
{
    public class MessageFactory : IMessageFactory
    {
        public Message Create(string messageId, byte[] body, string actionType, string cType)
        {
            Message message = new Message()
            {
                ContentType = cType,
                Body = body,
                CorrelationId = Guid.NewGuid().ToString(),
                Label = "Automated message",
                MessageId = messageId,
            };

            message.UserProperties.Add("ActionType", actionType.ToString());
            message.UserProperties.Add("CType", cType.ToString());
            return message;
        }
    }
}
