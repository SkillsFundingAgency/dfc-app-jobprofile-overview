﻿using System;
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

        public int SalaryStarter { get; set; }

        public int SalaryExperienced { get; set; }

        public decimal MinimumHours { get; set; }

        public decimal MaximumHours { get; set; }

        public string WorkingHoursDetails { get; set; }

        public string WorkingPattern { get; set; }

        public string WorkingPatternDetails { get; set; }
    }
}
