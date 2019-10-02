using AutoMapper;
using DFC.App.JobProfileOverview.Data.Models;
using DFC.App.JobProfileOverview.ViewModels;

namespace DFC.App.JobProfileOverview.AutoMapperProfiles
{
    public class JobProfileOverviewSegmentProfile : Profile
    {
        public JobProfileOverviewSegmentProfile()
        {
            CreateMap<Data.Models.GenericListContent, ViewModels.GenericListContent>();

            CreateMap<JobProfileOverviewSegmentModel, IndexDocumentViewModel>();

            CreateMap<JobProfileOverviewSegmentModel, DocumentViewModel>();

            CreateMap<JobProfileOverviewSegmentModel, BodyViewModel>();

            CreateMap<JobProfileOverviewSegmentDataModel, BodyDataViewModel>();
        }
    }
}