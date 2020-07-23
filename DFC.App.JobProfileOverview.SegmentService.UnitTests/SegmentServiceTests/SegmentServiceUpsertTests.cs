using DFC.App.JobProfileOverview.Data.Models;
using DFC.App.JobProfileOverview.Data.ServiceBusModels;
using DFC.App.JobProfileOverview.Repository.CosmosDb;
using FakeItEasy;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace DFC.App.JobProfileOverview.SegmentService.UnitTests.SegmentServiceTests
{
    [Trait("Profile Service", "Update Tests")]
    public class SegmentServiceUpsertTests
    {
        private readonly ICosmosRepository<JobProfileOverviewSegmentModel> repository;
        private readonly IJobProfileOverviewSegmentService jobProfileOverviewSegmentService;

        public SegmentServiceUpsertTests()
        {
            var jobProfileSegmentRefreshService = A.Fake<IJobProfileSegmentRefreshService<RefreshJobProfileSegmentServiceBusModel>>();
            var mapper = A.Fake<AutoMapper.IMapper>();
            repository = A.Fake<ICosmosRepository<JobProfileOverviewSegmentModel>>();
            jobProfileOverviewSegmentService = new JobProfileOverviewSegmentService(repository, mapper, jobProfileSegmentRefreshService);
        }

        [Fact]
        public async Task JobProfileOverviewSegmentServiceUpsertReturnsCreatedWhenDocumentCreated()
        {
            // arrange
            var jobProfileOverviewSegmentModel = A.Fake<JobProfileOverviewSegmentModel>();
            var expectedResult = HttpStatusCode.Created;

            A.CallTo(() => repository.UpsertAsync(jobProfileOverviewSegmentModel)).Returns(expectedResult);

            // act
            var result = await jobProfileOverviewSegmentService.UpsertAsync(jobProfileOverviewSegmentModel).ConfigureAwait(false);

            // assert
            A.CallTo(() => repository.UpsertAsync(jobProfileOverviewSegmentModel)).MustHaveHappenedOnceExactly();
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task JobProfileOverviewSegmentServiceUpsertReturnsSuccessWhenDocumentReplaced()
        {
            // arrange
            var jobProfileOverviewSegmentModel = A.Fake<JobProfileOverviewSegmentModel>();
            var expectedResult = HttpStatusCode.OK;

            A.CallTo(() => repository.UpsertAsync(jobProfileOverviewSegmentModel)).Returns(expectedResult);

            // act
            var result = await jobProfileOverviewSegmentService.UpsertAsync(jobProfileOverviewSegmentModel).ConfigureAwait(false);

            // assert
            A.CallTo(() => repository.UpsertAsync(jobProfileOverviewSegmentModel)).MustHaveHappenedOnceExactly();
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task JobProfileOverviewSegmentServiceUpsertReturnsArgumentNullExceptionWhenNullParamIsUsed()
        {
            // act
            var exceptionResult = await Assert.ThrowsAsync<ArgumentNullException>(async () => await jobProfileOverviewSegmentService.UpsertAsync(null).ConfigureAwait(false)).ConfigureAwait(false);

            // assert
            Assert.Equal("Value cannot be null. (Parameter 'jobProfileOverviewSegmentModel')", exceptionResult.Message);
        }
    }
}
