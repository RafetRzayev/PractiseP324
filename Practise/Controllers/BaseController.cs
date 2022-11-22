using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practise.DAL;
using Practise.DAL.Entities;

namespace Practise.Controllers
{
    public class BaseController : Controller
    {
        private readonly AppDbContext _dbContext;

        public BaseController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public AppDbContext DbContext => _dbContext;

        public Language Language
        {
            get
            {
                var culture = Request.Cookies[CookieRequestCultureProvider.DefaultCookieName];
                var isoCode = culture?.Substring(culture.IndexOf("uic=") + 4) ?? "en-Us";

                var language = _dbContext.Languages.Where(x => !x.IsDeleted && x.IsoCode.ToLower().Equals(isoCode.ToLower())).FirstOrDefault();

                return language ?? new Language
                {
                    IsoCode = "en-Us",
                    Name = "English",
                };
            }
        }
    }
}
