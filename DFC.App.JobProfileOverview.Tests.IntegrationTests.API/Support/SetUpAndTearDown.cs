using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model.ContentType.JobProfile;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model.Support;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.API;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.API.RestFactory;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.AzureServiceBus.ServiceBusFactory;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.AzureServiceBus.ServiceBusFactory.AzureServiceBus;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
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
            this.JobProfile = this.CommonAction.GenerateJobProfileContentType();
            SocCodeData jobProfileSOCCodeSection = this.CommonAction.GenerateSOCCodeJobProfileSection();
            WorkingHoursDetail workingHoursDetailSection = this.CommonAction.GenerateWorkingHoursDetailSection();
            WorkingPattern workingPattern = this.CommonAction.GenerateWorkingPatternSection();
            WorkingPatternDetail workingPatternDetails = this.CommonAction.GenerateWorkingPatternDetailsSection();
            this.JobProfile.SocCodeData = jobProfileSOCCodeSection;
            this.JobProfile.WorkingHoursDetails = new List<WorkingHoursDetail>() { workingHoursDetailSection };
            this.JobProfile.WorkingPattern = new List<WorkingPattern>() { workingPattern };
            this.JobProfile.WorkingPatternDetails = new List<WorkingPatternDetail>() { workingPatternDetails };
            var jobProfileMessageBody = this.CommonAction.ConvertObjectToByteArray(this.JobProfile);
            this.serviceBus = new ServiceBus(new TopicClientFactory(), this.appSettings);
            var message = new MessageFactory().Create(this.JobProfile.JobProfileId, jobProfileMessageBody, "Published", "JobProfile");
            await this.serviceBus.SendMessage(message).ConfigureAwait(true);
            await Task.Delay(5000);
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            var jobProfileDelete = ResourceManager.GetResource<JobProfileContentType>("JobProfileDelete");
            var messageBody = this.CommonAction.ConvertObjectToByteArray(jobProfileDelete);
            var message = new MessageFactory().Create(this.JobProfile.JobProfileId, messageBody, "Deleted", "JobProfile");
            await this.serviceBus.SendMessage(message).ConfigureAwait(true);
        }
    }
}
