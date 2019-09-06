﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DFC.App.JobProfileOverview.ViewModels
{
    public class BodyViewModel
    {
        [Display(Name = "Document Id")]
        public Guid? DocumentId { get; set; }

        [Display(Name = "Canonical Name")]
        public string CanonicalName { get; set; }

        public string Content { get; set; }

        [Display(Name = "Last Reviewed")]
        public DateTime LastReviewed { get; set; }

        public BodyDataViewModel Data { get; set; }
    }
}