﻿using System;

namespace DFC.App.JobProfileOverview.ApiModels
{
    public class OverviewApiModel
    {
        public string Title { get; set; }

        public DateTime LastUpdatedDate { get; set; }

        public string Url { get; set; }

        public string Soc { get; set; }

        public string ONetOccupationalCode { get; set; }

        public string AlternativeTitle { get; set; }

        public string Overview { get; set; }

        public string SalaryStarter { get; set; }

        public string SalaryExperienced { get; set; }

        public decimal MinimumHours { get; set; }

        public decimal MaximumHours { get; set; }

        public string WorkingHoursDetails { get; set; }

        public string WorkingPattern { get; set; }

        public string WorkingPatternDetails { get; set; }
    }
}
