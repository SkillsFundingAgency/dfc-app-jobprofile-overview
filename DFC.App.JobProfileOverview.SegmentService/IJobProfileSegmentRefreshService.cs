using System.Collections.Generic;
using System.Threading.Tasks;

namespace DFC.App.JobProfileOverview.SegmentService
{
    public interface IJobProfileSegmentRefreshService<TModel>
    {
        Task SendMessageAsync(TModel model);

        Task SendMessageListAsync(IList<TModel> models);
    }
}