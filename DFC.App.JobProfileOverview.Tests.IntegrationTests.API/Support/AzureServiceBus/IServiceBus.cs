using Microsoft.Azure.ServiceBus;
using System.Threading.Tasks;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.AzureServiceBus
{
    public interface IServiceBus
    {
        Task SendMessage(Message message);
    }
}
