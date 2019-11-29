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
    [Trait("Segment Service", "Delete Tests")]
    public class SegmentServiceDeleteTests
    {
        private readonly ICosmosRepository<JobProfileOverviewSegmentModel> repository;
        private readonly IJobProfileOverviewSegmentService jobProfileOverviewSegmentService;

        private readonly Guid documentId = Guid.NewGuid();

        public SegmentServiceDeleteTests()
        {
            var mapper = A.Fake<AutoMapper.IMapper>();
            var jobProfileSegmentRefreshService = A.Fake<IJobProfileSegmentRefreshService<RefreshJobProfileSegmentServiceBusModel>>();
            repository = A.Fake<ICosmosRepository<JobProfileOverviewSegmentModel>>();
            jobProfileOverviewSegmentService = new JobProfileOverviewSegmentService(repository, mapper, jobProfileSegmentRefreshService);
        }

        [Fact]
        public async Task JobProfileOverviewSegmentServiceDeleteReturnsSuccessWhenSegmentDeleted()
        {
            // arrange
            A.CallTo(() => repository.DeleteAsync(documentId)).Returns(HttpStatusCode.NoContent);

            // act
            var result = await jobProfileOverviewSegmentService.DeleteAsync(documentId).ConfigureAwait(false);

            // assert
            A.CallTo(() => repository.DeleteAsync(documentId)).MustHaveHappenedOnceExactly();
            Assert.True(result);
        }

        [Fact]
        public async Task JobProfileOverviewSegmentServiceDeleteReturnsFalseWhenDocumentNotFound()
        {
            // arrange
            A.CallTo(() => repository.DeleteAsync(documentId)).Returns(HttpStatusCode.NotFound);

            // act
            var result = await jobProfileOverviewSegmentService.DeleteAsync(documentId).ConfigureAwait(false);

            // assert
            A.CallTo(() => repository.DeleteAsync(documentId)).MustHaveHappenedOnceExactly();
            Assert.False(result);
        }

        [Fact]
        public async Task JobProfileOverviewSegmentServiceDeleteReturnsFalseWhenAnyOtherStatus()
        {
            // arrange
            A.CallTo(() => repository.DeleteAsync(documentId)).Returns(HttpStatusCode.BadRequest);

            // act
            var result = await jobProfileOverviewSegmentService.DeleteAsync(documentId).ConfigureAwait(false);

            // assert
            A.CallTo(() => repository.DeleteAsync(documentId)).MustHaveHappenedOnceExactly();
            Assert.False(result);
        }
    }
}
