using AutoMapper;
using DFC.App.JobProfileOverview.Data.Models;
using DFC.App.JobProfileOverview.Data.ServiceBusModels;
using DFC.App.JobProfileOverview.Repository.CosmosDb;
using FakeItEasy;
using Xunit;

namespace DFC.App.JobProfileOverview.SegmentService.UnitTests.SegmentServiceTests
{
    public class SegmentServicePingTests
    {
        private readonly IJobProfileSegmentRefreshService<RefreshJobProfileSegmentServiceBusModel> jobProfileSegmentRefreshService;
        private readonly IMapper mapper;

        public SegmentServicePingTests()
        {
            jobProfileSegmentRefreshService = A.Fake<IJobProfileSegmentRefreshService<RefreshJobProfileSegmentServiceBusModel>>();
            mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void PingReturnsSuccess()
        {
            // arrange
            var repository = A.Fake<ICosmosRepository<JobProfileOverviewSegmentModel>>();
            var expectedResult = true;

            A.CallTo(() => repository.PingAsync()).Returns(expectedResult);

            var overviewSegmentService = new JobProfileOverviewSegmentService(repository, mapper, jobProfileSegmentRefreshService);

            // act
            var result = overviewSegmentService.PingAsync().Result;

            // assert
            A.CallTo(() => repository.PingAsync()).MustHaveHappenedOnceExactly();
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void PingReturnsFalseWhenNotInRepository()
        {
            // arrange
            var repository = A.Dummy<ICosmosRepository<JobProfileOverviewSegmentModel>>();
            var expectedResult = false;

            A.CallTo(() => repository.PingAsync()).Returns(expectedResult);

            var overviewSegmentService = new JobProfileOverviewSegmentService(repository, mapper, jobProfileSegmentRefreshService);

            // act
            var result = overviewSegmentService.PingAsync().Result;

            // assert
            A.CallTo(() => repository.PingAsync()).MustHaveHappenedOnceExactly();
            Assert.Equal(expectedResult, result);
        }
    }
}