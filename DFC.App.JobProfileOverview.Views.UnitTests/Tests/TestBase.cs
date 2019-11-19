using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Net;

namespace DFC.App.JobProfileOverview.Views.UnitTests.Tests
{
    public class TestBase
    {
        public string ViewRootPath => "..\\..\\..\\..\\DFC.App.JobProfileOverview\\";

        protected string CurrencySymbol => CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol;

        protected string HtmlEncode(string value)
        {
            return WebUtility.HtmlEncode(value);
        }
    }
}
