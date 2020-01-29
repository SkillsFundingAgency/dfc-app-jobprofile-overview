﻿using DFC.Api.JobProfiles.Common.APISupport;
using System.Threading.Tasks;
using static DFC.Api.JobProfiles.Common.APISupport.GetRequest;

namespace DFC.App.JobProfileOverview.Tests.IntegrationTests.API.Support.Interface
{
    interface IAPISupport
    {
        Task<Response<T>> ExecuteGetRequest<T>(string endpoint, ContentType responseFormat, bool AuthoriseRequest = true);
    }
}
