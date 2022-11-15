using Microsoft.AspNetCore.Mvc;

namespace Practise.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
