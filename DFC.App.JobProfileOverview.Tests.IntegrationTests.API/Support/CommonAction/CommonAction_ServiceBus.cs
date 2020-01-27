﻿using DFC.Api.JobProfiles.Common.AzureServiceBusSupport;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.Interface;
using System;
using static DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.EnumLibrary;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support
{
    internal partial class CommonAction : IServiceBusSupport
    {
        public Message CreateServiceBusMessage(Guid messageId, byte[] messageBody, EnumLibrary.ContentType contentType, ActionType actionType, CType ctype)
        {
            Message message = new Message();
            message.ContentType = GetDescription(contentType);
            message.Body = messageBody;
            message.CorrelationId = Guid.NewGuid().ToString();
            message.Label = "Automated message";
            message.MessageId = messageId.ToString();
            message.UserProperties.Add("Id", messageId);
            message.UserProperties.Add("ActionType", actionType.ToString());
            message.UserProperties.Add("CType", ctype.ToString());
            return message;
        }
    }
}
