using AutoMapper;
using DFC.App.JobProfileOverview.Data.Models;
using DFC.App.JobProfileOverview.Data.ServiceBusModels;
using Newtonsoft.Json;

namespace DFC.App.JobProfileOverview.MessageFunctionApp.Services
{
    public class MappingService : IMappingService
    {
        private readonly IMapper mapper;

        public MappingService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public JobProfileOverviewSegmentModel MapToSegmentModel(string message, long sequenceNumber)
        {
            var fullJobProfileMessage = JsonConvert.DeserializeObject<JobProfileMessage>(message);
            var fullJobProfile = mapper.Map<JobProfileOverviewSegmentModel>(fullJobProfileMessage);
            fullJobProfile.SequenceNumber = sequenceNumber;

            return fullJobProfile;
        }
    }
}