using Microsoft.AspNetCore.Mvc;
using Practise.DAL;

namespace Practise.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(AppDbContext dbContext) : base(dbContext)
        {
        }

        public IActionResult Index()
        {
            var language = Language;
            
            return View();
        }
    }
}