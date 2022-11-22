using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Practise.Areas.Admin.Models
{
    public class UserCreateViewModel
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password),Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Role { get; set; }
        public List<SelectListItem>? Roles { get; set; }
    }
}
