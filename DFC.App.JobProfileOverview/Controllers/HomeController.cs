using DFC.App.JobProfileOverview.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DFC.App.JobProfileOverview.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
