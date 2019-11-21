using System;

namespace DFC.App.JobProfileOverview.Data.ServiceBusModels
{
    public class WorkingPatternDetail
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }
    }
}