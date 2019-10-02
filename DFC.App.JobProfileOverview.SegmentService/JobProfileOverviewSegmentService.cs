using DFC.App.JobProfileOverview.Data.Models;
using DFC.App.JobProfileOverview.Repository.CosmosDb;
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

        public async Task<bool> PingAsync()
        {
            return await repository.PingAsync().ConfigureAwait(false);
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

        public async Task<UpsertOverviewModelResponse> UpsertAsync(JobProfileOverviewSegmentModel jobProfileOverviewSegmentModel)
        {
            if (jobProfileOverviewSegmentModel == null)
            {
                throw new ArgumentNullException(nameof(jobProfileOverviewSegmentModel));
            }

            if (jobProfileOverviewSegmentModel.Data == null)
            {
                jobProfileOverviewSegmentModel.Data = new JobProfileOverviewSegmentDataModel();
            }

            var responseStatusCode = await repository.UpsertAsync(jobProfileOverviewSegmentModel).ConfigureAwait(false);

            return new UpsertOverviewModelResponse
            {
                JobProfileOverviewSegmentModel = jobProfileOverviewSegmentModel,
                ResponseStatusCode = responseStatusCode,
            };
        }

        public async Task<bool> DeleteAsync(Guid documentId)
        {
            var result = await repository.DeleteAsync(documentId).ConfigureAwait(false);
            return result == HttpStatusCode.NoContent;
        }
    }
}