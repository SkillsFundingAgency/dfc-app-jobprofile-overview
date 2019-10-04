using DFC.App.JobProfileOverview.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DFC.App.JobProfileOverview.SegmentService
{
    public interface IJobProfileOverviewSegmentService
    {
        Task<bool> PingAsync();

        Task<IEnumerable<JobProfileOverviewSegmentModel>> GetAllAsync();

        Task<JobProfileOverviewSegmentModel> GetByIdAsync(Guid documentId);

        Task<JobProfileOverviewSegmentModel> GetByNameAsync(string canonicalName, bool isDraft = false);

        Task<UpsertOverviewModelResponse> UpsertAsync(JobProfileOverviewSegmentModel jobProfileOverviewSegmentModel);

        Task<bool> DeleteAsync(Guid documentId);
    }
}