using Microsoft.AspNetCore.Mvc.Rendering;

namespace Practise.Areas.Admin.Models
{
    public class UserCreateViewModel
    {
        public string UserName { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
        public List<SelectListItem> Roles { get; set; } = new();
    }
}
