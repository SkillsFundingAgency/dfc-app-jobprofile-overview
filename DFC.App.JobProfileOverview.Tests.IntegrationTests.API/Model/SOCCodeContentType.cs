using System.Collections.Generic;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model
{
    public class SOCCodeContentType
    {
        public string SOCCode { get; set; }
        public string Description { get; set; }
        public string ONetOccupationalCode { get; set; }
        public string UrlName { get; set; }
        public List<ApprenticeshipFramework> ApprenticeshipFramework { get; set; }
        public List<ApprenticeshipStandard> ApprenticeshipStandards { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public string JobProfileId { get; set; }
        public string JobProfileTitle { get; set; }
    }

    public class ApprenticeshipFramework
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public object Description { get; set; }
    }

    public class ApprenticeshipStandard
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public object Description { get; set; }
    }
}
