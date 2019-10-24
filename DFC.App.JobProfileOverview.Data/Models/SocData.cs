using System;

namespace DFC.App.JobProfileOverview.Data.Models
{
    public class SocData
    {
        public Guid Id { get; set; }

        public string SocCode { get; set; }

        public string Description { get; set; }

        public string ONetOccupationalCode { get; set; }

        public string UrlName { get; set; }
    }
}