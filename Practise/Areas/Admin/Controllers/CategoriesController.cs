using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Practise.Areas.Admin.Data;
using Practise.Areas.Admin.Models;
using Practise.DAL;
using Practise.DAL.Entities;
using Practise.Data;

namespace Practise.Areas.Admin.Controllers
{
    public class CategoriesController : BaseController
    {
        private readonly AppDbContext _dbContext;

        public CategoriesController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _dbContext.Categories.ToListAsync();

            return View(categories);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _dbContext.Categories.Where(x => !x.IsDeleted && x.IsMain).ToListAsync();

            var categoryListItems = new List<SelectListItem>
            {
                new SelectListItem("--Select parent category--", "0")
            };
            categories.ForEach(x => categoryListItems.Add(new SelectListItem(x.Name, x.Id.ToString())));
           
            var model = new CategoryCreateViewModel()
            {
                ParentCategories = categoryListItems
            };


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateViewModel model)
        {
            var parentCategories = await _dbContext.Categories.Where(x => !x.IsDeleted && x.IsMain).Include(x=>x.Children).ToListAsync();

            var categoryListItems = new List<SelectListItem>
            {
                new SelectListItem("--Select parent category--", "0")
            };
            parentCategories.ForEach(x => categoryListItems.Add(new SelectListItem(x.Name, x.Id.ToString())));

            var viewModel = new CategoryCreateViewModel()
            {
                ParentCategories = categoryListItems
            };

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var createdCategory = new Category();

            if (model.IsMain)
            {
                if (!model.Image.IsImage())
                {
                    ModelState.AddModelError("", "Sekil secmelisiz");
                    return View(viewModel);
                }

                if (!model.Image.IsAllowedSize(10))
                {
                    ModelState.AddModelError("", "Sekil 10mb-den cox ola bilmez");
                    return View(viewModel);
                }

                if (parentCategories.Any(x => x.Name.ToLower().Equals(model.Name.ToLower())))
                {
                    ModelState.AddModelError("", "Bu adda kateqori var");
                    return View(viewModel);
                }

                var unicalName = await model.Image.GenerateFile(Constants.CategoryPath);
                createdCategory.ImageUrl = unicalName;
            }
            else
            {
                if (model.ParentId == 0)
                {
                    ModelState.AddModelError("", "Parent kateqori secmelisiz");
                    return View(viewModel);
                }

                var parentCategory = parentCategories.FirstOrDefault(x => x.Id == model.ParentId);

                if (parentCategory.Children.Any(x => x.Name.ToLower().Equals(model.Name.ToLower())))
                {
                    ModelState.AddModelError("", "Bu adda alt kateqori var");
                    return View(viewModel);
                }

                createdCategory.ImageUrl = "";
                createdCategory.ParentId = model.ParentId;
            }

            createdCategory.Name = model.Name;
            createdCategory.IsMain = model.IsMain;
            createdCategory.IsDeleted = false;

            await _dbContext.Categories.AddAsync(createdCategory);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
