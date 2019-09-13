using DFC.App.JobProfileOverview.Data.Contracts;
using DFC.App.JobProfileOverview.Data.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace DFC.App.JobProfileOverview.SegmentService
{
    public class JobProfileOverviewSegmentService : IJobProfileOverviewSegmentService
    {
        private readonly ICosmosRepository<JobProfileOverviewSegmentModel> repository;

        public JobProfileOverviewSegmentService(ICosmosRepository<JobProfileOverviewSegmentModel> repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<JobProfileOverviewSegmentModel>> GetAllAsync()
        {
            return await repository.GetAllAsync().ConfigureAwait(false);
        }

        public async Task<JobProfileOverviewSegmentModel> GetByIdAsync(Guid documentId)
        {
            return await repository.GetAsync(d => d.DocumentId == documentId).ConfigureAwait(false);
        }

        public async Task<JobProfileOverviewSegmentModel> GetByNameAsync(string canonicalName, bool isDraft = false)
        {
            if (string.IsNullOrWhiteSpace(canonicalName))
            {
                throw new ArgumentNullException(nameof(canonicalName));
            }

            return await repository.GetAsync(d => d.CanonicalName.ToLower() == canonicalName.ToLowerInvariant()).ConfigureAwait(false);
        }

        public async Task<JobProfileOverviewSegmentModel> CreateAsync(JobProfileOverviewSegmentModel overviewSegmentModel)
        {
            if (overviewSegmentModel == null)
            {
                throw new ArgumentNullException(nameof(overviewSegmentModel));
            }

            if (overviewSegmentModel.Data == null)
            {
                overviewSegmentModel.Data = new JobProfileOverviewSegmentDataModel();
            }

            var result = await repository.CreateAsync(overviewSegmentModel).ConfigureAwait(false);

            return result == HttpStatusCode.Created
                ? await GetByIdAsync(overviewSegmentModel.DocumentId).ConfigureAwait(false)
                : null;
        }

        public async Task<JobProfileOverviewSegmentModel> ReplaceAsync(JobProfileOverviewSegmentModel overviewSegmentModel)
        {
            if (overviewSegmentModel == null)
            {
                throw new ArgumentNullException(nameof(overviewSegmentModel));
            }

            if (overviewSegmentModel.Data == null)
            {
                overviewSegmentModel.Data = new JobProfileOverviewSegmentDataModel();
            }

            var result = await repository.UpdateAsync(overviewSegmentModel.DocumentId, overviewSegmentModel).ConfigureAwait(false);

            return result == HttpStatusCode.OK
                ? await GetByIdAsync(overviewSegmentModel.DocumentId).ConfigureAwait(false)
                : null;
        }

        public async Task<bool> DeleteAsync(Guid documentId, int partitionKeyValue)
        {
            var result = await repository.DeleteAsync(documentId, partitionKeyValue).ConfigureAwait(false);
            return result == HttpStatusCode.NoContent;
        }
    }
}
