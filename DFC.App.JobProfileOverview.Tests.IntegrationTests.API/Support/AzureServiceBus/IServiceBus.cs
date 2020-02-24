using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model.Support;
using Microsoft.Azure.ServiceBus;
using System.Threading.Tasks;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.AzureServiceBus
{
    internal interface IServiceBus
    {
        Task SendMessage(Message message);
    }
}
