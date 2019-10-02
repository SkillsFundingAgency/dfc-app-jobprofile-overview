using DFC.App.JobProfileOverview.Data.Models;
using DFC.App.JobProfileOverview.Repository.CosmosDb;
using FakeItEasy;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DFC.App.JobProfileOverview.SegmentService.UnitTests.SegmentServiceTests
{
    public class SegmentServiceGetAllTests
    {
        private readonly ICosmosRepository<JobProfileOverviewSegmentModel> repository;
        private readonly IJobProfileOverviewSegmentService jobProfileOverviewSegmentService;

        public SegmentServiceGetAllTests()
        {
            repository = A.Fake<ICosmosRepository<JobProfileOverviewSegmentModel>>();
            jobProfileOverviewSegmentService = new JobProfileOverviewSegmentService(repository);
        }

        [Fact]
        public async Task GetAllListReturnsSuccess()
        {
            // arrange
            var expectedResults = A.CollectionOfFake<JobProfileOverviewSegmentModel>(2);

            A.CallTo(() => repository.GetAllAsync()).Returns(expectedResults);

            // act
            var results = await jobProfileOverviewSegmentService.GetAllAsync().ConfigureAwait(false);

            // assert
            A.CallTo(() => repository.GetAllAsync()).MustHaveHappenedOnceExactly();
            A.Equals(results, expectedResults);
        }

        [Fact]
        public async Task GetAllListReturnsNullWhenMissingRepository()
        {
            // arrange
            IEnumerable<JobProfileOverviewSegmentModel> expectedResults = null;

            A.CallTo(() => repository.GetAllAsync()).Returns(expectedResults);

            // act
            var results = await jobProfileOverviewSegmentService.GetAllAsync().ConfigureAwait(false);

            // assert
            A.CallTo(() => repository.GetAllAsync()).MustHaveHappenedOnceExactly();
            A.Equals(results, expectedResults);
        }
    }
}