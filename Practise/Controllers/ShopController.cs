using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practise.DAL;
using Practise.Models;
using System.Linq;

namespace Practise.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ShopController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index(int? categoryId)
        {
            var mainCategories = await _dbContext
                .Categories
                .Where(x => !x.IsDeleted && x.IsMain)
                .Include(x => x.Children.Where(x => !x.IsDeleted))
                .ToListAsync();

            var selectedCategory = mainCategories.FirstOrDefault();

            if (categoryId != null)
            {
                selectedCategory = mainCategories.FirstOrDefault(x => x.Id == categoryId);
              
                selectedCategory ??= mainCategories.SelectMany(x => x.Children).FirstOrDefault(x => x.Id == categoryId);

                if (selectedCategory == null)
                    return NotFound();
            }

            var model = new ShopViewModel
            {
                SelectedCategory = selectedCategory,
                Categories = mainCategories
            };

            return View(model);
        }
    }
}
