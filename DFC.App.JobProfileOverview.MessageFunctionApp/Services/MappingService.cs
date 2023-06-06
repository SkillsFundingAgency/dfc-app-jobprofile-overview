using AutoMapper;
using DFC.App.JobProfileOverview.Data.Models;
using DFC.App.JobProfileOverview.Data.ServiceBusModels;
using DFC.Logger.AppInsights.Contracts;
using Newtonsoft.Json;

namespace DFC.App.JobProfileOverview.MessageFunctionApp.Services
{
    public class MappingService : IMappingService
    {
        private readonly IMapper mapper;
        private readonly ILogService logService;

        public MappingService(IMapper mapper,ILogService logService)
        {
            this.mapper = mapper;
            this.logService = logService;
        }

        public JobProfileOverviewSegmentModel MapToSegmentModel(string message, long sequenceNumber)
        {
            logService.LogInformation($"MapToSegmentModel message {message} sequenceNumber{sequenceNumber} ");
            var fullJobProfileMessage = JsonConvert.DeserializeObject<JobProfileMessage>(message);
            var fullJobProfile = mapper.Map<JobProfileOverviewSegmentModel>(fullJobProfileMessage);
            fullJobProfile.SequenceNumber = sequenceNumber;

            return fullJobProfile;
        }
    }
}