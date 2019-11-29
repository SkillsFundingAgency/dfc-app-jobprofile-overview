using AutoMapper;
using System.Diagnostics.CodeAnalysis;

namespace DFC.App.JobProfileOverview.AutoMapperProfiles.ValueConverters
{
    [ExcludeFromCodeCoverage]
    public class SalaryToStringFormatter : IValueConverter<decimal, string>
    {
        public string Convert(decimal sourceMember, ResolutionContext context)
        {
            return sourceMember == 0 ? "variable" : $"{sourceMember}";
        }
    }
}
