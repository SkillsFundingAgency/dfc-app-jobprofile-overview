using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model;
using System.Threading.Tasks;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.Interface
{
    interface IJobProfileOverviewSupport
    {
        SocCodeData GenerateSOCCodeJobProfileSection();
        SOCCodeContentType GenerateSOCCodeContentTypeForJobProfile(JobProfileContentType jobProfile);
        WorkingHoursDetail GenerateWorkingHoursDetailSection();
    }
}
