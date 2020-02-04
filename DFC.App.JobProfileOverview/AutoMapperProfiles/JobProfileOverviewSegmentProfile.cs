using AutoMapper;
using DFC.App.JobProfileOverview.Data.Models;
using DFC.App.JobProfileOverview.Data.Models.PatchModels;
using DFC.App.JobProfileOverview.Data.ServiceBusModels;
using DFC.App.JobProfileOverview.ViewModels;
using System.Diagnostics.CodeAnalysis;

namespace DFC.App.JobProfileOverview.AutoMapperProfiles
{
    [ExcludeFromCodeCoverage]
    public class JobProfileOverviewSegmentProfile : Profile
    {
        public JobProfileOverviewSegmentProfile()
        {
            CreateMap<JobProfileOverviewSegmentModel, IndexDocumentViewModel>();

            CreateMap<JobProfileOverviewSegmentModel, BodyViewModel>();

            CreateMap<JobProfileOverviewSegmentDataModel, BodyDataViewModel>()
                .ForMember(d => d.Breadcrumb, s => s.Ignore());

            CreateMap<JobProfileOverviewSegmentModel, RefreshJobProfileSegmentServiceBusModel>()
                .ForMember(d => d.JobProfileId, s => s.MapFrom(a => a.DocumentId))
                .ForMember(d => d.Segment, s => s.MapFrom(a => JobProfileOverviewSegmentDataModel.SegmentName));

            CreateMap<PatchWorkingPatternModel, Data.Models.WorkingPattern>();

            CreateMap<PatchHiddenAlternativeTitleModel, Data.Models.HiddenAlternativeTitle>();

            CreateMap<PatchJobProfileSpecialismModel, Data.Models.JobProfileSpecialism>();

            CreateMap<PatchWorkingHoursDetailModel, Data.Models.WorkingHoursDetail>();

            CreateMap<PatchWorkingPatternDetailModel, Data.Models.WorkingPatternDetail>();

            CreateMap<PatchSocDataModel, SocData>();
        }
    }
}