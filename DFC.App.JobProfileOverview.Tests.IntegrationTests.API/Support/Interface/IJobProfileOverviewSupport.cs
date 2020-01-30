using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.Interface
{
    interface IJobProfileOverviewSupport
    {
        SocCodeData GenerateSOCCodeJobProfileSection();
        SOCCodeContentType GenerateSOCCodeContentTypeForJobProfile(JobProfileContentType jobProfile);
        WorkingHoursDetail GenerateWorkingHoursDetailSection();
        WorkingHoursDetailsClassification GenerateWorkingHoursDetailsClassificationForJobProfile(JobProfileContentType jobProfile);
        WorkingPatternClassification GenerateWorkingPatternClassificationForJobProfile(JobProfileContentType jobProfile);
        WorkingPattern GenerateWorkingPatternSection();
        WorkingPatternDetailClassification GenerateWorkingPatternDetailsClassificationForJobProfile(JobProfileContentType jobProfile);
        WorkingPatternDetail GenerateWorkingPatternDetailsSection(); 
    }
}
