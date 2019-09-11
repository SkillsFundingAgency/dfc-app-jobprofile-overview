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
        public string CanonicalName { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;

        public int PartitionKey => Created.Second;

        public JobProfileOverviewSegmentDataModel Data { get; set; }
    }
}
