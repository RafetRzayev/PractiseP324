using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practise.Data;

namespace Practise.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =Constants.AdminRole)]
    public class BaseController : Controller
    {
    }
}
