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

        [Display(Name = "Sequence Number")]
        public long SequenceNumber { get; set; }

        public BodyDataViewModel Data { get; set; }
    }
}