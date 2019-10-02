using System.Net;

namespace DFC.App.JobProfileOverview.Data.Models
{
    public class UpsertOverviewModelResponse
    {
        public JobProfileOverviewSegmentModel JobProfileOverviewSegmentModel { get; set; }

        public HttpStatusCode ResponseStatusCode { get; set; }
    }
}