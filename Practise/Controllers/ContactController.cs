using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Practise.DAL;
using Practise.DAL.Entities;
using Practise.Models;
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

                model.ContactMessage = new Models.ContactMessage
                {
                    Name = user.UserName,
                    Email = user.Email
                };
            }

            return View(model);
        }
    }
}
