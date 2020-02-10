using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model.JobProfile;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.Interface;
using System;
using System.Collections.Generic;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support
{
    internal partial class CommonAction : IJobProfileOverviewSupport
    {
        public SOCCodeContentType GenerateSOCCodeContentTypeForJobProfile(JobProfileContentType jobProfile)
        {
            string socCode = "12345";

            return new SOCCodeContentType()
            {
                SOCCode = socCode,
                Id = jobProfile.SocCodeData.Id,
                JobProfileId = jobProfile.JobProfileId,
                JobProfileTitle = jobProfile.Title,
                UrlName = jobProfile.SocCodeData.UrlName,
                Title = socCode,
                Description = "This record has been updated",
                ONetOccupationalCode = "12.1234-00",
                ApprenticeshipFramework = jobProfile.SocCodeData.ApprenticeshipFramework,
                ApprenticeshipStandards = jobProfile.SocCodeData.ApprenticeshipStandards,
            };
        }

        public SocCodeData GenerateSOCCodeJobProfileSection()
        {
            string socCode = this.RandomString(5);
            return new SocCodeData()
            {
                SOCCode = socCode,
                Id = Guid.NewGuid().ToString(),
                UrlName = socCode.ToLower(),
                Description = "This record is the original record",
                ONetOccupationalCode = this.RandomString(5).ToLower(),
                ApprenticeshipFramework = new List<ApprenticeshipFramework>()
            {
                new ApprenticeshipFramework()
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = this.RandomString(10).ToLower(),
                    Title = this.RandomString(10).ToLower(),
                    Url = $"https://{this.RandomString(10).ToLower()}.com/",
                },
            },
                ApprenticeshipStandards = new List<ApprenticeshipStandard>()
            {
                new ApprenticeshipStandard()
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = this.RandomString(10).ToLower(),
                    Title = this.RandomString(10).ToLower(),
                    Url = $"https://{this.RandomString(10).ToLower()}.com/",
                },
            },
            };
        }

        public WorkingHoursDetail GenerateWorkingHoursDetailSection()
        {
            return new WorkingHoursDetail()
            {
                Id = Guid.NewGuid().ToString(),
                Description = "default-description",
                Title = "default-title",
                Url = $"https://{this.RandomString(10)}.com/",
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
                Title = "Updated working hours detail title",
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
                Title = "Updated working pattern title",
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
                Url = $"https://{this.RandomString(10)}.com/",
            };
        }

        public WorkingPatternDetailClassification GenerateWorkingPatternDetailsClassificationForJobProfile(JobProfileContentType jobProfile)
        {
            return new WorkingPatternDetailClassification()
            {
                Id = jobProfile.WorkingPatternDetails[0].Id,
                Description = "Updated description",
                JobProfileId = jobProfile.JobProfileId,
                JobProfileTitle = jobProfile.Title,
                Title = "Updated working pattern detail title",
                Url = jobProfile.WorkingPatternDetails[0].Url,
            };
        }

        public WorkingPatternDetail GenerateWorkingPatternDetailsSection()
        {
            return new WorkingPatternDetail()
            {
                Id = Guid.NewGuid().ToString(),
                Description = "default-description",
                Title = "default-title",
                Url = $"https://{RandomString(10)}.com/",
            };
        }
    }
}