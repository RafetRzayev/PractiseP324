using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practise.Areas.Admin.Models;
using Practise.DAL;

namespace Practise.Areas.Admin.ViewComponenets
{
    public class ContactMessageViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public ContactMessageViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var messages = await _dbContext.ContactMessages.ToListAsync();

            var isAllRead = messages.All(x => x.IsRead);

            return View(new ContactMessageViewModel
            {
                ContactMessages = messages,
                IsAllRead = isAllRead
            });
        }
    }
}
