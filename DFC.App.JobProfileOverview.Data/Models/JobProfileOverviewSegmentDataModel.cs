﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace DFC.App.JobProfileOverview.Data.Models
{
    public class JobProfileOverviewSegmentDataModel
    {
        [JsonProperty(PropertyName = "socCode")]
        public string SocCode { get; set; }

        [JsonProperty(PropertyName = "socDescription")]
        public string SocDescription { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "alternativeTitle")]
        public string AlternativeTitle { get; set; }

        [JsonProperty(PropertyName = "hiddenAlternativeTitle")]
        public List<string> HiddenAlternativeTitle { get; set; }

        [JsonProperty(PropertyName = "jobProfileSpecialism")]
        public List<string> JobProfileSpecialism { get; set; }

        [JsonProperty(PropertyName = "overview")]
        public string Overview { get; set; }

        [JsonProperty(PropertyName = "salaryExperienced")]
        public string SalaryExperienced { get; set; }

        [JsonProperty(PropertyName = "minimumHours")]
        public decimal MinimumHours { get; set; }

        [JsonProperty(PropertyName = "maximumHours")]
        public decimal MaximumHours { get; set; }

        [JsonProperty(PropertyName = "workingHoursDetails")]
        public List<string> WorkingHoursDetails { get; set; }

        [JsonProperty(PropertyName = "workingPattern")]
        public List<string> WorkingPattern { get; set; }

        [JsonProperty(PropertyName = "workingPatternDetails")]
        public List<string> WorkingPatternDetails { get; set; }
    }
}