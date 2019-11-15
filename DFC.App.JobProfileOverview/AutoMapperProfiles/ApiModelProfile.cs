using AutoMapper;
using DFC.App.JobProfileOverview.ApiModels;
using DFC.App.JobProfileOverview.AutoMapperProfiles.ValueConverters;
using DFC.App.JobProfileOverview.Data.Models;
using System.Linq;

namespace DFC.App.JobProfileOverview.AutoMapperProfiles
{
    public class ApiModelProfile : Profile
    {
        public ApiModelProfile()
        {
            CreateMap<JobProfileOverviewSegmentDataModel, OverviewApiModel>()
                .ForMember(d => d.LastUpdatedDate, s => s.MapFrom(a => a.LastReviewed))
                .ForMember(d => d.Soc, s => s.MapFrom(a => a.Soc.SocCode))
                .ForMember(d => d.ONetOccupationalCode, s => s.MapFrom(a => a.Soc.ONetOccupationalCode))
                .ForMember(d => d.Url, s => s.Ignore())
                .ForMember(d => d.SalaryStarter, opt => opt.ConvertUsing(new SalaryToStringFormatter()))
                .ForMember(d => d.SalaryExperienced, opt => opt.ConvertUsing(new SalaryToStringFormatter()))
                .ForMember(d => d.WorkingHoursDetails, s => s.MapFrom(a => a.WorkingHoursDetails.FirstOrDefault().Title))
                .ForMember(d => d.WorkingPattern, s => s.MapFrom(a => a.WorkingPattern.FirstOrDefault().Title))
                .ForMember(d => d.WorkingPatternDetails, s => s.MapFrom(a => a.WorkingPatternDetails.FirstOrDefault().Title))
                ;
        }
    }
}