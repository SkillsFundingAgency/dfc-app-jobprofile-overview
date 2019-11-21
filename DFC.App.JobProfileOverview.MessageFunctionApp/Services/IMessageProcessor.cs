using DFC.App.JobProfileOverview.Data.Enums;
using System.Net;
using System.Threading.Tasks;

namespace DFC.App.JobProfileOverview.MessageFunctionApp.Services
{
    public interface IMessageProcessor
    {
        Task<HttpStatusCode> ProcessAsync(string message, long sequenceNumber, MessageContentType messageContentType, MessageAction messageAction);
    }
}