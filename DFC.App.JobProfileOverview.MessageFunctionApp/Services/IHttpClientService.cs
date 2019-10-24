using DFC.App.JobProfileOverview.Data.Models;
using DFC.App.JobProfileOverview.Data.Models.PatchModels;
using System;
using System.Net;
using System.Threading.Tasks;

namespace DFC.App.JobProfileOverview.MessageFunctionApp.Services
{
    public interface IHttpClientService
    {
        Task<HttpStatusCode> PostAsync(JobProfileOverviewSegmentModel overviewSegmentModel);

        Task<HttpStatusCode> PutAsync(JobProfileOverviewSegmentModel overviewSegmentModel);

        Task<HttpStatusCode> PatchAsync<T>(T patchModel, string patchTypeEndpoint)
            where T : BasePatchModel;

        Task<HttpStatusCode> DeleteAsync(Guid id);
    }
}