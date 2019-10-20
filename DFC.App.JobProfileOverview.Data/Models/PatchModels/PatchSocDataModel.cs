using System.Collections.Generic;

namespace DFC.App.JobProfileOverview.Data.Models.PatchModels
{
    public class PatchSocDataModel : BasePatchModel
    {
        public string SocCode { get; set; }

        public string Description { get; set; }

        public string ONetOccupationalCode { get; set; }

        public string UrlName { get; set; }

        public IEnumerable<ApprenticeshipStandards> ApprenticeshipStandards { get; set; }

        public IEnumerable<ApprenticeshipFrameworks> ApprenticeshipFramework { get; set; }
    }
}