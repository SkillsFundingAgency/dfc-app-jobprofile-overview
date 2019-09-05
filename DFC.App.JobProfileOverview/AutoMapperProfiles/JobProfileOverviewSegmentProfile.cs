using AutoMapper;
using DFC.App.JobProfileOverview.Data.Models;
using DFC.App.JobProfileOverview.ViewModels;
using Microsoft.AspNetCore.Html;

namespace DFC.App.JobProfileOverview.AutoMapperProfiles
{
    public class JobProfileOverviewSegmentProfile : Profile
    {
        public JobProfileOverviewSegmentProfile()
        {
            CreateMap<JobProfileOverviewSegmentModel, IndexDocumentViewModel>()
                .ForMember(d => d.CanonicalName, s => s.MapFrom(x => x.CanonicalName));

            CreateMap<JobProfileOverviewSegmentModel, DocumentViewModel>()
                .ForMember(d => d.Content, s => s.MapFrom(x => new HtmlString(x.Content)));

            CreateMap<JobProfileOverviewSegmentModel, BodyViewModel>()
             .ForMember(d => d.Content, s => s.MapFrom(x => new HtmlString(x.Content)));
        }
    }
}
