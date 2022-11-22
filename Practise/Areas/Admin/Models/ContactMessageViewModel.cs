using NuGet.Repositories;
using Practise.DAL.Entities;

namespace Practise.Areas.Admin.Models
{
    public class ContactMessageViewModel
    {
        public List<ContactMessage> ContactMessages { get; set; }
        public bool IsAllRead { get; set; }
    }
}
