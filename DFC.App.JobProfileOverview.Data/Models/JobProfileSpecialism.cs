﻿using System;

namespace DFC.App.JobProfileOverview.Data.Models
{
    public class JobProfileSpecialism
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Uri Url { get; set; }
    }
}