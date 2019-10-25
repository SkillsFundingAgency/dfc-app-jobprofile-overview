using AutoMapper;
using DFC.App.JobProfileOverview.Data.Models;
using DFC.App.JobProfileOverview.Data.ServiceBusModels;
using DFC.App.JobProfileOverview.MessageFunctionApp.Services;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xunit;
using HiddenAlternativeTitle = DFC.App.JobProfileOverview.Data.ServiceBusModels.HiddenAlternativeTitle;
using JobProfileSpecialism = DFC.App.JobProfileOverview.Data.ServiceBusModels.JobProfileSpecialism;
using WorkingHoursDetail = DFC.App.JobProfileOverview.Data.ServiceBusModels.WorkingHoursDetail;
using WorkingPattern = DFC.App.JobProfileOverview.Data.ServiceBusModels.WorkingPattern;
using WorkingPatternDetail = DFC.App.JobProfileOverview.Data.ServiceBusModels.WorkingPatternDetail;

namespace DFC.App.JobProfileOverview.MessageFunctionAppTests
{
    public class MappingServiceTests
    {
        private const int SequenceNumber = 123;
        private const string TestJobName = "Test Job name";
        private const string Title = "Title 1";
        private const string AlternativeTitle = "Alternative Title 1";
        private const string SocCodeId = "99";
        private const string Overview = "Some overview text";
        private const decimal MinimumHours = 5.0M;
        private const decimal MaximumHours = 50.0M;
        private const decimal SalaryExperienced = 20000.0M;
        private const decimal SalaryStarter = 5000.0M;
        private const string SocDescription = "Soc Description";
        private const string SocOnetCode = "Soc Onet code";
        private const string WorkingPatternTitle1 = "Working Pattern title 1";
        private const string WorkingPatternDescription1 = "Working Pattern description 1";
        private const string HiddenAlternativeTitle1 = "Hidden Alternative title 1";
        private const string HiddenAlternativeDescription1 = "Hidden Alternative description 1";
        private const string SpecialismTitle1 = "Specialism title 1";
        private const string SpecialismDescription1 = "Specialism description 1";
        private const string WorkingPatternDetailTitle1 = "Working Pattern Detail title 1";
        private const string WorkingPatternDetailDescription1 = "Working Pattern Detail description 1";
        private const string WorkingHoursDetailTitle1 = "Working Hours Detail title 1";
        private const string WorkingHoursDetailDescription1 = "Working Hours Detail description 1";
        private const string WorkingPatternUrl1 = "http://WorkingPattern1";
        private const string HiddenAlternativeUrl1 = "http://HiddenAlternativeUrl1";
        private const string SpecialismUrl1 = "http://SpecialismUrl1";
        private const string WorkingPatternDetailUrl1 = "http://WorkingPatternDetailUrl1";
        private const string WorkingHoursDetailUrl1 = "http://WorkingHoursDetailUrl1";

        private static readonly DateTime LastModified = DateTime.UtcNow.AddDays(-1);
        private static readonly Guid JobProfileId = Guid.NewGuid();
        private static readonly Guid SocCode = Guid.NewGuid();
        private static readonly Guid WorkingPatternId1 = Guid.NewGuid();
        private static readonly Guid AlternativeTitleId1 = Guid.NewGuid();
        private static readonly Guid SpecialismId1 = Guid.NewGuid();
        private static readonly Guid WorkingPatternDetailId1 = Guid.NewGuid();
        private static readonly Guid WorkingHoursDetailId1 = Guid.NewGuid();

        private readonly IMappingService mappingService;

        public MappingServiceTests()
        {
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile(new MessageFunctionApp.AutoMapperProfile.OverviewModelProfile());
            });

            var mapper = new Mapper(config);

            mappingService = new MappingService(mapper);
        }

        [Fact]
        public void MapToSegmentModelWhenJobProfileMessageSentThenItIsMappedCorrectly()
        {
            var fullJPMessage = BuildJobProfileMessage();
            var message = JsonConvert.SerializeObject(fullJPMessage);
            var expectedResponse = BuildExpectedResponse();

            // Act
            var actualMappedModel = mappingService.MapToSegmentModel(message, SequenceNumber);

            // Assert
            expectedResponse.Should().BeEquivalentTo(actualMappedModel);
        }

        private static JobProfileMessage BuildJobProfileMessage()
        {
            return new JobProfileMessage
            {
                JobProfileId = JobProfileId,
                CanonicalName = TestJobName,
                LastModified = LastModified,
                Title = Title,
                AlternativeTitle = AlternativeTitle,
                SocLevelTwo = SocCodeId,
                Overview = Overview,
                MaximumHours = MaximumHours,
                MinimumHours = MinimumHours,
                SalaryExperienced = SalaryExperienced,
                SalaryStarter = SalaryStarter,
                SocCodeData = new SocCodeData
                {
                    Description = SocDescription,
                    Id = SocCode,
                    SOCCode = SocCodeId,
                    ONetOccupationalCode = SocOnetCode,
                    UrlName = TestJobName,
                },
                WorkingPattern = new List<WorkingPattern>
                {
                    new WorkingPattern
                    {
                        Id = WorkingPatternId1,
                        Title = WorkingPatternTitle1,
                        Description = WorkingPatternDescription1,
                        Url = WorkingPatternUrl1,
                    },
                },
                HiddenAlternativeTitle = new List<HiddenAlternativeTitle>
                {
                    new HiddenAlternativeTitle
                    {
                        Id = AlternativeTitleId1,
                        Title = HiddenAlternativeTitle1,
                        Description = HiddenAlternativeDescription1,
                        Url = HiddenAlternativeUrl1,
                    },
                },
                JobProfileSpecialism = new List<JobProfileSpecialism>
                {
                    new JobProfileSpecialism
                    {
                        Id = SpecialismId1,
                        Title = SpecialismTitle1,
                        Description = SpecialismDescription1,
                        Url = SpecialismUrl1,
                    },
                },
                WorkingPatternDetails = new List<WorkingPatternDetail>
                {
                    new WorkingPatternDetail
                    {
                        Id = WorkingPatternDetailId1,
                        Title = WorkingPatternDetailTitle1,
                        Description = WorkingPatternDetailDescription1,
                        Url = WorkingPatternDetailUrl1,
                    },
                },
                WorkingHoursDetails = new List<WorkingHoursDetail>
                {
                    new WorkingHoursDetail
                    {
                        Id = WorkingHoursDetailId1,
                        Title = WorkingHoursDetailTitle1,
                        Description = WorkingHoursDetailDescription1,
                        Url = WorkingHoursDetailUrl1,
                    },
                },
            };
        }

        private static JobProfileOverviewSegmentModel BuildExpectedResponse()
        {
            return new JobProfileOverviewSegmentModel
            {
                CanonicalName = TestJobName,
                DocumentId = JobProfileId,
                SequenceNumber = SequenceNumber,
                SocLevelTwo = SocCodeId,
                Etag = null,
                Data = new JobProfileOverviewSegmentDataModel
                {
                    Title = Title,
                    AlternativeTitle = AlternativeTitle,
                    LastReviewed = LastModified,
                    MaximumHours = MaximumHours,
                    MinimumHours = MinimumHours,
                    Overview = Overview,
                    SalaryExperienced = SalaryExperienced,
                    SalaryStarter = SalaryStarter,
                    Soc = new SocData
                    {
                        Id = SocCode,
                        Description = SocDescription,
                        SocCode = SocCodeId,
                        ONetOccupationalCode = SocOnetCode,
                        UrlName = TestJobName,
                    },
                    WorkingPattern = new List<Data.Models.WorkingPattern>
                    {
                        new Data.Models.WorkingPattern
                        {
                            Id = WorkingPatternId1,
                            Title = WorkingPatternTitle1,
                            Description = WorkingPatternDescription1,
                            Url = WorkingPatternUrl1,
                        },
                    },
                    HiddenAlternativeTitle = new List<Data.Models.HiddenAlternativeTitle>
                    {
                        new Data.Models.HiddenAlternativeTitle
                        {
                            Id = AlternativeTitleId1,
                            Title = HiddenAlternativeTitle1,
                            Description = HiddenAlternativeDescription1,
                            Url = HiddenAlternativeUrl1,
                        },
                    },
                    JobProfileSpecialism = new List<Data.Models.JobProfileSpecialism>
                    {
                        new Data.Models.JobProfileSpecialism
                        {
                            Id = SpecialismId1,
                            Title = SpecialismTitle1,
                            Description = SpecialismDescription1,
                            Url = SpecialismUrl1,
                        },
                    },
                    WorkingPatternDetails = new List<Data.Models.WorkingPatternDetail>
                    {
                        new Data.Models.WorkingPatternDetail
                        {
                            Id = WorkingPatternDetailId1,
                            Title = WorkingPatternDetailTitle1,
                            Description = WorkingPatternDetailDescription1,
                            Url = WorkingPatternDetailUrl1,
                        },
                    },
                    WorkingHoursDetails = new List<Data.Models.WorkingHoursDetail>
                    {
                        new Data.Models.WorkingHoursDetail
                        {
                            Id = WorkingHoursDetailId1,
                            Title = WorkingHoursDetailTitle1,
                            Description = WorkingHoursDetailDescription1,
                            Url = WorkingHoursDetailUrl1,
                        },
                    },
                },
            };
        }
    }
}