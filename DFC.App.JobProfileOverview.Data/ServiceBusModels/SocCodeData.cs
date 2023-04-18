using System;

namespace DFC.App.JobProfileOverview.Data.ServiceBusModels
{
    public class SocCodeData
    {
        public Guid Id { get; set; }

        public string SOCCode { get; set; }
        public string Soc2020 { get; set; }

        public string Soc2020Extension { get; set; }

        public string Description { get; set; }

        public string ONetOccupationalCode { get; set; }

        public string UrlName { get; set; }
    }
}