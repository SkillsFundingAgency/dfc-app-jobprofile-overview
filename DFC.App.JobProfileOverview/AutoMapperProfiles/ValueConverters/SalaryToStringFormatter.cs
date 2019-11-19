using AutoMapper;

namespace DFC.App.JobProfileOverview.AutoMapperProfiles.ValueConverters
{
    public class SalaryToStringFormatter : IValueConverter<decimal, string>
    {
        public string Convert(decimal sourceMember, ResolutionContext context)
        {
            return sourceMember == 0 ? "variable" : $"{sourceMember}";
        }
    }
}
