using DFC.App.JobProfileOverview.Data.Contracts;
using DFC.App.JobProfileOverview.Data.Models;
using System;
using System.Threading.Tasks;

namespace DFC.App.JobProfileOverview.DraftSegmentService
{
    public class DraftJobProfileOverviewSegmentService : IDraftJobProfileOverviewSegmentService
    {
        public Task<JobProfileOverviewSegmentModel> GetSitefinityData(string canonicalName)
        {
            throw new NotImplementedException();
        }
    }
}
