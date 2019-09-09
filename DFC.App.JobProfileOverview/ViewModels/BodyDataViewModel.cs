using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DFC.App.JobProfileOverview.ViewModels
{
    public class BodyDataViewModel
    {
        public string SocCode { get; set; }

        public string SocDescription { get; set; }

        public string Title { get; set; }

        public string AlternativeTitle { get; set; }

        public List<string> HiddenAlternativeTitle { get; set; }

        public List<string> JobProfileSpecialism { get; set; }

        public string Overview { get; set; }

        public string SalaryExperienced { get; set; }

        public decimal MinimumHours { get; set; }

        public decimal MaximumHours { get; set; }

        public List<string> WorkingHoursDetails { get; set; }

        public List<string> WorkingPattern { get; set; }

        public List<string> WorkingPatternDetails { get; set; }
    }
}
