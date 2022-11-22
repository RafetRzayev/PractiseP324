using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Practise.Areas.Admin.Models
{
    public class UserUpdateViewModel
    {
        public string Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string CurrentRole { get; set; }
        public string Role { get; set; }
        public List<SelectListItem>? Roles { get; set; }
    }
}
