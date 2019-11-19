using AutoMapper;
using DFC.App.JobProfileOverview.Data.Enums;
using DFC.App.JobProfileOverview.Data.Models;
using DFC.App.JobProfileOverview.Data.Models.PatchModels;
using DFC.App.JobProfileOverview.Data.ServiceBusModels;
using DFC.App.JobProfileOverview.Repository.CosmosDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DFC.App.JobProfileOverview.SegmentService
{
    public class JobProfileOverviewSegmentService : IJobProfileOverviewSegmentService
    {
        private readonly ICosmosRepository<JobProfileOverviewSegmentModel> repository;
        private readonly IMapper mapper;
        private readonly IJobProfileSegmentRefreshService<RefreshJobProfileSegmentServiceBusModel> jobProfileSegmentRefreshService;

        public JobProfileOverviewSegmentService(ICosmosRepository<JobProfileOverviewSegmentModel> repository, IMapper mapper, IJobProfileSegmentRefreshService<RefreshJobProfileSegmentServiceBusModel> jobProfileSegmentRefreshService)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.jobProfileSegmentRefreshService = jobProfileSegmentRefreshService;
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

        public async Task<JobProfileOverviewSegmentModel> GetByNameAsync(string canonicalName)
        {
            if (string.IsNullOrWhiteSpace(canonicalName))
            {
                throw new ArgumentNullException(nameof(canonicalName));
            }

            return await repository.GetAsync(d => d.CanonicalName.ToLower() == canonicalName.ToLowerInvariant()).ConfigureAwait(false);
        }

        public async Task<HttpStatusCode> UpsertAsync(JobProfileOverviewSegmentModel jobProfileOverviewSegmentModel)
        {
            if (jobProfileOverviewSegmentModel == null)
            {
                throw new ArgumentNullException(nameof(jobProfileOverviewSegmentModel));
            }

            if (jobProfileOverviewSegmentModel.Data == null)
            {
                jobProfileOverviewSegmentModel.Data = new JobProfileOverviewSegmentDataModel();
            }

            return await UpsertAndRefreshSegmentModel(jobProfileOverviewSegmentModel).ConfigureAwait(false);
        }

        public async Task<HttpStatusCode> PatchWorkingPatternAsync(PatchWorkingPatternModel patchModel, Guid documentId)
        {
            if (patchModel is null)
            {
                throw new ArgumentNullException(nameof(patchModel));
            }

            var existingSegmentModel = await GetByIdAsync(documentId).ConfigureAwait(false);
            if (existingSegmentModel is null)
            {
                return HttpStatusCode.NotFound;
            }

            if (patchModel.SequenceNumber <= existingSegmentModel.SequenceNumber)
            {
                return HttpStatusCode.AlreadyReported;
            }

            var existingWorkingPattern = existingSegmentModel.Data.WorkingPattern.SingleOrDefault(r => r.Id == patchModel.Id);
            if (existingWorkingPattern is null)
            {
                return patchModel.MessageAction == MessageAction.Deleted ? HttpStatusCode.AlreadyReported : HttpStatusCode.NotFound;
            }

            var existingIndex = existingSegmentModel.Data.WorkingPattern.ToList().FindIndex(ai => ai.Id == patchModel.Id);

            if (patchModel.MessageAction == MessageAction.Deleted)
            {
                existingSegmentModel.Data.WorkingPattern.RemoveAt(existingIndex);
            }
            else
            {
                var updatedAdditionalInfo = mapper.Map<Data.Models.WorkingPattern>(patchModel);
                existingSegmentModel.Data.WorkingPattern[existingIndex] = updatedAdditionalInfo;
            }

            existingSegmentModel.SequenceNumber = patchModel.SequenceNumber;

            return await UpsertAndRefreshSegmentModel(existingSegmentModel).ConfigureAwait(false);
        }

        public async Task<HttpStatusCode> PatchHiddenAlternativeTitleAsync(PatchHiddenAlternativeTitleModel patchModel, Guid documentId)
        {
            if (patchModel is null)
            {
                throw new ArgumentNullException(nameof(patchModel));
            }

            var existingSegmentModel = await GetByIdAsync(documentId).ConfigureAwait(false);
            if (existingSegmentModel is null)
            {
                return HttpStatusCode.NotFound;
            }

            if (patchModel.SequenceNumber <= existingSegmentModel.SequenceNumber)
            {
                return HttpStatusCode.AlreadyReported;
            }

            var existingHiddenAltTitle = existingSegmentModel.Data.HiddenAlternativeTitle.SingleOrDefault(r => r.Id == patchModel.Id);
            if (existingHiddenAltTitle is null)
            {
                return patchModel.MessageAction == MessageAction.Deleted ? HttpStatusCode.AlreadyReported : HttpStatusCode.NotFound;
            }

            var existingIndex = existingSegmentModel.Data.HiddenAlternativeTitle.ToList().FindIndex(ai => ai.Id == patchModel.Id);
            if (patchModel.MessageAction == MessageAction.Deleted)
            {
                existingSegmentModel.Data.HiddenAlternativeTitle.RemoveAt(existingIndex);
            }
            else
            {
                var updatedHiddenAltTitle = mapper.Map<Data.Models.HiddenAlternativeTitle>(patchModel);
                existingSegmentModel.Data.HiddenAlternativeTitle[existingIndex] = updatedHiddenAltTitle;
            }

            existingSegmentModel.SequenceNumber = patchModel.SequenceNumber;

            return await UpsertAndRefreshSegmentModel(existingSegmentModel).ConfigureAwait(false);
        }

        public async Task<HttpStatusCode> PatchJobProfileSpecialismAsync(PatchJobProfileSpecialismModel patchModel, Guid documentId)
        {
            if (patchModel is null)
            {
                throw new ArgumentNullException(nameof(patchModel));
            }

            var existingSegmentModel = await GetByIdAsync(documentId).ConfigureAwait(false);
            if (existingSegmentModel is null)
            {
                return HttpStatusCode.NotFound;
            }

            if (patchModel.SequenceNumber <= existingSegmentModel.SequenceNumber)
            {
                return HttpStatusCode.AlreadyReported;
            }

            var existingSpecialism = existingSegmentModel.Data.JobProfileSpecialism.SingleOrDefault(r => r.Id == patchModel.Id);
            if (existingSpecialism is null)
            {
                return patchModel.MessageAction == MessageAction.Deleted ? HttpStatusCode.AlreadyReported : HttpStatusCode.NotFound;
            }

            var existingIndex = existingSegmentModel.Data.JobProfileSpecialism.ToList().FindIndex(ai => ai.Id == patchModel.Id);
            if (patchModel.MessageAction == MessageAction.Deleted)
            {
                existingSegmentModel.Data.JobProfileSpecialism.RemoveAt(existingIndex);
            }
            else
            {
                var updatedSpecialism = mapper.Map<Data.Models.JobProfileSpecialism>(patchModel);
                existingSegmentModel.Data.JobProfileSpecialism[existingIndex] = updatedSpecialism;
            }

            existingSegmentModel.SequenceNumber = patchModel.SequenceNumber;

            return await UpsertAndRefreshSegmentModel(existingSegmentModel).ConfigureAwait(false);
        }

        public async Task<HttpStatusCode> PatchWorkingHoursDetailAsync(PatchWorkingHoursDetailModel patchModel, Guid documentId)
        {
            if (patchModel is null)
            {
                throw new ArgumentNullException(nameof(patchModel));
            }

            var existingSegmentModel = await GetByIdAsync(documentId).ConfigureAwait(false);
            if (existingSegmentModel is null)
            {
                return HttpStatusCode.NotFound;
            }

            if (patchModel.SequenceNumber <= existingSegmentModel.SequenceNumber)
            {
                return HttpStatusCode.AlreadyReported;
            }

            var existingWorkingHoursDetail = existingSegmentModel.Data.WorkingHoursDetails.SingleOrDefault(r => r.Id == patchModel.Id);
            if (existingWorkingHoursDetail is null)
            {
                return patchModel.MessageAction == MessageAction.Deleted ? HttpStatusCode.AlreadyReported : HttpStatusCode.NotFound;
            }

            var existingIndex = existingSegmentModel.Data.WorkingHoursDetails.ToList().FindIndex(ai => ai.Id == patchModel.Id);

            if (patchModel.MessageAction == MessageAction.Deleted)
            {
                existingSegmentModel.Data.WorkingHoursDetails.RemoveAt(existingIndex);
            }
            else
            {
                var updatedWorkingHoursDetail = mapper.Map<Data.Models.WorkingHoursDetail>(patchModel);
                existingSegmentModel.Data.WorkingHoursDetails[existingIndex] = updatedWorkingHoursDetail;
            }

            existingSegmentModel.SequenceNumber = patchModel.SequenceNumber;

            return await UpsertAndRefreshSegmentModel(existingSegmentModel).ConfigureAwait(false);
        }

        public async Task<HttpStatusCode> PatchWorkingPatternDetailAsync(PatchWorkingPatternDetailModel patchModel, Guid documentId)
        {
            if (patchModel is null)
            {
                throw new ArgumentNullException(nameof(patchModel));
            }

            var existingSegmentModel = await GetByIdAsync(documentId).ConfigureAwait(false);
            if (existingSegmentModel is null)
            {
                return HttpStatusCode.NotFound;
            }

            if (patchModel.SequenceNumber <= existingSegmentModel.SequenceNumber)
            {
                return HttpStatusCode.AlreadyReported;
            }

            var existingWorkingPatternDetail = existingSegmentModel.Data.WorkingPatternDetails.SingleOrDefault(r => r.Id == patchModel.Id);
            if (existingWorkingPatternDetail is null)
            {
                return patchModel.MessageAction == MessageAction.Deleted ? HttpStatusCode.AlreadyReported : HttpStatusCode.NotFound;
            }

            var existingIndex = existingSegmentModel.Data.WorkingPatternDetails.ToList().FindIndex(ai => ai.Id == patchModel.Id);

            if (patchModel.MessageAction == MessageAction.Deleted)
            {
                existingSegmentModel.Data.WorkingPatternDetails.RemoveAt(existingIndex);
            }
            else
            {
                var updatedWorkingPatternDetail = mapper.Map<Data.Models.WorkingPatternDetail>(patchModel);
                existingSegmentModel.Data.WorkingPatternDetails[existingIndex] = updatedWorkingPatternDetail;
            }

            existingSegmentModel.SequenceNumber = patchModel.SequenceNumber;

            return await UpsertAndRefreshSegmentModel(existingSegmentModel).ConfigureAwait(false);
        }

        public async Task<HttpStatusCode> PatchSocCodeDataAsync(PatchSocDataModel patchModel, Guid documentId)
        {
            if (patchModel is null)
            {
                throw new ArgumentNullException(nameof(patchModel));
            }

            var existingSegmentModel = await GetByIdAsync(documentId).ConfigureAwait(false);
            if (existingSegmentModel is null)
            {
                return HttpStatusCode.NotFound;
            }

            if (patchModel.SequenceNumber <= existingSegmentModel.SequenceNumber)
            {
                return HttpStatusCode.AlreadyReported;
            }

            var existingSocData = existingSegmentModel.Data.Soc;
            if (existingSocData is null)
            {
                return patchModel.MessageAction == MessageAction.Deleted ? HttpStatusCode.AlreadyReported : HttpStatusCode.NotFound;
            }

            if (patchModel.MessageAction == MessageAction.Deleted) // What should this do on delete of SocData - null or new SocData?
            {
                existingSegmentModel.Data.Soc = new SocData();
            }
            else
            {
                var updatedSocData = mapper.Map<SocData>(patchModel);
                existingSegmentModel.Data.Soc = updatedSocData;
            }

            existingSegmentModel.SequenceNumber = patchModel.SequenceNumber;

            return await UpsertAndRefreshSegmentModel(existingSegmentModel).ConfigureAwait(false);
        }

        public async Task<bool> DeleteAsync(Guid documentId)
        {
            var result = await repository.DeleteAsync(documentId).ConfigureAwait(false);
            return result == HttpStatusCode.NoContent;
        }

        private async Task<HttpStatusCode> UpsertAndRefreshSegmentModel(JobProfileOverviewSegmentModel existingSegmentModel)
        {
            var result = await repository.UpsertAsync(existingSegmentModel).ConfigureAwait(false);

            if (result == HttpStatusCode.OK || result == HttpStatusCode.Created)
            {
                var refreshJobProfileSegmentServiceBusModel = mapper.Map<RefreshJobProfileSegmentServiceBusModel>(existingSegmentModel);

                await jobProfileSegmentRefreshService.SendMessageAsync(refreshJobProfileSegmentServiceBusModel).ConfigureAwait(false);
            }

            return result;
        }
    }
}