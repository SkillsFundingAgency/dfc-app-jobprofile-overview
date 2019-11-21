using DFC.App.JobProfileOverview.Data.Models;

namespace DFC.App.JobProfileOverview.MessageFunctionApp.Services
{
    public interface IMappingService
    {
        JobProfileOverviewSegmentModel MapToSegmentModel(string message, long sequenceNumber);
    }
}