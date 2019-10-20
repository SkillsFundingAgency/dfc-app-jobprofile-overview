using System;

namespace DFC.App.JobProfileOverview.Data.ServiceBusModels.PatchModels
{
    public class PatchApprenticeshipStandardsServiceBusModel : BaseJobProfileMessage
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Uri Url { get; set; }
    }
}