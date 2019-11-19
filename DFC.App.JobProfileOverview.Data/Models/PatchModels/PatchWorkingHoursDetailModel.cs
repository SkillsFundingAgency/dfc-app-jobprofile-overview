using System;
using System.ComponentModel.DataAnnotations;

namespace DFC.App.JobProfileOverview.Data.Models.PatchModels
{
    public class PatchWorkingHoursDetailModel : BasePatchModel
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }
    }
}