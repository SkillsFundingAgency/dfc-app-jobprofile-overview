using DFC.App.JobProfileOverview.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DFC.App.JobProfileOverview.ViewModels
{
    public class BodyDataViewModel
    {
        public BreadcrumbViewModel Breadcrumb { get; set; }

        public DateTime LastReviewed { get; set; }

        public SocData Soc { get; set; }

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

        public string WorkingHoursDetailTitle => WorkingHoursDetails?.FirstOrDefault()?.Title;

        public string WorkingPatternTitle => WorkingPattern?.FirstOrDefault()?.Title;

        public string WorkingPatternDetailTitle => WorkingPatternDetails?.FirstOrDefault()?.Title;
    }
}