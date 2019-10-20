using System;
using System.Collections.Generic;

namespace DFC.App.JobProfileOverview.Data.ServiceBusModels.PatchModels
{
    public class PatchSocDataServiceBusModel : BaseJobProfileMessage
    {
        public Guid Id { get; set; }

        public string SocCode { get; set; }

        public string Description { get; set; }

        public string ONetOccupationalCode { get; set; }

        public string UrlName { get; set; }

        public IEnumerable<Models.ApprenticeshipStandards> ApprenticeshipStandards { get; set; }

        public IEnumerable<Models.ApprenticeshipFrameworks> ApprenticeshipFramework { get; set; }
    }
}