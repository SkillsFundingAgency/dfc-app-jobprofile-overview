using System.Threading.Tasks;

namespace DFC.App.JobProfileOverview.SegmentService
{
    public interface IJobProfileSegmentRefreshService<in TModel>
    {
        Task SendMessageAsync(TModel model);
    }
}