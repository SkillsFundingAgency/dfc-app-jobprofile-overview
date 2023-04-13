namespace DFC.App.JobProfileOverview.Data.Models.PatchModels
{
    public class PatchSocDataModel : BasePatchModel
    {
        public string SocCode { get; set; }

        public string Description { get; set; }

        public string ONetOccupationalCode { get; set; }
        public string Soc2020 { get; set; }

        public string Soc2020Extension { get; set; }

        public string UrlName { get; set; }
    }
}