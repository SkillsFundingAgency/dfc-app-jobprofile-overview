using DFC.App.JobProfileOverview.Data.Models;
using DFC.App.JobProfileOverview.Data.Models.PatchModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace DFC.App.JobProfileOverview.SegmentService
{
    public interface IJobProfileOverviewSegmentService
    {
        Task<bool> PingAsync();

        Task<IEnumerable<JobProfileOverviewSegmentModel>> GetAllAsync();

        Task<JobProfileOverviewSegmentModel> GetByIdAsync(Guid documentId);

        Task<JobProfileOverviewSegmentModel> GetByNameAsync(string canonicalName, bool isDraft = false);

        Task<HttpStatusCode> UpsertAsync(JobProfileOverviewSegmentModel jobProfileOverviewSegmentModel);

        Task<bool> DeleteAsync(Guid documentId);

        Task<HttpStatusCode> PatchApprenticeshipFrameworksAsync(PatchApprenticeshipFrameworksModel patchModel, Guid documentId);

        Task<HttpStatusCode> PatchApprenticeshipStandardsAsync(PatchApprenticeshipStandardsModel patchModel, Guid documentId);

        Task<HttpStatusCode> PatchWorkingPatternAsync(PatchWorkingPatternModel patchModel, Guid documentId);

        Task<HttpStatusCode> PatchHiddenAlternativeTitleAsync(PatchHiddenAlternativeTitleModel patchModel, Guid documentId);

        Task<HttpStatusCode> PatchJobProfileSpecialismAsync(PatchJobProfileSpecialismModel patchModel, Guid documentId);

        Task<HttpStatusCode> PatchWorkingHoursDetailAsync(PatchWorkingHoursDetailModel patchModel, Guid documentId);

        Task<HttpStatusCode> PatchWorkingPatternDetailAsync(PatchWorkingPatternDetailModel patchModel, Guid documentId);

        Task<HttpStatusCode> PatchSocCodeDataAsync(PatchSocDataModel patchModel, Guid documentId);
    }
}