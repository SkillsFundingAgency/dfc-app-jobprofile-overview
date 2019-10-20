using System;
using System.ComponentModel.DataAnnotations;

namespace DFC.App.JobProfileOverview.Data.Models.PatchModels
{
    public class PatchApprenticeshipFrameworksModel : BasePatchModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Url { get; set; }
    }
}