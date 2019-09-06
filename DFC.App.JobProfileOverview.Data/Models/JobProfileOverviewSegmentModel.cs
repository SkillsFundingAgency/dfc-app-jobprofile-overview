using DFC.App.JobProfileOverview.Data.Contracts;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace DFC.App.JobProfileOverview.Data.Models
{
    public class JobProfileOverviewSegmentModel : IDataModel
    {
        public JobProfileOverviewSegmentModel()
        {
            Data = new JobProfileOverviewSegmentDataModel();
        }

        [JsonProperty(PropertyName = "id")]
        public Guid DocumentId { get; set; }

        [Required]
        [JsonProperty(PropertyName = "canonicalName")]
        public string CanonicalName { get; set; }

        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }

        [Display(Name = "Last Reviewed")]
        [JsonProperty(PropertyName = "lastReviewed")]
        public DateTime LastReviewed { get; set; }

        [JsonProperty(PropertyName = "data")]
        public JobProfileOverviewSegmentDataModel Data { get; set; }
    }
}
