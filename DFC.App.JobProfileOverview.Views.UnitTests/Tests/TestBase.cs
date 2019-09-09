using Microsoft.Extensions.Configuration;
using System.Net;

namespace DFC.App.JobProfileOverview.Views.UnitTests.Tests
{
    public class TestBase
    {
        private readonly IConfigurationRoot Configuration;
        protected string ViewRootPath;

        public TestBase()
        {
            var config = new ConfigurationBuilder();
            config.AddJsonFile("appsettings.json");
            Configuration = config.Build();

            ViewRootPath = Configuration["ViewRootPath"];
        }

        protected string HtmlEncode(string value)
        {
            return WebUtility.HtmlEncode(value);
        }
    }
}
