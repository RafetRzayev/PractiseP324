using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practise.Areas.Admin.Data;
using Practise.Areas.Admin.Models;
using Practise.DAL;
using Practise.Data;

namespace Practise.Areas.Admin.Controllers
{
    public class LanguagesController : BaseController
    {
        private readonly AppDbContext _dbContext;

        public LanguagesController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var langugages = await _dbContext.Languages.Where(x => !x.IsDeleted).ToListAsync();

            return View(langugages);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LanguageCreateViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            if (!model.Image.IsImage())
            {
                ModelState.AddModelError("", "Sekil secmelisiz");
                return View();
            }

            if (!model.Image.IsAllowedSize(10))
            {
                ModelState.AddModelError("", "Sekil 10mb-den cox ola bilmez");
                return View();
            }

            var unicalName = await model.Image.GenerateFile(Constants.FlagPath);

            await _dbContext.Languages.AddAsync(new DAL.Entities.Language { ImageUrl = unicalName, Name = model.Name, IsoCode = model.IsoCode });
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
