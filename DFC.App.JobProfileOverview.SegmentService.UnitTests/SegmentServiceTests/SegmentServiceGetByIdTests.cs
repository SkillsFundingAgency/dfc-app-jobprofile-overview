using DFC.App.JobProfileOverview.Data.Contracts;
using DFC.App.JobProfileOverview.Data.Models;
using FakeItEasy;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace DFC.App.JobProfileOverview.SegmentService.UnitTests.SegmentServiceTests
{
    public class SegmentServiceGetByIdTests
    {
        private readonly ICosmosRepository<JobProfileOverviewSegmentModel> repository;
        private readonly IDraftJobProfileOverviewSegmentService draftJobProfileOverviewSegmentService;
        private readonly IJobProfileOverviewSegmentService jobProfileOverviewSegmentService;

        public SegmentServiceGetByIdTests()
        {
            repository = A.Fake<ICosmosRepository<JobProfileOverviewSegmentModel>>();
            draftJobProfileOverviewSegmentService = A.Fake<IDraftJobProfileOverviewSegmentService>();
            jobProfileOverviewSegmentService = new JobProfileOverviewSegmentService(repository, draftJobProfileOverviewSegmentService);
        }

        [Fact]
        public async Task SegmentServiceGetByIdReturnsSuccess()
        {
            // arrange
            var documentId = Guid.NewGuid();
            var expectedResult = A.Fake<JobProfileOverviewSegmentModel>();

            A.CallTo(() => repository.GetAsync(A<Expression<Func<JobProfileOverviewSegmentModel, bool>>>.Ignored)).Returns(expectedResult);

            // act
            var result = await jobProfileOverviewSegmentService.GetByIdAsync(documentId).ConfigureAwait(false);

            // assert
            A.CallTo(() => repository.GetAsync(A<Expression<Func<JobProfileOverviewSegmentModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
            A.Equals(result, expectedResult);
        }

        [Fact]
        public async Task SegmentServiceGetByIdReturnsNullWhenMissingInRepository()
        {
            // arrange
            var documentId = Guid.NewGuid();
            JobProfileOverviewSegmentModel expectedResult = null;

            A.CallTo(() => repository.GetAsync(A<Expression<Func<JobProfileOverviewSegmentModel, bool>>>.Ignored)).Returns(expectedResult);

            // act
            var result = await jobProfileOverviewSegmentService.GetByIdAsync(documentId).ConfigureAwait(false);

            // assert
            A.CallTo(() => repository.GetAsync(A<Expression<Func<JobProfileOverviewSegmentModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
            A.Equals(result, expectedResult);
        }
    }
}
