using DFC.App.JobProfileOverview.Data.Models;
using System.Threading.Tasks;

namespace DFC.App.JobProfileOverview.Data.Contracts
{
    public interface IDraftJobProfileOverviewSegmentService
    {
        Task<JobProfileOverviewSegmentModel> GetSitefinityData(string canonicalName);
    }
}
