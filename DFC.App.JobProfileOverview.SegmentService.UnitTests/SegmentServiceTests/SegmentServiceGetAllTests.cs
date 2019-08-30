using DFC.App.JobProfileOverview.Data.Contracts;
using DFC.App.JobProfileOverview.Data.Models;
using FakeItEasy;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DFC.App.JobProfileOverview.SegmentService.UnitTests.SegmentServiceTests
{
    public class SegmentServiceGetAllTests
    {
        private readonly ICosmosRepository<JobProfileOverviewSegmentModel> repository;
        private readonly IDraftJobProfileOverviewSegmentService draftJobProfileOverviewSegmentService;
        private readonly IJobProfileOverviewSegmentService jobProfileOverviewSegmentService;

        public SegmentServiceGetAllTests()
        {
            repository = A.Fake<ICosmosRepository<JobProfileOverviewSegmentModel>>();
            draftJobProfileOverviewSegmentService = A.Fake<IDraftJobProfileOverviewSegmentService>();
            jobProfileOverviewSegmentService = new JobProfileOverviewSegmentService(repository, draftJobProfileOverviewSegmentService);
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
