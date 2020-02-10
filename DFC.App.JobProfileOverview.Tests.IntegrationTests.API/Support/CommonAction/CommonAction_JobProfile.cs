﻿using DFC.Api.JobProfiles.Common.AzureServiceBusSupport;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Model;
using DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.Interface;
using System;
using System.Threading.Tasks;
using static DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.EnumLibrary;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support
{
    internal partial class CommonAction : IJobProfileSupport
    {
       public JobProfileContentType GenerateJobProfileContentType()
        {
            string canonicalName = this.RandomString(10).ToLower();
            JobProfileContentType jobProfileContentType = ResourceManager.GetResource<JobProfileContentType>("JobProfileContentType");
            jobProfileContentType.JobProfileId = Guid.NewGuid().ToString();
            jobProfileContentType.UrlName = canonicalName;
            jobProfileContentType.CanonicalName = canonicalName;
            return jobProfileContentType;
        }

       public async Task DeleteJobProfile(Topic topic, JobProfileContentType jobProfile)
        {
            JobProfileDelete messageBody = ResourceManager.GetResource<JobProfileDelete>("JobProfileDelete");
            messageBody.JobProfileId = jobProfile.JobProfileId;
            Message deleteMessage = this.CreateServiceBusMessage(jobProfile.JobProfileId, this.ConvertObjectToByteArray(messageBody), ContentType.JSON, ActionType.Deleted, CType.JobProfile);
            await topic.SendAsync(deleteMessage);
        }
    }
}