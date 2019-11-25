﻿using AutoMapper;
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
    [Trait("Profile Service", "Patch Working PatternDetail Tests")]
    public class SegmentServicePatchWorkingPatternDetailTests
    {
        private readonly IMapper mapper;
        private readonly ICosmosRepository<JobProfileOverviewSegmentModel> repository;
        private readonly IJobProfileOverviewSegmentService jobProfileOverviewSegmentService;
        private readonly IJobProfileSegmentRefreshService<RefreshJobProfileSegmentServiceBusModel> jobProfileSegmentRefreshService;

        public SegmentServicePatchWorkingPatternDetailTests()
        {
            jobProfileSegmentRefreshService = A.Fake<IJobProfileSegmentRefreshService<RefreshJobProfileSegmentServiceBusModel>>();
            mapper = A.Fake<IMapper>();
            repository = A.Fake<ICosmosRepository<JobProfileOverviewSegmentModel>>();
            jobProfileOverviewSegmentService = new JobProfileOverviewSegmentService(repository, mapper, jobProfileSegmentRefreshService);
        }

        [Fact]
        public async Task JobProfileOverviewSegmentServicePatchWorkingPatternDetailReturnsExceptionWhenPatchmodelIsNull()
        {
            // arrange
            PatchWorkingPatternDetailModel patchModel = null;
            var documentId = Guid.NewGuid();

            // act
            var exceptionResult = await Assert.ThrowsAsync<ArgumentNullException>(async () => await jobProfileOverviewSegmentService.PatchWorkingPatternDetailAsync(patchModel, documentId).ConfigureAwait(false)).ConfigureAwait(false);

            // assert
            Assert.Equal("Value cannot be null.\r\nParameter name: patchModel", exceptionResult.Message);
        }

        [Fact]
        public async Task JobProfileOverviewSegmentServicePatchWorkingPatternDetailReturnsNotFoundWhenDocumentNotExists()
        {
            // arrange
            var patchModel = A.Fake<PatchWorkingPatternDetailModel>();
            var documentId = Guid.NewGuid();
            JobProfileOverviewSegmentModel existingSegmentModel = null;
            var expectedResult = HttpStatusCode.NotFound;

            A.CallTo(() => repository.GetAsync(A<Expression<Func<JobProfileOverviewSegmentModel, bool>>>.Ignored)).Returns(existingSegmentModel);

            // act
            var result = await jobProfileOverviewSegmentService.PatchWorkingPatternDetailAsync(patchModel, documentId).ConfigureAwait(false);

            // assert
            A.CallTo(() => repository.GetAsync(A<Expression<Func<JobProfileOverviewSegmentModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task JobProfileOverviewSegmentServicePatchWorkingPatternDetailReturnsAlreadyReportedWhenOutOfSequence()
        {
            // arrange
            var patchModel = A.Fake<PatchWorkingPatternDetailModel>();
            var documentId = Guid.NewGuid();
            var existingSegmentModel = A.Fake<JobProfileOverviewSegmentModel>();
            var expectedResult = HttpStatusCode.AlreadyReported;

            patchModel.SequenceNumber = 1;
            patchModel.MessageAction = MessageAction.Published;
            existingSegmentModel.SequenceNumber = patchModel.SequenceNumber + 1;

            A.CallTo(() => repository.GetAsync(A<Expression<Func<JobProfileOverviewSegmentModel, bool>>>.Ignored)).Returns(existingSegmentModel);

            // act
            var result = await jobProfileOverviewSegmentService.PatchWorkingPatternDetailAsync(patchModel, documentId).ConfigureAwait(false);

            // assert
            A.CallTo(() => repository.GetAsync(A<Expression<Func<JobProfileOverviewSegmentModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task JobProfileOverviewSegmentServicePatchWorkingPatternDetailReturnsAlreadyReportedWhenEntryRequirementForDeleted()
        {
            // arrange
            var patchModel = A.Fake<PatchWorkingPatternDetailModel>();
            var documentId = Guid.NewGuid();
            var existingSegmentModel = A.Fake<JobProfileOverviewSegmentModel>();
            var expectedResult = HttpStatusCode.AlreadyReported;

            existingSegmentModel.SequenceNumber = 1;
            patchModel.SequenceNumber = existingSegmentModel.SequenceNumber + 1;
            patchModel.MessageAction = MessageAction.Deleted;

            A.CallTo(() => repository.GetAsync(A<Expression<Func<JobProfileOverviewSegmentModel, bool>>>.Ignored)).Returns(existingSegmentModel);

            // act
            var result = await jobProfileOverviewSegmentService.PatchWorkingPatternDetailAsync(patchModel, documentId).ConfigureAwait(false);

            // assert
            A.CallTo(() => repository.GetAsync(A<Expression<Func<JobProfileOverviewSegmentModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task JobProfileOverviewSegmentServicePatchWorkingPatternDetailReturnsNotFoundWhenMissingEntryRequirementForPublished()
        {
            // arrange
            var patchModel = A.Fake<PatchWorkingPatternDetailModel>();
            var documentId = Guid.NewGuid();
            var existingSegmentModel = A.Fake<JobProfileOverviewSegmentModel>();
            var expectedResult = HttpStatusCode.NotFound;

            existingSegmentModel.SequenceNumber = 1;
            patchModel.SequenceNumber = existingSegmentModel.SequenceNumber + 1;
            patchModel.MessageAction = MessageAction.Published;

            A.CallTo(() => repository.GetAsync(A<Expression<Func<JobProfileOverviewSegmentModel, bool>>>.Ignored)).Returns(existingSegmentModel);

            // act
            var result = await jobProfileOverviewSegmentService.PatchWorkingPatternDetailAsync(patchModel, documentId).ConfigureAwait(false);

            // assert
            A.CallTo(() => repository.GetAsync(A<Expression<Func<JobProfileOverviewSegmentModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task JobProfileOverviewSegmentServicePatchWorkingPatternDetailReturnsOkWhenDeleted()
        {
            // arrange
            var patchModelId = Guid.NewGuid();
            var patchModel = A.Fake<PatchWorkingPatternDetailModel>();
            var documentId = Guid.NewGuid();
            var existingSegmentModel = A.Fake<JobProfileOverviewSegmentModel>();
            var refreshJobProfileSegmentServiceBusModel = A.Fake<RefreshJobProfileSegmentServiceBusModel>();
            var expectedResult = HttpStatusCode.OK;

            existingSegmentModel.SequenceNumber = 1;
            existingSegmentModel.Data = A.Fake<JobProfileOverviewSegmentDataModel>();
            existingSegmentModel.Data.WorkingPatternDetails = new List<Data.Models.WorkingPatternDetail>
            {
                new Data.Models.WorkingPatternDetail()
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
            var result = await jobProfileOverviewSegmentService.PatchWorkingPatternDetailAsync(patchModel, documentId).ConfigureAwait(false);

            // assert
            A.CallTo(() => repository.GetAsync(A<Expression<Func<JobProfileOverviewSegmentModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => mapper.Map<RefreshJobProfileSegmentServiceBusModel>(existingSegmentModel)).MustHaveHappenedOnceExactly();
            A.CallTo(() => repository.UpsertAsync(existingSegmentModel)).MustHaveHappenedOnceExactly();
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task JobProfileOverviewSegmentServicePatchWorkingPatternDetailReturnsNotFoundWhenCreated()
        {
            // arrange
            var patchModel = A.Fake<PatchWorkingPatternDetailModel>();
            var documentId = Guid.NewGuid();
            var existingSegmentModel = A.Fake<JobProfileOverviewSegmentModel>();
            var expectedResult = HttpStatusCode.NotFound;

            existingSegmentModel.SequenceNumber = 1;
            patchModel.SequenceNumber = existingSegmentModel.SequenceNumber + 1;
            patchModel.MessageAction = MessageAction.Published;

            A.CallTo(() => repository.GetAsync(A<Expression<Func<JobProfileOverviewSegmentModel, bool>>>.Ignored)).Returns(existingSegmentModel);

            // act
            var result = await jobProfileOverviewSegmentService.PatchWorkingPatternDetailAsync(patchModel, documentId).ConfigureAwait(false);

            // assert
            A.CallTo(() => repository.GetAsync(A<Expression<Func<JobProfileOverviewSegmentModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task JobProfileOverviewSegmentServicePatchWorkingPatternDetailReturnsOkWhenUpdated()
        {
            // arrange
            var patchModelId = Guid.NewGuid();
            var patchModel = A.Fake<PatchWorkingPatternDetailModel>();
            var documentId = Guid.NewGuid();
            var existingSegmentModel = A.Fake<JobProfileOverviewSegmentModel>();
            var refreshJobProfileSegmentServiceBusModel = A.Fake<RefreshJobProfileSegmentServiceBusModel>();
            var expectedResult = HttpStatusCode.OK;

            existingSegmentModel.SequenceNumber = 1;
            existingSegmentModel.Data = A.Fake<JobProfileOverviewSegmentDataModel>();
            existingSegmentModel.Data.WorkingPatternDetails = new List<Data.Models.WorkingPatternDetail>
            {
                new Data.Models.WorkingPatternDetail()
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
            var result = await jobProfileOverviewSegmentService.PatchWorkingPatternDetailAsync(patchModel, documentId).ConfigureAwait(false);

            // assert
            A.CallTo(() => repository.GetAsync(A<Expression<Func<JobProfileOverviewSegmentModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => mapper.Map<RefreshJobProfileSegmentServiceBusModel>(existingSegmentModel)).MustHaveHappenedOnceExactly();
            A.CallTo(() => repository.UpsertAsync(existingSegmentModel)).MustHaveHappenedOnceExactly();
            Assert.Equal(expectedResult, result);
        }
    }
}