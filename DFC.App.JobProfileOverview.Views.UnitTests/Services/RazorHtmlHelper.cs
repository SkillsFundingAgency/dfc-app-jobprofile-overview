using RazorEngine.Text;

namespace DFC.App.JobProfileOverview.Views.UnitTests.Services
{
    public class RazorHtmlHelper
    {
        public IEncodedString Raw(string rawString)
        {
            return new RawString(rawString);
        }
    }
}
