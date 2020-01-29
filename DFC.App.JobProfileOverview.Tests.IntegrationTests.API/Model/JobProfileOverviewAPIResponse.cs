using System;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model
{
    public class JobProfileOverviewAPIResponse
    {
        public string title { get; set; }
        public DateTime lastUpdatedDate { get; set; }
        public string url { get; set; }
        public string soc { get; set; }
        public string oNetOccupationalCode { get; set; }
        public string alternativeTitle { get; set; }
        public string overview { get; set; }
        public string salaryStarter { get; set; }
        public string salaryExperienced { get; set; }
        public double minimumHours { get; set; }
        public double maximumHours { get; set; }
        public string workingHoursDetails { get; set; }
        public string workingPattern { get; set; }
        public string workingPatternDetails { get; set; }
    }
}
