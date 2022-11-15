using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practise.DAL;

namespace Practise.ViewComponents
{
    public class FeaturedCategoryViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public FeaturedCategoryViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _dbContext.Categories.Where(x => !x.IsDeleted && x.IsMain).ToListAsync();

            return View(categories);
        }
    }
}
