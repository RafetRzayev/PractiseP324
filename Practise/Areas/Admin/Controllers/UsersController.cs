using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using Practise.Areas.Admin.Models;
using Practise.DAL;
using Practise.DAL.Entities;
using Practise.Data;

namespace Practise.Areas.Admin.Controllers
{
    public class UsersController : BaseController
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(AppDbContext dbContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var roles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _dbContext.UserRoles.ToListAsync();   

            var model = new List<UserViewModel>();

            foreach (var user in users)
            {
                var userRole = userRoles.FirstOrDefault(x => x.UserId == user.Id);
                var role = roles.FirstOrDefault(x => x.Id == userRole?.RoleId)?.Name;

                model.Add(new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Email = user.Email,
                    Role = role
                });
            }

            return View(model);
        }

        public IActionResult Create()
        {
            var roles = new List<string> { Constants.AdminRole, Constants.UserRole };
            var roleSelectedListItems = new List<SelectListItem>();
            roles.ForEach(x => roleSelectedListItems.Add(new SelectListItem(x, x)));

            var model = new UserCreateViewModel
            {
                Roles = roleSelectedListItems
            };

            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateViewModel model)
        {
            var roles = new List<string> { Constants.AdminRole, Constants.UserRole };
            var roleSelectedListItems = new List<SelectListItem>();
            roles.ForEach(x => roleSelectedListItems.Add(new SelectListItem(x, x)));

            var viewModel = new UserCreateViewModel
            {
                Roles = roleSelectedListItems
            };

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = new User
            {
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View(viewModel);
            }

            var createdUser = await _userManager.FindByNameAsync(model.UserName);

            result = await _userManager.AddToRoleAsync(createdUser, model.RoleName);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View(viewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var userCurrentRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            var roles = new List<string> { Constants.AdminRole, Constants.UserRole };
            var roleSelectedListItems = new List<SelectListItem>();
            roles.ForEach(x => roleSelectedListItems.Add(new SelectListItem(x, x)));

            var model = new UserUpdateViewModel
            {
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                UserName = user.UserName,
                Email = user.Email,
                Roles = roleSelectedListItems,
                RoleName = userCurrentRole,
                CurrentRoleName = userCurrentRole
            };       

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UserUpdateViewModel model,string id)
        {
            var roles = new List<string> { Constants.AdminRole, Constants.UserRole };
            var roleSelectedListItems = new List<SelectListItem>();
            roles.ForEach(x => roleSelectedListItems.Add(new SelectListItem(x, x)));

            var viewModel = new UserUpdateViewModel
            {
                Roles = roleSelectedListItems
            };

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var existUser = await _userManager.FindByIdAsync(id);

            existUser.Firstname = model.Firstname;
            existUser.Lastname = model.Lastname;
            existUser.UserName = model.UserName;
            existUser.Email = model.Email;

            if (model.CurrentRoleName != model.RoleName)
            {
                await _userManager.RemoveFromRoleAsync(existUser, model.CurrentRoleName);
                await _userManager.AddToRoleAsync(existUser, model.RoleName);
            }

            var result = await _userManager.UpdateAsync(existUser);
          
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View(viewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var existUser = await _userManager.FindByIdAsync(id);

            var result = await _userManager.DeleteAsync(existUser);

            return RedirectToAction(nameof(Index));
        }
    }
}
