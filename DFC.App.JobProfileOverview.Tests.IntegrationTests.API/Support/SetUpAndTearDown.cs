using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model.ContentType.JobProfile;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model.Support;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.API;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.API.RestFactory;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.AzureServiceBus.ServiceBusFactory;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.AzureServiceBus.ServiceBusFactory.AzureServiceBus;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support
{
    public class SetUpAndTearDown
    {
        internal AppSettings appSettings;
        internal JobProfileContentType JobProfile;
        internal CommonAction CommonAction;
        internal ServiceBus serviceBus;
        internal JobProfileOverviewAPI API;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            this.appSettings = configuration.Get<AppSettings>();
            this.CommonAction = new CommonAction();
            this.API = new JobProfileOverviewAPI(new RestClientFactory(), new RestRequestFactory(), this.appSettings);
            string canonicalName = this.CommonAction.RandomString(10).ToLower();
            this.JobProfile = this.CommonAction.GetResource<JobProfileContentType>("JobProfileContentType");
            this.JobProfile.JobProfileId = Guid.NewGuid().ToString();
            this.JobProfile.UrlName = canonicalName;
            this.JobProfile.CanonicalName = canonicalName;
            this.JobProfile.SocCodeData = this.CommonAction.GenerateSOCCodeJobProfileSection();
            this.JobProfile.WorkingHoursDetails = new List<WorkingHoursDetail>() { this.CommonAction.GenerateWorkingHoursDetailSection() };
            this.JobProfile.WorkingPattern = new List<WorkingPattern>() { this.CommonAction.GenerateWorkingPatternSection() };
            this.JobProfile.WorkingPatternDetails = new List<WorkingPatternDetail>() { this.CommonAction.GenerateWorkingPatternDetailsSection() };
            var jobProfileMessageBody = this.CommonAction.ConvertObjectToByteArray(this.JobProfile);
            this.serviceBus = new ServiceBus(new TopicClientFactory(), this.appSettings);
            var message = new MessageFactory().Create(this.JobProfile.JobProfileId, jobProfileMessageBody, "Published", "JobProfile");
            await this.serviceBus.SendMessage(message).ConfigureAwait(true);
            await Task.Delay(5000);
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            var jobProfileDelete = this.CommonAction.GetResource<JobProfileContentType>("JobProfileDelete");
            var messageBody = this.CommonAction.ConvertObjectToByteArray(jobProfileDelete);
            var message = new MessageFactory().Create(this.JobProfile.JobProfileId, messageBody, "Deleted", "JobProfile");
            await this.serviceBus.SendMessage(message).ConfigureAwait(true);
        }
    }
}
