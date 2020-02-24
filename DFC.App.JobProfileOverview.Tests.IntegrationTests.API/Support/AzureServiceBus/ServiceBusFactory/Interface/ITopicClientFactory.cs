using DFC.Api.JobProfiles.Common.AzureServiceBusSupport;
using Microsoft.Azure.ServiceBus;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.AzureServiceBus.ServiceBusFactory.Interface
{
    internal interface ITopicClientFactory
    {
        ITopicClient Create(string connectionString);
    }
}
