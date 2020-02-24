using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.API.RestFactory.Interface;
using FakeItEasy;
using NUnit.Framework;

namespace DFC.App.JobProfileOverview.IntegrationTestFramework.UnitTests
{
    public class APITests
    {
        private IRestClientFactory restClientFactory;

        [SetUp]
        public void Setup()
        {
            this.restClientFactory = A.Fake<IRestClientFactory>();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}