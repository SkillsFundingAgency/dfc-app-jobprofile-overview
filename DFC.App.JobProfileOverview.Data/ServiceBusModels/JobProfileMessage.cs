using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DFC.App.JobProfileOverview.Data.ServiceBusModels
{
    public class JobProfileMessage : BaseJobProfileMessage
    {
        [Required]
        public string CanonicalName { get; set; }

        public DateTime LastModified { get; set; }

        [Required]
        public string SocCodeId { get; set; }

        public SocCodeData SocCodeData { get; set; }

        public string Title { get; set; }

        public string AlternativeTitle { get; set; }

        public IEnumerable<HiddenAlternativeTitle> HiddenAlternativeTitle { get; set; }

        public IEnumerable<JobProfileSpecialism> JobProfileSpecialism { get; set; }

        public string Overview { get; set; }

        public decimal SalaryStarter { get; set; }

        public decimal SalaryExperienced { get; set; }

        public decimal MinimumHours { get; set; }

        public decimal MaximumHours { get; set; }

        public IEnumerable<WorkingHoursDetail> WorkingHoursDetails { get; set; }

        public IEnumerable<WorkingPattern> WorkingPattern { get; set; }

        public IEnumerable<WorkingPatternDetail> WorkingPatternDetails { get; set; }
    }
}