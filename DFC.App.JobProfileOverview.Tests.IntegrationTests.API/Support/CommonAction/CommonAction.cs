using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model.Classification;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model.ContentType;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model.ContentType.JobProfile;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support
{
    internal class CommonAction : IGeneralSupport, IJobProfileOverviewSupport
    {
        private static readonly Random Random = new Random();

        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        public byte[] ConvertObjectToByteArray(object obj)
        {
            string serialisedContent = JsonConvert.SerializeObject(obj);
            return Encoding.ASCII.GetBytes(serialisedContent);
        }

        public T GetResource<T>(string resourceName)
        {
            DirectoryInfo resourcesDirectory = Directory.CreateDirectory(System.Environment.CurrentDirectory).GetDirectories("Resource")[0];
            FileInfo[] files = resourcesDirectory.GetFiles();
            FileInfo selectedResource = null;

            for (int fileIndex = 0; fileIndex < files.Length; fileIndex++)
            {
                if (files[fileIndex].Name.ToLower().StartsWith(resourceName.ToLower()))
                {
                    selectedResource = files[fileIndex];
                    break;
                }
            }

            if (selectedResource == null)
            {
                throw new Exception($"No resource with the name {resourceName} was found");
            }

            using (StreamReader streamReader = new StreamReader(selectedResource.FullName))
            {
                string content = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(content);
            }
        }

        public SOCCodeContentType GenerateSOCCodeContentTypeForJobProfile(JobProfileContentType jobProfile)
        {
            return new SOCCodeContentType()
            {
                SOCCode = "12345",
                Id = jobProfile.SocCodeData.Id,
                JobProfileId = jobProfile.JobProfileId,
                JobProfileTitle = jobProfile.Title,
                UrlName = jobProfile.SocCodeData.UrlName,
                Title = "12345",
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
                Url = $"https://{this.RandomString(10)}.com/",
            };
        }
    }
}
