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
            socCodeContentType.SOCCode = jobProfile.SocCodeData.SOCCode;
            socCodeContentType.Id = jobProfile.SocCodeData.Id;
            socCodeContentType.JobProfileId = jobProfile.JobProfileId;
            socCodeContentType.JobProfileTitle = jobProfile.Title;
            socCodeContentType.UrlName = jobProfile.SocCodeData.UrlName;
            socCodeContentType.Title = socCodeContentType.SOCCode;
            socCodeContentType.Description = "This record has been updated";
            socCodeContentType.ONetOccupationalCode = RandomString(5).ToLower();
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
    }
}