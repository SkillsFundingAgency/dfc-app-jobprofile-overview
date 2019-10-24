﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DFC.App.JobProfileOverview.Data.ServiceBusModels
{
    public class RefreshJobProfileSegmentServiceBusModel
    {
        [Required]
        public Guid JobProfileId { get; set; }

        [Required]
        public string CanonicalName { get; set; }

        public string Segment { get; set; }
    }
}