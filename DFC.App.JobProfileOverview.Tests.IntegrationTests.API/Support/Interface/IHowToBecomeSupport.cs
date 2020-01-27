using DFC.Api.JobProfiles.Common.APISupport;
using DFC.Api.JobProfiles.Common.AzureServiceBusSupport;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.EnumLibrary;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.Interface
{
    internal interface IHowToBecomeSupport
    {
        void AddEntryRequirementToRouteEntry(string entryRequirementInformation, RouteEntry routeEntry);
        void AddMoreInformationLinkToRouteEntry(string linkText, RouteEntry routeEntry);
        RouteEntry CreateARouteEntry(RequirementType requirementType);
        Dictionary<RequirementType, HowToBecomeRouteEntry> GetRouteEntriesFromHtmlResponse(Response<HtmlDocument> response);
        UpdateRouteRequirement GenerateRouteRequirementUpdate(Guid id, string title);
        Task UpdateRouteRequirement(Topic topic, UpdateRouteRequirement updateRouteRequirement, RequirementType requirementType);
        Task UpdateRegistration(Topic topic, UpdateRegistration updateRegistration);
        UpdateRegistration GenerateRegistrationUpdate(Guid id, Guid jobProfileId, string info);
        Task DeleteJobProfileWithId(Topic topic, Guid jobProfileId);
        UpdateMoreInformationLink GenerateMoreInformationLinkUpdate(string id, Guid jobProfileId, string linkText);
        Task UpdateMoreInformationLinksForRequirementType(Topic topic, UpdateMoreInformationLink updateMoreInformationLink, RequirementType requirementType);
        Task UpdateEntryRequirementForRequirementType(Topic topic, EntryRequirementMessageBody updateEntryRequirement, RequirementType requirementType);
        EntryRequirementMessageBody CreateEntryRequirementMessageBody(string entryRequirementId, Guid jobProfileId, string entryRequirementInfo);
        EntryRequirement CreateEntryRequirement(string requirementInformation);
        MoreInformationLink CreateMoreInformationLink(string linkText);
    }
}
