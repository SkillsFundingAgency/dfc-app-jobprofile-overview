using System.Collections.Generic;

namespace DFC.App.JobProfileOverview.Views.UnitTests.Services
{
    public interface IViewRenderer
    {
        string Render(string template, object model, IDictionary<string, object> viewBag);
    }
}
