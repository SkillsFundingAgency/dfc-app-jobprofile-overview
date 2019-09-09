using RazorEngine.Templating;

namespace DFC.App.JobProfileOverview.Views.UnitTests.Services
{
    public class HtmlSupportTemplateBase<T> : TemplateBase<T>
    {
        public HtmlSupportTemplateBase()
        {
            Html = new RazorHtmlHelper();
        }

        public RazorHtmlHelper Html { get; set; }

        public void IgnoreSection(string sectionName)
        {

        }
    }
}
