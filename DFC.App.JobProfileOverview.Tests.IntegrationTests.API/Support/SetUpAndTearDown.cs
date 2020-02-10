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

        public Topic Topic { get; private set; }

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            CommonAction.InitialiseAppSettings();
            Topic = new Topic(Settings.ServiceBusConfig.Endpoint);
            JobProfile = CommonAction.GenerateJobProfileContentType();
            SocCodeData jobProfileSOCCodeSection = CommonAction.GenerateSOCCodeJobProfileSection();
            WorkingHoursDetail workingHoursDetailSection = CommonAction.GenerateWorkingHoursDetailSection();
            WorkingPattern workingPattern = CommonAction.GenerateWorkingPatternSection();
            WorkingPatternDetail workingPatternDetails = CommonAction.GenerateWorkingPatternDetailsSection();
            JobProfile.SocCodeData = jobProfileSOCCodeSection;
            JobProfile.WorkingHoursDetails = new List<WorkingHoursDetail>() { workingHoursDetailSection };
            JobProfile.WorkingPattern = new List<WorkingPattern>() { workingPattern };
            JobProfile.WorkingPatternDetails = new List<WorkingPatternDetail>() { workingPatternDetails };
            byte[] jobProfileMessageBody =  CommonAction.ConvertObjectToByteArray(JobProfile);
            Message jobProfileMessage = CommonAction.CreateServiceBusMessage(JobProfile.JobProfileId, jobProfileMessageBody, ContentType.JSON, ActionType.Published, CType.JobProfile);
            await CommonAction.SendMessage(Topic, jobProfileMessage);
            await Task.Delay(5000);
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            await CommonAction.DeleteJobProfile(Topic, JobProfile);
        }
    }
}
