using AutoMapper;
using DFC.App.JobProfileOverview.Data.Enums;
using DFC.App.JobProfileOverview.Data.Models;
using DFC.App.JobProfileOverview.Data.Models.PatchModels;
using DFC.App.JobProfileOverview.Data.ServiceBusModels;
using DFC.App.JobProfileOverview.Repository.CosmosDb;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace DFC.App.JobProfileOverview.SegmentService.UnitTests.SegmentServiceTests
{
    [Trait("Profile Service", "Patch Working Hours Detail Tests")]
    public class SegmentServicePatchWorkingHoursDetailTests
    {
        private readonly IMapper mapper;
        private readonly ICosmosRepository<JobProfileOverviewSegmentModel> repository;
        private readonly IJobProfileOverviewSegmentService jobProfileOverviewSegmentService;
        private readonly IJobProfileSegmentRefreshService<RefreshJobProfileSegmentServiceBusModel> jobProfileSegmentRefreshService;

        public SegmentServicePatchWorkingHoursDetailTests()
        {
            jobProfileSegmentRefreshService = A.Fake<IJobProfileSegmentRefreshService<RefreshJobProfileSegmentServiceBusModel>>();
            mapper = A.Fake<IMapper>();
            repository = A.Fake<ICosmosRepository<JobProfileOverviewSegmentModel>>();
            jobProfileOverviewSegmentService = new JobProfileOverviewSegmentService(repository, mapper, jobProfileSegmentRefreshService);
        }

        [Fact]
        public async Task JobProfileOverviewSegmentServicePatchWorkingHoursDetailReturnsExceptionWhenPatchmodelIsNull()
        {
            // arrange
            PatchWorkingHoursDetailModel patchModel = null;
            var documentId = Guid.NewGuid();

            // act
            var exceptionResult = await Assert.ThrowsAsync<ArgumentNullException>(async () => await jobProfileOverviewSegmentService.PatchWorkingHoursDetailAsync(patchModel, documentId).ConfigureAwait(false)).ConfigureAwait(false);

            // assert
            Assert.Equal("Value cannot be null. (Parameter 'patchModel')", exceptionResult.Message);
        }

        [Fact]
        public async Task JobProfileOverviewSegmentServicePatchWorkingHoursDetailReturnsNotFoundWhenDocumentNotExists()
        {
            // arrange
            var patchModel = A.Fake<PatchWorkingHoursDetailModel>();
            var documentId = Guid.NewGuid();
            JobProfileOverviewSegmentModel existingSegmentModel = null;
            var expectedResult = HttpStatusCode.NotFound;

            A.CallTo(() => repository.GetAsync(A<Expression<Func<JobProfileOverviewSegmentModel, bool>>>.Ignored)).Returns(existingSegmentModel);

            // act
            var result = await jobProfileOverviewSegmentService.PatchWorkingHoursDetailAsync(patchModel, documentId).ConfigureAwait(false);

            // assert
            A.CallTo(() => repository.GetAsync(A<Expression<Func<JobProfileOverviewSegmentModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task JobProfileOverviewSegmentServicePatchWorkingHoursDetailReturnsAlreadyReportedWhenOutOfSequence()
        {
            // arrange
            var patchModel = A.Fake<PatchWorkingHoursDetailModel>();
            var documentId = Guid.NewGuid();
            var existingSegmentModel = A.Fake<JobProfileOverviewSegmentModel>();
            var expectedResult = HttpStatusCode.AlreadyReported;

            patchModel.SequenceNumber = 1;
            patchModel.MessageAction = MessageAction.Published;
            existingSegmentModel.SequenceNumber = patchModel.SequenceNumber + 1;

            A.CallTo(() => repository.GetAsync(A<Expression<Func<JobProfileOverviewSegmentModel, bool>>>.Ignored)).Returns(existingSegmentModel);

            // act
            var result = await jobProfileOverviewSegmentService.PatchWorkingHoursDetailAsync(patchModel, documentId).ConfigureAwait(false);

            // assert
            A.CallTo(() => repository.GetAsync(A<Expression<Func<JobProfileOverviewSegmentModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task JobProfileOverviewSegmentServicePatchWorkingHoursDetailReturnsAlreadyReportedWhenEntryRequirementForDeleted()
        {
            // arrange
            var patchModel = A.Fake<PatchWorkingHoursDetailModel>();
            var documentId = Guid.NewGuid();
            var existingSegmentModel = A.Fake<JobProfileOverviewSegmentModel>();
            var expectedResult = HttpStatusCode.AlreadyReported;

            existingSegmentModel.SequenceNumber = 1;
            patchModel.SequenceNumber = existingSegmentModel.SequenceNumber + 1;
            patchModel.MessageAction = MessageAction.Deleted;

            A.CallTo(() => repository.GetAsync(A<Expression<Func<JobProfileOverviewSegmentModel, bool>>>.Ignored)).Returns(existingSegmentModel);

            // act
            var result = await jobProfileOverviewSegmentService.PatchWorkingHoursDetailAsync(patchModel, documentId).ConfigureAwait(false);

            // assert
            A.CallTo(() => repository.GetAsync(A<Expression<Func<JobProfileOverviewSegmentModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task JobProfileOverviewSegmentServicePatchWorkingHoursDetailReturnsNotFoundWhenMissingEntryRequirementForPublished()
        {
            // arrange
            var patchModel = A.Fake<PatchWorkingHoursDetailModel>();
            var documentId = Guid.NewGuid();
            var existingSegmentModel = A.Fake<JobProfileOverviewSegmentModel>();
            var expectedResult = HttpStatusCode.NotFound;

            existingSegmentModel.SequenceNumber = 1;
            patchModel.SequenceNumber = existingSegmentModel.SequenceNumber + 1;
            patchModel.MessageAction = MessageAction.Published;

            A.CallTo(() => repository.GetAsync(A<Expression<Func<JobProfileOverviewSegmentModel, bool>>>.Ignored)).Returns(existingSegmentModel);

            // act
            var result = await jobProfileOverviewSegmentService.PatchWorkingHoursDetailAsync(patchModel, documentId).ConfigureAwait(false);

            // assert
            A.CallTo(() => repository.GetAsync(A<Expression<Func<JobProfileOverviewSegmentModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task JobProfileOverviewSegmentServicePatchWorkingHoursDetailReturnsOkWhenDeleted()
        {
            // arrange
            var patchModelId = Guid.NewGuid();
            var patchModel = A.Fake<PatchWorkingHoursDetailModel>();
            var documentId = Guid.NewGuid();
            var existingSegmentModel = A.Fake<JobProfileOverviewSegmentModel>();
            var refreshJobProfileSegmentServiceBusModel = A.Fake<RefreshJobProfileSegmentServiceBusModel>();
            var expectedResult = HttpStatusCode.OK;

            existingSegmentModel.SequenceNumber = 1;
            existingSegmentModel.Data = A.Fake<JobProfileOverviewSegmentDataModel>();
            existingSegmentModel.Data.WorkingHoursDetails = new List<Data.Models.WorkingHoursDetail>
            {
                new Data.Models.WorkingHoursDetail()
                {
                     Id = patchModelId,
                },
            };

            patchModel.SequenceNumber = existingSegmentModel.SequenceNumber + 1;
            patchModel.MessageAction = MessageAction.Deleted;
            patchModel.Id = patchModelId;

            A.CallTo(() => repository.GetAsync(A<Expression<Func<JobProfileOverviewSegmentModel, bool>>>.Ignored)).Returns(existingSegmentModel);
            A.CallTo(() => mapper.Map<RefreshJobProfileSegmentServiceBusModel>(existingSegmentModel)).Returns(refreshJobProfileSegmentServiceBusModel);
            A.CallTo(() => repository.UpsertAsync(existingSegmentModel)).Returns(expectedResult);

            // act
            var result = await jobProfileOverviewSegmentService.PatchWorkingHoursDetailAsync(patchModel, documentId).ConfigureAwait(false);

            // assert
            A.CallTo(() => repository.GetAsync(A<Expression<Func<JobProfileOverviewSegmentModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => mapper.Map<RefreshJobProfileSegmentServiceBusModel>(existingSegmentModel)).MustHaveHappenedOnceExactly();
            A.CallTo(() => repository.UpsertAsync(existingSegmentModel)).MustHaveHappenedOnceExactly();
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task JobProfileOverviewSegmentServicePatchWorkingHoursDetailReturnsNotFoundWhenCreated()
        {
            // arrange
            var patchModel = A.Fake<PatchWorkingHoursDetailModel>();
            var documentId = Guid.NewGuid();
            var existingSegmentModel = A.Fake<JobProfileOverviewSegmentModel>();
            var expectedResult = HttpStatusCode.NotFound;

            existingSegmentModel.SequenceNumber = 1;
            patchModel.SequenceNumber = existingSegmentModel.SequenceNumber + 1;
            patchModel.MessageAction = MessageAction.Published;

            A.CallTo(() => repository.GetAsync(A<Expression<Func<JobProfileOverviewSegmentModel, bool>>>.Ignored)).Returns(existingSegmentModel);

            // act
            var result = await jobProfileOverviewSegmentService.PatchWorkingHoursDetailAsync(patchModel, documentId).ConfigureAwait(false);

            // assert
            A.CallTo(() => repository.GetAsync(A<Expression<Func<JobProfileOverviewSegmentModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task JobProfileOverviewSegmentServicePatchWorkingHoursDetailReturnsOkWhenUpdated()
        {
            // arrange
            var patchModelId = Guid.NewGuid();
            var patchModel = A.Fake<PatchWorkingHoursDetailModel>();
            var documentId = Guid.NewGuid();
            var existingSegmentModel = A.Fake<JobProfileOverviewSegmentModel>();
            var refreshJobProfileSegmentServiceBusModel = A.Fake<RefreshJobProfileSegmentServiceBusModel>();
            var expectedResult = HttpStatusCode.OK;

            existingSegmentModel.SequenceNumber = 1;
            existingSegmentModel.Data = A.Fake<JobProfileOverviewSegmentDataModel>();
            existingSegmentModel.Data.WorkingHoursDetails = new List<Data.Models.WorkingHoursDetail>
            {
                new Data.Models.WorkingHoursDetail()
                {
                     Id = patchModelId,
                },
            };

            patchModel.SequenceNumber = existingSegmentModel.SequenceNumber + 1;
            patchModel.MessageAction = MessageAction.Published;
            patchModel.Id = patchModelId;

            A.CallTo(() => repository.GetAsync(A<Expression<Func<JobProfileOverviewSegmentModel, bool>>>.Ignored)).Returns(existingSegmentModel);
            A.CallTo(() => mapper.Map<RefreshJobProfileSegmentServiceBusModel>(existingSegmentModel)).Returns(refreshJobProfileSegmentServiceBusModel);
            A.CallTo(() => repository.UpsertAsync(existingSegmentModel)).Returns(expectedResult);

            // act
            var result = await jobProfileOverviewSegmentService.PatchWorkingHoursDetailAsync(patchModel, documentId).ConfigureAwait(false);

            // assert
            A.CallTo(() => repository.GetAsync(A<Expression<Func<JobProfileOverviewSegmentModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => mapper.Map<RefreshJobProfileSegmentServiceBusModel>(existingSegmentModel)).MustHaveHappenedOnceExactly();
            A.CallTo(() => repository.UpsertAsync(existingSegmentModel)).MustHaveHappenedOnceExactly();
            Assert.Equal(expectedResult, result);
        }
    }
}
