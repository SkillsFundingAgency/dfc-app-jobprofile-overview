﻿using System.Collections.Generic;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model.JobProfile
{
    public class RouteEntry
    {
        public int RouteName { get; set; }

        public List<EntryRequirement> EntryRequirements { get; set; }

        public List<MoreInformationLink> MoreInformationLinks { get; set; }

        public string RouteSubjects { get; set; }

        public string FurtherRouteInformation { get; set; }

        public string RouteRequirement { get; set; }
    }
}