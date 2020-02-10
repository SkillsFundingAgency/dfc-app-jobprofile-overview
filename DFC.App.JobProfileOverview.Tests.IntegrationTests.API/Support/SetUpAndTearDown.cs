using DFC.Api.JobProfiles.Common.AzureServiceBusSupport;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model.JobProfile;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.EnumLibrary;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support
{
    public class SetUpAndTearDown
    {
        internal JobProfileContentType JobProfile { get; private set; }

        internal CommonAction CommonAction { get; } = new CommonAction();

        internal Topic Topic { get; private set; }

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            this.CommonAction.InitialiseAppSettings();
            this.Topic = new Topic(Settings.ServiceBusConfig.Endpoint);
            this.JobProfile = this.CommonAction.GenerateJobProfileContentType();
            SocCodeData jobProfileSOCCodeSection = this.CommonAction.GenerateSOCCodeJobProfileSection();
            WorkingHoursDetail workingHoursDetailSection = this.CommonAction.GenerateWorkingHoursDetailSection();
            WorkingPattern workingPattern = this.CommonAction.GenerateWorkingPatternSection();
            WorkingPatternDetail workingPatternDetails = this.CommonAction.GenerateWorkingPatternDetailsSection();
            this.JobProfile.SocCodeData = jobProfileSOCCodeSection;
            this.JobProfile.WorkingHoursDetails = new List<WorkingHoursDetail>() { workingHoursDetailSection };
            this.JobProfile.WorkingPattern = new List<WorkingPattern>() { workingPattern };
            this.JobProfile.WorkingPatternDetails = new List<WorkingPatternDetail>() { workingPatternDetails };
            byte[] jobProfileMessageBody = this.CommonAction.ConvertObjectToByteArray(this.JobProfile);
            Message jobProfileMessage = this.CommonAction.CreateServiceBusMessage(this.JobProfile.JobProfileId, jobProfileMessageBody, ContentType.JSON, ActionType.Published, CType.JobProfile);
            await this.CommonAction.SendMessage(this.Topic, jobProfileMessage);
            await Task.Delay(5000);
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            await this.CommonAction.DeleteJobProfile(this.Topic, this.JobProfile);
        }
    }
}
