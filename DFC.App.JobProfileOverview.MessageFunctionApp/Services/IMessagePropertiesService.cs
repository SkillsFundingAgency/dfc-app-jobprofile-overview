using Microsoft.Azure.ServiceBus;

namespace DFC.App.JobProfileOverview.MessageFunctionApp.Services
{
    public interface IMessagePropertiesService
    {
        long GetSequenceNumber(Message message);
    }
}
