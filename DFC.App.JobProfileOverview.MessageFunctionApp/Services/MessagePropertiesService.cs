using Microsoft.Azure.ServiceBus;
using System.Diagnostics.CodeAnalysis;

namespace DFC.App.JobProfileOverview.MessageFunctionApp.Services
{
    [ExcludeFromCodeCoverage]
    public class MessagePropertiesService : IMessagePropertiesService
    {
        public long GetSequenceNumber(Message message)
        {
            return (message?.SystemProperties?.SequenceNumber).GetValueOrDefault();
        }
    }
}
