using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Practise.DAL;
using Practise.DAL.Entities;
using Practise.Models;
using System.Security.AccessControl;
using ContactMessage = Practise.DAL.Entities.ContactMessage;

namespace Practise.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public ContactController(AppDbContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var model = new ContactViewModel();

            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                model.ContactMessage = new Models.ContactMessageViewModel
                {
                    Name = user.UserName,
                    Email = user.Email
                };
            }

            return View(model);
        }

        public async Task<IActionResult> AddMessage(ContactMessageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(viewName: nameof(Index), model);
            }

            var message = new ContactMessage
            {
                Name = model.Name,
                Subject = model.Subject,
                Email = model.Email,
                Message = model.Message
            };

            await _dbContext.ContactMessages.AddAsync(message);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
