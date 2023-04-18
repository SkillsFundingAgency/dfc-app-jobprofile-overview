using AutoMapper;
using DFC.App.JobProfileOverview.Data.Models;
using DFC.App.JobProfileOverview.Data.Models.PatchModels;
using DFC.App.JobProfileOverview.Data.ServiceBusModels;
using DFC.App.JobProfileOverview.Data.ServiceBusModels.PatchModels;
using System.Diagnostics.CodeAnalysis;

namespace DFC.App.JobProfileOverview.MessageFunctionApp.AutoMapperProfile
{
    [ExcludeFromCodeCoverage]
    public class OverviewModelProfile : Profile
    {
        public OverviewModelProfile()
        {
            CreateMap<JobProfileMessage, JobProfileOverviewSegmentModel>()
                .ForMember(d => d.Data, s => s.MapFrom(a => a))
                .ForMember(d => d.DocumentId, s => s.MapFrom(a => a.JobProfileId))
                .ForMember(d => d.Etag, s => s.Ignore());

            CreateMap<JobProfileMessage, JobProfileOverviewSegmentDataModel>()
                    .ForMember(d => d.HiddenAlternativeTitle, s => s.MapFrom(a => a.HiddenAlternativeTitle))
                    .ForMember(d => d.JobProfileSpecialism, s => s.MapFrom(a => a.JobProfileSpecialism))
                    .ForMember(d => d.LastReviewed, s => s.MapFrom(a => a.LastModified))
                    .ForMember(d => d.Soc, s => s.MapFrom(a => a.SocCodeData));

            CreateMap<Data.ServiceBusModels.HiddenAlternativeTitle, Data.Models.HiddenAlternativeTitle>();

            CreateMap<Data.ServiceBusModels.JobProfileSpecialism, Data.Models.JobProfileSpecialism>();

            CreateMap<Data.ServiceBusModels.WorkingHoursDetail, Data.Models.WorkingHoursDetail>();

            CreateMap<Data.ServiceBusModels.WorkingPattern, Data.Models.WorkingPattern>();

            CreateMap<Data.ServiceBusModels.WorkingPatternDetail, Data.Models.WorkingPatternDetail>();

            CreateMap<Data.ServiceBusModels.SocCodeData, Data.Models.SocData>();

            CreateMap<SocCodeData, SocData>();

            CreateMap<PatchWorkingPatternServiceBusModel, PatchWorkingPatternModel>();

            CreateMap<PatchHiddenAlternativeTitleServiceBusModel, PatchHiddenAlternativeTitleModel>();

            CreateMap<PatchJobProfileSpecialismServiceBusModel, PatchJobProfileSpecialismModel>();

            CreateMap<PatchWorkingHoursDetailServiceBusModel, PatchWorkingHoursDetailModel>();

            CreateMap<PatchWorkingPatternDetailServiceBusModel, PatchWorkingPatternDetailModel>();

            CreateMap<PatchSocDataServiceBusModel, PatchSocDataModel>();

         
        }
    }
}