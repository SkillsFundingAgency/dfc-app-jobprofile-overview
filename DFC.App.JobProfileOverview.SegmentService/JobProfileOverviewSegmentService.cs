using DFC.App.JobProfileOverview.Data.Contracts;
using DFC.App.JobProfileOverview.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DFC.App.JobProfileOverview.SegmentService
{
    public class JobProfileOverviewSegmentService : IJobProfileOverviewSegmentService
    {
        private readonly ICosmosRepository<JobProfileOverviewSegmentModel> repository;
        private readonly IDraftJobProfileOverviewSegmentService jobProfileOverviewSegmentService;

        public JobProfileOverviewSegmentService(ICosmosRepository<JobProfileOverviewSegmentModel> repository, IDraftJobProfileOverviewSegmentService jobProfileOverviewSegmentService)
        {
            this.repository = repository;
            this.jobProfileOverviewSegmentService = jobProfileOverviewSegmentService;
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

            return await repository.GetAsync(d => d.CanonicalName == canonicalName.ToLowerInvariant()).ConfigureAwait(false);

        }
    }
}
