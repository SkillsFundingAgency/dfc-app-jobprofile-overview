using DFC.App.JobProfileOverview.Data.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace DFC.App.JobProfileOverview.IntegrationTests.Data
{
    public class DataSeeding
    {
        private const string Segment = "segment";

        public DataSeeding()
        {
            Article1Id = Guid.NewGuid();
            Article2Id = Guid.NewGuid();
            Article3Id = Guid.NewGuid();

            Article1Name = "nurse";
            Article2Name = Article2Id.ToString();
            Article3Name = Article3Id.ToString();
            Article2SocCode = "23456";
        }

        public Guid Article1Id { get; private set; }

        public Guid Article2Id { get; private set; }

        public Guid Article3Id { get; private set; }

        public string Article1Name { get; private set; }

        public string Article2Name { get; private set; }

        public string Article3Name { get; private set; }

        public string Article2SocCode { get; private set; }

        public async Task AddData(CustomWebApplicationFactory<Startup> factory)
        {
            var url = $"/{Segment}";
            var models = CreateModels();

            var client = factory?.CreateClient();

            client?.DefaultRequestHeaders.Accept.Clear();

            foreach (var model in models)
            {
                await client.PostAsync(url, model, new JsonMediaTypeFormatter()).ConfigureAwait(false);
            }
        }

        public async Task RemoveData(CustomWebApplicationFactory<Startup> factory)
        {
            var models = CreateModels();

            var client = factory?.CreateClient();

            client?.DefaultRequestHeaders.Accept.Clear();

            foreach (var model in models)
            {
                var url = string.Concat("/", Segment, "/", model.DocumentId);
                await client.DeleteAsync(url).ConfigureAwait(false);
            }
        }

        private List<JobProfileOverviewSegmentModel> CreateModels()
        {
            var models = new List<JobProfileOverviewSegmentModel>()
            {
                new JobProfileOverviewSegmentModel
                {
                    DocumentId = Article1Id,
                    CanonicalName = Article1Name,
                    SocLevelTwo = "12345",
                    Data = new JobProfileOverviewSegmentDataModel
                    {
                        WorkingHoursDetails = new List<WorkingHoursDetail> { new WorkingHoursDetail { Id = Guid.NewGuid(), Title = "Hours Detail Title 1", Description = "Working Hours Details 1", Url = "http://something.com" } },
                        WorkingPattern = new List<WorkingPattern> { new WorkingPattern { Id = Guid.NewGuid(), Title = "Pattern Title 1", Description = "Working Pattern 1", Url = "http://something.com" } },
                        WorkingPatternDetails = new List<WorkingPatternDetail> { new WorkingPatternDetail { Id = Guid.NewGuid(), Title = "Pattern detail Title 1", Description = "Working Pattern detail1", Url = "http://something.com" } },
                        Title = Article1Name,
                        Overview = "Children's nurses provide care for children and young people with acute or long-term health problems.",
                        HiddenAlternativeTitle = new List<HiddenAlternativeTitle> { new HiddenAlternativeTitle { Id = Guid.NewGuid(), Title = "Alternative Title 1", Description = "Alternative title 1", Url = "http://something.com" } },
                        JobProfileSpecialism = new List<JobProfileSpecialism> { new JobProfileSpecialism { Id = Guid.NewGuid(), Title = "Specialism 1", Description = "Job Profile Specialism 1", Url = "http://something.com" } },
                        AlternativeTitle = "Alternative 1, alternative 2",
                        LastReviewed = DateTime.UtcNow,
                        MinimumHours = 5.0M,
                        MaximumHours = 50.0M,
                        SalaryExperienced = 50000.0M,
                        SalaryStarter = 20000.0M,
                        Soc = new SocData
                        {
                            Id = Guid.NewGuid(),
                            Description = "Soc description",
                            ONetOccupationalCode = "Onet code",
                            SocCode = "Full soc code",
                            UrlName = Article1Name,
                        },
                    },
                },
                new JobProfileOverviewSegmentModel
                {
                    DocumentId = Article2Id,
                    CanonicalName = Article2Name,
                    SocLevelTwo = Article2SocCode,
                    Data = new JobProfileOverviewSegmentDataModel
                    {
                        WorkingHoursDetails = new List<WorkingHoursDetail> { new WorkingHoursDetail { Id = Guid.NewGuid(), Title = "Hours Detail Title 1", Description = "Working Hours Details 1", Url = "http://something.com" } },
                        WorkingPattern = new List<WorkingPattern> { new WorkingPattern { Id = Guid.NewGuid(), Title = "Pattern Title 1", Description = "Working Pattern 1", Url = "http://something.com" } },
                        WorkingPatternDetails = new List<WorkingPatternDetail> { new WorkingPatternDetail { Id = Guid.NewGuid(), Title = "Pattern detail Title 1", Description = "Working Pattern detail1", Url = "http://something.com" } },
                        Title = Article2Name,
                        Overview = "Children's nurses provide care for children and young people with acute or long-term health problems.",
                        HiddenAlternativeTitle = new List<HiddenAlternativeTitle> { new HiddenAlternativeTitle { Id = Guid.NewGuid(), Title = "Alternative Title 1", Description = "Alternative title 1", Url = "http://something.com" } },
                        JobProfileSpecialism = new List<JobProfileSpecialism> { new JobProfileSpecialism { Id = Guid.NewGuid(), Title = "Specialism 1", Description = "Job Profile Specialism 1", Url = "http://something.com" } },
                        AlternativeTitle = "Alternative 1, alternative 2",
                        LastReviewed = DateTime.UtcNow,
                        MinimumHours = 5.0M,
                        MaximumHours = 50.0M,
                        SalaryExperienced = 50000.0M,
                        SalaryStarter = 20000.0M,
                        Soc = new SocData
                        {
                            Id = Guid.NewGuid(),
                            Description = "Soc description",
                            ONetOccupationalCode = "Onet code",
                            SocCode = "Full soc code",
                            UrlName = Article2Name,
                        },
                    },
                },
                new JobProfileOverviewSegmentModel
                {
                    DocumentId = Article3Id,
                    CanonicalName = Article3Name,
                    SocLevelTwo = "34567",
                    Data = new JobProfileOverviewSegmentDataModel
                    {
                        WorkingHoursDetails = new List<WorkingHoursDetail> { new WorkingHoursDetail { Id = Guid.NewGuid(), Title = "Hours Detail Title 1", Description = "Working Hours Details 1", Url = "http://something.com" } },
                        WorkingPattern = new List<WorkingPattern> { new WorkingPattern { Id = Guid.NewGuid(), Title = "Pattern Title 1", Description = "Working Pattern 1", Url = "http://something.com" } },
                        WorkingPatternDetails = new List<WorkingPatternDetail> { new WorkingPatternDetail { Id = Guid.NewGuid(), Title = "Pattern detail Title 1", Description = "Working Pattern detail1", Url = "http://something.com" } },
                        Title = Article3Name,
                        Overview = "Children's nurses provide care for children and young people with acute or long-term health problems.",
                        HiddenAlternativeTitle = new List<HiddenAlternativeTitle> { new HiddenAlternativeTitle { Id = Guid.NewGuid(), Title = "Alternative Title 1", Description = "Alternative title 1", Url = "http://something.com" } },
                        JobProfileSpecialism = new List<JobProfileSpecialism> { new JobProfileSpecialism { Id = Guid.NewGuid(), Title = "Specialism 1", Description = "Job Profile Specialism 1", Url = "http://something.com" } },
                        AlternativeTitle = "Alternative 1, alternative 2",
                        LastReviewed = DateTime.UtcNow,
                        MinimumHours = 5.0M,
                        MaximumHours = 50.0M,
                        SalaryExperienced = 50000.0M,
                        SalaryStarter = 20000.0M,
                        Soc = new SocData
                        {
                            Id = Guid.NewGuid(),
                            Description = "Soc description",
                            ONetOccupationalCode = "Onet code",
                            SocCode = "Full soc code",
                            UrlName = Article3Name,
                        },
                    },
                },
            };

            return models;
        }
    }
}