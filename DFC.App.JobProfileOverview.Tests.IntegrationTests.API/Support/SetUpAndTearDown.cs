using DFC.Api.JobProfiles.Common.AzureServiceBusSupport;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support
{
    public class SetUpAndTearDown
    {
        internal CommonAction CommonAction { get; } = new CommonAction();
        public Topic Topic { get; set; }
        public Guid JobProfileId { get; set; }
        public string CanonicalName { get; set; }

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            JobProfileId = Guid.NewGuid();
            CanonicalName = CommonAction.RandomString(10).ToLower();
            CommonAction.InitialiseAppSettings();
            Topic = new Topic(Settings.ServiceBusConfig.Endpoint);
            await CommonAction.CreateJobProfile(Topic, JobProfileId, CanonicalName);
            await Task.Delay(5000);
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            await CommonAction.DeleteJobProfileWithId(Topic, JobProfileId);
        }
    }
}
