using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.Interface;
using System;
using System.Collections.Generic;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support
{
    internal partial class CommonAction : IJobProfileOverviewSupport
    {
        public SOCCodeContentType GenerateSOCCodeContentTypeForJobProfile(JobProfileContentType jobProfile)
        {
            SOCCodeContentType socCodeContentType = ResourceManager.GetResource<SOCCodeContentType>("SOCCodeContentType");
            socCodeContentType.SOCCode = "12345";
            socCodeContentType.Id = jobProfile.SocCodeData.Id;
            socCodeContentType.JobProfileId = jobProfile.JobProfileId;
            socCodeContentType.JobProfileTitle = jobProfile.Title;
            socCodeContentType.UrlName = jobProfile.SocCodeData.UrlName;
            socCodeContentType.Title = socCodeContentType.SOCCode;
            socCodeContentType.Description = "This record has been updated";
            socCodeContentType.ONetOccupationalCode = "12.1234-00";
            socCodeContentType.ApprenticeshipFramework = jobProfile.SocCodeData.ApprenticeshipFramework;
            socCodeContentType.ApprenticeshipStandards = jobProfile.SocCodeData.ApprenticeshipStandards;
            return socCodeContentType;
        }

        public SocCodeData GenerateSOCCodeJobProfileSection()
        {
            SocCodeData jobProfileSOCCodeSection = new SocCodeData();
            jobProfileSOCCodeSection.SOCCode = RandomString(5);
            jobProfileSOCCodeSection.Id = Guid.NewGuid().ToString();
            jobProfileSOCCodeSection.UrlName = jobProfileSOCCodeSection.SOCCode.ToLower();
            jobProfileSOCCodeSection.Description = "This record is the original record";
            jobProfileSOCCodeSection.ONetOccupationalCode = RandomString(5).ToLower();
            jobProfileSOCCodeSection.ApprenticeshipFramework = new List<ApprenticeshipFrameworkContentType>()
            {
                new ApprenticeshipFrameworkContentType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = RandomString(10).ToLower(),
                    Title = RandomString(10).ToLower(),
                    Url = $"https://{RandomString(10).ToLower()}.com/"
                }
            };
            jobProfileSOCCodeSection.ApprenticeshipStandards = new List<ApprenticeshipStandard>()
            {
                new ApprenticeshipStandard()
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = RandomString(10).ToLower(),
                    Title = RandomString(10).ToLower(),
                    Url = $"https://{RandomString(10).ToLower()}.com/"
                }
            };

            return jobProfileSOCCodeSection;
        }

        public WorkingHoursDetail GenerateWorkingHoursDetailSection()
        {
            return new WorkingHoursDetail()
            {
                Id = Guid.NewGuid().ToString(),
                Description = "default-description",
                Title = "default-title",
                Url = $"https://{RandomString(10)}.com/"
            };
        }

        public WorkingHoursDetailsClassification GenerateWorkingHoursDetailsClassificationForJobProfile(JobProfileContentType jobProfile)
        {
            return new WorkingHoursDetailsClassification()
            {
                Id = jobProfile.WorkingHoursDetails[0].Id,
                Description = "Updated description",
                JobProfileId = jobProfile.JobProfileId,
                JobProfileTitle = jobProfile.Title,
                Title = "Updated title",
                Url = jobProfile.WorkingHoursDetails[0].Url,
            };
        }

        public WorkingPatternClassification GenerateWorkingPatternClassificationForJobProfile(JobProfileContentType jobProfile)
        {
            return new WorkingPatternClassification()
            {
                Id = jobProfile.WorkingPattern[0].Id,
                Description = "Updated description",
                JobProfileId = jobProfile.JobProfileId,
                JobProfileTitle = jobProfile.Title,
                Title = "Updated title",
                Url = jobProfile.WorkingPattern[0].Url,
            };
        }

        public WorkingPattern GenerateWorkingPatternSection()
        {
            return new WorkingPattern()
            {
                Id = Guid.NewGuid().ToString(),
                Description = "default-description",
                Title = "default-title",
                Url = $"https://{RandomString(10)}.com/"
            };
        }

        public WorkingPatternDetailClassification GenerateWorkingPatternDetailsClassificationForJobProfile(JobProfileContentType jobProfile)
        {
            return new WorkingPatternDetailClassification()
            {
                Id = jobProfile.WorkingPattern[0].Id,
                Description = "Updated description",
                JobProfileId = jobProfile.JobProfileId,
                JobProfileTitle = jobProfile.Title,
                Title = "Updated title",
                Url = jobProfile.WorkingPattern[0].Url,
            };
        }

        public WorkingPatternDetail GenerateWorkingPatternDetailsSection()
        {
            return new WorkingPatternDetail()
            {
                Id = Guid.NewGuid().ToString(),
                Description = "default-description",
                Title = "default-title",
                Url = $"https://{RandomString(10)}.com/"
            };
        }
    }
}