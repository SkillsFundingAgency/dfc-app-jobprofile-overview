using Newtonsoft.Json;
using System;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model.API
{
    public class JobProfileOverviewResponseBody
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("lastUpdatedDate")]
        public DateTime LastUpdatedDate { get; set; }

        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("soc")]
        public string SOC { get; set; }

        [JsonProperty("oNetOccupationalCode")]
        public string ONetOccupationalCode { get; set; }

        [JsonProperty("alternativeTitle")]
        public string AlternativeTitle { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonProperty("salaryStarter")]
        public string SalaryStarter { get; set; }

        [JsonProperty("salaryExperienced")]
        public string SalaryExperienced { get; set; }

        [JsonProperty("minimumHours")]
        public double MinimumHours { get; set; }

        [JsonProperty("maximumHours")]
        public double MaximumHours { get; set; }

        [JsonProperty("workingHoursDetails")]
        public string WorkingHoursDetails { get; set; }

        [JsonProperty("workingPattern")]
        public string WorkingPattern { get; set; }

        [JsonProperty("workingPatternDetails")]
        public string WorkingPatternDetails { get; set; }
    }
}
