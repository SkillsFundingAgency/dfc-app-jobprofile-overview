using Microsoft.Azure.ServiceBus;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.AzureServiceBus.ServiceBusFactory.Interface
{
    internal interface IMessageFactory
    {
        Message Create(string messageId, byte[] messageBody, string actionType, string cType);
    }
}
