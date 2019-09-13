using DFC.App.JobProfileOverview.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DFC.App.JobProfileOverview.Data.Contracts
{
    public interface IJobProfileOverviewSegmentService
    {
        Task<IEnumerable<JobProfileOverviewSegmentModel>> GetAllAsync();

        Task<JobProfileOverviewSegmentModel> GetByIdAsync(Guid documentId);

        Task<JobProfileOverviewSegmentModel> GetByNameAsync(string canonicalName, bool isDraft = false);

        Task<JobProfileOverviewSegmentModel> CreateAsync(JobProfileOverviewSegmentModel careerPathSegmentModel);

        Task<JobProfileOverviewSegmentModel> ReplaceAsync(JobProfileOverviewSegmentModel careerPathSegmentModel);

        Task<bool> DeleteAsync(Guid documentId, object partitionKeyValue);
    }
}
